namespace DomainEntities.Application
{
    public static class Globals
    {
        public static Credentials Credentials { get; set; }

        public static Configuration Configuration => Configuration.Instance;
    }
}