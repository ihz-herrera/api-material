using APIRest.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIRest.Controllers
{
    [Route("api/v1/seguridad")]
    public class SeguridadController : ControllerBase
    {
        private readonly ILogger<SeguridadController> _logger;

        public SeguridadController(ILogger<SeguridadController> logger)
        {
            _logger = logger;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] User usuario)
        {
            if(usuario == null)
            {
                return BadRequest("Usuario Invalido.");
            }

            //Todo: Buscar a la base de datos si el usuario es valido
            if(usuario.Name == "ivanh" && usuario.Password == "contraseña1234")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("contraseñasupersecreta"));
                var creditials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOption = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creditials,
                    claims: new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,"ivanh"),
                        new Claim(ClaimTypes.Email,"ivan.herrera@techsoft.com.mx"),
                        new Claim(ClaimTypes.Name,"Ivan Herrea")
                    }
                    ) ;

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

                return Ok(tokenString);
            }
            else
            {
                return Unauthorized("Usuario o contraseña no validos.");
            }
        }

    }
}
