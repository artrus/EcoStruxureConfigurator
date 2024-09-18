using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IO_List_Scripts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ExcelHelper excelHelper = new ExcelHelper();


            List<ReplaceText> replaceTexts = excelHelper.ReadReplaceTexts(@"d:\Projects\СКА\ЩАВК\Test\ReplaceTexts.xlsx");

            excelHelper.Replace(@"d:\Projects\СКА\ЩАВК\Test\IO LIST СКА АВК.xlsx", replaceTexts);



            List<string> doFirstList = GetList(replaceTexts);

            excelHelper.doFirstText(@"d:\Projects\СКА\ЩАВК\Test\IO LIST СКА АВК---Replace.xlsx", doFirstList);


            excelHelper.DeleteSpacesEnd(@"d:\Projects\СКА\ЩАВК\Test\IO LIST СКА АВК---Replace---DoFirst.xlsx");

        }

        private static List<string> GetList(List<ReplaceText> replaceTexts)
        {
            List<string> doFirstList = new List<string>();
            foreach (var s in replaceTexts)
            {
                doFirstList.Add(s.To);
            }

            return doFirstList;
        }
    }
}
