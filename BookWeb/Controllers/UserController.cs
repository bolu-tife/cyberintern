using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookWeb.Models;
using BookWeb.Interface;
using Microsoft.Extensions.Configuration;
using BookWeb.Dto;
using BookWeb.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BookWeb.Controllers
{
    public class UserController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}


       
            private IUser _user;
            private IConfiguration _config;

            public UserController(IUser user, IConfiguration config)
            {
                _user = user;
                _config = config;
            }
            // POST: /<controller>/

            [HttpPost("register")]
            public IActionResult Register([FromBody] UserDto userDto)
            {
                // map dto to entity
                // var userdto = _mapper.Map<User>(userDto);

                var user = new User
                {
                    //FirstName = userDto.FirstName,
                    //LastName = userDto.LastName,
                    Email = userDto.Email,
                    Username = userDto.Username,
                    //PhoneNo = userDto.PhoneNo
                };
                try
                {
                    // save 
                    var userCreated = _user.Create(user, userDto.Password);
                    return Ok();
                }
                catch (Exception ex)
                {
                    // return error message if there was an exception
                    return BadRequest(new { message = ex.Message });
                }
            }


            [HttpPost("authenticate")]
            public IActionResult Authenticate([FromBody] LoginDto userDto)
            {
                var user = _user.Authenticate(userDto.Username, userDto.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });



                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                //var claims = new List<Claim>
                //        {
                //        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                //        };

                //var tokenDescriptor = new JwtSecurityToken(
                //        issuer: "http://cyberinterns.slack.com",
                //        audience: "http://api.cyberinterns.com",
                //        expires: DateTime.UtcNow.AddDays(7),
                //        cliams: claims,
                //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                //        );






                var token = tokenHandler.CreateToken(tokenDescriptor);

                var Expires = tokenDescriptor.Expires.ToString();





                // return basic user info (without password) and token to store client side
                return Ok(new
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    access_token = tokenHandler.WriteToken(token),
                    expires = Expires
                });
            }




            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var users = await _user.GetAll();
                return Ok(users);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var user = await _user.GetById(id);
                return Ok(user);
            }


            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] User user)
            {
                user.Id = id;
                var updateUser = await _user.Update(user);

                if (updateUser)
                {
                    return Ok("User Updated");
                }
                else
                {
                    return BadRequest(new { message = "Unable to update User details" });
                }

            }


            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var deleteUser = await _user.Delete(id);
                if (deleteUser)
                {
                    return Ok("User Deleted");
                }
                else
                {
                    return BadRequest(new { message = "Unable to delete User details" });
                }
            }



        }

    }
