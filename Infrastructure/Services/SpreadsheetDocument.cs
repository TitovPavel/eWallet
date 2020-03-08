using Core.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;



namespace Infrastructure.Services
{
    public class SpreadsheetDocumentExcel : ISpreadsheetDocument
    {
        public MemoryStream DataTableToMemoryStream(DataTable dataTable)
        {
            
            MemoryStream ms = new MemoryStream();

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);
                Row headerRow = new Row();
                List<String> columns = new List<string>();
                foreach (System.Data.DataColumn column in dataTable.Columns)
                {
                    columns.Add(column.ColumnName);
                    AddCell(headerRow, column.ColumnName);
                }

                sheetData.AppendChild(headerRow);
                foreach (DataRow dsrow in dataTable.Rows)
                {
                    Row newRow = new Row();
                    foreach (String col in columns)
                    {
                        AddCell(newRow, dsrow[col].ToString());
                    }

                    sheetData.AppendChild(newRow);
                }
                workbookPart.Workbook.Save();
            }

            return ms;
        }

        private void AddCell(Row row, string value)
        {
            Cell cell = new Cell();
            cell.DataType = CellValues.String;
            cell.CellValue = new CellValue(value);
            row.AppendChild(cell);
        }
    }
}
