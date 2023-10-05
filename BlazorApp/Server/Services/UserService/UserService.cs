using BlazorAADE.Server.Data;
using BlazorApp.Shared.UserManagement;
using BlazorApp.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<bool>> Register(UserRegister userRegister)
        {
                if (await UserExists(userRegister.Email))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Ο χρήστης υπάρχει ήδη!"
                    };
                }
                try
                {
                    var userToCreate = new User
                    {
                        Email = userRegister.Email,
                    };

                    CreatePasswordHash(userRegister.Password, out byte[] hash, out byte[] salt);
                    userToCreate.PasswordHash = hash;
                    userToCreate.PasswordSalt = salt;

                    _context.Users.Add(userToCreate);
                    await _context.SaveChangesAsync();

                    return new ServiceResponse<bool>
                    {
                        Message = "Ο χρήστης δημιουργήθηκε με επιτυχία"
                    };
                }
                catch
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "αδυναμία δημιουργίας χρήστη"
                    };
                }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<ServiceResponse<string>> Login(UserLogin userLogin)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(userLogin.Email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "Ο χρήστης δεν βρέθηκε";
            }
            else if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Λάθος κωδικός";
            }
            else
            {
                response.Data = CreateToken(user);
                response.Message = "Επιτυχής είσοδος";

            }
            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }

}
