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

namespace Face
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        public Image Image { get; set; }
        bool isOrNot = false;
        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBox1.Text != null && comboBox1.Text != null)
                {
                    if (isOrNot)
                    {
                        BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
                        var result = baiduRecognitionProvider.NetFaceRegister(Image, comboBox1.Text, textBox1.Text, textBox2.Text);
                        if (result != null)
                        {
                            button1.Text = "录进去了";
                            button1.Enabled = false;
                        }
                        else { MessageBox.Show("好像没录进去"); }
                    }
                    else
                    {
                        label3.Text = "只能输字母数字";
                    }
                }
                else if(textBox1.Text == null)
                {
                    label3.Text = "姓名不能为空啊。。。";
                    
                }
                else if(textBox2.Text == null)
                {
                    label4.Text = "组名不能为空啊。。。";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("出错了：" + ex);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            if (!reg.IsMatch(textBox1.Text))
            {
                isOrNot = false;
            }
            else
            {
                label3.Text = "";
                isOrNot = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
