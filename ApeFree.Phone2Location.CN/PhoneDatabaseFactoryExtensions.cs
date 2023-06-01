using ApeFree.Phone2Location.CN;

namespace ApeFree.Phone2Location
{
    public static class PhoneDatabaseFactoryExtensions
    {
        /// <summary>
        /// 构造中国手机号归属地数据库
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public static ChinaPhoneDatabase CreateChinaPhoneDatabase(this PhoneDatabaseFactory _)
        {
            return new ChinaPhoneDatabase();
        }
    }
}
