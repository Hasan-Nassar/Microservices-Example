using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Common.Exception;
using Actio.Service.Identity.Domain.Core;
using Actio.Service.Identity.Domain.Repository;
using Actio.Service.Identity.Domain.Service;

namespace Actio.Service.Identity.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository, IEncrypter encrypter,IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new ActioException("email_in-use",
                    $"Email:'email' is already in use"); 
            }

            user = new User(email,name);
            user.Setpassword(password,_encrypter);
            await _userRepository.AddAsync(user);
        }
        
        public async Task<JsonWebToken> LoginAsync(string email, string passowrd)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new ActioException("Invalid_credentials",
                    $"Invalid_credentials"); 
            }

            if (!user.ValidatePassword(passowrd,_encrypter))
            {
                throw new ActioException("Invalid_credentials",
                    $"Invalid_credentials");
            }

            return _jwtHandler.Create(user.Id);
        }
        }
}