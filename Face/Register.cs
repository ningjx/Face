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
using Face.Data;

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
                if (skinTextBox1.Text != "")
                {
                    string userName = skinTextBox1.Text;
                    string info = skinTextBox3.Text;
                    JObject faceInfo = new JObject { { "value", info } };
                    //skinButton1.Text = "正在往里录";
                    Task<Tuple<bool, string>> task = new Task<Tuple<bool, string>>(
                    () =>
                    {
                        BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
                        //LocalDataProvider localDataProvider = new LocalDataProvider();
                        Tuple<bool, string> tuple = baiduDataProvider.NetFaceRegisterData(Image, userName, faceInfo);
                        //Tuple<bool, string> localTuple = localDataProvider.LocalFaceRegisterData(Image, userName, faceInfo);
                        //if (tuple.Item1 && localTuple.Item1)
                        //    return tuple;
                        //else if (!localTuple.Item1)
                        //    return localTuple;
                        //else if (!tuple.Item1)
                        //    return tuple;
                        if (tuple.Item1)
                            return tuple;
                        else
                            return new Tuple<bool, string>(false, "本地保存出错，网络保存出错");

                    });
                    task.Start();
                    task.Wait();
                    if (task.Result != null)
                    {
                        if (task.Result.Item1)
                        {
                            MessageBox.Show("录进去了");
                            skinButton1.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show(task.Result.Item2);
                        }

                    }
                    else { MessageBox.Show("好像没录进去"); }

                }
                else
                {
                    if (skinTextBox1.Text == null) { label3.Text = "姓名不能为空啊。。。"; }
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
