using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insa
{
    class OracleDBManager
    {
        public string LastExceptionString = string.Empty;
        public string ConnectionString = string.Empty;
        public string Address = string.Empty;
        public string Port = string.Empty;

        public OracleConnection Connection { get; private set; }
        
        public bool GetConnection()
        {
            try
            {
                if (this.Connection != null)
                {
                    this.Connection.Close();
                    this.Connection.Dispose();
                    this.Connection = null;
                }

                if (ConnectionString == string.Empty)
                    SetConnectionString();

                Connection = new OracleConnection(ConnectionString);
                Connection.Open();

                if (this.Address != string.Empty) //주소가 없을 경우 그냥 리턴
                    Connection.Open();
            }
            catch (Exception ex)
            {
                System.Reflection.MemberInfo info = System.Reflection.MethodInfo.GetCurrentMethod();
                string id = string.Format("{0}.{1}", info.ReflectedType.Name, info.Name);
                Console.WriteLine(id);
                Console.WriteLine(ex);
                return false;
            }

            if (Connection.State == ConnectionState.Open)
                return true;
            else
                return false;
        }

        private void SetConnectionString()
        {
            this.ConnectionString = "User id=edu;Password=edu1234;" +
                "Data Source=(DESCRIPTION=(ADDRESS=" +
                "(PROTOCOL=tcp)(HOST=222.237.134.74)" +
                "(PORT=1522))(CONNECT_DATA=" +
                "(SERVICE_NAME=ora7)))";
        }
    }
}
