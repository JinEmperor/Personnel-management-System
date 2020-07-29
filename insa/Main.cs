using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace insa
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Text=="인사기본사항")
            {
                
                Form1 ss = new Form1();
                ss.ShowDialog();

            }
           
          if(e.Node.Text == "인사기록 조회(통합)")
            {
                Form8 aa = new Form8();
                aa.ShowDialog();
            }
          if(e.Node.Text == "부서별 인원현황")
            {

               onlychart bb = new onlychart();
                bb.ShowDialog();
            }
           if(e.Node.Text == "인사코드 관리")
            {
                code cc = new code();
                cc.ShowDialog();
            }
           if(e.Node.Text == "재직증명서")
            {
                jaejic dd = new jaejic();
                dd.ShowDialog();
            }
           if(e.Node.Text == "경력증명서")
            {
                certificate ee = new certificate();
                ee.ShowDialog();
            }
           if(e.Node.Text == "영문증명서")
            {
                english ff = new english();
                ff.ShowDialog();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
