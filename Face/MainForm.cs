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

namespace Face
{
    public partial class MainForm : Skin_Mac
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Login loginform = new Login();
            loginform.Show();
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

        //private void FrmMain_FormClosde(object sender, FormClosingEventArgs e)

        //{

        //    System.Environment.Exit(0);

        //}
    }
}
