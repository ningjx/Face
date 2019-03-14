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

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaiduRecognitionProvider baiduRecognitionProvider = new BaiduRecognitionProvider();
            baiduRecognitionProvider.NetFaceRegister(Image,textBox1.Text,comboBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
