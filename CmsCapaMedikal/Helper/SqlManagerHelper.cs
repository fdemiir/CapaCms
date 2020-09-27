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
            sqlConnection = new SqlConnection(configuration.GetConnectionString("CapaMedikalDB"));
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
                    cmd.Parameters.Add("@code", SqlDbType.NVarChar).Value = product.ProductCode;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = product.ProductName;
                    cmd.Parameters.Add("@area", SqlDbType.NVarChar).Value = product.ProductArea;
                    cmd.Parameters.Add("@class", SqlDbType.NVarChar).Value = product.ProductClass;
                    cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = product.ProductType;
                    cmd.Parameters.Add("@bottombrand", SqlDbType.NVarChar).Value = product.ProductBottomBrand;
                    cmd.Parameters.Add("@image", SqlDbType.NVarChar).Value = product.ProductImage;
                    cmd.Parameters.Add("@categoryname", SqlDbType.NVarChar).Value = product.ProductCategoryName;

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
            using (var conn = sqlConnection)
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
                            product.ProductCode = reader["ProductCode"].ToString();
                            product.ProductName = reader["ProductName"].ToString();
                            product.ProductArea = reader["ProductArea"].ToString();
                            product.ProductClass = reader["ProductClass"].ToString();
                            product.ProductType = reader["ProductType"].ToString();
                            product.ProductBottomBrand = reader["ProductBottomBrand"].ToString();
                            product.ProductImage = reader["ProductImage"].ToString();
                            product.ProductCategoryName = reader["ProductCategoryName"].ToString();

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
        public List<Products> GetAllProductsHasTheSameId(string categorName)
        {
            using (var conn = new SqlConnection("Server=MONSTEROFFATIH;Database=CapaMedikalDB;User id=sa; Password=1234;"))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("[dbo].[GetAllProductsHasTheSameCategory]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@categoryName", SqlDbType.NVarChar).Value = categorName;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var allProducts = new List<Products>();
                        while (reader.Read())
                        {
                            var product = new Products();
                            product.Id = Convert.ToInt32(reader["Id"]);
                            product.ProductCode = reader["ProductCode"].ToString();
                            product.ProductName = reader["ProductName"].ToString();
                            product.ProductArea = reader["ProductArea"].ToString();
                            product.ProductClass = reader["ProductClass"].ToString();
                            product.ProductType = reader["ProductType"].ToString();
                            product.ProductBottomBrand = reader["ProductBottomBrand"].ToString();
                            product.ProductImage = reader["ProductImage"].ToString();
                            product.ProductCategoryName = reader["ProductCategoryName"].ToString();

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
                            category.CategoryName = reader["CategoryName"].ToString();
                            category.CategoryPath = reader["CategoryPath"].ToString();
                            category.CategoryUrl = reader["CategoryUrl"].ToString();
                            category.CategoryInfo = reader["CategoryInfo"].ToString();
                            category.Items = GetAllProductsHasTheSameId(category.CategoryName);

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
