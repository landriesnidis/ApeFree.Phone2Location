using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ApeFree.Phone2Location.Demo
{
    public partial class TestForm : Form
    {
        private const int TotalCycles = 1000000;

        private PhoneDatabase db;

        public TestForm()
        {
            InitializeComponent();

            // 创建数据库
            db = PhoneDatabase.Factory.CreateChinaPhoneDatabase();
        }

        private void tbPhone_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 通过手机号码获取归属地信息
                var result = db.GetLocationByPhone(tbPhone.Text);

                if (result == null)
                {
                    labResult.Text = "未查询到结果";
                }
                else
                {
                    // 打印显示
                    labResult.Text = $"国家：{result.Country}\r\n省份：{result.Province}\r\n城市：{result.City}\r\n运营商：{result.TelecomOperator}\r\n区号：{result.AreaCode}\r\n邮编：{result.PostalCode}";
                }

            }
            catch (Exception ex)
            {
                // 打印错误信息
                labResult.Text = ex.Message;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                tbPhone.Text = "18066668888";
            }

            string phone = tbPhone.Text;

            int i = TotalCycles;

            Stopwatch sw = new Stopwatch();

            sw.Start(); //计时开始

            while (i-- > 0)
            {
                _ = db.GetLocationByPhone(phone);
            }

            sw.Stop(); //计时结束

            MessageBox.Show($"调用 {TotalCycles} 次。\r\n总耗时 {sw.ElapsedMilliseconds} ms", "测试结束");
        }

        private void btnReleaseTime_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"数据发布时间：{db.ReleaseTime}", "发布时间");
        }
    }
}
