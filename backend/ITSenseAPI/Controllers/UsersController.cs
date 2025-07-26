using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ITSenseAPI.Context;
using ITSenseAPI.Utilities;
using ITSenseAPI.Entities;
using ITSenseAPI.DTOs;

namespace ITSenseAPI.Controllers
{

   [ApiController]
   [Route("api/users")]
   [Authorize]
   public class UsersController(ApplicationDbContext context, TokenGenerator tokenGenerator) : ControllerBase
   {
      private readonly ApplicationDbContext _context = context;
      private readonly TokenGenerator _tokenGenerator = tokenGenerator;

      [HttpGet]
      public async Task<ActionResult<IEnumerable<UserValidatedDto>>> GetAll()
      {
         var users = await _context.Users
             .Select(u => new UserDto
             {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email
             })
             .ToListAsync();

         return Ok(users);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<UserValidatedDto>> GetById(int id)
      {
         var user = await _context.Users
             .Where(u => u.Id == id)
             .Select(u => new UserDto
             {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email
             })
             .SingleOrDefaultAsync();

         if (user == null)
            return NotFound();

         return Ok(user);
      }

      [HttpPost("register")]
      [AllowAnonymous]
      public async Task<ActionResult> Register(UserCreateUpdateDto userDto)
      {
         byte[] hashWithSalt = PasswordHasher.HashPassword(userDto.Password, out byte[] salt);

         var user = new User
         {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Password = hashWithSalt,
            RegistrationDate = DateTime.UtcNow
         };

         _context.Users.Add(user);
         await _context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetById), new { id = user.Id }, new { user.Id, user.Email });
      }

      [HttpPost("login")]
      [AllowAnonymous]
      public async Task<ActionResult<UserValidatedDto>> Login([FromBody] LoginUserDto loginDto)
      {
         var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
         if (user == null || !PasswordHasher.VerifyPassword(loginDto.Password, user.Password))
         {
            return Unauthorized("Usuario o contraseña inválidos.");
         }

         var token = _tokenGenerator.GenerateToken(user.Email);

         UserValidatedDto userValidated = new()
         {
            Id = user.Id,
            Name = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            Token = token
         };

         return Ok(userValidated);
      }

      [HttpPut("{id}")]
      public async Task<ActionResult> Update(int id, UserCreateUpdateDto userDto)
      {
         var user = await _context.Users
             .SingleOrDefaultAsync(v => v.Id == id);

         if (user == null)
            return NotFound();

         byte[] hashWithSalt = PasswordHasher.HashPassword(userDto.Password, out byte[] salt);
         user.FirstName = userDto.FirstName;
         user.LastName = userDto.LastName;
         user.Email = userDto.Email;
         user.Password = hashWithSalt;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (Exception e)
         {
            Conflict(new { message = $"Ha ocurrido un error: {e.Message}" });
         }

         return NoContent();
      }

      [HttpDelete("{id}")]
      public async Task<IActionResult> Delete(int id)
      {
         var user = await _context.Users.FindAsync(id);
         if (user == null)
            return NotFound();

         _context.Users.Remove(user);
         await _context.SaveChangesAsync();

         return NoContent();
      }
   }
}