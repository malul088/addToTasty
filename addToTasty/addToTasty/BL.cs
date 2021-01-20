using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addToTasty
{
    public static class BL
    {
        static List<SqlParameter> param = new List<SqlParameter>();
        static SqlParameter p;

        public static DataTable getTable(string sp, string tableName, int code)
        {
            param.Clear();

            if (code == 0)
            {
                p = new SqlParameter("@tableName", SqlDbType.NChar);
                p.Value = tableName;
            }
            else
            {
                p = new SqlParameter("@recipeCode", SqlDbType.Int);
                p.Value = code;
            }

            p.Direction = ParameterDirection.Input;
            param.Add(p);


            return Dal.getTable(sp, param);
        }

        public static int getCode(string sp, string name)
        {
            param.Clear();
            p = new SqlParameter("@name", name);
            p.SqlDbType = SqlDbType.NVarChar;
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            return (int)Dal.Scalar(sp, param);
        }

        public static void addRecipe(int categryCode, string preperation, string picture, string recipeName)
        {
            param.Clear();
            
            p = new SqlParameter("@categoryCode", categryCode);
            p.SqlDbType = SqlDbType.Int;
            p.Direction = ParameterDirection.Input;
            param.Add(p);

            p = new SqlParameter("@preperation", preperation);
            p.SqlDbType = SqlDbType.NVarChar;
            p.Direction = ParameterDirection.Input;
            param.Add(p); 
            
            p = new SqlParameter("@picture", picture);
            p.SqlDbType = SqlDbType.NVarChar;
            p.Direction = ParameterDirection.Input;
            param.Add(p); 
            
            p = new SqlParameter("@recipeName", recipeName);
            p.SqlDbType = SqlDbType.NVarChar;
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            
            Dal.ExecuteNonQuery("SP_addRecipe", param);
        }

        public static void addIngredient(string ingredientName, string ingredientCategory)
        {
            int code = getCode("SP_getIngCategoryCode", ingredientCategory);
            param.Clear();

            p = new SqlParameter("@ingredientName", SqlDbType.NVarChar);
            p.Value = ingredientName;
            p.Direction = ParameterDirection.Input;
            param.Add(p);

            p = new SqlParameter("@ingredientCategory", SqlDbType.Int);
            p.Value = code;
            p.Direction = ParameterDirection.Input;
            param.Add(p);

            Dal.ExecuteNonQuery("SP_addIngredient", param);
        }
        
        public static void addIngredientToRecipe(int ingredientCode, string amount, int recipeCode)
        {
            param.Clear();

            p = new SqlParameter("@ingredientCode", ingredientCode);
            p.SqlDbType = SqlDbType.Int;
            p.Direction = ParameterDirection.Input;
            param.Add(p);

            p = new SqlParameter("@amount", amount);
            p.SqlDbType = SqlDbType.NVarChar;
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            
            p = new SqlParameter("@recipeCode", recipeCode);
            p.SqlDbType = SqlDbType.Int;
            p.Direction = ParameterDirection.Input;
            param.Add(p);

            Dal.ExecuteNonQuery("SP_addIngredientToRecipe", param);
        }

    }
}
