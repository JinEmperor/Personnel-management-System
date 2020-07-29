using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace insa
{
    public partial class Form8 : Form
    {
        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;
        SqlDataAdapter adapter = null;
        SqlConnection conn = null;

        string dbIp = "222.237.134.74";
        string dbName = "Ora7";
        string dbId = "edu";
        string dbPw = "edu1234";


        internal PrintPreviewDialog PrintPreviewDialog1;
        private System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();
        private int curPageNumber;
        private int curPageNumber_bak;

       

        public Form8()
        {
            InitializeComponent();

            //printPreviewControl1.Document = printDocument1;

        }


        public void textenabled()
        {
            button3.Enabled = false;
            button2.Enabled = false;
            button9.Enabled = false;
            bas_empno.Enabled = false;
            bas_resno.Enabled = false;
            bas_name.Enabled = false;
            bas_cname.Enabled = false;
            bas_ename.Enabled = false;
            bas_fix.Enabled = false;
            bas_zip.Enabled = false;
            bas_addr.Enabled = false;
            bas_residence.Enabled = false;
            bas_hdpno.Enabled = false;
            bas_telno.Enabled = false;
            bas_email.Enabled = false;
            bas_mil_sta.Enabled = false;
            bas_mil_mil.Enabled = false;
            bas_mil_rnk.Enabled = false;
            bas_mar.Enabled = false;
            bas_acc_bank1.Enabled = false;
            bas_acc_name1.Enabled = false;
            bas_acc_no1.Enabled = false;
            bas_acc_bank2.Enabled = false;
            bas_acc_name2.Enabled = false;
            bas_acc_no2.Enabled = false;
            bas_cont.Enabled = false;
            bas_intern.Enabled = false;
            bas_intern_no.Enabled = false;
            bas_emp_sdate.Enabled = false;
            bas_emp_edate.Enabled = false;
            bas_entdate.Enabled = false;
            bas_resdate.Enabled = false;
            bas_levdate.Enabled = false;
            bas_reidate.Enabled = false;
            bas_wsta.Enabled = false;
            p_bas_dept.Enabled = false;
            p_bas_dut.Enabled = false;
            p_bas_pos.Enabled = false;
            p_bas_sts.Enabled = false;
            bas_intern_dt.Enabled = false;
            bas_dept_dt.Enabled = false;
            bas_dut_dt.Enabled = false;
            bas_pos_dt.Enabled = false;
            fam_empno.Enabled = false;
            fam_name.Enabled = false;
            fam_bth.Enabled = false;
            fam_itg.Enabled = false;
            fam_rel.Enabled = false;
            bas_dept_f.Enabled = false;
            bas_name_f.Enabled = false;
            bas_pos_f.Enabled = false;
            edu_empno.Enabled = false;
            edu_dept.Enabled = false;
            edu_name.Enabled = false;
            edu_pos.Enabled = false;
            edu_dept_e.Enabled = false;
            edu_entdate.Enabled = false;
            edu_degree.Enabled = false;
            edu_gradate.Enabled = false;
            edu_schnm.Enabled = false;
            edu_loe.Enabled = false;
            edu_last.Enabled = false;
            edu_gra.Enabled = false;
            edu_grade.Enabled = false;
            award_empno.Enabled = false;
            award_name.Enabled = false;
            award_pos_a.Enabled = false;
            award_dept_a.Enabled = false;
            award_no.Enabled = false;
            award_organ.Enabled = false;
            award_date.Enabled = false;
            award_content.Enabled = false;
            award_type.Enabled = false;
            award_inout.Enabled = false;
            award_kind.Enabled = false;
            award_pos.Enabled = false;
            award_dept.Enabled = false;
            car_empno.Enabled = false;
            car_name.Enabled = false;
            car_pos_c.Enabled = false;
            car_dept_c.Enabled = false;
            car_com.Enabled = false;
            car_yyyymm_f.Enabled = false;
            car_yyyymm_t.Enabled = false;
            car_region.Enabled = false;
            car_pos.Enabled = false;
            car_dept.Enabled = false;
            car_job.Enabled = false;
            car_reason.Enabled = false;
            lic_empno.Enabled = false;
            lic_name.Enabled = false;
            lic_pos.Enabled = false;
            lic_dept.Enabled = false;
            lic_code.Enabled = false;
            lic_grade.Enabled = false;
            lic_acqdate.Enabled = false;
            lic_organ.Enabled = false;
            forl_empno.Enabled = false;
            forl_name.Enabled = false;
            forl_pos.Enabled = false;
            forl_dept.Enabled = false;
            forl_code.Enabled = false;
            forl_score.Enabled = false;
            forl_acqdate.Enabled = false;
            forl_organ.Enabled = false;

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            
            pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1522)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
            pgOraConn.Open();
            pgOraCmd = pgOraConn.CreateCommand();
            textenabled();
        }


        private void sabun_display()  //인사기본사항 데이터 리스트 불러오기
        {
            function func = new function();

            //data 읽어오기
            String sabun_sql = "select * from THRM_BAS_pjh where BAS_EMPNO='" + bas_empno.Text + "'";
            //MessageBox.Show(sabun_sql);
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sabun_sql;

            OracleDataReader rd = cmd.ExecuteReader();

            if (rd.Read()) // select 했을때 data 가 있는 경우 data 값을 텍스트박스안에 넣는다.
            {

                bas_empno.Text = rd["bas_empno"].ToString();
                bas_resno.Text = rd["bas_resno"].ToString();
                bas_name.Text = rd["bas_name"].ToString();
                bas_cname.Text = rd["bas_cname"].ToString();
                bas_ename.Text = rd["bas_ename"].ToString();
                bas_fix.Text = rd["bas_fix"].ToString();
                bas_zip.Text = rd["bas_zip"].ToString();
                bas_addr.Text = rd["bas_addr"].ToString();
                bas_residence.Text = rd["bas_residence"].ToString();
                bas_hdpno.Text = rd["bas_hdpno"].ToString();
                bas_telno.Text = rd["bas_telno"].ToString();
                bas_email.Text = rd["bas_email"].ToString();
                bas_mil_sta.Text = rd["bas_mil_sta"].ToString();
                bas_mil_mil.Text = rd["bas_mil_mil"].ToString();
                bas_mil_rnk.Text = rd["bas_mil_rnk"].ToString();
                bas_mar.Text = rd["bas_mar"].ToString();
                bas_acc_bank1.Text = rd["bas_acc_bank1"].ToString();
                bas_acc_name1.Text = rd["bas_acc_name1"].ToString();
                bas_acc_no1.Text = rd["bas_acc_no1"].ToString();
                bas_acc_no1.Text = rd["bas_acc_bank2"].ToString();
                bas_acc_name2.Text = rd["bas_acc_name2"].ToString();
                bas_acc_no2.Text = rd["bas_acc_no2"].ToString();
                bas_cont.Text = rd["bas_cont"].ToString();
                bas_intern.Text = rd["bas_intern"].ToString();
                bas_intern_no.Text = rd["bas_intern_no"].ToString();
                func.insertdatetime(bas_emp_sdate, rd["bas_emp_sdate"].ToString());
                func.insertdatetime(bas_emp_edate, rd["bas_emp_edate"].ToString());
                func.insertdatetime(bas_entdate, rd["bas_entdate"].ToString());
                func.insertdatetime(bas_resdate, rd["bas_resdate"].ToString());
                func.insertdatetime(bas_levdate, rd["bas_levdate"].ToString());
                func.insertdatetime(bas_reidate, rd["bas_reidate"].ToString());
                bas_wsta.Text = rd["bas_wsta"].ToString();
                p_bas_sts.Text = rd["bas_sts"].ToString();
                p_bas_pos.Text = rd["bas_pos"].ToString();
                p_bas_dut.Text = rd["bas_dut"].ToString();
                p_bas_dept.Text = rd["bas_dept"].ToString();
                bas_rmk.Text = rd["bas_rmk"].ToString();
                func.insertdatetime(bas_pos_dt, rd["bas_pos_dt"].ToString());
                func.insertdatetime(bas_dut_dt, rd["bas_dut_dt"].ToString());
                func.insertdatetime(bas_dept_dt, rd["bas_dept_dt"].ToString());
                func.insertdatetime(bas_intern_dt, rd["bas_intern_dt"].ToString());




                edu_empno.Text = rd["bas_empno"].ToString();
                edu_name.Text = rd["bas_name"].ToString();
                edu_pos.Text = rd["bas_pos"].ToString();
                edu_dept_e.Text = rd["bas_dept"].ToString();

                award_empno.Text = rd["bas_empno"].ToString();
                award_name.Text = rd["bas_name"].ToString();
                award_pos_a.Text = rd["bas_pos"].ToString();
                award_dept_a.Text = rd["bas_dept"].ToString();

                lic_empno.Text = rd["bas_empno"].ToString();
                lic_name.Text = rd["bas_name"].ToString();
                lic_pos.Text = rd["bas_pos"].ToString();
                lic_dept.Text = rd["bas_dept"].ToString();
            }
            else
            {
                MessageBox.Show("입력하신 사원번호가 없습니다.");
                return;
            }
            string sql1 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'STS' and cd_code = (select bas_sts from thrm_bas_pjh where bas_empno ='" + bas_empno.Text + "')";
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                p_bas_sts.Text = (rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);

            }

            sql1 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'POS' and cd_code = (select bas_sts from thrm_bas_pjh where bas_empno ='" + bas_empno.Text + "')";
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                p_bas_pos.Text = (rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);

            }
            sql1 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'DUT' and cd_code = (select bas_sts from thrm_bas_pjh where bas_empno ='" + bas_empno.Text + "')";
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                p_bas_dut.Text = (rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);

            }
            sql1 = "select dept_name from THRM_DEPT_PJH where dept_code = '" + p_bas_code.Text + "'";

            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;
            OracleDataReader rd1 = cmd.ExecuteReader();

            while (rd1.Read())
            {
                p_bas_dept.Text = rd1["dept_name"].ToString();
            }

            string sql7 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'POS' and cd_code = (select bas_pos from thrm_bas_pjh where bas_empno ='" +
                bas_empno.Text + "')"; //가족직급
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql7;
            OracleDataReader rddddddd = cmd.ExecuteReader();

            while (rddddddd.Read())
            {
                bas_pos_f.Text = (rddddddd["cd_code"].ToString() + ":" + rddddddd["cd_codnms"].ToString());
            }

            string sql8 = "select dept_name from THRM_DEPT_PJH where dept_code = '" + bas_dept_f.Text + "'"; //가족부서
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql8;
            OracleDataReader rdd = cmd.ExecuteReader();

            while (rdd.Read())
            {
                bas_dept_f.Text = rdd["dept_name"].ToString();
            }

            string sql9 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'POS' and cd_code = (select bas_pos from thrm_bas_pjh where bas_empno ='" +
                bas_empno.Text + "')"; //학력직급
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql9;
            OracleDataReader rrd = cmd.ExecuteReader();

            while (rrd.Read())
            {
                award_pos_a.Text = (rrd["cd_code"].ToString() + ":" + rrd["cd_codnms"].ToString());
            }

            string sql10 = "select dept_name from THRM_DEPT_PJH where dept_code = '" + award_dept_a.Text + "'"; //학력부서
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql10;
            OracleDataReader rrrd = cmd.ExecuteReader();

            while (rrrd.Read())
            {
                award_dept_a.Text = rrrd["dept_name"].ToString();
            }

        }

        private DateTime tp(String tpp)
        {
            var tmp = tpp;
            tmp = tmp.Insert(4, "-");
            tmp = tmp.Insert(7, "-");
            DateTime tm = Convert.ToDateTime(tmp);

            return tm;
        } // datetime 날짜 바꾸기

        private DateTime tp1(string tpp)
        {
            var tmp = tpp;
            tmp = tmp.Insert(4, "-");
            DateTime tm = Convert.ToDateTime(tmp);
            return tm;
        }
        private void car_display()
        {
            function func = new function();

            String car_sql = "SELECT * FROM THRM_BAS_PJH," +
                            "(SELECT car_com,car_region, car_yyyymm_f, car_yyyymm_t,car_pos,car_dept,car_job,car_reason FROM THRM_CAR_PJH)" +
                            "WHERE bas_empno = car_region and car_region = '" + car_region.Text + "'";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = car_sql;

            OracleDataReader rd = cmd.ExecuteReader();


            if (rd.Read())
            {
                car_com.Text = rd["car_com"].ToString();
                car_region.Text = rd["car_region"].ToString();
                func.insertdatetime(car_yyyymm_f, rd["car_yyyymm_f"].ToString());
                func.insertdatetime(car_yyyymm_t, rd["car_yyyymm_t"].ToString()); 
                car_pos.Text = rd["car_pos"].ToString();
                car_dept.Text = rd["car_dept"].ToString();
                car_job.Text = rd["car_job"].ToString();
                car_reason.Text = rd["car_reason"].ToString();

            }
        } // 경력사항

        private void lic_display()
        {
            string lic_sql = "select * from THRM_BAS_pjh where BAS_EMPNO='" + bas_empno.Text + "'";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = lic_sql;
            OracleDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                lic_empno.Text = rd["bas_empno"].ToString();
                lic_name.Text = rd["bas_name"].ToString();
                lic_pos.Text = rd["bas_pos"].ToString();
                lic_dept.Text = rd["bas_dept"].ToString();

            }
        }




            private void pictureBox4_Click(object sender, EventArgs e)
        {
           


        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Add("bas_empno", "사번");
                dataGridView1.Columns.Add("bas_name", "성명");
                dataGridView1.Columns.Add("cd_codnms", "직급");
                dataGridView1.Columns.Add("dept_name", "부서");
                string sql1 = " select bas_empno, bas_name, bas_pos, cd_codnms, bas_dept, dept_name " +
                                " from THRM_BAS_PJH," +
                                "       (select cd_grpcd, cd_code, cd_codnms " +
                                "       from TIEAS_CD_PJH " +
                                "       where cd_grpcd = 'POS'), " +
                                "       THRM_DEPT_PJH " +
                                " where bas_pos = cd_code " +
                                " and bas_dept = dept_code " +
                                " and bas_empno like '%" + bas_empno.Text + "%' " +
                                " and bas_name like '%" + textBox22.Text + "%' " +
                                " and bas_dept like'" + textBox1.Text + "%' order by bas_empno";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                OracleDataReader rd = cmd.ExecuteReader();
                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cnt].Cells["bas_empno"].Value = rd["bas_empno"].ToString();
                    dataGridView1.Rows[cnt].Cells["bas_name"].Value = rd["bas_name"].ToString();
                    dataGridView1.Rows[cnt].Cells["cd_codnms"].Value = rd["cd_codnms"].ToString();
                    dataGridView1.Rows[cnt].Cells["dept_name"].Value = rd["dept_name"].ToString();
                    cnt++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.tab1.Width, this.tab1.Height);
            tab1.DrawToBitmap(bitmap, new Rectangle(0, 0, this.tab1.Width+60, this.tab1.Height));
            e.Graphics.DrawImage(bitmap, -30, 80, 900, 900);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog2.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bas_empno.Text = dataGridView1.Rows[e.RowIndex].Cells["bas_empno"].Value.ToString();
            fam_empno.Text = dataGridView1.Rows[e.RowIndex].Cells["bas_empno"].Value.ToString();
            car_empno.Text = dataGridView1.Rows[e.RowIndex].Cells["bas_empno"].Value.ToString();
            lic_empno.Text = dataGridView1.Rows[e.RowIndex].Cells["bas_empno"].Value.ToString();
            forl_empno.Text = dataGridView1.Rows[e.RowIndex].Cells["bas_empno"].Value.ToString();
            sabun_display();
            if (index_number == 5)
            {
                car_display();
            }
            if (index_number == 6)
            {
                lic_display();
            }
        }
        public static int index_number { get; set; }

        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index_number = tab1.SelectedIndex;

        }

        private void fam_bth_ValueChanged(object sender, EventArgs e)
        {
            fam_bth.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_dut_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_dut_dt.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_resdate_ValueChanged(object sender, EventArgs e)
        {
            bas_resdate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_entdate_ValueChanged(object sender, EventArgs e)
        {
            bas_entdate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_reidate_ValueChanged(object sender, EventArgs e)
        {
            bas_reidate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_levdate_ValueChanged(object sender, EventArgs e)
        {
            bas_levdate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_intern_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_intern_dt.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_emp_edate_ValueChanged(object sender, EventArgs e)
        {
            bas_emp_edate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_pos_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_pos_dt.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_dept_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_dept_dt.CustomFormat = "yyyy-MM-dd";
        }

        private void edu_gradate_ValueChanged(object sender, EventArgs e)
        {
            edu_gradate.CustomFormat = "yyyy-MM-dd";
        }

        private void edu_entdate_ValueChanged(object sender, EventArgs e)
        {
            edu_entdate.CustomFormat = "yyyy-MM-dd";
        }

        private void award_date_ValueChanged(object sender, EventArgs e)
        {
            award_date.CustomFormat = "yyyy-MM-dd";
        }

        private void car_yyyymm_f_ValueChanged(object sender, EventArgs e)
        {
            car_yyyymm_f.CustomFormat = "yyyy-MM-dd";

        }

        private void car_yyyymm_t_ValueChanged(object sender, EventArgs e)
        {
            car_yyyymm_t.CustomFormat = "yyyy-MM-dd";
        }

        private void lic_acqdate_ValueChanged(object sender, EventArgs e)
        {
            lic_acqdate.CustomFormat = "yyyy-MM-dd";

        }

        private void forl_acqdate_ValueChanged(object sender, EventArgs e)
        {
            forl_acqdate.CustomFormat = "yyyy-MM-dd";
        }
    }


}

