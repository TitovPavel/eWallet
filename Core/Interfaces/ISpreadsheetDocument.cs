using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Core.Interfaces
{
    public interface ISpreadsheetDocument
    {
        MemoryStream DataTableToMemoryStream(DataTable dataTable);
    }
}
