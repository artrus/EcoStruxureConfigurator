using IO_List_Scripts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinderModbusRegs
{
    public partial class FormFinderModbusRegs : Form
    {
        public FormFinderModbusRegs()
        {
            InitializeComponent();
        }

        private void Btn_Find_Click(object sender, EventArgs e)
        {
            var path = @"d:\_Test FindTags\";

            var matches = new List<string>
            {
                //TODO Test
                "Avar_Global",
                "SYS.ST.Avar_Common"
            };

            var finder = new FinderInExcel();

            var findTags = finder.Find(path, matches);

            foreach (var tag in findTags)
            {
                rtb_log.AppendText(tag.ToString() + "\n");
            }

            var streamWriter = new StreamWriter(path + "OUT FILE.csv", false, Encoding.GetEncoding("windows-1251"));
            foreach (var tag in findTags)
            {
                streamWriter.WriteLine(tag.ToCSVString());
            }
            streamWriter.Close();
        }
    }
}
