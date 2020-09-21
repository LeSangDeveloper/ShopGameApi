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

namespace ShopGameApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class UserController : ControllerBase
    {
        private readonly ShopGameApiDBContext _context;
        private readonly IConfiguration _config;

        public UserController(ShopGameApiDBContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [Authorize]
        [HttpGet]
        public List<User> GetUsers()
        {
            List<User> users = _context.Users.ToList<User>();
            List<UserGame> userGame = _context.UserGame.ToList<UserGame>();
            List<Game> games = _context.Games.ToList<Game>();
            
            return users;
        }

        [Authorize]
        [HttpPost("AddGame")]
        public async Task<IActionResult> PostAddUserGame(Game game)
        {   
            _context.Games.ToList();
            _context.Users.ToList();
            _context.UserGame.ToList();

            string authorization = this.Request.Headers["Authorization"];
            authorization = authorization.Remove(0, 7);
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(authorization);
            IEnumerable<Claim> claims = token.Claims;
            Claim claim =  claims.FirstOrDefault<Claim>(c => c.Type == "int");
            if (claim != null)
            {
                User user = await  _context.Users.FindAsync(Int32.Parse(claim.Value));
                if (user != null)
                {
                    Game gameChoose = await _context.Games.FindAsync(game.GameId);
                    if (gameChoose != null)
                    {
                        // Add Game and User to UserGame
                        UserGame userGame = _context.UserGame.FirstOrDefault(ug => (ug.UserId == user.UserId && ug.GameId == gameChoose.GameId));
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
                            return BadRequest(new { error = "Cannot add game!" } );
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
            return new OkResult();
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
                    new Claim("int", userInfo.UserId.ToString()),
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private User AuthenticateUser(User UserLogin)
        {
            User user;

            user = _context.Users.FirstOrDefault<User>(u => ((u.Name == UserLogin.Name) && (u.Password == UserLogin.Password)));

            return user;
        }

    }
}