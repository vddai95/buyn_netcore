using System.Configuration;

namespace caching_componant.Configuration
{
    public class RedisConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public int DatabaseID { get; set; }
    }
}
