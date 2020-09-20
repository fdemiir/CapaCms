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
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = product.Code;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = product.Name;
                    cmd.Parameters.Add("@area", SqlDbType.NVarChar).Value = product.Area;
                    cmd.Parameters.Add("@class", SqlDbType.NVarChar).Value = product.Class;
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = product.Type;
                    cmd.Parameters.Add("@bottombrand", SqlDbType.NVarChar).Value = product.BottomBrand;
                    cmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = product.Image;
                    //cmd.Parameters.Add("@categoryid", SqlDbType.Int).Value = product.CategoryId;
                    //cmd.Parameters.Add("@photo", SqlDbType.VarBinary).Value = product.Photo;


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
                            product.Id =  Convert.ToInt32(reader["Id"]);
                            product.Code = reader["ProductCode"].ToString();
                            product.Name = reader["ProductName"].ToString();
                            product.Area = reader["ProductArea"].ToString();
                            product.Class = reader["ProductClass"].ToString();
                            product.Type = reader["ProductType"].ToString();
                            product.BottomBrand = reader["ProductBottomBrand"].ToString();
                            product.Image = reader["ProductImage"].ToString();
                            product.CategoryId = Convert.ToInt32(reader["ProductCategoryId"]);
                            //product.Photo = (byte[])reader["ProductPhoto"];

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
        public List<Products> GetAllProductsHasTheSameId(int categorId)
        {
            using (var conn = new SqlConnection("Server=MONSTEROFFATIH;Database=CapaMedikalDB;User id=sa; Password=1234;"))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetAllProductsHasTheSameId]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = categorId;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var allProducts = new List<Products>();
                        while (reader.Read())
                        {
                            var product = new Products();
                            product.Id = Convert.ToInt32(reader["Id"]);
                            product.Code = reader["ProductCode"].ToString();
                            product.Name = reader["ProductName"].ToString();
                            product.Area = reader["ProductArea"].ToString();
                            product.Class = reader["ProductClass"].ToString();
                            product.Type = reader["ProductType"].ToString();
                            product.BottomBrand = reader["ProductBottomBrand"].ToString();
                            product.Image = reader["ProductImage"].ToString();
                            product.CategoryId = Convert.ToInt32(reader["ProductCategoryId"]);

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

        public List<Categories> GetAllCategories()
        {
            using (var conn = new SqlConnection("Server=MONSTEROFFATIH;Database=CapaMedikalDB;User id=sa; Password=1234;"))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetAllCategories]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var allCategories= new List<Categories>();
                        while (reader.Read())
                        {
                            var category = new Categories();
                            category.Id = Convert.ToInt32(reader["Id"]);
                            category.Name = reader["CategoryName"].ToString();
                            category.Path = reader["CategoryPath"].ToString();
                            category.Url = reader["CategoryUrl"].ToString();
                            category.Info = reader["CategoryInfo"].ToString();
                            category.Items = GetAllProductsHasTheSameId(category.Id);

                            allCategories.Add(category);
                        }
                        return allCategories;
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
