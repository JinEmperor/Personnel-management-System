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
using System.Drawing.Text;
using insa.Properties;

namespace insa
{
    public partial class login : Form
    {
        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;
        SqlDataAdapter adapter = null;
        SqlConnection conn = null;

        string dbIp = "222.237.134.74";
        string dbName = "Ora7";
        string dbId = "edu";
        string dbPw = "edu1234";



        public login()
        {
            InitializeComponent();
        }



        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1522)))(CONNECT_DATA = (SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID = {dbId}; Password={dbPw};Connection Timeout = 30;");
                pgOraConn.Open();
                pgOraCmd = pgOraConn.CreateCommand(); //oracle db 연동

                id.Text = Settings.Default["id"].ToString();
                checkBox1.Checked = Settings.Default.checkBox1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             
        }


        private void button2_Click(object sender, EventArgs e)
        {


            if (id.Text == "")
            {
                MessageBox.Show("아이디를 입력하세요.");
            }

            String sql1 = "select * from ADMIN where " +
                   "id='" + id.Text + "' and password='" + password.Text + "'";

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;

           // MessageBox.Show(sql1);

            //cmd.ExecuteNonQuery();
            OracleDataReader rd = cmd.ExecuteReader();

            if (rd.Read()) // select 했을때 data 가 있는 경우 data 값을 텍스트박스안에 넣는다.
            {

                id.Text = rd["id"].ToString();
                password.Text = rd["password"].ToString();

                if (checkBox1.Checked == true)
                {
                    //bas_resno.Focus(); //커서
                    Settings.Default["id"] = id.Text;
                    Settings.Default["checkBox1"] = checkBox1.Checked;
                    Settings.Default.Save();
                }
                else
                {
                    Settings.Default["id"] = "";
                    Settings.Default["checkBox1"] = checkBox1.Checked;
                    Settings.Default.Save();
                }
            }
            else
            {
            }
            this.Hide();
            Main ss = new Main();
            ss.Show();
             
            pgOraConn.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // 아이디 저장
        {
            if (this.id.Text == "")
            {
                MessageBox.Show("아이디를 입력하세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.id.Focus();
            }
            else if (this.password.Text == "")
            {
                //      MessageBox.Show("비밀번호를 입력하세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.password.Focus();
            }



        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            String msg = "종료하시겠습니까";
            DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
        }

 
    }

  

    }
