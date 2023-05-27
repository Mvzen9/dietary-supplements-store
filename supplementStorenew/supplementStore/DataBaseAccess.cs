using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace supplementStore
{
   public class DataBaseAccess
    {
       public SqlConnection conn;
        public DataBaseAccess()
            {
          
          
                 conn = new SqlConnection(@"Server=.\SQLEXPRESS; Database=supplementStore; Integrated Security=true ");
           
        }

        public void Open()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        //Method to close the connection
        public void Close()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }




    }

}
