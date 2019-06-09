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
    public partial class SearchByName : Skin_Mac
    {
        public SearchByName()
        {
            InitializeComponent();
        }

        private void SearchByName_Load(object sender, EventArgs e)
        {

        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            FaceDataProvider baiduDataProvider = new FaceDataProvider();
            skinTextBox2.Text =  baiduDataProvider.GetData(skinTextBox1.Text);
            LocalDataProvider localDataProvider = new LocalDataProvider();
            skinPictureBox1.Image = localDataProvider.LocalFaceGetData(skinTextBox1.Text);
        }

        private void SkinTextBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SkinButton2_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Close();
        }
    }
}
