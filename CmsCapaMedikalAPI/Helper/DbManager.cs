using CmsCapaMedikalAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCapaMedikalAPI.Helper
{
    public class DbManager
    {
        SqlConnection sqlConnection;

        public DbManager()
        {
            var configuration = GetConfiguration();
            sqlConnection = new SqlConnection(configuration.GetConnectionString("ConnectionString:CapaMedikalDB"));
        }

        IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public List<Products> GetAllProducts()
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
                            product.Id = Convert.ToInt32(reader["Id"]);
                            product.Name = reader["ProductName"].ToString();
                            product.Category = reader["ProductCategory"].ToString();
                            product.Description = reader["ProductDescription"].ToString();
                            product.Photo = (byte[])reader["ProductPhoto"];

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
