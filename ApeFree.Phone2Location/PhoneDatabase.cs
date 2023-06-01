using System;

namespace ApeFree.Phone2Location
{
    /// <summary>
    /// 手机号码归属地数据库
    /// </summary>
    public abstract class PhoneDatabase : IDisposable
    {
        private static readonly Lazy<PhoneDatabaseFactory> lazyInstance = new Lazy<PhoneDatabaseFactory>(() => new PhoneDatabaseFactory());

        /// <summary>
        /// 获取手机号码归属地构造工厂
        /// </summary>
        public static PhoneDatabaseFactory Factory
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        /// <summary>
        /// 数据发布时间
        /// </summary>
        public abstract DateTime ReleaseTime { get; }

        /// <summary>
        /// 通过手机号码获取归属地信息
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <returns>查询结果</returns>
        public abstract QueryResult GetLocationByPhone(string phone);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract void Dispose();
    }

    /// <summary>
    /// 手机号码归属地数据库构造工厂
    /// </summary>
    public class PhoneDatabaseFactory
    {
        internal PhoneDatabaseFactory() { }
    }
}
