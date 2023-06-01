using System;
using System.Collections.Generic;

namespace ApeFree.Phone2Location.CN
{
    public class ChinaPhoneQueryResult : QueryResult
    {
        private const string CountryName = "中国";

        private readonly byte[] data;
        private readonly byte telecomOperatorCode;
        private Lazy<string[]> fields;

        public override string Country => CountryName;
        public override string Province => fields.Value[0];
        public override string City => fields.Value[1];
        public override string PostalCode => fields.Value[2];
        public override string AreaCode => fields.Value[3];
        public override string TelecomOperator => telecomOperatorCode switch
        {
            1 => "移动",
            2 => "联通",
            3 => "电信",
            4 => "电信虚拟运营商",
            5 => "联通虚拟运营商",
            6 => "移动虚拟运营商",
            7 => "广电",
            8 => "广电虚拟运营商",
            _ => "未知",
        };

        internal ChinaPhoneQueryResult(byte[] data, byte telecomOperatorCode)
        {
            this.data = data;
            this.telecomOperatorCode = telecomOperatorCode;
            fields = new Lazy<string[]>(() => this.data.EncodeToString().Split('|'));
        }
    }
}
