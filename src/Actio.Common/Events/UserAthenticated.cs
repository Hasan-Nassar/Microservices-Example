namespace Actio.Common.Events
{
    public class UserAthenticated: IEvent
    {
        public string Email { get;  }

        protected UserAthenticated()
        {
        }

        public UserAthenticated(string email)
        {
            Email = email;
        }
        
    } 
}