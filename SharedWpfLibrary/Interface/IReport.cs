using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SharedWpfLibrary.Interface
{
    public interface IReport
    {
        public MemoryStream GenerateReport();
        public void SaveReportImage();
        public void SaveReportPDF();
    }
}
