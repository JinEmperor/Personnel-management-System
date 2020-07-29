using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace insa
{
    class function
    {

        public string insertdate(DateTimePicker datebox, string date)
        {
            string bas_dut2 = "";
            if (datebox.CustomFormat == "날짜 선택")
            {
                bas_dut2 = null;
            }
            else
            {
                bas_dut2 = date;
                bas_dut2 = date.Substring(0, 4) + date.Substring(5, 2) + date.Substring(8);
               
            }
            

            return bas_dut2;

        }

        public void insertdatetime(DateTimePicker picker, string datetextbox)
        {
           
            string year = "";
            string mon = "";
            string day = "";
            if(datetextbox == "" || datetextbox == null)
            {
                picker.CustomFormat = " 날짜 선택";
            }
            else
            {
                if (datetextbox.Length>6)
                {
                    year = datetextbox.Substring(0, 4) ;
                    mon = datetextbox.Substring(4, 2);
                    day = datetextbox.Substring(6);
                    DateTime dt = new DateTime(int.Parse(year), int.Parse(mon), int.Parse(day));
                    //MessageBox.Show(dt.ToString());
                    picker.Value = dt;
                }

            }

            

        }
        public void gridview()
        {

        }
    }
}
