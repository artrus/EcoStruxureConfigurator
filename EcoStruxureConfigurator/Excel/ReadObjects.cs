using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoStruxureConfigurator.Excel
{
    public class ReadObjects
    {
        private readonly Settings Settings;

        public ReadObjects(Settings settings)
        {
            Settings = settings;
        }

        public List<ObjectBase> OpenObjects(string path)
        {
            if (path == null)
                throw new ArgumentNullException("Не найден файл Objects");

            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                return new List<ObjectBase>();
            }
            ExcelPackage package = new ExcelPackage(existingFile);
            try
            {
                //get the first worksheet in the workbook
                ExcelWorksheets worksheets = package.Workbook.Worksheets;
                var objects = GetObjects(worksheets);
                return objects;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка парсинга Objects.xlsx");
                return null;
            }
        }

        private List<ObjectBase> GetObjects(ExcelWorksheets worksheets)
        {
            List<ObjectBase> objects = new List<ObjectBase>();
            if (worksheets.Count == 0)
                return new List<ObjectBase>();

            foreach (ExcelWorksheet worksheet in worksheets)
            {    
                ObjectBase obj = new ObjectBase();
                obj.Type = worksheet.Name;
                int rowCount = worksheet.Dimension.End.Row;     //get row count

                for (int i = 4; i <= rowCount; i++)
                {
                    string IOinput = worksheet.Cells[i, Settings.ROW_OBJECT_IO_INPUT].Value?.ToString();
                    string IOoutput = worksheet.Cells[i, Settings.ROW_OBJECT_IO_OUTPUT].Value?.ToString();
                    string controlIn = worksheet.Cells[i, Settings.ROW_OBJECT_CONTROL_IN].Value?.ToString();
                    string controlOut = worksheet.Cells[i, Settings.ROW_OBJECT_CONTROL_OUT].Value?.ToString();
                    string SP = worksheet.Cells[i, Settings.ROW_OBJECT_SP].Value?.ToString();
                    string ST = worksheet.Cells[i, Settings.ROW_OBJECT_ST].Value?.ToString();
                    string nameIn = worksheet.Cells[i, Settings.ROW_OBJECT_DESCR_IN].Value?.ToString();
                    string nameOut = worksheet.Cells[i, Settings.ROW_OBJECT_DESCR_OUT].Value?.ToString();
                    string typeIn = worksheet.Cells[i, Settings.ROW_OBJECT_TYPE_IN].Value?.ToString();
                    string typeOut = worksheet.Cells[i, Settings.ROW_OBJECT_TYPE_OUT].Value?.ToString();

                    obj.AddIOinput(nameIn, IOinput, typeIn);
                    obj.AddControlInput( nameIn, controlIn, typeIn);
                    obj.AddSP(nameIn, SP, typeIn);
                    obj.AddControlOutput(nameOut, controlOut, typeOut);
                    obj.AddST(nameOut, ST, typeOut);
                    obj.AddIOoutput(nameOut, IOoutput, typeOut);
                }
                objects.Add(obj);
            }
            return objects;
        }

    }
}
