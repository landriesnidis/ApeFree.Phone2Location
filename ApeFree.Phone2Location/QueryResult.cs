using System;
using System.Collections.Generic;
using System.Text;

namespace ApeFree.Phone2Location
{
    public abstract class QueryResult
    {
        /// <summary>
        /// 国家名称
        /// </summary>
        public virtual string Country { get; }

        /// <summary>
        /// 省份
        /// </summary>
        public virtual string Province { get; }

        /// <summary>
        /// 城市
        /// </summary>
        public virtual string City { get; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public virtual string PostalCode { get; }

        /// <summary>
        /// 区号
        /// </summary>
        public virtual string AreaCode { get; }

        /// <summary>
        /// 电信运营商
        /// </summary>
        public virtual string TelecomOperator { get; }
    }
}
