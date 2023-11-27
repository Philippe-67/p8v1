namespace Microservice.Authentication.Controllers
{
    using Microservice.Authentication.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
 
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    namespace P7CreateRestApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AuthenticationController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IConfiguration _configuration;
            private readonly ILogger<AuthenticationController> _logger;

            public AuthenticationController(UserManager<IdentityUser> userManager,
                                            RoleManager<IdentityRole> roleManager,
                                            IConfiguration configuration,
                                            ILogger<AuthenticationController> logger)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
                _logger = logger;
            }

            [HttpPost]
            public async Task<IActionResult> Register([FromBody] UserRegister userRegister, string role)
            {
                _logger.LogInformation($"Tentative d'inscription pour l'utilisateur avec l'e-mail : {userRegister.Email}");

                // check user exist
                var userExist = await _userManager.FindByEmailAsync(userRegister.Email);
                if (userExist != null)
                {
                    _logger.LogWarning($"L'utilisateur avec l'e-mail {userRegister.Email} existe déjà");
                    return BadRequest();
                }

                // add the user in the database
                IdentityUser user = new()
                {
                    Email = userRegister.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = userRegister.Username
                };

                if (await _roleManager.RoleExistsAsync(role))
                {
                    var result = await _userManager.CreateAsync(user, userRegister.Password);
                    if (!result.Succeeded)
                    {
                        _logger.LogError("Échec de la création de l'utilisateur");
                        if (!result.Succeeded)
                        {
                            _logger.LogError("Échec de la création de l'utilisateur");

                            return BadRequest();
                        
                        }

                    }

                    //add role to the user
                    await _userManager.AddToRoleAsync(user, role);

                    _logger.LogInformation($"Utilisateur avec l'e-mail {userRegister.Email} créé avec succès");
                    return  Ok(); 
                }
                else
                {
                    _logger.LogError($"Le rôle {role} n'existe pas");
                    return BadRequest(); 
                }
            }

            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
            {
                _logger.LogInformation($"Tentative de connexion pour l'utilisateur avec le nom d'utilisateur : {userLogin.Email}");

                // checking the User
                var user = await _userManager.FindByNameAsync(userLogin.Email);

                if (user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password))
                {
                    _logger.LogInformation($"Utilisateur avec le nom d'utilisateur {userLogin.Email} authentifié avec succès");

                    // claimList creation
                    var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                    // we add roles to the claims list
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        expiration = jwtToken.ValidTo
                    });
                }

                _logger.LogWarning($"Échec de l'authentification pour l'utilisateur avec le nom d'utilisateur {userLogin.Email}");
                return Unauthorized();
            }

            private JwtSecurityToken GetToken(List<Claim> authClaims)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:ValidIssuer"],
                audience: _configuration["JwtConfig:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return token;
            }
        }
    }
}
