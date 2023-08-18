using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserController(UserManager<IdentityUser> userManager, ILogger<UserController> logger,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] Login model)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                _logger.LogInformation($"User {model.Username} authorized.");
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            _logger.LogWarning($"User {model.Username} not authorized.");
            return Unauthorized();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on Authenticate: " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] Register model)
    {
        try
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = "This user already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).Aggregate((a, b) => $"{a}; {b}");
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponse { Status = "Error", Message = errors });
            }

            if (model.Role == UserRoles.Admin || model.Role == UserRoles.PowerUser)
            {
                if (model.Role == UserRoles.Admin)
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.PowerUser))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.PowerUser));
                await _userManager.AddToRoleAsync(user, UserRoles.PowerUser);
            }

            _logger.LogInformation($"User {model.Username} created.");
            return Ok(new AuthResponse { Status = "Success", Message = "User created successfully!" });
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on CreateUser: " + ex.Message);
            return BadRequest(ex.Message);
        }
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
