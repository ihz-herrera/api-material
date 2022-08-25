using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace APIRest.Seguridad
{
    public class BasicAuthHandler: AuthenticationHandler<AuthenticationSchemeOptions>
    {

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock )
            :base(options,logger, encoder, clock)
        {
            
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Validar el encabezado Authorization
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Usuario no valido");

            try
            {
                var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(header.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":", 2);

                //Todo: buscar usuario en la base de datos
                if(credentials[0] == "ivanh" && credentials[1] == "supersecreto123")
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,"ivanh"),
                        new Claim(ClaimTypes.Email,"ivanh@techsoft.com.mx")

                    };


                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal( identity);
                    var ticket = new AuthenticationTicket(principal,Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("No Authorizado");
                }


            }
            catch(Exception ex)
            {
                return AuthenticateResult.Fail("No Authorizado");

            }



            return AuthenticateResult.Fail("No Authorizado");

        }
    }
}
