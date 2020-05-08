using ExpenseManagerAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ExpenseManagerAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("auth")]
    public class AuthenticationController : ApiController
    {
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            try
            {
                using(var db = new ExpensesDbContext())
                {
                    var userExists = db.Users.Any(u => u.UserName == user.UserName && user.Password == user.Password);

                    if (userExists)
                    {
                        var token = CreateToken(user);
                        return Ok(token);
                    }
                    
                    return BadRequest("username or password is not correct");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("register")]
        [HttpPost]
        public IHttpActionResult Register(User user)
        {
            try
            {
                using(var db = new ExpensesDbContext())
                {
                    var userExisits = db.Users.Any(u => u.UserName == user.UserName);

                    if (userExisits) return BadRequest("user already exists");

                    db.Users.Add(user);
                    db.SaveChanges();

                    var token = CreateToken(user);
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public JwtPackage CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new ClaimsIdentity(new[] { 
                new Claim(ClaimTypes.Email, user.UserName)
            });

            const string secretKey = "secrete goes here";
            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = (JwtSecurityToken)tokenHandler.CreateJwtSecurityToken(subject: claims, signingCredentials: signingCredentials);
            string tokenString = tokenHandler.WriteToken(token);

            return new JwtPackage()
            {
                UserName = user.UserName,
                Token = tokenString
            };
        }
    }
}
