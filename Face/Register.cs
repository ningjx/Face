using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Face.Recognition;
using Newtonsoft.Json.Linq;
using CCWin;

namespace Face
{
    public partial class Register : Skin_Mac
    {
        public Register()
        {
            InitializeComponent();
        }
        public Image Image { get; set; }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (skinTextBox2.Text != null && skinComboBox1.Text != null && skinTextBox1.Text != null)
                {

                    System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
                    if (!reg.IsMatch(skinTextBox2.Text))
                    {
                        label3.Text = "只能输字母数字";
                    }
                    else
                    {
                        string group = skinComboBox1.Text;
                        string id = skinTextBox2.Text;
                        string faceInfo = skinTextBox1.Text + "`" + skinTextBox3.Text;
                        //skinButton1.Text = "正在往里录";
                        Task<JObject> task = new Task<JObject>(
                        () =>
                        {
                            BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
                            return baiduRecognitionProvider.NetFaceRegister(Image, group, id, faceInfo);
                        });
                        task.Start();
                        task.Wait();
                        if (task.Result != null)
                        {
                            MessageBox.Show("录进去了");
                            skinButton1.Enabled = false;
                        }
                        else { MessageBox.Show("好像没录进去"); }
                    }


                }
                else
                {
                    if (skinTextBox2.Text == null) { label3.Text = "姓名不能为空啊。。。"; }
                    else if (skinComboBox1.Text == null) { label4.Text = "组名不能为空啊。。。"; }
                    else if (skinTextBox1.Text == null) { label5.Text = "ID不能为空啊。。。"; }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("出错了：" + ex);
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
