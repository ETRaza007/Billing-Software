using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BillSwap
{
    class DBConnect
    {
        private SqlConnection con = new SqlConnection("Data Source=RAZA;Initial Catalog=SwapBilling;Integrated Security=True;Connect Timeout=30");
        public SqlConnection GetCon()
        {
            return con;
        }
        public void OpenCon()
        {
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void CloseCon() 
        { 
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
