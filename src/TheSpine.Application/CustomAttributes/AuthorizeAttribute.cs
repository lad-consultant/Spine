namespace TheSpine.Application.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AuthorizeAttribute : Attribute
    {
        public string Role { get; }

        public AuthorizeAttribute(string role)
        {
            Role = role;
        }
    }
}
