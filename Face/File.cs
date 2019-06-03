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
    public partial class File : Skin_Mac
    {
        public File()
        {
            InitializeComponent();
        }

        private void File_Load(object sender, EventArgs e)
        {

        }

        private void SkinButton3_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void SkinTextBox1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
