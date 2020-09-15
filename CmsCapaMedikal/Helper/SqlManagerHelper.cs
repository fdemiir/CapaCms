using CmsCapaMedikal.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCapaMedikal.Helper
{
    public class SqlManagerHelper
    {
        SqlConnection sqlConnection;

        public SqlManagerHelper()
        {
            var configuration = GetConfiguration();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("ConnectionString:CapaMedikalDB"));
        }

        IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        public void InsertProducts(Products product)
        {
            using (var conn = new SqlConnection("Server=MONSTEROFFATIH;Database=CapaMedikalDB;User id=sa; Password=1234;"))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[InsertProducts]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = product.Id;
                    cmd.Parameters.Add("@category", SqlDbType.NVarChar).Value = product.Category;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = product.Name;
                    cmd.Parameters.Add("@detail", SqlDbType.NVarChar).Value = product.Description;
                    cmd.Parameters.Add("@photo", SqlDbType.NVarChar).Value = product.Photo;


                    int result = cmd.ExecuteNonQuery();
                    if (result < 0)
                    {
                        Console.WriteLine("error inserting data into database");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Products> GetProducts()
        {
            using (var conn = new SqlConnection("Server=MONSTEROFFATIH;Database=CapaMedikalDB;User id=sa; Password=1234;"))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetAllProducts]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var allProducts = new List<Products>();
                        while (reader.Read())
                        {
                            var product = new Products();
                            product.Id =  Convert.ToInt32(reader["Id"]);
                            product.Name =reader["ProductName"].ToString();
                            product.Category = reader["ProductCategory"].ToString();
                            product.Description = reader["ProductDescription"].ToString();
                            product.Photo = reader["ProductPhoto"].ToString();

                            allProducts.Add(product);
                        }
                        return allProducts;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
