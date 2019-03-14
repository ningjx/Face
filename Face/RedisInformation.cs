using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisConfig;
using 
namespace RedisConfig
{
    class RedisInformation
    {
        public static RedisClient GetConnect()
        {
            //string host = "localhost";
            //int port = 6379;
            RedisConfig redisConfig = new RedisConfig();
            RedisClient redisClient = new RedisClient(redisConfig.Host, redisConfig.Port);
            return redisClient;
        }
        public bool SetRedisConfig()
        {
            string host = "localhost";int port = 6379;
            try
            {
                RedisConfig redisConfig = new RedisConfig();
                redisConfig.Host = host;
                redisConfig.Port = port;
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
