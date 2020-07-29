using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insa
{
     class allchart : OracleDBManager
    {
        public int chartResult3(String pos)
        {
            int chartResult = 0;
            try
            {
                // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_pos='" + pos + "'";
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                chartResult = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return chartResult;
        }
        public int chartResult4(String pos)
        {
            int chartResult1 = 0;
            try
            {
                // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_pos='" + pos + "'";
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                chartResult1 = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return chartResult1;
        }
    }
}
