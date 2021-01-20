using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace addToTasty
{
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addRecipe ar = new addRecipe();
            this.Hide();
            ar.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateRecipe ar = new updateRecipe();
            this.Hide();
            ar.ShowDialog();
            this.Show();
        }
    }
}
