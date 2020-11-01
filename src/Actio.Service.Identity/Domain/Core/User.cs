using System;
using Actio.Common.Exception;
using Actio.Service.Identity.Domain.Service;

namespace Actio.Service.Identity.Domain.Core
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public User()
        {
        }

        public User(string email,string name)
        {
            
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ActioException("Empty_user_Email",
                    $"User email can not be empty.");
            }
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ActioException("Empty_user_Name",
                    $"User name can not be empty.");
            }
            
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void Setpassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ActioException("Empty_Password",
                    $"Password can not be empty.");
            }

            Salt = encrypter.GetSalt(password);
            password = encrypter.GetHash(password, Salt);
            
        }
        
        public bool ValidatePassword(string password, IEncrypter encrypter)
            => password.Equals(encrypter.GetHash(password, Salt));
    }
}