using System;
using System.Data;
using System.Data.SqlClient;

/*
 * Creating a Simple SQL Database Wrapper using C# 
 * https://mydevtalks.blogspot.com/2013/02/creating-simple-sql-database-wrapper.html
 */

namespace DBWrapperLib
{
  public class DBWrapper
  {
    private SqlConnection oSqlCon;
    private SqlCommand oSqlCom;
    private SqlDataAdapter oSqlDtAdptr;
    private SqlDataReader oSqlDtRdr;
    private SqlConnectionStringBuilder oConStringBuilder;

        public SqlConnection Connection { get; set; }

        //Constructor 1 : Accepts Sql server instance name, 
        // and the database name
        public DBWrapper(string sqlInstanceName, string dbName)
    {
      oConStringBuilder = new SqlConnectionStringBuilder();
      oConStringBuilder.DataSource = sqlInstanceName;
      oConStringBuilder.InitialCatalog = dbName;
      // since you are not giving the sql user name password, you must   
      // enable integrated security  
      oConStringBuilder.IntegratedSecurity = true;
      // create an object from the SqlConnectionClass  
      oSqlCon = new SqlConnection();
      // set the connection string from SqlCommandBuilder object  
      oSqlCon.ConnectionString = oConStringBuilder.ConnectionString;
    }

    //Constructor 2 : Accepts Sql server instance name, database name, 
    //  database login user name, database login password
    public DBWrapper(string sqlInstanceName, string dbName, string dbUserName = "", string dbPass = "")
    {
      oConStringBuilder = new SqlConnectionStringBuilder
      {
        DataSource = sqlInstanceName,
        InitialCatalog = dbName,
        UserID = dbUserName,
        Password = dbPass
      };
      //since you are giving the sql user name password, its optional to  
      // disable integrated security   
      // oConStringBuilder.IntegratedSecurity = false;   
      // create an object from the SqlConnectionClass   
      oSqlCon = new SqlConnection();
      // set the connection string from SqlCommandBuilder object     
      oSqlCon.ConnectionString = oConStringBuilder.ConnectionString;
    }
    //******************************************************************
    public int InsertUpdateDelete(string SqlCommandAsString)
    {
      try
      {
        // check whether the connection is not open  
        if (oSqlCon.State != ConnectionState.Open)
        {
          oSqlCon.Open();
        }
        using (oSqlCom = new SqlCommand())
        {
          // set the connection for the commnad  
          oSqlCom.Connection = oSqlCon;
          // assign the insert query as a text to the sql command  
          oSqlCom.CommandText = SqlCommandAsString;
          // this will return no of rows affected, by executing the query  
          return oSqlCom.ExecuteNonQuery();
        }
      }
      catch (Exception Ex)
      {
        throw Ex;
      }
      finally
      {
        if (oSqlCon.State == ConnectionState.Open)
        {
          oSqlCon.Close();
        }
      }
    }
    //***********************************
    public DataTable FillDataSet(string SqlSelectCommandAsString)
    {
      try
      {
        // check whether the connection is not open  
        if (oSqlCon.State != ConnectionState.Open)
        {
          oSqlCon.Open();
        }
        var ds = new DataTable();
        using (oSqlCom = new SqlCommand())
        {
          // set the connection for the commnad  
          oSqlCom.Connection = oSqlCon;
          // assign the insert query as a text to the sql command  
          oSqlCom.CommandText = SqlSelectCommandAsString;
          using (oSqlDtAdptr = new SqlDataAdapter())
          {
            oSqlDtAdptr.SelectCommand = oSqlCom;
            oSqlDtAdptr.Fill(ds);
            return ds;
          }
        }
      }
      catch (Exception Ex)
      {
        throw Ex;
      }
      finally
      {
        if (oSqlCon.State == ConnectionState.Open)
        {
          oSqlCon.Close();
        }
      }
    }

        

        public void ExecuteNonQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public bool UpdateClient(int clientId, string newName, string newPhone)
        {
            throw new NotImplementedException();
        }
    }
}
