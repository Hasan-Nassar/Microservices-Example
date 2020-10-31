namespace Actio.Common.Events
{
    public class UserCreated: IEvent
    {
        public string Name { get; set; }
        
        public string Email { get; set; }

        protected UserCreated()
        {
        }

        public UserCreated(string email,string name)
        {
            Email = email;
            Name = name;
        }
        
    }
}