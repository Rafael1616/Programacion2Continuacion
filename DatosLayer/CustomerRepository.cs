﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository
    {
        public List<Customers> ObtenerTodos()
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String selectFrom = "";
                    selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                    selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                    selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                    selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                    selectFrom = selectFrom + "      ,[Address] " + "\n";
                    selectFrom = selectFrom + "      ,[City] " + "\n";
                    selectFrom = selectFrom + "      ,[Region] " + "\n";
                    selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                    selectFrom = selectFrom + "      ,[Country] " + "\n";
                    selectFrom = selectFrom + "      ,[Phone] " + "\n";
                    selectFrom = selectFrom + "      ,[Fax] " + "\n";
                    selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader();
                    List<Customers> customer = new List<Customers>();

                    while (reader.Read())
                    {
                        var custo = LeerDelDataReader(reader);
                        customer.Add(custo);
                    }
                    return customer;
                }
            }
        }

        public Customers OntenerPorID(String id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String selectForID = "";
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n";
                selectForID = selectForID + $"  WHERE CustomerID = @customerId";

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    comando.Parameters.AddWithValue("customerId", id);
                    var reader = comando.ExecuteReader();
                    Customers customer = null;
                    //Validacion
                    if (reader.Read())
                    {
                        customer = LeerDelDataReader(reader);
                    }
                    return customer;

                }
            }
        }

        public Customers LeerDelDataReader(SqlDataReader reader)
        {
            Customers customers = new Customers();
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? "" : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];
            return customers;
        }

        public int InsertarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                String insertInto = "";
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                using (var comando = new SqlCommand(insertInto, conexion))
                {
                    comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
                    comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
                    comando.Parameters.AddWithValue("ContactName", customer.ContactName);
                    comando.Parameters.AddWithValue("ContactTitle", customer.ContactName);
                    comando.Parameters.AddWithValue("Address", customer.Address);
                    comando.Parameters.AddWithValue("City", customer.City);
                    var insertados = comando.ExecuteNonQuery();
                    return insertados;
                }
            }
        }
    }
}
