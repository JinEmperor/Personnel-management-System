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
using System.Text.RegularExpressions;


namespace insa
{
    /// <summary>
    /// 1. 시스템명 인사관리시스템
    /// 2. 단위업무명:인사기록관리
    /// 3. 프로그래머: 박진현
    /// 4. 생성일자: 2020.05.29
    /// 5. 최종수정일: 2020.05.25

    /// </summary>

    public partial class Form1 : Form
    {


        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;
        string dbIp = "222.237.134.74";
        string dbName = "Ora7";
        string dbId = "edu";
        string dbPw = "edu1234";

        public static int index_number { get; set; }

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            index_number = 0;
            try
            {
                //db 연동
                pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1522)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
                pgOraConn.Open();
                pgOraCmd = pgOraConn.CreateCommand();
                //MessageBox.Show("db연결");



                string sql1 = "select cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'MIL'";


                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    bas_mil_mil.Items.Add(rd["cd_codnms"] as string);
                }


                sql1 = "select cd_code, cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'RNK'";


                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    bas_mil_rnk.Items.Add(rd["cd_codnms"] as string);
                }


                sql1 = "select cd_code, cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'POS'";
                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    p_bas_pos.Items.Add(rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);
                }

                sql1 = "select cd_code, cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'POS'";
                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    award_pos.Items.Add(rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);
                }

                sql1 = "select cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'BNK'";
                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    bas_acc_bank1.Items.Add(rd["cd_codnms"] as string);
                    bas_acc_bank2.Items.Add(rd["cd_codnms"] as string);
                }

                sql1 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'STS' and cd_code = (select bas_sts from thrm_bas_pjh where bas_empno ='" + bas_empno.Text + "')";
                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    p_bas_sts.Items.Add(rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);

                }

                sql1 = "select cd_code, cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'DUT'";
                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    p_bas_dut.Items.Add(rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);
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

                sql1 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'STS'";
                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    p_bas_sts.Items.Add(rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);

                }

                sql1 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'REL'";
                cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = sql1;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    fam_rel.Items.Add(rd["cd_code"] as string + ":" + rd["cd_codnms"] as string);

                }
                // 버튼 체크(입력, 수정, 삭제 -> 활성화 확인,취소 -> 비활성화)
                intbtn.Enabled = true;
                modbtn.Enabled = true;
                delbtn.Enabled = true;
                canbtn.Enabled = false;
                cfmbtn.Enabled = false;
                p_bas_dept.Enabled = false;
                bas_zip.Enabled = false;
                bas_addr.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        } // code 관리 및 oracle 
        private void setcombobox(string grpcode, ComboBox cmd)
        {
            string sql1 = "select cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'MIL'";


            OracleCommand cmd5 = new OracleCommand();
            cmd5.Connection = pgOraConn;
            cmd5.CommandText = sql1;
            cmd5.Parameters.Add("cd_grpcd", grpcode);
            OracleDataReader rd = cmd5.ExecuteReader();

            while (rd.Read())
            {
                bas_mil_mil.Items.Add(rd["cd_codnms"] as string);
            }
        }



        private void btn_controll()
        {
            intbtn.Enabled = false;
            modbtn.Enabled = false;
            delbtn.Enabled = false;
            canbtn.Enabled = true;
            cfmbtn.Enabled = true;
        } // 버튼 컨트롤
        private void award_clear()
        {
            award_empno.Text = "";
            award_type.Text = "";
            award_no.Text = "";
            award_kind.Text = "";
            award_organ.Text = "";
            award_content.Text = "";
            award_inout.Text = "";
            award_pos.Text = "";
            award_dept.Text = "";

        } // 상벌이력 값 비우기
        private void clear()
        {
            bas_empno.Text = "";
            bas_resno.Text = "";
            bas_name.Text = "";
            bas_cname.Text = "";
            bas_ename.Text = "";
            bas_zip.Text = "";
            bas_addr.Text = "";
            bas_residence.Text = "";
            bas_hdpno.Text = "";
            bas_telno.Text = "";
            bas_email.Text = "";
            bas_mil_sta.Text = "";
            bas_mil_mil.Text = "";
            bas_mil_rnk.Text = "";
            bas_acc_bank1.Text = "";
            bas_acc_name1.Text = "";
            bas_acc_no1.Text = "";
            bas_acc_no1.Text = "";
            bas_acc_name2.Text = "";
            bas_acc_no2.Text = "";
            p_bas_sts.Text = "";
            p_bas_pos.Text = "";
            p_bas_dut.Text = "";
            p_bas_dept.Text = "";
            bas_rmk.Text = "";
            fam_empno.Text = "";
            fam_name.Text = "";
            bas_pos_f.Text = "";
            bas_dept_f.Text = "";
        } // 텍스트 값 비우기
        private void fam_clear()
        {
            fam_empno.Text = "";
            fam_name.Text = "";
            fam_rel.Text = "";
            fam_itg.Text = "";

        } // 가족사항 값 비우기
        private void edu_clear()
        {
            edu_empno.Text = "";
            edu_loe.Text = "";
            edu_entdate.Text = "";
            edu_gradate.Text = "";
            edu_schnm.Text = "";
            edu_dept.Text = "";
            edu_degree.Text = "";
            edu_grade.Text = "";
            edu_gra.Text = "";
            edu_last.Text = "";


        } // 학력사항 값 비우기
        private void car_clear()
        {
            car_empno.Text = "";
            car_com.Text = "";
            car_region.Text = "";
            car_yyyymm_f.Text = "";
            car_yyyymm_t.Text = "";
            car_pos.Text = "";
            car_dept.Text = "";
            car_job.Text = "";
            car_reason.Text = "";
        } //경력사항 값 비우기

        private void lic_clear()
        {
            lic_code.Text = "";
            lic_grade.Text = "";
            lic_acqdate.Text = "";
            lic_organ.Text = "";
        }

        private void intbtn_Click(object sender, EventArgs e)
        {
            // 입력시
            if (index_number == 0)
            {
                btnchk.Text = "I";
                bas_empno.Focus();
                clear();

                MessageBox.Show("인사기본사항을 입력하세요");
                btn_controll();
            }
            if (index_number == 1)
            {
                btnchk.Text = "I";
                MessageBox.Show("가족사항을 입력하세요");
                btn_controll();
            }
            if (index_number == 2)
            {
                btnchk.Text = "I";
                edu_schnm.Focus();
                MessageBox.Show("학력사항을 입력하세요");
                btn_controll();
            }
            if (index_number == 3)
            {
                btnchk.Text = "I";
                award_no.Focus();

                MessageBox.Show("상벌이력을 입력하세요");
                btn_controll();
            }
            if (index_number == 4)
            {
                btnchk.Text = "I";
                car_com.Focus();

                MessageBox.Show("경력사항을 입력하세요");
                btn_controll();
            }
            if (index_number == 5)
            {
                btnchk.Text = "I";
                car_com.Focus();

                MessageBox.Show("자격면허을 입력하세요");
                btn_controll();
            }
            //control_reset();
        } // 등록버튼


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
                clear();
                award_clear();
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
        String tab;


        function func = new function();

        private void tab1_Click(object sender, EventArgs e)
        {
            int tab_index = tab1.SelectedIndex;
            tab = tab_index.ToString();
        } // 탭 인덱스
        private void cfmbtn_Click(object sender, EventArgs e)
        {
            string resultsp = p_bas_sts.Text; //result="1:교원"
            string[] sts1 = resultsp.Split(':'); // sts1[0]:1 sts[1]:교원

            string result1 = p_bas_pos.Text;
            string[] pos1 = result1.Split(':');

            string result2 = p_bas_dut.Text;
            string[] dut1 = result2.Split(':');

            string result3 = fam_rel.Text;
            string[] rel1 = resultsp.Split(':');

            string result4 = award_pos.Text;
            string[] pos = result1.Split(':');

            string result5 = lic_code.Text;
            string[] lic = result5.Split(':');

            string result6 = forl_code.Text;
            string[] forl = result6.Split(':');








            try
            {
                if (index_number == 0)
                {
                    if (btnchk.Text == "I")// sql insert 문
                    {

                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into thrm_bas_pjh" +
                                "(bas_empno,bas_resno,bas_name,bas_cname,bas_ename,bas_fix,bas_zip ,bas_addr,bas_residence,bas_hdpno,bas_telno,bas_email,bas_mil_sta,bas_mil_mil,bas_mil_rnk,bas_mar,bas_acc_bank1,bas_acc_name1,bas_acc_no1,bas_acc_bank2,bas_acc_name2,bas_acc_no2,bas_cont,bas_intern,bas_intern_no,bas_emp_sdate,bas_emp_edate,bas_entdate,bas_resdate,bas_levdate,bas_reidate,bas_wsta,bas_sts,bas_pos,bas_dut,bas_dept,bas_rmk,bas_pos_dt,bas_dut_dt,bas_dept_dt,bas_intern_dt) values('" + bas_empno.Text + "','" + bas_resno.Text + "','" + bas_name.Text + "','" + bas_cname.Text + "','" + bas_ename.Text + "','" + bas_fix.Text + "','" + bas_zip.Text + "','" + bas_addr.Text + "','" + bas_residence.Text + "','" + bas_hdpno.Text + "','" + bas_telno.Text + "','" + bas_email.Text + "','" + bas_mil_sta.Text + "','" + bas_mil_mil.Text + "','" + bas_mil_rnk.Text + "','" + bas_mar.Text + "','" + bas_acc_bank1.Text + "','" + bas_acc_name1.Text + "','" + bas_acc_no1.Text + "','" + bas_acc_bank2.Text + "','" + bas_acc_name2.Text + "','" + bas_acc_no2.Text + "','" + bas_cont.Text + "','" + bas_intern.Text + "','" + bas_intern_no.Text + "','" + func.insertdate(bas_emp_sdate, bas_emp_sdate.Text) + "','" + func.insertdate(bas_emp_edate, bas_emp_edate.Text) + "','" + func.insertdate(bas_entdate, bas_entdate.Text) + "','" + func.insertdate(bas_resdate, bas_resdate.Text) + "','" + func.insertdate(bas_levdate, bas_levdate.Text) + "','" + func.insertdate(bas_reidate, bas_reidate.Text) + "','" + bas_wsta.Text + "','" + sts1[0] + "','" + pos1[0] + "','" + dut1[0] + "','" + p_bas_code.Text + "','" + bas_rmk.Text + "','" + func.insertdate(bas_pos_dt, bas_pos_dt.Text) + "','" + func.insertdate(bas_dut_dt, bas_dut_dt.Text) + "','" + func.insertdate(bas_dept_dt, bas_pos_dt.Text) + "','" + func.insertdate(bas_intern_dt, bas_intern_dt.Text) + "')";




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
                            clear();
                        }
                    }






                    if (btnchk.Text == "M")
                    {
                        String mmsg = "수정하시겠습니까";
                        DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        if (results == DialogResult.Yes)
                        {

                            string sql2 = "update thrm_bas_pjh set bas_resno='" + bas_resno.Text + "',bas_name='" + bas_name.Text + "',bas_cname='" + bas_cname.Text + "',bas_ename='" + bas_ename.Text + "',bas_fix='" + bas_fix.Text + "',bas_zip='" + bas_zip.Text + "',bas_addr='" + bas_addr.Text + "',bas_residence='" + bas_residence.Text + "',bas_hdpno='" + bas_hdpno.Text + "',bas_telno='" + bas_telno.Text + "',bas_email='" + bas_email.Text + "',bas_mil_sta='" + bas_mil_sta.Text + "',bas_mil_mil='" + bas_mil_mil.Text + "',bas_mil_rnk='" + bas_mil_rnk.Text + "',bas_mar='" + bas_mar.Text + "',bas_acc_bank1='" + bas_acc_bank1.Text + "',bas_acc_name1='" + bas_acc_name1.Text + "',bas_acc_no1='" + bas_acc_no1.Text + "',bas_acc_bank2='" + bas_acc_bank2.Text + "',bas_acc_name2='" + bas_acc_name2.Text + "',bas_acc_no2='" + bas_acc_no2.Text + "',bas_cont='" + bas_cont.Text + "',bas_intern='" + bas_intern.Text + "',bas_intern_no='" + bas_intern_no.Text + "',bas_emp_sdate='" + bas_emp_sdate.Value.ToString("yyyyMMdd") + "',bas_emp_edate='" + bas_emp_edate.Value.ToString("yyyyMMdd") + "',bas_entdate='" + bas_entdate.Value.ToString("yyyyMMdd") + "',bas_resdate='" + bas_resdate.Value.ToString("yyyyMMdd") + "',bas_levdate='" + bas_levdate.Value.ToString("yyyyMMdd") + "',bas_reidate='" + bas_reidate.Value.ToString("yyyyMMdd") + "',bas_wsta='" + bas_wsta.Text + "',bas_sts='" + sts1[0] + "',bas_pos='" + pos1[0] + "',bas_dut='" + dut1[0] + "',bas_dept='" + p_bas_dept.Text + "',bas_rmk='" + bas_rmk.Text + "',bas_pos_dt='" + bas_pos_dt.Value.ToString("yyyyMMdd") + "',bas_dut_dt='" + bas_dut_dt.Value.ToString("yyyyMMdd") + "',bas_dept_dt='" + bas_dept_dt.Value.ToString("yyyyMMdd") + "',bas_intern_dt='" + bas_intern_dt.Value.ToString("yyyyMMdd") + "' where bas_empno='" + bas_empno.Text + "'";

                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql2;
                            cmd.ExecuteNonQuery();



                            MessageBox.Show("성공");
                            clear();
                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;
                        }
                    }







                    if (btnchk.Text == "D")
                    {

                        String Dmsg = "삭제하시겠습니까";
                        DialogResult resultD = MessageBox.Show(this, Dmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultD == DialogResult.Yes)
                        {
                            string sql2 = "delete from thrm_bas_pjh where bas_empno= '" + bas_empno.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql2;
                            cmd.ExecuteNonQuery();
                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;
                            MessageBox.Show("삭제되었습니다.");
                            clear();
                        }
                    }
                }

            }


            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // 인사기본사항




            if (index_number == 1)
            {
                try
                {
                    if (btnchk.Text == "I")
                    {
                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into THRM_FAM_PJH(fam_empno, fam_rel, fam_name, fam_bth, fam_itg) values('" + fam_empno.Text + "', '" + rel1[0] + "','" + fam_name.Text + "','" + fam_bth.Value.ToString("yyyyMMdd") + "','" + fam_itg.Text + "')";



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
                            clear();
                        }
                    }

                    if (btnchk.Text == "M")
                    {

                        String msg1 = "수정하시겠습니까";
                        DialogResult resultM = MessageBox.Show(this, msg1, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultM == DialogResult.Yes)
                        {
                            string sql1 = $"update THRM_FAM_PJH set fam_bth='" + fam_bth.Value.ToString("yyyyMMdd") + "', fam_itg='" + fam_itg.Text + "' where fam_empno='" + fam_empno.Text + "' and fam_name='" + fam_name.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("수정되었습니다.");
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("등록되지 않은 정보다 ");
                        }
                    }
                    if (btnchk.Text == "D")
                    {
                        String msgD1 = "삭제하시겠습니까";
                        DialogResult resultD = MessageBox.Show(this, msgD1, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultD == DialogResult.Yes)
                        {
                            string sql1 = "delete from THRM_EDU_PJH where eud_loe = '" + edu_loe.Text + "'";



                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("삭제되었습니다.");
                            clear();
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // 가족사항

            try
            {
                if (index_number == 2)
                {
                    if (btnchk.Text == "I")
                    {
                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into thrm_edu_pjh(edu_empno,edu_loe,edu_entdate,edu_gradate,edu_schnm,edu_dept,edu_degree,edu_grade,edu_gra,edu_last) values('" + edu_empno.Text + "','" + edu_loe.Text + "','" + edu_entdate.Value.ToString("yyyyMMdd") + "','" + edu_gradate.Value.ToString("yyyyMMdd") + "','" + edu_schnm.Text + "','" + edu_dept.Text + "','" + edu_degree.Text + "','" + edu_grade.Text + "','" + edu_gra.Text + "','" + edu_last.Text + "')";


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
                            clear();
                        }
                    }
                    if (btnchk.Text == "M")
                    {
                        String mmsg = "수정하시겠습니까";
                        DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (results == DialogResult.Yes)
                        {
                            string sql1 = $"update THRM_EDU_PJH set edu_entdate='" + edu_entdate.Value.ToString("yyyyMMdd") + "', edu_gradate ='" + edu_gradate.Value.ToString("yyyyMMdd") + "',  edu_schnm ='" + edu_schnm.Text + "', edu_dept = '" + edu_dept.Text + "',edu_degree='" + edu_degree.Text + "',edu_grade = '" + edu_grade.Text + "', edu_gra = '" + edu_gra.Text + "', edu_last = '" + edu_last.Text + "' where edu_empno='" + edu_empno.Text + "' and edu_loe='" + edu_loe.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("수정되었습니다.");
                            clear();
                        }
                    }
                    if (btnchk.Text == "D")
                    {
                        String Dmsg = "삭제하시겠습니까";
                        DialogResult resultD = MessageBox.Show(this, Dmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultD == DialogResult.Yes)
                        {
                            string sql2 = "delete from thrm_bas_pjh where bas_empno= '" + bas_empno.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql2;
                            cmd.ExecuteNonQuery();
                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;
                            MessageBox.Show("삭제되었습니다.");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // 학력사항
            try
            {
                if (index_number == 3)
                {
                    if (btnchk.Text == "I")
                    {
                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into thrm_award_pjh(award_empno,award_date,award_type,award_no,award_kind,award_organ,award_content,award_inout,award_pos,award_dept) values('" + award_empno.Text + "','" + award_date.Value.ToString("yyyyMMdd") + "','" + award_type.Text + "','" + award_no.Text + "','" + award_kind.Text + "','" + award_organ.Text + "','" + award_content.Text + "','" + award_inout.Text + "','" + award_pos.Text + "','" + award_dept.Text + "')";


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
                            award_clear();
                        }
                    }
                    if (btnchk.Text == "M")
                    {
                        String mmsg = "수정하시겠습니까";
                        DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (results == DialogResult.Yes)
                        {
                            string sql1 = $"update THRM_AWARD_PJH set award_type='" + award_type.Text + "', award_no ='" + award_no.Text + "',  award_kind ='" + award_kind.Text + "', award_organ = '" + award_organ.Text + "', award_content='" + award_content.Text + "',award_inout = '" + award_inout.Text + "', award_pos = '" + award_pos.Text + "', award_dept = '" + award_dept.Text + "' where award_empno='" + award_empno.Text + "' and award_date='" + award_date.Value.ToString("yyyyMMdd") + "'";



                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("수정되었습니다.");
                            award_clear();
                        }
                    }
                    if (btnchk.Text == "D")
                    {

                        String Dmsg = "삭제하시겠습니까";
                        DialogResult resultD = MessageBox.Show(this, Dmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultD == DialogResult.Yes)
                        {
                            string sql2 = "delete from thrm_award_pjh where award_empno= '" + award_empno.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql2;
                            cmd.ExecuteNonQuery();
                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;
                            MessageBox.Show("삭제되었습니다.");
                            award_clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } //상벌이력
            try
            {

                if (index_number == 4)
                {
                    if (btnchk.Text == "I")
                    {
                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into thrm_car_pjh(car_empno,car_com,car_region,car_yyyymm_f,car_yyyymm_t,car_pos,car_dept,car_job,car_reason) values('" + car_empno.Text + "','" + car_com.Text + "','" + car_region.Text + "','" + car_yyyymm_f.Value.ToString("yyyyMM") + "','" + car_yyyymm_t.Value.ToString("yyyyMM") + "','" + car_pos.Text + "','" + car_dept.Text + "','" + car_job.Text + "','" + car_reason.Text + "')";


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
                            car_clear();
                        }
                    }
                    if (btnchk.Text == "M")
                    {
                        String mmsg = "수정하시겠습니까";
                        DialogResult results = MessageBox.Show(this, mmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (results == DialogResult.Yes)
                        {
                            string sql1 = $"update THRM_CAR_PJH set car_region='" + car_region.Text + "', car_yyyymm_f ='" + car_yyyymm_f.Value.ToString("yyyyMM") + "',  car_yyyymm_t ='" + car_yyyymm_t.Value.ToString("yyyyMM") + "', car_pos = '" + car_pos.Text + "', car_dept='" + car_dept.Text + "', car_job = '" + car_job.Text + "', car_reason = '" + car_reason.Text + "' where car_empno='" + car_empno.Text + "' and car_com='" + car_com.Text + "'";

                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("수정되었습니다.");
                            car_clear();
                        }
                    }

                    if (btnchk.Text == "D")
                    {

                        String Dmsg = "삭제하시겠습니까";
                        DialogResult resultD = MessageBox.Show(this, Dmsg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultD == DialogResult.Yes)
                        {
                            string sql2 = "delete from thrm_car_pjh where car_empno= '" + car_empno.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql2;
                            cmd.ExecuteNonQuery();
                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;
                            MessageBox.Show("삭제되었습니다.");
                            car_clear();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } //경력사항

            if (index_number == 5)
            {
                try
                {
                    if (btnchk.Text == "I")
                    {
                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into THRM_LIC_PJH(lic_empno, lic_code, lic_grade, lic_acqdate, lic_organ) values('" + lic_empno.Text + "', '" + lic[0] + "','" + lic_grade.Text + "','" + lic_acqdate.Value.ToString("yyyyMMdd") + "','" + lic_organ.Text + "')";



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
                            clear();
                        }
                    }

                    if (btnchk.Text == "M")
                    {

                        String msg1 = "수정하시겠습니까";
                        DialogResult resultM = MessageBox.Show(this, msg1, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultM == DialogResult.Yes)
                        {
                            string sql1 = $"update THRM_LIC_PJH set lic_grade='" + lic_grade.Text + "', lic_acqdate ='" + lic_acqdate.Value.ToString("yyyyMMdd") + "', lic_organ = '" + lic_organ.Text + "' where lic_empno='" + lic_empno.Text + "' and lic_code='" + lic_code.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("수정되었습니다.");
                            try
                            {
                                dataGridView6.Rows.Clear();
                                dataGridView6.Columns.Clear();
                                dataGridView6.Columns.Add("lic_code", "자격면허코드");
                                dataGridView6.Columns.Add("lic_grade", "급수");
                                dataGridView6.Columns.Add("lic_acqdate", "취득일");
                                dataGridView6.Columns.Add("lic_organ", "발급기관");



                                string selectsqll = "select lic_empno, lic_code, lic_grade, lic_acqdate, lic_organ  " +
                                                    "from thrm_lic_pjh," +
                                                    "(select bas_empno from THRM_BAS_PJH where bas_empno = '" + lic_empno.Text + "')" +
                                                    " where lic_empno = bas_empno and lic_empno = '" + lic_empno.Text + "'";


                                cmd = new OracleCommand();
                                cmd.Connection = pgOraConn;
                                cmd.CommandText = selectsqll;
                                OracleDataReader rd = cmd.ExecuteReader();

                                int cnt = 0;
                                while (rd.Read())
                                {
                                    dataGridView6.Rows.Add();
                                    dataGridView6.Rows[cnt].Cells["lic_code"].Value = rd["lic_code"].ToString();
                                    dataGridView6.Rows[cnt].Cells["lic_grade"].Value = rd["lic_grade"].ToString();
                                    dataGridView6.Rows[cnt].Cells["lic_acqdate"].Value = rd["lic_acqdate"].ToString();
                                    dataGridView6.Rows[cnt].Cells["lic_organ"].Value = rd["lic_organ"].ToString();

                                    cnt++;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("등록되지 않은 정보다 ");
                        }
                    }
                    if (btnchk.Text == "D")
                    {
                        String msgD1 = "삭제하시겠습니까";
                        DialogResult resultD = MessageBox.Show(this, msgD1, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultD == DialogResult.Yes)
                        {
                            string sql1 = "delete from THRM_LIC_PJH where lic_grade = '" + lic_grade.Text + "'";



                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("삭제되었습니다.");
                            clear();
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // 자격면허
            if (index_number == 6)
            {
                try
                {
                    if (btnchk.Text == "I")
                    {
                        String msg = "저장하시겠습니까";
                        DialogResult result = MessageBox.Show(this, msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.Yes)
                        {
                            string sql1 = "insert into THRM_FORL_PJH(forl_code, forl_score, forl_acqdate, forl_organ) values('" + forl_code.Text + "', '" + forl_score.Text + "','" + forl_acqdate.Value.ToString("yyyyMMdd") + "','" + forl_organ.Text + "')";



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
                            clear();
                        }
                    }

                    if (btnchk.Text == "M")
                    {

                        String msg1 = "수정하시겠습니까";
                        DialogResult resultM = MessageBox.Show(this, msg1, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultM == DialogResult.Yes)
                        {
                            string sql1 = $"update THRM_FORL_PJH set forl_acqdate='" + forl_acqdate.Value.ToString("yyyyMMdd") + "', forl_score='" + forl_score.Text + "' where lic_empno='" + lic_empno.Text + "' and forl_code='" + forl_code.Text + "'";


                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("수정되었습니다.");
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("등록되지 않은 정보다 ");
                        }
                    }
                    if (btnchk.Text == "D")
                    {
                        String msgD1 = "삭제하시겠습니까";
                        DialogResult resultD = MessageBox.Show(this, msgD1, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (resultD == DialogResult.Yes)
                        {
                            string sql1 = "delete from THRM_EDU_PJH where eud_loe = '" + edu_loe.Text + "'";



                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = pgOraConn;
                            cmd.CommandText = sql1;
                            cmd.ExecuteNonQuery();


                            intbtn.Enabled = true;
                            modbtn.Enabled = true;
                            delbtn.Enabled = true;
                            canbtn.Enabled = false;
                            cfmbtn.Enabled = false;


                            MessageBox.Show("삭제되었습니다.");
                            clear();
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // 가족사항
        }  // 확인





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
        private void fam_list()
        {

            String fam_sql = "select * from THRM_BAS_pjh where BAS_EMPNO='" + bas_empno.Text + "'";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = fam_sql;

            OracleDataReader rd = cmd.ExecuteReader();


            if (rd.Read())
            {
                fam_empno.Text = rd["bas_empno"].ToString();
                bas_name_f.Text = rd["bas_name"].ToString();
                bas_pos_f.Text = rd["bas_pos"].ToString();
                bas_dept_f.Text = rd["bas_dept"].ToString();
                //가족인사

            }
        }
        private void fam_display() // 가족사항 리스트 불러오기
        {
            String fam_sql = "SELECT * FROM THRM_BAS_PJH," +
                               "(SELECT fam_name, fam_itg, fam_rel, fam_bth FROM THRM_FAM_PJH)" +
                               "WHERE bas_empno = fam_name and fam_name = '" + fam_name.Text + "'";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = fam_sql;

            OracleDataReader rd = cmd.ExecuteReader();


            if (rd.Read())
            {
                fam_name.Text = rd["fam_name"].ToString();
                fam_itg.Text = rd["fam_itg"].ToString();
                fam_rel.Text = rd["fam_rel"].ToString();
                fam_bth.Value = tp(rd["fam_bth"].ToString());
                //가족인사
            }
            string sql1 = "select cd_code,cd_codnms from TIEAS_CD_PJH where cd_grpcd = '' and cd_code = (select fam_rel from THRM_FAM_PJH where fam_empno ='" +
                bas_empno.Text + "')"; //가족관계
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;
            OracleDataReader rdd = cmd.ExecuteReader();

            while (rdd.Read())
            {
                fam_rel.Text = (rdd["cd_code"].ToString() + ":" + rdd["cd_codnms"].ToString());
            }
        }


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

        private void sabun_display()  //인사기본사항 데이터 리스트 불러오기
        {

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
                p_bas_code.Text = rd["bas_dept"].ToString();
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
        private void distinct(string code_name, ComboBox combo)
        {
            string sql10 = "select dept_name from THRM_DEPT_PJH where dept_code = '" + award_dept_a.Text + "'"; //학력부서
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql10;
            OracleDataReader rrrd = cmd.ExecuteReader();

            while (rrrd.Read())
            {
                combo.Text = rrrd["code_name"].ToString();
            }
        }

        private void edu_display()
        {
            String edu_sql = "SELECT * FROM THRM_BAS_PJH," +
                             "(SELECT edu_loe,edu_schnm, edu_entdate, edu_gradate,edu_dept,edu_degree,edu_grade,edu_gra,edu_last FROM THRM_EDU_PJH)" +
                             "WHERE bas_empno = edu_schnm and edu_schnm = '" + edu_schnm.Text + "'";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = edu_sql;

            OracleDataReader rd = cmd.ExecuteReader();


            if (rd.Read())
            {
                edu_loe.Text = rd["edu_loe"].ToString();
                func.insertdatetime(edu_entdate, rd["edu_entdate"].ToString());
                func.insertdatetime(edu_gradate, rd["edu_gradate"].ToString());
                edu_schnm.Text = rd["edu_schnm"].ToString();
                edu_dept.Text = rd["edu_dept"].ToString();
                edu_degree.Text = rd["edu_degree"].ToString();
                edu_grade.Text = rd["edu_grade"].ToString();
                edu_gra.Text = rd["edu_gra"].ToString();
                edu_last.Text = rd["edu_last"].ToString();
                //학력사항
            }

        }  // 가족사항

        private void award_display()
        {
            String award_sql = "SELECT * FROM THRM_BAS_PJH," +
                            "(SELECT award_date,award_type, award_no, award_kind,award_organ,award_content,award_inout,award_pos,award_dept FROM THRM_AWARD_PJH)" +
                            "WHERE bas_empno = award_type and award_type = '" + award_type.Text + "'";
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = award_sql;

            OracleDataReader rd = cmd.ExecuteReader();


            if (rd.Read())
            {
                func.insertdatetime(award_date, rd["award_date"].ToString());
                award_type.Text = rd["award_type"].ToString();
                award_no.Text = rd["award_no"].ToString();
                award_kind.Text = rd["award_kind"].ToString();
                award_organ.Text = rd["award_organ"].ToString();
                award_content.Text = rd["award_content"].ToString();
                award_inout.Text = rd["award_inout"].ToString();
                award_pos.Text = rd["award_pos"].ToString();
                award_dept.Text = rd["award_dept"].ToString();
            }
        } //상벌이력
        private void car_display()
        {
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
        private void modbtn_Click(object sender, EventArgs e)//db에서 데이터를 읽어올 부분
        {
            btnchk.Text = "M";
            if (bas_empno.Text == "")
            {
                MessageBox.Show("사원번호를 입력하시오");


                return;
            }
            btn_controll();
            sabun_display();
        }

        private void delbtn_Click(object sender, EventArgs e) //삭제 버튼
        {
            if (bas_empno.Text == "")
            {
                MessageBox.Show("사원번호를 입력하시오");
                return;
            }
            btnchk.Text = "D";
            btn_controll();

        }

        public void tabgate(int get)
        {
            tab1.SelectedIndex = get;
        } //탭 컨트롤

        private void bas_emp_sdate_ValueChanged(object sender, EventArgs e)
        {
            bas_emp_sdate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_emp_edate_ValueChanged(object sender, EventArgs e)
        {
            bas_emp_edate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_wsta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnchk.Text == "I")
            {
                if (bas_wsta.Text == "재직")
                {
                    bas_resdate.Enabled = false;
                    bas_levdate.Enabled = false;
                    bas_reidate.Enabled = false;
                    bas_levdate.Format = DateTimePickerFormat.Custom;
                    bas_reidate.Format = DateTimePickerFormat.Custom;
                    bas_resdate.Format = DateTimePickerFormat.Custom;
                }
                if (bas_wsta.Text == "휴직")
                {
                    bas_resdate.Enabled = false;
                    bas_resdate.Format = DateTimePickerFormat.Custom;
                    bas_levdate.Enabled = true;
                    bas_reidate.Enabled = true;
                    bas_levdate.Format = DateTimePickerFormat.Short;
                    bas_reidate.Format = DateTimePickerFormat.Short;
                }
                if (bas_wsta.Text == "퇴직")
                {
                    bas_reidate.Enabled = false;
                    bas_levdate.Enabled = false;
                    bas_resdate.Enabled = true;
                    bas_reidate.Format = DateTimePickerFormat.Short;
                    bas_levdate.Format = DateTimePickerFormat.Short;
                    bas_resdate.Format = DateTimePickerFormat.Short;


                }
            }
            if (btnchk.Text == "M")
            {
                if (bas_wsta.Text == "재직")
                {
                    bas_resdate.Enabled = false;
                    bas_reidate.Enabled = false;
                    bas_levdate.Enabled = false;
                    bas_levdate.Format = DateTimePickerFormat.Custom;
                    bas_resdate.Format = DateTimePickerFormat.Custom;
                    bas_reidate.Format = DateTimePickerFormat.Custom;
                }
                else if (bas_wsta.Text == "휴직")
                {

                    bas_emp_sdate.Enabled = false;
                    bas_intern_dt.Enabled = false;
                    bas_pos_dt.Enabled = true;
                    bas_dept_dt.Enabled = true;
                    bas_dut_dt.Enabled = true;
                    bas_levdate.Enabled = true;
                    bas_emp_edate.Enabled = false;
                    bas_resdate.Enabled = false;
                    bas_resdate.Format = DateTimePickerFormat.Custom;

                    bas_levdate.Format = DateTimePickerFormat.Short;
                    bas_reidate.Format = DateTimePickerFormat.Short;
                    bas_reidate.Enabled = true;
                }
                else
                {
                    bas_emp_sdate.Enabled = false;
                    bas_emp_edate.Enabled = false;
                    bas_levdate.Enabled = false;
                    bas_reidate.Enabled = false;
                }
            }
        }  //신분 



        private void bas_intern_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_intern_dt.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_intern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bas_intern.Text == "수습")
            {
                bas_intern_no.Text = "3";
            }
            else
            {
                bas_intern_no.Text = "9";
            }
        }

        private void bas_cont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnchk.Text == "I")
            {
                if (bas_cont.Text == "정규직")
                {
                    bas_intern_dt.Enabled = false;
                    bas_emp_edate.Enabled = false;
                    bas_emp_sdate.Enabled = false;
                    bas_emp_edate.Enabled = false;
                    bas_emp_sdate.Enabled = false;
                    bas_emp_edate.Format = DateTimePickerFormat.Custom;
                    bas_emp_sdate.Format = DateTimePickerFormat.Custom;
                    bas_intern_dt.Format = DateTimePickerFormat.Custom;
                }
                else if (bas_cont.Text == "계약직")
                {
                    bas_reidate.Enabled = false;
                    bas_intern_dt.Enabled = true;
                    bas_emp_edate.Enabled = true;
                    bas_emp_sdate.Enabled = true;

                    bas_emp_edate.Format = DateTimePickerFormat.Short;
                    bas_emp_sdate.Format = DateTimePickerFormat.Short;
                    bas_intern_dt.Format = DateTimePickerFormat.Short;

                }
            }
            if (btnchk.Text == "M")
            {
                if (bas_cont.Text == "정규직")
                {
                    bas_emp_edate.Enabled = false;
                    bas_emp_sdate.Enabled = false;

                    bas_emp_edate.Format = DateTimePickerFormat.Custom;
                    bas_emp_sdate.Format = DateTimePickerFormat.Custom;
                    bas_levdate.Format = DateTimePickerFormat.Custom;


                }
                if (bas_cont.Text == "계약직")
                {
                    bas_intern_dt.Enabled = true;

                    bas_intern_no.Enabled = true;
                    bas_intern.Enabled = true;
                    bas_emp_edate.Enabled = true;
                    bas_emp_sdate.Enabled = true;
                    bas_emp_edate.Format = DateTimePickerFormat.Short;
                    bas_emp_sdate.Format = DateTimePickerFormat.Short;
                }
            }
        }//버튼 관리



        private void bas_fix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bas_fix.Text == "여")
            {

                bas_mil_sta.Enabled = false;
                bas_mil_mil.Enabled = false;
                bas_mil_rnk.Enabled = false;
            }
            else
            {
                bas_mil_sta.Enabled = true;
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("bas_empno", "사번");
                dataGridView1.Columns.Add("bas_name", "성명");
                dataGridView1.Columns.Add("cd_codnms", "직급");
                dataGridView1.Columns.Add("dept_name", "부서");
                String sql1 = " select bas_empno, bas_name, bas_pos, cd_codnms, bas_dept, dept_name " +
                              " from THRM_BAS_pjh, " +
                              " (select cd_grpcd, cd_code, cd_codnms from TIEAS_CD_pjh where cd_grpcd = 'POS')," +
                              " THRM_DEPT_pjh" +
                              " where bas_pos = cd_code " +
                              " and bas_dept = dept_code " +
                              " and bas_empno like '%' " +
                              " and bas_name like '%' " +
                              " and dept_name like '%' " +
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


        private void button1_Click(object sender, EventArgs e)
        {
            //부서버튼 클릭시
            buseo frm2 = new buseo(this);
            frm2.ShowDialog();
        }
        public void awo(string oo)
        {
            award_dept.Text = oo;
        }
        public void awa(string oop)
        {
            award_dept_1.Text = oop;

        }
        public void oo(string opop)
        {
            p_bas_code.Text = opop;

        }
        public void op(string opd)
        {
            p_bas_dept.Text = opd;
        }
        public void zipg(string zipg)
        {
            bas_zip.Text = zipg;
        }
        public void addrg(string addrg)
        {
            bas_addr.Text = addrg;
        }
            

        private void bas_email_TextChanged(object sender, EventArgs e)
        {

            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(bas_email.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.bas_email, "plzzz provide valid Mail address");

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string image_file = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"D:\";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image_file = dialog.FileName;
            }
            else if (dialog.ShowDialog() == DialogResult.Cancel)
            {

                return;
            }
            pictureBox1.Image = Bitmap.FromFile(image_file);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        private void button9_Click(object sender, EventArgs e)
        {
            Form6 s = new Form6(this);
            s.Show();

        }



        private void p_bas_code_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "select dept_name from THRM_DEPT_PJH where dept_code = '" + p_bas_code.Text + "' ";
            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;

            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) // select 했을때 data 가 있는 경우 data 값을 텍스트박스안에 넣는다.
            {

                p_bas_dept.Text = rd["dept_name"].ToString();

            }
        }


        private void bas_dut_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_dut_dt.CustomFormat = "yyyy-MM-dd";
        }


        private void tab1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index_number = tab1.SelectedIndex;

            // 만약에 셀을 눌렀다 사원번호가 있다.
            //  - 이제 탭 이벤트 실행이 됐다.
            //      - 그거에 맞춰서 list 뽑기. (list를 인자를 받아서 여러개를 가져와야함)
            // ex) 인자를 숫자로 받는다 fam_list(SelectedIndex);


            if (tab1.SelectedIndex == 1)
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    fam_list();
                }


            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            fam_name.Text = dataGridView2.Rows[e.RowIndex].Cells["fam_name"].Value.ToString();
            fam_rel.Text = dataGridView2.Rows[e.RowIndex].Cells["cd_codnms"].Value.ToString();
            fam_itg.Text = dataGridView2.Rows[e.RowIndex].Cells["fam_itg"].Value.ToString();
            fam_bth.Value = tp(dataGridView2.Rows[e.RowIndex].Cells["fam_bth"].Value.ToString());
            fam_display();
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
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

        private void fam_empno_TextChanged(object sender, EventArgs e) // e
        {
            try
            {
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Clear();
                dataGridView2.Columns.Add("cd_codnms", "관계");
                dataGridView2.Columns.Add("fam_name", "성명");
                dataGridView2.Columns.Add("fam_bth", "생년월일");
                dataGridView2.Columns.Add("fam_itg", "동거여부");

                string selectsqll = " select Fam_empno, fam_Rel, cd_codnms, fam_name, fam_bth, fam_itg " +
                                    " from THRM_FAM_PJH," +
                                    " (select cd_grpcd, cd_code, cd_codnms from TIEAS_CD_PJH where cd_grpcd = 'REL') " +
                                    " where fam_Rel = cd_code and fam_empno = '" + fam_empno.Text + "' ORDER BY fam_bth";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = selectsqll;
                OracleDataReader rd = cmd.ExecuteReader();

                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[cnt].Cells["cd_codnms"].Value = rd["cd_codnms"].ToString();
                    dataGridView2.Rows[cnt].Cells["fam_name"].Value = rd["fam_name"].ToString();
                    dataGridView2.Rows[cnt].Cells["fam_bth"].Value = rd["fam_bth"].ToString();
                    dataGridView2.Rows[cnt].Cells["fam_itg"].Value = rd["fam_itg"].ToString();
                    cnt++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView3_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            edu_loe.Text = dataGridView3.Rows[e.RowIndex].Cells["edu_loe"].Value.ToString();
            edu_entdate.Value = tp(dataGridView3.Rows[e.RowIndex].Cells["edu_entdate"].Value.ToString());
            edu_gradate.Value = tp(dataGridView3.Rows[e.RowIndex].Cells["edu_gradate"].Value.ToString());
            edu_schnm.Text = dataGridView3.Rows[e.RowIndex].Cells["edu_schnm"].Value.ToString();
            edu_dept.Text = dataGridView3.Rows[e.RowIndex].Cells["edu_dept"].Value.ToString();
            edu_degree.Text = dataGridView3.Rows[e.RowIndex].Cells["edu_degree"].Value.ToString();
            edu_grade.Text = dataGridView3.Rows[e.RowIndex].Cells["edu_grade"].Value.ToString();
            edu_gra.Text = dataGridView3.Rows[e.RowIndex].Cells["edu_gra"].Value.ToString();
            edu_last.Text = dataGridView3.Rows[e.RowIndex].Cells["edu_last"].Value.ToString();
            edu_display();
        }


        private void edu_empno_TextChanged_2(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Columns.Clear();
                dataGridView3.Columns.Add("edu_loe", "학력구분");
                dataGridView3.Columns.Add("edu_entdate", "입학일자");
                dataGridView3.Columns.Add("edu_gradate", "졸업일자");
                dataGridView3.Columns.Add("edu_schnm", "학교명");
                dataGridView3.Columns.Add("edu_dept", "학과(전공)");
                dataGridView3.Columns.Add("edu_degree", "학위");
                dataGridView3.Columns.Add("edu_grade", "성적");
                dataGridView3.Columns.Add("edu_gra", "졸업구분");
                dataGridView3.Columns.Add("edu_last", "최종여부");

                string selectsqll = "select edu_empno, edu_loe, edu_entdate, edu_gradate, edu_schnm, edu_dept, edu_degree, edu_grade, edu_gra, edu_last  " +
                                    "from thrm_edu_pjh," +
                                    "(select bas_empno from THRM_BAS_PJH where bas_empno = '" + edu_empno.Text + "')" +
                                    " where edu_empno = bas_empno and edu_empno = '" + edu_empno.Text + "'";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = selectsqll;
                OracleDataReader rd = cmd.ExecuteReader();

                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows[cnt].Cells["edu_loe"].Value = rd["edu_loe"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_entdate"].Value = rd["edu_entdate"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_gradate"].Value = rd["edu_gradate"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_schnm"].Value = rd["edu_schnm"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_dept"].Value = rd["edu_dept"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_degree"].Value = rd["edu_degree"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_grade"].Value = rd["edu_grade"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_gra"].Value = rd["edu_gra"].ToString();
                    dataGridView3.Rows[cnt].Cells["edu_last"].Value = rd["edu_last"].ToString();
                    cnt++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // 학력사항

        private void award_empno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView4.Columns.Clear();
                dataGridView4.Columns.Clear();
                dataGridView4.Columns.Add("award_date", "상벌일자");
                dataGridView4.Columns.Add("award_type", "상벌구분");
                dataGridView4.Columns.Add("award_no", "상벌번호");
                dataGridView4.Columns.Add("award_kind", "상별종별");
                dataGridView4.Columns.Add("award_organ", "시행처");
                dataGridView4.Columns.Add("award_content", "상벌내용");
                dataGridView4.Columns.Add("award_inout", "내외구분");
                dataGridView4.Columns.Add("award_pos", "직급(당시)");
                dataGridView4.Columns.Add("award_dept", "소속(당시)");

                string selectsqll = "select award_empno, award_date, award_type, award_no, award_kind, award_organ, award_content, award_inout, award_pos, award_dept  " +
                                    "from thrm_award_pjh," +
                                    "(select bas_empno from THRM_BAS_PJH where bas_empno = '" + award_empno.Text + "')" +
                                    " where award_empno = bas_empno and award_empno = '" + award_empno.Text + "'";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = selectsqll;
                OracleDataReader rd = cmd.ExecuteReader();

                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView4.Rows.Add();
                    dataGridView4.Rows[cnt].Cells["award_date"].Value = rd["award_date"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_type"].Value = rd["award_type"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_no"].Value = rd["award_no"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_kind"].Value = rd["award_kind"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_organ"].Value = rd["award_organ"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_content"].Value = rd["award_content"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_inout"].Value = rd["award_inout"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_pos"].Value = rd["award_pos"].ToString();
                    dataGridView4.Rows[cnt].Cells["award_dept"].Value = rd["award_dept"].ToString();
                    cnt++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // 상벌이력사항



        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            award_date.Value = tp(dataGridView4.Rows[e.RowIndex].Cells["award_date"].Value.ToString());
            award_type.Text = dataGridView4.Rows[e.RowIndex].Cells["award_type"].Value.ToString();
            award_no.Text = dataGridView4.Rows[e.RowIndex].Cells["award_no"].Value.ToString();
            award_kind.Text = dataGridView4.Rows[e.RowIndex].Cells["award_kind"].Value.ToString();
            award_organ.Text = dataGridView4.Rows[e.RowIndex].Cells["award_organ"].Value.ToString();
            award_content.Text = dataGridView4.Rows[e.RowIndex].Cells["award_content"].Value.ToString();
            award_inout.Text = dataGridView4.Rows[e.RowIndex].Cells["award_inout"].Value.ToString();
            award_pos.Text = dataGridView4.Rows[e.RowIndex].Cells["award_pos"].Value.ToString();
            award_dept.Text = dataGridView4.Rows[e.RowIndex].Cells["award_dept"].Value.ToString();
            award_display();

        }//경력사항

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 frm3 = new Form7(this);
            frm3.ShowDialog();
        }



        private void dataGridView5_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            car_com.Text = dataGridView5.Rows[e.RowIndex].Cells["car_com"].Value.ToString();
            car_region.Text = dataGridView5.Rows[e.RowIndex].Cells["car_region"].Value.ToString();
            car_yyyymm_f.Value = tp1(dataGridView5.Rows[e.RowIndex].Cells["car_yyyymm_f"].Value.ToString());
            car_yyyymm_t.Value = tp1(dataGridView5.Rows[e.RowIndex].Cells["car_yyyymm_t"].Value.ToString());
            car_pos.Text = dataGridView5.Rows[e.RowIndex].Cells["car_pos"].Value.ToString();
            car_dept.Text = dataGridView5.Rows[e.RowIndex].Cells["car_dept"].Value.ToString();
            car_job.Text = dataGridView5.Rows[e.RowIndex].Cells["car_job"].Value.ToString();
            car_reason.Text = dataGridView5.Rows[e.RowIndex].Cells["car_reason"].Value.ToString();
            car_display();
        }

        private void car_empno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView5.Columns.Clear();
                dataGridView5.Columns.Clear();
                dataGridView5.Columns.Add("car_com", "근무처명");
                dataGridView5.Columns.Add("car_region", "소재지");
                dataGridView5.Columns.Add("car_yyyymm_f", "근무시작월");
                dataGridView5.Columns.Add("car_yyyymm_t", "근무종료월");
                dataGridView5.Columns.Add("car_pos", "최종직급");
                dataGridView5.Columns.Add("car_dept", "담당부서");
                dataGridView5.Columns.Add("car_job", "담당업무");
                dataGridView5.Columns.Add("car_reason", "퇴직/이직 사유");


                string selectsqll = "select car_empno, car_com, car_region, car_yyyymm_f, car_yyyymm_t, car_pos, car_pos, car_dept, car_job, car_reason  " +
                                    "from thrm_car_pjh," +
                                    "(select bas_empno from THRM_BAS_PJH where bas_empno = '" + car_empno.Text + "')" +
                                    " where car_empno = bas_empno and car_empno = '" + car_empno.Text + "'";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = selectsqll;
                OracleDataReader rd = cmd.ExecuteReader();

                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView5.Rows.Add();
                    dataGridView5.Rows[cnt].Cells["car_com"].Value = rd["car_com"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_region"].Value = rd["car_region"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_yyyymm_f"].Value = rd["car_yyyymm_f"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_yyyymm_t"].Value = rd["car_yyyymm_t"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_pos"].Value = rd["car_pos"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_dept"].Value = rd["car_dept"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_job"].Value = rd["car_job"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_reason"].Value = rd["car_reason"].ToString();

                    cnt++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                dataGridView5.Columns.Clear();
                dataGridView5.Columns.Clear();
                dataGridView5.Columns.Add("car_com", "근무처명");
                dataGridView5.Columns.Add("car_region", "소재지");
                dataGridView5.Columns.Add("car_yyyymm_f", "근무시작월");
                dataGridView5.Columns.Add("car_yyyymm_t", "근무종료월");
                dataGridView5.Columns.Add("car_pos", "최종직급");
                dataGridView5.Columns.Add("car_dept", "담당부서");
                dataGridView5.Columns.Add("car_job", "담당업무");
                dataGridView5.Columns.Add("car_reason", "퇴직/이직 사유");


                string selectsqll = "select car_empno, car_com, car_region, car_yyyymm_f, car_yyyymm_t, car_pos, car_pos, car_dept, car_job, car_reason  " +
                                    "from thrm_car_pjh," +
                                    "(select bas_empno from THRM_BAS_PJH where bas_empno = '" + car_empno.Text + "')" +
                                    " where car_empno = bas_empno and car_empno = '" + car_empno.Text + "'";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = selectsqll;
                OracleDataReader rd = cmd.ExecuteReader();

                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView5.Rows.Add();
                    dataGridView5.Rows[cnt].Cells["car_com"].Value = rd["car_com"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_region"].Value = rd["car_region"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_yyyymm_f"].Value = rd["car_yyyymm_f"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_yyyymm_t"].Value = rd["car_yyyymm_t"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_pos"].Value = rd["car_pos"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_dept"].Value = rd["car_dept"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_job"].Value = rd["car_job"].ToString();
                    dataGridView5.Rows[cnt].Cells["car_reason"].Value = rd["car_reason"].ToString();

                    cnt++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lic_empno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView6.Columns.Clear();
                dataGridView6.Columns.Clear();
                dataGridView6.Columns.Add("lic_code", "자격면허코드");
                dataGridView6.Columns.Add("lic_grade", "급수");
                dataGridView6.Columns.Add("lic_acqdate", "취득일");
                dataGridView6.Columns.Add("lic_organ", "발급기관");



                string selectsqll = "select lic_empno, lic_code, lic_grade, lic_acqdate, lic_organ  " +
                                    "from thrm_lic_pjh," +
                                    "(select bas_empno from THRM_BAS_PJH where bas_empno = '" + lic_empno.Text + "')" +
                                    " where lic_empno = bas_empno and lic_empno = '" + lic_empno.Text + "'";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = selectsqll;
                OracleDataReader rd = cmd.ExecuteReader();

                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView6.Rows.Add();
                    dataGridView6.Rows[cnt].Cells["lic_code"].Value = rd["lic_code"].ToString();
                    dataGridView6.Rows[cnt].Cells["lic_grade"].Value = rd["lic_grade"].ToString();
                    dataGridView6.Rows[cnt].Cells["lic_acqdate"].Value = rd["lic_acqdate"].ToString();
                    dataGridView6.Rows[cnt].Cells["lic_organ"].Value = rd["lic_organ"].ToString();

                    cnt++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView6_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            lic_code.Text = dataGridView6.Rows[e.RowIndex].Cells["lic_code"].Value.ToString();
            lic_grade.Text = dataGridView6.Rows[e.RowIndex].Cells["lic_grade"].Value.ToString();
            lic_acqdate.Value = tp(dataGridView6.Rows[e.RowIndex].Cells["lic_acqdate"].Value.ToString());
            lic_organ.Text = dataGridView6.Rows[e.RowIndex].Cells["lic_organ"].Value.ToString();
            lic_display();
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView7.Columns.Clear();
                dataGridView7.Columns.Clear();
                dataGridView7.Columns.Add("forl_code", "외국어코드");
                dataGridView7.Columns.Add("forl_score", "점수");
                dataGridView7.Columns.Add("forl_acqdate", "취득일");
                dataGridView7.Columns.Add("forl_organ", "발급기관");



                string selectsqll = "select forl_empno, forl_code,forl_score,forl_acqdate, forl_organ  " +
                                    "from thrm_FORL_pjh," +
                                    "(select bas_empno from THRM_BAS_PJH where bas_empno = '" + forl_empno.Text + "')" +
                                    " where forl_empno = bas_empno and forl_empno = '" + forl_empno.Text + "'";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = pgOraConn;
                cmd.CommandText = selectsqll;
                OracleDataReader rd = cmd.ExecuteReader();

                int cnt = 0;
                while (rd.Read())
                {
                    dataGridView6.Rows.Add();
                    dataGridView6.Rows[cnt].Cells["forl_code"].Value = rd["forl_code"].ToString();
                    dataGridView6.Rows[cnt].Cells["forl_grade"].Value = rd["forl_grade"].ToString();
                    dataGridView6.Rows[cnt].Cells["forl_acqdate"].Value = rd["forl_acqdate"].ToString();
                    dataGridView6.Rows[cnt].Cells["forl_organ"].Value = rd["forl_organ"].ToString();

                    cnt++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView7_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            forl_code.Text = dataGridView7.Rows[e.RowIndex].Cells["forl_code"].Value.ToString();
            forl_score.Text = dataGridView7.Rows[e.RowIndex].Cells["forl_score"].Value.ToString();
            forl_acqdate.Value = tp(dataGridView6.Rows[e.RowIndex].Cells["forl_acqdate"].Value.ToString());
            forl_organ.Text = dataGridView7.Rows[e.RowIndex].Cells["forl_organ"].Value.ToString();
        }

        private void index_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void bas_entdate_ValueChanged(object sender, EventArgs e)
        {
            bas_entdate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_resdate_ValueChanged(object sender, EventArgs e)
        {
            bas_resdate.CustomFormat = "yyyy-MM-dd";

        }



        private void bas_dept_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_dept_dt.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_reidate_ValueChanged(object sender, EventArgs e)
        {
            bas_reidate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_levdate_ValueChanged(object sender, EventArgs e)
        {
            bas_levdate.CustomFormat = "yyyy-MM-dd";
        }

        private void bas_pos_dt_ValueChanged(object sender, EventArgs e)
        {
            bas_pos_dt.CustomFormat = "yyyy-MM-dd";

        }

        bool IsAvailableRRN(string RRN)

        {

            //공백 제거

            RRN = RRN.Replace(" ", "");

            //문자 '-' 제거

            RRN = RRN.Replace("-", "");

            //주민등록번호가 13자리인가?

            if (RRN.Length != 13)

            {

                return false;

            }



            int sum = 0;

            for (int i = 0; i < RRN.Length - 1; i++)

            {

                char c = RRN[i];

                //숫자로 이루어져 있는가?

                if (!char.IsNumber(c))

                {

                    return false;

                }

                else

                {

                    if (i < RRN.Length)

                    {

                        //지정된 숫자로 각 자리를 나눈 후 더한다.

                        sum += int.Parse(c.ToString()) * ((i % 8) + 2);

                    }

                }

            }

            // 검증코드와 결과 값이 같은가?

            if (!((((11 - (sum % 11)) % 10).ToString()) == ((RRN[RRN.Length - 1]).ToString())))

            {

                return false;

            }

            return true;

        }

        private void bas_resno_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {



            if (bas_resno.Text.Length > 7)
            {
                if (bas_resno.Text.Substring(7, 1) == "1" || bas_resno.Text.Substring(7, 1) == "3" || bas_resno.Text.Substring(7, 1) == "5")
                {
                    bas_fix.Text = "남";

                }
                else
                {
                    bas_fix.Text = "여";
                }
            }

        }

        private void bas_resno_Click(object sender, EventArgs e)
        {
            if (bas_resno.Text.Length == 7)// 기본 7자 부터 시작한다.
            {
                string txt = bas_resno.Text.Substring(0, 6).Trim(); // '-'제외하고 공백 제거
                if (bas_resno.SelectionStart > txt.Length)  // 입력된 곳을 벗어나 click 하면
                {
                    bas_resno.SelectionStart = txt.Length; // 입력된 곳의 맨끝으로 감
                }

            }
            else
            {
                string txt = bas_resno.Text.Trim(); // 공백 제거

                if (bas_resno.SelectionStart > txt.Length) // 입력된 곳을 벗어나 click하면
                {
                    bas_resno.SelectionStart = txt.Length; // 입력한 곳의 맨끝으로 감
                }


            }
        }

        private void bas_resno_KeyUp(object sender, KeyEventArgs e)
        {
            if (bas_resno.Text.Length == 14)
            {
                if (IsAvailableRRN(bas_resno.Text) == true)
                {
                    bas_name.Focus();

                }
                else
                {
                    bas_resno.Focus();
                }
            }
        }

        private void p_bas_dut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void p_bas_dept_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


