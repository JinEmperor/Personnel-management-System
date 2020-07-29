using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Oracle.ManagedDataAccess.Client;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace insa
{
    public partial class english : Form
    {
        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;
        string dbIp = "222.237.134.74";
        string dbName = "Ora7";
        string dbId = "edu";
        string dbPw = "edu1234";

        public english()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void english_Load(object sender, EventArgs e)
{

            pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1522)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
            pgOraConn.Open();
            pgOraCmd = pgOraConn.CreateCommand();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("bas_empno", "사번");
                dataGridView1.Columns.Add("bas_name", "성명");
                dataGridView1.Columns.Add("bas_resno", "주민번호");
                dataGridView1.Columns.Add("cd_codnms", "직급");
                dataGridView1.Columns.Add("bas_dut", "직위");
                dataGridView1.Columns.Add("dept_name", "부서");
                dataGridView1.Columns.Add("bas_residence", "주소");
                dataGridView1.Columns.Add("bas_entdate", "입사일자");
                dataGridView1.Columns.Add("bas_resdate", "퇴사일자");
                string sql1 = " select bas_empno, bas_name, bas_resno, cd_codnms, bas_dut,  dept_name, bas_residence,bas_entdate,bas_resdate " +
                                " from THRM_BAS_PJH," +
                                "       (select cd_grpcd, cd_code, cd_codnms " +
                                "       from TIEAS_CD_PJH " +
                                "       where cd_grpcd = 'POS'), " +
                                "       THRM_DEPT_PJH " +
                                " where bas_pos = cd_code " +
                                " and bas_dept = dept_code ";


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
                    dataGridView1.Rows[cnt].Cells["bas_resno"].Value = rd["bas_resno"].ToString();
                    dataGridView1.Rows[cnt].Cells["cd_codnms"].Value = rd["cd_codnms"].ToString();
                    dataGridView1.Rows[cnt].Cells["bas_dut"].Value = rd["bas_dut"].ToString();
                    dataGridView1.Rows[cnt].Cells["dept_name"].Value = rd["dept_name"].ToString();
                    dataGridView1.Rows[cnt].Cells["bas_residence"].Value = rd["bas_residence"].ToString();
                    dataGridView1.Rows[cnt].Cells["bas_entdate"].Value = rd["bas_entdate"].ToString();
                    dataGridView1.Rows[cnt].Cells["bas_resdate"].Value = rd["bas_resdate"].ToString();
                    cnt++;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        private string Papago(string kor)
        {
            // 요청 URL
            string sUrl = "https://openapi.naver.com/v1/papago/n2mt";

            // 파라미터에 값넣기 (파파고 NMT API 가이드에서 -d 부분이 파라미터이다)
            string sParam = string.Format("source={0}&target={1}&text={2}", "ko", "en", kor);

            // 파라미터를 Character Set에 맞게 변경
            byte[] bytearry = Encoding.UTF8.GetBytes(sParam);

            // 서버에 요청
            WebRequest webRequest = WebRequest.Create(sUrl);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";

            // 헤더 추가하기 (파파고 NMT API 가이드에서 -h 부분이 헤더이다)
            webRequest.Headers.Add("X-Naver-Client-Id", "gRjJL6fiOepvBWzPyYho");
            webRequest.Headers.Add("X-Naver-Client-Secret", "94sEmdE0hL");

            // 요청 데이터 길이
            webRequest.ContentLength = bytearry.Length;

            Stream stream = webRequest.GetRequestStream();
            stream.Write(bytearry, 0, bytearry.Length);
            stream.Close();

            // 응답 데이터 가져오기(출력포맷)
            WebResponse webResponse = webRequest.GetResponse();
            stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string sReturn = streamReader.ReadToEnd();

            streamReader.Close();
            stream.Close();
            webResponse.Close();

            JObject jObject = JObject.Parse(sReturn);

            // JSON 출력포맷에서 필요한 부분(번역된 문장)만 가져오기
            return jObject["message"]["result"]["translatedText"].ToString();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            name.Text = Papago(dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString());
            resno.Text = Papago(dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString());
            pos.Text = Papago(dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString());
            textBox1.Text = Papago(dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString());
            dept.Text = Papago(dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString());
            addr.Text = Papago(dataGridView1.Rows[e.RowIndex].Cells[6].FormattedValue.ToString());

            date.Text = dataGridView1.Rows[e.RowIndex].Cells[7].FormattedValue.ToString() + " ~ " + dataGridView1.Rows[e.RowIndex].Cells[8].FormattedValue.ToString();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.groupBox2.Width, this.groupBox2.Height);
            groupBox2.DrawToBitmap(bitmap, new Rectangle(0, 0, this.groupBox2.Width, this.groupBox2.Height));
            e.Graphics.DrawImage(bitmap, -30, 80, 900, 900);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "select cd_code,cd_codnms from tieas_cd_pjh where cd_code = '" + textBox1.Text + "' and cd_grpcd='DUT'";

            OracleCommand cmd = new OracleCommand();
            cmd = new OracleCommand();
            cmd.Connection = pgOraConn;
            cmd.CommandText = sql1;

            OracleDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) // select 했을때 data 가 있는 경우 data 값을 텍스트박스안에 넣는다.
            {

                dut.Text = Papago(rd["cd_codnms"].ToString());
            }
        }

    }
}
