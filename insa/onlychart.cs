using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using LiveCharts;
using LiveCharts.Wpf;

namespace insa
{
    public partial class onlychart : Form
    {
        OracleConnection pgOraConn;
        OracleCommand pgOraCmd;
        SqlDataAdapter adapter = null;
        SqlConnection conn = null;

        insa.chart chart = new insa.chart();

        string dbIp = "222.237.134.74";
        string dbName = "Ora7";
        string dbId = "edu";
        string dbPw = "edu1234";

        public onlychart()
        {
            InitializeComponent();

            if (select_number == 0)
            {
                int reuslt = chart.chartResult("001");
                int reusltdept = chart.chartResultdept("002");

                Func<ChartPoint, string> labelPoint = chartPoint =>
                  string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                pieChart1.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "총무부",
                    Values = new ChartValues<double> {reuslt},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "인사부",
                    Values = new ChartValues<double> {reusltdept},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }
            };

                pieChart1.LegendLocation = LegendLocation.Bottom;
            }

        }

        public static int select_number { get; set; }


        private void Form12_Load(object sender, EventArgs e)

        {  //db 연동
            pgOraConn = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dbIp})(PORT=1522)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dbName})));User ID={dbId};Password={dbPw};Connection Timeout=30;");
            pgOraConn.Open();
            pgOraCmd = pgOraConn.CreateCommand();
            //MessageBox.Show("db연결");

            try
            {
            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB connection fail.\n {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
           
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


         
            select_number = tabControl1.SelectedIndex;

         
            if (select_number == 1)
            {

             

                int reuslt1 = chart.chartResult1("1"); //직급코드에서 뽑아오는 것 
                int reuslt2 = chart.chartResult1("2");
                int reuslt3 = chart.chartResult1("3");
                int reuslt4 = chart.chartResult1("4");
                int reuslt5 = chart.chartResult1("5");
                int reuslt6 = chart.chartResult1("6");
                int reuslt7 = chart.chartResult1("7");
                int reuslt8 = chart.chartResult1("8");
                int reuslt9 = chart.chartResult1("9");
                int reuslt10 = chart.chartResult1("10");
                int reuslt11 = chart.chartResult1("11");


                Func<ChartPoint, string> labelPoint = chartPoint =>
                  string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                pieChart2.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "사원",
                    Values = new ChartValues<double> {reuslt1},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "대리",
                    Values = new ChartValues<double> {reuslt2},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                 new PieSeries
                {
                    Title = "과장",
                    Values = new ChartValues<double> {reuslt3},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                  new PieSeries
                {
                    Title = "차장",
                    Values = new ChartValues<double> {reuslt4},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                   new PieSeries
                {
                    Title = "부장",
                    Values = new ChartValues<double> {reuslt5},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                    new PieSeries
                {
                    Title = "상무",
                    Values = new ChartValues<double> {reuslt6},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                     new PieSeries
                {
                    Title = "전무",
                    Values = new ChartValues<double> {reuslt7},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                      new PieSeries
                {
                    Title = "부사장",
                    Values = new ChartValues<double> {reuslt8},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                       new PieSeries
                {
                    Title = "사장",
                    Values = new ChartValues<double> {reuslt9},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                        new PieSeries
                {
                    Title = "부회장",
                    Values = new ChartValues<double> {reuslt10},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                        new PieSeries
                {
                    Title = "회장", 
                    Values = new ChartValues<double> {reuslt11},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },

            };


                pieChart2.LegendLocation = LegendLocation.Bottom;
            }

            
            if(select_number == 2)
            {
               

                int reusltentdate = chart.chartResultentdate20(" ");
                int resultentdate1 = chart.chartResultentdate19(" ");

                Func<ChartPoint, string> labelPoint = chartPoint =>
                 string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                pieChart3.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "2020년",
                    Values = new ChartValues<double> {reusltentdate},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                 new PieSeries
                {
                    Title = "2019년",
                    Values = new ChartValues<double> {resultentdate1},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }

            };

                pieChart3.LegendLocation = LegendLocation.Bottom;
            }
            if (select_number == 3)
            {
                int reusltentdate = chart.chartResultresdate(" ");
                int resultentdate1 = chart.chartResultentdate19(" ");
                

                Func<ChartPoint, string> labelPoint = chartPoint =>
                 string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

                pieChart4.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "2020년",
                    Values = new ChartValues<double> {reusltentdate},
                    DataLabels = true,
                    LabelPoint = labelPoint
                },
                new PieSeries
                {
                    Title = "2019년",
                    Values = new ChartValues<double> {resultentdate1},
                    DataLabels = true,
                    LabelPoint = labelPoint
                }

            };

                pieChart4.LegendLocation = LegendLocation.Bottom;
            }

        }

    }
    
}

