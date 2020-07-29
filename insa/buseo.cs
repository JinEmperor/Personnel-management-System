using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace insa
{
    
    public partial class buseo : Form
    {
        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;
        SqlDataAdapter adapter = null;
        SqlConnection conn = null;

        string dbIp = "222.237.134.74";
        string dbName = "Ora7";
        string dbId = "edu";
        string dbPw = "edu1234";
        
        public buseo(Form1 set)
        {

            InitializeComponent();
            bunno = set;           
        }
        Form1 bunno;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            try { 
            dataGridView1.Columns.Add("dept_code", "부서코드");
            dataGridView1.Columns.Add("dept_name", "부서명");

            //db 연동
            pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1522)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
            pgOraConn.Open();
            pgOraCmd = pgOraConn.CreateCommand();
                //MessageBox.Show("db연결");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void 조회_Click(object sender, EventArgs e)
        {
            string sql1 = "select dept_code, dept_name from THRM_DEPT_PJH" +
                " where dept_edate is null and dept_name like '" + p_dept.Text + "%' ";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;
            OracleDataReader rd = cmd.ExecuteReader();
            int cnt = 0;
            while (rd.Read())
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[cnt].Cells["dept_code"].Value = rd["dept_code"].ToString();
                dataGridView1.Rows[cnt].Cells["dept_name"].Value = rd["dept_name"].ToString();
                cnt++;

            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bunno.op(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            bunno.oo(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            this.Close();
        }

    }
}
