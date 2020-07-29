using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace insa
{
    public partial class code : Form
    {
        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;
        SqlDataAdapter adapter = null;
        SqlConnection conn = null;

        string dbIp = "222.237.134.74";
        string dbName = "Ora7";
        string dbId = "edu";
        string dbPw = "edu1234";


        public code()
        {
            InitializeComponent();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
        public static int index_number { get; set; }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Columns.Clear();
                dataGridView3.Columns.Add("dept_code", "부서코드");
                dataGridView3.Columns.Add("dept_seq", "부서 seq");
                dataGridView3.Columns.Add("dept_upp", "상위부서코드");
                dataGridView3.Columns.Add("dept_dept", "학과코드");
                dataGridView3.Columns.Add("dept_sdate", "생성일자");
                dataGridView3.Columns.Add("dept_edate", "폐기일자");
                dataGridView3.Columns.Add("dept_name", "부서명칭");
                dataGridView3.Columns.Add("dept_names", "직책명");
 
                string sql1 = " select dept_code, dept_seq, dept_upp, dept_dept, dept_sdate, dept_edate ,dept_name,dept_names" +
                              " from thrm_dept_pjh";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                OracleDataReader rd = cmd.ExecuteReader();
                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[cnt].Cells["dept_code"].Value = rd["dept_code"].ToString();
                    dataGridView3.Rows[cnt].Cells["dept_seq"].Value = rd["dept_seq"].ToString();
                    dataGridView3.Rows[cnt].Cells["dept_upp"].Value = rd["dept_upp"].ToString();
                    dataGridView3.Rows[cnt].Cells["dept_dept"].Value = rd["dept_dept"].ToString();
                    dataGridView3.Rows[cnt].Cells["dept_sdate"].Value = rd["dept_sdate"].ToString();
                    dataGridView3.Rows[cnt].Cells["dept_edate"].Value = rd["dept_edate"].ToString();
                    dataGridView3.Rows[cnt].Cells["dept_name"].Value = rd["dept_name"].ToString();
                    dataGridView3.Rows[cnt].Cells["dept_names"].Value = rd["dept_names"].ToString();
                    cnt++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void code_Load(object sender, EventArgs e)
        {
            try
            {
                //db 연동
                pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1522)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
                pgOraConn.Open();
                pgOraCmd = pgOraConn.CreateCommand();
                //MessageBox.Show("db연결");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cdg_clear()
        {
            cdg_grpcd.Text = "";
            cdg_grpnm.Text = "";
            cdg_digit.Text = "";
            cdg_length.Text = "";
            cdg_use.Text = "";
            cdg_kind.Text = "";
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            index_number = tabControl2.SelectedIndex;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("cdg_grpcd", "그룹코드");
                dataGridView1.Columns.Add("cdg_grpnm", "그룹코드명");
                dataGridView1.Columns.Add("cdg_digit", "단위코드 자리수");
                dataGridView1.Columns.Add("cdg_length", "단위코드(원형) 길이");
                dataGridView1.Columns.Add("cdg_use", "길이");
                dataGridView1.Columns.Add("cdg_kind", "분류");
                string sql1 = " select cdg_grpcd, cdg_grpnm, cdg_digit, cdg_length, cdg_use, cdg_kind " +
                              " from tieas_cdg_pjh";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                OracleDataReader rd = cmd.ExecuteReader();
                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cnt].Cells["cdg_grpcd"].Value = rd["cdg_grpcd"].ToString();
                    dataGridView1.Rows[cnt].Cells["cdg_grpnm"].Value = rd["cdg_grpnm"].ToString();
                    dataGridView1.Rows[cnt].Cells["cdg_digit"].Value = rd["cdg_digit"].ToString();
                    dataGridView1.Rows[cnt].Cells["cdg_length"].Value = rd["cdg_length"].ToString();
                    dataGridView1.Rows[cnt].Cells["cdg_use"].Value = rd["cdg_use"].ToString();
                    dataGridView1.Rows[cnt].Cells["cdg_kind"].Value = rd["cdg_kind"].ToString();
                    cnt++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void tieas_display()
        {
            String tieas_display = "select * from tieas_cdg_pjh where cdg_grpcd='" + cdg_grpcd.Text + "'";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = tieas_display;

            OracleDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                cdg_grpcd.Text = rd["cdg_grpcd"].ToString();
                cdg_grpnm.Text = rd["cdg_grpnm"].ToString();
                cdg_digit.Text = rd["cdg_digit"].ToString();
                cdg_length.Text = rd["cdg_length"].ToString();
                cdg_use.Text = rd["cdg_use"].ToString();
                cdg_kind.Text = rd["cdg_kind"].ToString();




            }
        }
        private void btn_controll()
        {
            intbtn.Enabled = false;
            modbtn.Enabled = false;
            delbtn.Enabled = false;
            canbtn.Enabled = true;
            cfmbtn.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cdg_grpcd.Text = dataGridView1.Rows[e.RowIndex].Cells["cdg_grpcd"].Value.ToString();
            tieas_display();

        }

        private void button7_Click(object sender, EventArgs e)
        {   
           if(index_number == 0 || index_number == ' ')
            {
                try
                {

                    dataGridView1.Columns.Clear();
                    dataGridView1.Columns.Add("cdg_grpcd", "그룹코드");
                    dataGridView1.Columns.Add("cdg_grpnm", "그룹코드명");
                    dataGridView1.Columns.Add("cdg_digit", "단위코드 자리수");
                    dataGridView1.Columns.Add("cdg_length", "단위코드(원형) 길이");
                    dataGridView1.Columns.Add("cdg_use", "사용여부");
                    dataGridView1.Columns.Add("cdg_kind", "분류");
                    string sql1 = " select cdg_grpcd, cdg_grpnm, cdg_digit, cdg_length, cdg_use, cdg_kind " +
                                  " from tieas_cdg_pjh";

                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = pgOraConn;
                    cmd.CommandText = sql1;
                    OracleDataReader rd = cmd.ExecuteReader();
                    int cnt = 0;
                    while (rd.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[cnt].Cells["cdg_grpcd"].Value = rd["cdg_grpcd"].ToString();
                        dataGridView1.Rows[cnt].Cells["cdg_grpnm"].Value = rd["cdg_grpnm"].ToString();
                        dataGridView1.Rows[cnt].Cells["cdg_digit"].Value = rd["cdg_digit"].ToString();
                        dataGridView1.Rows[cnt].Cells["cdg_length"].Value = rd["cdg_length"].ToString();
                        dataGridView1.Rows[cnt].Cells["cdg_use"].Value = rd["cdg_use"].ToString();
                        dataGridView1.Rows[cnt].Cells["cdg_kind"].Value = rd["cdg_kind"].ToString();
                        cnt++;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           if(index_number ==1)
            {
                try
                {
                    dataGridView2.Columns.Clear();
                    dataGridView2.Columns.Add("cd_grpcd", "그룹코드");
                    dataGridView2.Columns.Add("cd_code", "코드");
                    dataGridView2.Columns.Add("cd_seq", "코드 Seq");
                    dataGridView2.Columns.Add("cd_addinfo", "추가정보");
                    dataGridView2.Columns.Add("cd_upper", "상위분류");
                    dataGridView2.Columns.Add("cd_use", "사용여부");
                    dataGridView2.Columns.Add("cd_sdate", "생성일자");
                    dataGridView2.Columns.Add("cd_edate", "폐기일자");
                    dataGridView2.Columns.Add("cd_codnms", "코드명(축약)");
                    dataGridView2.Columns.Add("cd_codnm", "코드명(원형)");
                    this.dataGridView2.Columns["cd_grpcd"].Frozen = true;
                    string sql1 = " select cd_grpcd, cd_code, cd_seq, cd_addinfo, cd_upper, cd_use,cd_sdate,cd_edate,cd_codnms,cd_codnm " +
                                  " from tieas_cd_pjh";

                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = pgOraConn;
                    cmd.CommandText = sql1;
                    OracleDataReader rd = cmd.ExecuteReader();
                    int cnt = 0;
                    while (rd.Read())
                    {
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows[cnt].Cells["cd_grpcd"].Value = rd["cd_grpcd"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_code"].Value = rd["cd_code"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_seq"].Value = rd["cd_seq"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_addinfo"].Value = rd["cd_addinfo"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_upper"].Value = rd["cd_upper"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_use"].Value = rd["cd_use"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_sdate"].Value = rd["cd_sdate"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_edate"].Value = rd["cd_edate"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_codnms"].Value = rd["cd_codnms"].ToString();
                        dataGridView2.Rows[cnt].Cells["cd_codnm"].Value = rd["cd_codnm"].ToString();                        
                        cnt++;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        private void intbtn_Click(object sender, EventArgs e)
        {
             //입력시
            if (index_number == 0)
            {
                index.Text = "I";
                cdg_grpcd.Focus();
                cdg_clear();
                btn_controll();
                MessageBox.Show("그룹코드를 입력하세요");
            }
            if(index_number == 1)
            {
                index.Text = "I";

                btn_controll();
                MessageBox.Show("단위코드를 입력하세요");
            }
            if(index_number == 2)
            {
                index.Text = "I";

                btn_controll();
                MessageBox.Show("부서코드를 입력하세요");
            }
        }
        private void cd_clear()
        {
            cd_grpcd.Text = " ";
            cd_code.Text = " ";
            cd_seq.Text = " ";
            cd_upper.Text = " ";
            cd_use.Text = " ";
            cd_codnm.Text = " ";
            cd_codnms.Text = " ";
        }
        function fun = new function();
        private void cfmbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (index_number == 0)
                {
                    if (index.Text == "I")// sql insert 문
                    {

                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into tieas_cdg_pjh" +
                                "(cdg_grpcd,cdg_grpnm,cdg_digit,cdg_length,cdg_use,cdg_kind) " +
                                "values('" + cdg_grpcd.Text + "','" + cdg_grpnm.Text + "'," +
                                "'" + cdg_digit.Text + "','" + cdg_length.Text + "','" + cdg_use.Text + "'," +
                                "'" + cdg_kind.Text + "' )";



                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("등록되었습니다.");
                            cdg_clear();
                        }
                    }
                }
                if (index.Text == "U")
                {
                    String mmsg = "수정하시겠습니까";
                    DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (results == DialogResult.Yes)
                    {

                        string sql2 = "update tieas_cdg_pjh set cdg_grpnm='" + cdg_grpnm.Text + "',cdg_digit='" + cdg_digit.Text + "',cdg_length='" + cdg_length.Text + "',cdg_use='" + cdg_use.Text + "',cdg_kind='" + cdg_kind.Text + "' where cdg_grpcd='" + cdg_grpcd.Text + "'";


                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = pgOraConn;
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();



                        MessageBox.Show("성공");
                        cdg_clear();
                        intbtn.Enabled = true;
                        modbtn.Enabled = true;
                        delbtn.Enabled = true;
                        canbtn.Enabled = false;
                        cfmbtn.Enabled = false;
                    }
                }
                if (index.Text == "D")
                {
                    String mmsg = "삭제하시겠습니까";
                    DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (results == DialogResult.Yes)
                    {

                        string sql2 = "delete from tieas_cdg_pjh where cdg_grpcd= '" + cdg_grpcd.Text + "'";


                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = pgOraConn;
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();



                        MessageBox.Show("성공");
                        cdg_clear();
                        intbtn.Enabled = true;
                        modbtn.Enabled = true;
                        delbtn.Enabled = true;
                        canbtn.Enabled = false;
                        cfmbtn.Enabled = false;
                    }
                }
                if (index_number == 1)
                {
                    if (index.Text == "I")// sql insert 문
                    {

                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into tieas_cd_pjh" +
                                "(cd_grpcd,cd_code,cd_seq,cd_addinfo,cd_upper,cd_use,cd_sdate,cd_edate,cd_codnms,cd_codnm) " +
                                "values('" + cd_grpcd.Text + "','" + cd_code.Text + "'," +
                                "'" + cd_seq.Text + "','" + cd_addinfo.Text + "','" + cd_upper.Text + "'," +
                                "'" + cd_use.Text + "','" + fun.insertdate(cd_sdate, cd_sdate.Text) + "','" + fun.insertdate(cd_edate, cd_edate.Text) + "','" + cd_codnms.Text + "','" + cd_codnm.Text + "' )";



                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("등록되었습니다.");
                            cd_clear();
                        }
                    }
                }
                if (index.Text == "U")
                {
                    String mmsg = "수정하시겠습니까";
                    DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (results == DialogResult.Yes)
                    {

                        string sql2 = "update tieas_cd_pjh set cd_grpcd='" + cd_grpcd.Text + "',cd_code='" + cd_code.Text + "',cd_seq='" + cd_seq.Text + "',cd_addinfo='" + cd_addinfo.Text + "',cd_upper='" + cd_upper.Text + "',cd_use='"+ cd_use.Text+"',cd_sdate='"+cd_sdate.Text +"',cd_edate='"+cd_edate.Text+"',cd_codnms='"+cd_codnms.Text+"' where cd_codnm='" + cd_codnm.Text + "'";


                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = pgOraConn;
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();



                        MessageBox.Show("성공");
                        cdg_clear();
                        intbtn.Enabled = true;
                        modbtn.Enabled = true;
                        delbtn.Enabled = true;
                        canbtn.Enabled = false;
                        cfmbtn.Enabled = false;
                    }
                }
                if (index.Text == "D")
                {
                    String mmsg = "삭제하시겠습니까";
                    DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (results == DialogResult.Yes)
                    {

                        string sql2 = "delete from tieas_cdg_pjh where cd_grpcd= '" + cd_grpcd.Text + "'";


                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = pgOraConn;
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();



                        MessageBox.Show("성공");
                        cdg_clear();
                        intbtn.Enabled = true;
                        modbtn.Enabled = true;
                        delbtn.Enabled = true;
                        canbtn.Enabled = false;
                        cfmbtn.Enabled = false;
                    }
                }
                if (index_number == 2)
                {
                    if (index.Text == "I")// sql insert 문
                    {

                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                        
                            string sql1 = "insert into thrm_dept_pjh" +
                                "(dept_code,dept_name,dept_names,dept_seq,dept_upp,dept_dept,dept_sdate,dept_edate) " +
                                "values('" + dept_code.Text + "','" + dept_name.Text + "','" + dept_names.Text + "'," +
                                "'" + dept_seq.Text + "','" + dept_upp.Text + "','" + dept_dept.Text + "','" + fun.insertdate(dept_sdate, dept_sdate.Text) + "'," +
                                "'" + fun.insertdate(dept_edate, dept_edate.Text) + "' )";



                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("등록되었습니다.");
                           
                        }
                    }
                     if (index.Text == "U")
                    {
                        String mmsg = "수정하시겠습니까";
                        DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        if (results == DialogResult.Yes)
                        {

                            string sql2 = "update thrm_dept_pjh set dept_name='" + dept_name.Text + "',dept_names='" + dept_names.Text + "',dept_seq='" + dept_seq.Text + "',dept_upp='" + dept_upp.Text + "',dept_dept='" + dept_dept.Text + "',dept_sdate='" + dept_dept.Text + "',dept_edate='" + dept_edate.Text + "'where dept_code='" + dept_code.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql2;
                            cmd.ExecuteNonQuery();



                            MessageBox.Show("성공");

                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;
                        }
                    }
                    if (index.Text == "D")
                    {
                        String mmsg = "삭제하시겠습니까";
                        DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        if (results == DialogResult.Yes)
                        {

                            string sql2 = "delete from thrm_dept_pjh where dept_code= '" + dept_code.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql2;
                            cmd.ExecuteNonQuery();



                            MessageBox.Show("성공");
                            cdg_clear();
                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;
                        }
                    }

                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        private void modbtn_Click(object sender, EventArgs e)
        {
            btn_controll();
            index.Text = "U";
            if (cdg_grpcd.Text ==  "")
            {
                MessageBox.Show("사원번호를 입력하시오");


                return;
            }
        
            tieas_display();
        }

        private void delbtn_Click(object sender, EventArgs e)
        {
            btn_controll();
            if (cdg_grpcd.Text == "")
            {
                MessageBox.Show("사원번호를 입력하시오");

                return;
            }
         
            index.Text = "D";
          
        }

        private void canbtn_Click(object sender, EventArgs e)
        {
            String msg = "취소하면 입력하신 자료가 모두 저장되지 않습니다.";
            DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("취소되었습니다.");
                intbtn.Enabled = true;
                modbtn.Enabled = true;
                delbtn.Enabled = true;
                canbtn.Enabled = false;
                cfmbtn.Enabled = false;
                cdg_clear();

                return;
            }
            else
            {
                intbtn.Enabled = false;
                modbtn.Enabled = false;
                delbtn.Enabled = false;
                canbtn.Enabled = true;
                cfmbtn.Enabled = true;

            }
        } // 취소버튼

      

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cdg_clear();
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
            
        }
        
        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {             
            cd_grpcd.Text = dataGridView2.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            cd_code.Text = dataGridView2.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            cd_seq.Text = dataGridView2.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            cd_addinfo.Text = dataGridView2.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            cd_upper.Text = dataGridView2.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            cd_use.Text = dataGridView2.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();                  
            cd_codnms.Text = dataGridView2.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();
            cd_codnm.Text = dataGridView2.Rows[e.RowIndex].Cells[9].FormattedValue.ToString();
            string a = dataGridView2.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            cd_sdate.Value = Convert.ToDateTime(a.Substring(0, 4) + "-" + a.Substring(4, 2) + "-" + a.Substring(6));
            string b = dataGridView2.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
            cd_edate.Value = Convert.ToDateTime(b.Substring(0, 4) + "-" + b.Substring(4, 2) + "-" + b.Substring(6));

        }

        private void cd_sdate_ValueChanged(object sender, EventArgs e)
        {
            cd_sdate.CustomFormat = "yyyy-MM-dd";
        }

        private void cd_edate_ValueChanged(object sender, EventArgs e)
        {
            cd_edate.CustomFormat = "yyyy-MM-dd";
        }

        private void dept_sdate_ValueChanged(object sender, EventArgs e)
        {
            dept_sdate.CustomFormat = "yyyy-MM-dd";
        }

        private void dept_edate_ValueChanged(object sender, EventArgs e)
        {
            dept_edate.CustomFormat = "yyyy-MM-dd";
        }

        private void dataGridView3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dept_code.Text = dataGridView3.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            dept_name.Text = dataGridView3.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            dept_names.Text = dataGridView3.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            dept_seq.Text = dataGridView3.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            dept_upp.Text = dataGridView3.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
            dept_dept.Text = dataGridView3.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();
            string a = dataGridView2.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
            dept_sdate.Value = Convert.ToDateTime(a.Substring(0, 4) + "-" + a.Substring(4, 2) + "-" + a.Substring(6));
            string b = dataGridView2.Rows[e.RowIndex].Cells[7].FormattedValue.ToString();
            dept_edate.Value = Convert.ToDateTime(b.Substring(0, 4) + "-" + b.Substring(4, 2) + "-" + b.Substring(6));
        }
    }
        
      
}
    

