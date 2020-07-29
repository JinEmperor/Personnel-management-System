using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insa
{
    class chart : OracleDBManager
    {
        public int chartResult(String dept)
        {
            int result = 0;
            try
            {
                // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_dept='"+dept+"'";
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result =  Convert.ToInt32(reader[0]);
                                
                                
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                
            }
            return result;
        }
        public int chartResultdept(String dept)
        {
            int result1 = 0;
            try
            {
                // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_dept='" + dept + "'";
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result1 = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            return result1;
        }
        public int chartResult1(String pos) //사원
        {
            int result1 = 0;
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
                                result1 = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
            return result1;
        }
        public int chartResultentdate20(string entdate)
        {
            int resultentdate = 0;
            
            try
            {
                // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_entdate like '2020%'";
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultentdate = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return resultentdate;

        }
        public int chartResultentdate19(string entdate)
        {
            int resultentdate1 = 0;

            try
            {
                // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_entdate like '2019%'";
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultentdate1 = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return resultentdate1;

        }
        public int chartResultentdate18()
        {
            int resultentdate2 = 0;

            try
            {
                // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_entdate like '2018%'";
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultentdate2 = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return resultentdate2;

        }
        public int chartResultresdate(string resdate)
        {
            int resultentdate = 0;

            try
            {
              // db접속 완료
                if (GetConnection() == true)
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = Connection;
                        cmd.CommandText = "select count(*) from thrm_bas_pjh where bas_entdate like '2020%'"; 
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultentdate = Convert.ToInt32(reader[0]);

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
               
            }
            return resultentdate;

        }
       
    }
}
