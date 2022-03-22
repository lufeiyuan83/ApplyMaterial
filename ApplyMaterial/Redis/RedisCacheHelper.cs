using System;
using ServiceStack.Redis;
using System.Configuration;

namespace ERP.Redis
{
    /// <summary>
    /// redis 操作帮助类
    /// </summary>
    public class RedisCacheHelper
    {
        private static readonly PooledRedisClientManager pool = null;
        private static readonly string[] redisUrl = null;
        static RedisCacheHelper()
        {
            string redisHosts = ConfigurationManager.AppSettings["redisServer"];
            if (!string.IsNullOrEmpty(redisHosts))
            {
                redisUrl = redisHosts.Split(',');
                if (redisHosts.Length > 0)
                {
                    pool = new PooledRedisClientManager(redisUrl, redisUrl, new RedisClientManagerConfig()
                    {
                        //WriteServerList：可写的Redis链接地址。
                        //ReadServerList：可读的Redis链接地址。
                        //MaxWritePoolSize：最大写链接数。
                        //MaxReadPoolSize：最大读链接数。
                        //AutoStart：自动重启。
                        //LocalCacheTime：本地缓存到期时间，单位:秒。
                        //RecordeLog：是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项。
                        //RedisConfigInfo类是记录redis连接信息，此信息和配置文件中的RedisConfig相呼应
                        MaxWritePoolSize = int.Parse(ConfigurationManager.AppSettings["redisMaxWritePoolSize"]),//“写”链接池链接数
                        MaxReadPoolSize = int.Parse(ConfigurationManager.AppSettings["redisMaxReadPoolSize"]),//“读”链接池链接数
                        AutoStart = true//自动重启
                    });
                }
            }
        }
        public static void Add<T>(string key, T value, DateTime expiry)
        {
            if (value == null)
            {
                return;
            }
            if (expiry <= DateTime.Now)
            {
                Remove(key);
                return;
            }
            try
            {
                if (pool != null)
                {
                    using (var P = pool.GetClient())
                    {
                        if (P != null)
                        {
                            P.SendTimeout = 1000;
                            P.Set(key, value, expiry - DateTime.Now);
                        }
                    }
                }
            }
            catch
            {
                //这里可以进行日志操作
            }
        }
        public static void Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            if (value == null)
            {
                return;
            }
            if (slidingExpiration.TotalSeconds <= 0)
            {
                Remove(key);
                return;
            }
            try
            {
                if (pool != null)
                {
                    using (var P = pool.GetClient())
                    {
                        if (P != null)
                        {
                            P.SendTimeout = 1000;
                            P.Set(key, value, slidingExpiration);
                        }
                    }
                }
            }
            catch
            {
                //这里可以进行日志操作
            }
        }
        /// <summary>
        /// 读取redis数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }
            T obj = default(T);
            try
            {
                if (pool != null)
                {
                    using (var P = pool.GetClient())
                    {

                        if (P != null)
                        {
                            P.SendTimeout = 1000;
                            obj = P.Get<T>(key);
                        }
                    }
                }
            }
            catch
            {
                //这里可以进行日志操作
            }
            return obj;
        }
        /// <summary>
        /// 删除redis缓存
        /// </summary>
        /// <param name="key">键</param>
        public static void Remove(string key)
        {
            try
            {
                if (pool != null)
                {
                    using (var P = pool.GetClient())
                    {
                        if (P != null)
                        {
                            P.SendTimeout = 1000;
                            P.Remove(key);
                        }
                    }
                }
            }
            catch
            {
                //这里可以进行日志操作
            }
        }
        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            try
            {
                if (pool != null)
                {
                    using (var P = pool.GetClient())
                    {
                        if (P != null)
                        {
                            P.SendTimeout = 1000;
                            return P.ContainsKey(key);
                        }
                    }
                }
            }
            catch
            {
                //这里可以进行日志操作
            }
            return false;
        }
    }
}