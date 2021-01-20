using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace addToTasty
{
    public partial class updateRecipe : Form
    {
        DataTable dt;
        SqlDataAdapter da;
        int rowsNum;
        public updateRecipe()
        {
            InitializeComponent();
            da = new SqlDataAdapter("SELECT name FROM sys.Tables", Dal.con);
            dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                switch(item["name"].ToString())
                {
                    case "Users": continue;
                    case "lastRecipesEntered": continue;
                    case "likedRecipes": continue;
                    case "sysdiagrams": continue;

                    default: comboBox1.Items.Add(item["name"].ToString());
                        break;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            da = new SqlDataAdapter("select * from " + comboBox1.Text, Dal.con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            rowsNum = (dt.Rows.Count == 0) ? 0 : dt.Rows.Count - 1;
            int identity = identityColumns(comboBox1.Text);
            if (identity >= 0)
                dataGridView1.Columns[identity].ReadOnly = true;
            dataGridView1.Visible = true;
            button2.Visible = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            da.Update(dt);
            dt.AcceptChanges();
            dataGridView1.DataSource = dt;
        }

        public int identityColumns(string tableName)
        {
            SqlDataAdapter sda = new SqlDataAdapter("exec sp_columns " + tableName, Dal.con);
            DataTable table = new DataTable();
            sda.Fill(table);
            int i = 0;
            foreach (DataRow item in table.Rows)
            {
                Type type;
                if (string.Compare(item["TYPE_NAME"].ToString(), "int identity") == 0)
                    return i;
                i++;
            }
            return -1;
        }

        
    }
}
