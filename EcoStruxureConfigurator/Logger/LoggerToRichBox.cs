using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoStruxureConfigurator.Logger
{
    public class LoggerToRichBox : ILogger
    {
        private readonly RichTextBox box;

        public LoggerToRichBox(RichTextBox box)
        {
            this.box = box;
        }

        public void Clear()
        {
            box.Clear();
        }

        public void NewLine()
        {
            box.AppendText("\n");
        }

        public void WriteLine(string message)
        {
            box.AppendText(message + "\n");
        }

        public void WriteText(string message)
        {
            box.AppendText(message + " ");
        }
    }
}
