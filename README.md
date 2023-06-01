# ApeFree.Phone2Location
通过手机号码获取号码归属地信息（运营商、国家、省份、城市、邮政编码、区号）。


## 快速上手

以查询中国大陆的手机号码归属地信息为例：

在NuGet页面中勾选`预发行版本`并搜索 **ApeFree.Phone2Location.CN** 。

查询手机号码归属地的源码如下：
```csharp
  // 创建数据库
  var db = PhoneDatabase.Factory.CreateChinaPhoneDatabase();

  // 通过手机号码获取归属地信息
  var result = db.GetLocationByPhone("13500008888");

  if (result == null)
  {
    Console.WriteLine("未查询到结果");
  }
  else
  {
    // 打印显示
    Console.WriteLine($"国家：{result.Country}\r\n省份：{result.Province}\r\n城市：{result.City}\r\n运营商：{result.TelecomOperator}\r\n区号：{result.AreaCode}\r\n邮编：{result.PostalCode}");
  }
```

此代码的输出结果如下：
```
国家:中国
省份:广东
城市:广州
运营商:移动
区号:020
邮编:510000
```
