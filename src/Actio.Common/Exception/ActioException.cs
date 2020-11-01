namespace Actio.Common.Exception
{
    public class ActioException : System.Exception
    {
        public string Code { get; }

        public ActioException()
        {
        }

        public ActioException(string code)
        {
            Code = code;
        }

        public ActioException(string message ,params object [] args):this (string.Empty,message,args)
        {
        }
        
        public ActioException(string code,string message ,params object [] args):this (null,code,message,args)
        {
        }
        
        public ActioException(System.Exception innerException,string message ,params object [] args)
            :this (string.Format(message,args),innerException)
        {
        }
        
        public ActioException(System.Exception innerException,string code,string message ,params object [] args)
            :base (string.Format(message,args),innerException)
        {
            Code = code;
        }
        
        
        
        
        
        
    }
}