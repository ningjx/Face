using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using Face.Data;

namespace Face
{
    
    public partial class MainForm : Skin_Mac
    {
        public MainForm()
        {
            InitializeComponent();    
        }
        public class PublicValue
        {
            public static bool Locker;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //BaiduDataProvider baiduDataProvider = new BaiduDataProvider();
            //baiduDataProvider.NetRecognitionData(Resource1.Image1);
            //if(Config.MainLoc)
            //button1.Enabled = PublicValue.Locker;
            //button2.Enabled = PublicValue.Locker;
            //button3.Enabled = PublicValue.Locker;
            if (PublicValue.Locker)
            {
                button5.Text = "注销";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File file = new File();
            file.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Camera camera = new Camera();
            camera.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {

        }



        private void skinButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void SkinButton1_Click_1(object sender, EventArgs e)
        {
            Login loginform = new Login();
            loginform.Show();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SearchByName search = new SearchByName();
            search.Show();
            this.Hide();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "注销")
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button5.Text = "登录";
                PublicValue.Locker = false;
            }
            else
            {
                Login loginform = new Login();
                loginform.Show();
                this.Hide();
            }
        }

        //private void FrmMain_FormClosde(object sender, FormClosingEventArgs e)

        //{

        //    System.Environment.Exit(0);

        //}
    }
}
