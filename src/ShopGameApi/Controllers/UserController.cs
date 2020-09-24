using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopGameApi.Data;
using Microsoft.Extensions.Configuration;
using ShopGameApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace ShopGameApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ShopGameApiDBContext _context;
        private readonly IConfiguration _config;

        public UserController(ShopGameApiDBContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            List<User> users = _context.Users.Include(u => u.UserGame).ThenInclude(ug => ug.Game).ToList();
            
            return users;
        }

        [Authorize]
        [HttpGet("GetGames")]
        public async Task<List<Game>> GetUserGames()
        {
            int UserId = Int32.Parse(HttpContext.User.FindFirstValue("User ID"));
            User user = await  _context.Users.Include(u => u.UserGame).ThenInclude(ug => ug.Game).FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user != null)
            {
                List<Game> games = new List<Game>();

                foreach (UserGame userGame in user.UserGame)
                {   
                    games.Add(userGame.Game);
                }

                return games;
            }
            else
            {
                return null;
            }
        }

        [Authorize]
        [HttpPost("AddGame")]
        public async Task<IActionResult> PostAddUserGame(Game game)
        {   
            _context.Users.Include(u => u.UserGame).ThenInclude(ug => ug.Game);

            int UserId = Int32.Parse(HttpContext.User.FindFirstValue("User ID"));
            User user = await  _context.Users.FindAsync(UserId);
            if (user != null)
            {
                Game gameChoose = await _context.Games.FindAsync(game.GameId);
                if (gameChoose != null)
                {
                    // Add Game and User to UserGame
                    UserGame userGame = await _context.UserGame.
                    FirstOrDefaultAsync(ug => (ug.UserId == user.UserId && ug.GameId == gameChoose.GameId));
                    
                    if (userGame == null)
                    {
                        userGame = new UserGame 
                        {
                            GameId = gameChoose.GameId,
                            UserId = user.UserId
                        };
                        
                        await _context.UserGame.AddAsync(userGame);
                            
                        if (user.UserGame == null)
                            user.UserGame = new List<UserGame>();
                        if (gameChoose.UserGame == null)
                            gameChoose.UserGame = new List<UserGame>();

                        user.UserGame.Add(userGame);
                        gameChoose.UserGame.Add(userGame);

                        await _context.SaveChangesAsync();

                        return Ok(userGame);
                    }
                    else
                    {
                        return BadRequest(new { error = userGame } );
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                    return NotFound();
            }
            
        }

        [HttpPost("signin")]
        public IActionResult PostSigin(User user)
        {
            var result = AuthenticateUser(user);

            if (result != null)
            {
                string token = GenerateJSONWebToken(result);
                return Ok(new { token = token });
            }

            return BadRequest(new { error = "Invalid Username/Password" });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> PostSignupAsync(User user)
        {
            User result = _context.Users.FirstOrDefault<User>(u => u.Name == user.Name);

            if (result != null)
            {
                return BadRequest("User name is established!");
            }

            user.Password = new PasswordHasher<User>().HashPassword(user, user.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[JWT.Key]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config[JWT.Issuer],
                _config[JWT.Issuer],
                new Claim[] 
                {
                    new Claim("User ID", userInfo.UserId.ToString()),
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private User AuthenticateUser(User userLogin)
        {
            User user;

            user = _context.Users.FirstOrDefault<User>(u => ((u.Name == userLogin.Name)));

            if (user != null)
            {
                PasswordVerificationResult result = new PasswordHasher<User>().VerifyHashedPassword(userLogin, user.Password, userLogin.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return null;
                }
            }

            return user;
        }

    }
}