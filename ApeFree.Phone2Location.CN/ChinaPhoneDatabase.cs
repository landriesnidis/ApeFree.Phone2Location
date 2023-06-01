using STTech.CodePlus.Algorithm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace ApeFree.Phone2Location.CN
{
    public class ChinaPhoneDatabase : PhoneDatabase
    {
        //private static readonly Lazy<ChinaPhoneDatabase> lazyInstance = new Lazy<ChinaPhoneDatabase>(() => new ChinaPhoneDatabase());

        ///// <summary>
        ///// 获取中国手机号码归属地数据库实例
        ///// </summary>
        //public static ChinaPhoneDatabase Instance
        //{
        //    get
        //    {
        //        return lazyInstance.Value;
        //    }
        //}


        private readonly MemoryStream stream;
        private readonly BinaryReader reader;
        private readonly string versionCode;
        private readonly int indexOffset;
        private readonly int indexEndPos;
        private readonly int indexCount;
        private readonly object _locker = new object();

        public override DateTime ReleaseTime { get; } = new DateTime(2023, 4, 1);

        internal ChinaPhoneDatabase()
        {
            stream = new MemoryStream(Resources.ChinaPhoneDatabase);
            reader = new BinaryReader(stream);

            versionCode = reader.ReadBytes(4).EncodeToString();
            indexOffset = reader.ReadInt32();
            indexEndPos = Resources.ChinaPhoneDatabase.Length - 1;
            indexCount = (indexEndPos - indexOffset) / 9;
        }

        public override void Dispose()
        {
            reader.Dispose();
            stream.Dispose();
        }

        public override QueryResult GetLocationByPhone(string phone)
        {
            if (phone.Length < 7)
            {
                throw new ArgumentException("手机号码长度不符合要求（识别归属地至少需要手机号码前7位）。", nameof(phone));
            }

            var isNum = int.TryParse(phone.Substring(0, 7), out int num);

            if (!isNum)
            {
                throw new ArgumentException($"手机号码格式不正确。", nameof(phone));
            }

            lock (_locker)
            {
                int offset = 0;

                // 使用二分法查询号码信息所处的偏移地址
                var result = Dichotomy.BinarySearch(0, indexCount, index =>
                {
                    offset = indexOffset + index * 9;
                    stream.Position = offset;
                    var value = reader.ReadInt32();

                    return value.CompareTo(num) switch
                    {
                        -1 => ComparisonResult.Less,
                        0 => ComparisonResult.Equal,
                        _ => ComparisonResult.Greater
                    };

                }, MatchStrategy.ExactMatch);

                // 如果二分法未查询到结果则返回空
                if (result == -1)
                {
                    return null;
                }

                // 获取运营商代号和详细信息的偏移地址
                stream.Position = offset + 4;
                offset = reader.ReadInt32();
                var telecomOperatorCode = reader.ReadByte();

                // 读取详细信息并构造查询结果
                stream.Position = offset;
                List<byte> data = new List<byte>();
                byte temp;
                while ((temp = reader.ReadByte()) != '\0')
                {
                    data.Add(temp);
                }
                return new ChinaPhoneQueryResult(data.ToArray(), telecomOperatorCode);
            }
        }
    }
}
