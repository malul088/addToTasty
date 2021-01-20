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
    public partial class addRecipe : Form
    {
        DataTable Recipes;
        DataTable RecipesIngredients;

        int code = 0;

        AutoCompleteStringCollection ingredientList = new AutoCompleteStringCollection();

        List<string> list = new List<string>();//for categories names or ingredients names

        DataTable tableForList;

        


        public addRecipe()
        {
            InitializeComponent();

            Recipes = BL.getTable("SP_getTable", "Recipes", 0);
            tableForList = BL.getTable("SP_getTable", "Categories", 0);

            dataGridView2.DataSource = Recipes;

            //adding categories names into the choose category  combobox
            addToCollection("Categories", "categoryName");
            int i;
            for (i = 0; i < list.Count; i++)
            {
                comboBox2.Items.Add(list[i]);
            }

            //ingredient categories list
            list.Clear();
            addToCollection("IngredientCategory", "ingCategoryName");
            for (i = 0; i < list.Count; i++)
            {
                comboBox1.Items.Add(list[i]);
            }

            //ingerdient autoComplete list
            list.Clear();
            addToCollection("Ingredient", "ingredientName");
            for (i=0 ; i < list.Count; i++)
            {
                ingredientList.Add(list[i]);
            }
            textBox3.AutoCompleteCustomSource = ingredientList;
            textBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("One or more of the fields are empty", "Error");
                return;
            }

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            textBox3.Visible = true;
            textBox6.Visible = true;
            button3.Visible = true;
            button2.Visible = true;
            dataGridView1.Visible = true;

            textBox2.Enabled = false;
            textBox5.Enabled = false;
            textBox4.Enabled = false;
            comboBox2.Enabled = false;
            button1.Visible = false;

            //the recipe name
            label1.Text = textBox2.Text;

            //add the recipe
            code = BL.getCode("getCategoryCodeByName", comboBox2.Text);
            BL.addRecipe(code, textBox4.Text, textBox5.Text, textBox2.Text);
            Recipes = BL.getTable("SP_getTable", "Recipes", 0);
            dataGridView2.DataSource = Recipes;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("One or more of the fields are empty", "Error");
                return;
            }

            //new ingredient
            int exsist = BL.getCode("SP_isIngredientExists", textBox3.Text);
            if (exsist == 0)
            {
                if (label4.Visible==false)
                {
                    MessageBox.Show("מצרך חדש: בחרי את קטגוריית המצרך, והוסיפי תמונה בשם של המתכון");
                    label4.Visible = true;
                    comboBox1.Visible = true;
                    return;
                }

                else
                    BL.addIngredient(textBox3.Text, comboBox1.Text);

                //update the autoCompleteList
                ingredientList.Add(textBox3.Text);
            }

            //add the ingredient to the recipe
            code = BL.getCode("SP_getRecipeCodeByName", textBox2.Text);
            exsist = BL.getCode("getIngredientCodeByName", textBox3.Text);
            BL.addIngredientToRecipe(exsist, textBox6.Text, code);

            //cleaning
            textBox3.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            label4.Visible = false;
            comboBox1.Visible = false;

            //the ingredients of the specific recipe
            RecipesIngredients = BL.getTable("SP_getIngredientsByRecipe", "Recipe_Ingredient", code);
            dataGridView1.DataSource = RecipesIngredients;
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            textBox6.Text = "";
            textBox6.ForeColor = Color.Black;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            textBox6.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            dataGridView1.Visible = false;

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = @"C:\racheli\TastyDatabase\pictures\---name-- -.JPG";
            textBox6.Text = "";
            comboBox2.Text = "";

            dataGridView1.DataSource = null;

            textBox2.Enabled = true;
            textBox5.Enabled = true;
            textBox4.Enabled = true;
            comboBox2.Enabled = true;
            button1.Visible = true;
        }

        //
        //functions
        //

        void addToCollection(string tableName, string columnName)
        {
            tableForList = BL.getTable("SP_getTable", tableName, 0);
            for (int i = 0; i < tableForList.Rows.Count; i++)
            {
                list.Add(tableForList.Rows[i][columnName].ToString());
            }
        }

        private void addRecipe_Load(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void addRecipe_Load_1(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
