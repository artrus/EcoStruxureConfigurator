using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoStruxureConfigurator
{
    public class ReadIO
    {
        private readonly Settings settings;

        public ReadIO(Settings settings)
        {
            this.settings = settings;
        }

        public List<TagIO> OpenIO(string path)
        {
            FileInfo existingFile = new FileInfo(path);
            if (!existingFile.Exists)
            {
                return new List<TagIO>();
            }
            ExcelPackage package = new ExcelPackage(existingFile);
            try
            {
                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[settings.NAME_LIST_IO];
                return GetTagsIO(worksheet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "    Ошибка парсинга");
                return null;
            }
        }

        private List<TagIO> GetTagsIO(ExcelWorksheet worksheet)
        {
            List<TagIO> tags = new List<TagIO>();
            int rowCount = worksheet.Dimension.End.Row;     //get row count

            int id = 0;
            string moduleName;
            string moduleType;
            Module module = null; ;
            for (int i = 2; i < rowCount; i++)
            {
                string name = worksheet.Cells[i, settings.ROW_IO_NAME].Value?.ToString();
                string descr = worksheet.Cells[i, settings.ROW_IO_DESCR].Value?.ToString();
                string channel = worksheet.Cells[i, settings.ROW_IO_CHANNEL].Value?.ToString();
                string system = worksheet.Cells[i, settings.ROW_IO_SYSTEM].Value?.ToString();

                try
                {
                    id = Int32.Parse(worksheet.Cells[i, settings.ROW_IO_ID].Value?.ToString());
                }
                catch
                { }

                if (worksheet.Cells[i, settings.ROW_IO_TYPE_MODULE].Value?.ToString() != null)  //if find new module in IO
                {
                    moduleType = worksheet.Cells[i, settings.ROW_IO_TYPE_MODULE].Value?.ToString();
                    moduleName = worksheet.Cells[i, settings.ROW_IO_NAME_MODULE].Value?.ToString();
                    module = new Module(id, moduleName, moduleType, settings.GetModuleInfoByType(moduleType));
                }
                string tagType = worksheet.Cells[i, settings.ROW_IO_TYPE_IO].Value?.ToString();
                tags.Add(new TagIO(name, descr, system, module, Int32.Parse(channel), settings.GetTagIOInfoByType(tagType)));
            }
            return tags;
        }
    }
}
