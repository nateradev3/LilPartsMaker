using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilPartsMaker
{
    class ExcelReader
    {

        public static Net.SourceForge.Koogra.Excel.Worksheet loadFirstExcelSheetInBook(string file) {
            Net.SourceForge.Koogra.Excel.Workbook wb = new Net.SourceForge.Koogra.Excel.Workbook(file);

            // load by index                                                                                

            return wb.Sheets[0];
        }
        public static void test()
        {
            // Load .xls sample

            Net.SourceForge.Koogra.Excel.Workbook wb = new Net.SourceForge.Koogra.Excel.Workbook("path to some file.xls");

            // load by index

            Net.SourceForge.Koogra.Excel.Worksheet worksheet = wb.Sheets[0];

            // load by name

            worksheet = wb.Sheets.GetByName("Sheet1");

            // cell stuff
            for (uint r = worksheet.Rows.MinRow; r <= worksheet.Rows.MaxRow; ++r)
            {
                Net.SourceForge.Koogra.Excel.Row row = worksheet.Rows[r];

                for (uint c = row.Cells.MinCol; c <= row.Cells.MaxCol; ++c)
                {
                    // raw value
                    Console.WriteLine(row.Cells[c].Value);

                    // formatted value
                    Console.WriteLine(row.Cells[c].FormattedValue());
                }
            }

            // enumerate sheets

            foreach (var ws2 in wb.Sheets)
                Console.WriteLine(ws2.Name);

            // load .xlsx sample

            Net.SourceForge.Koogra.Excel2007.Workbook wb2007 = new Net.SourceForge.Koogra.Excel2007.Workbook("path to some file.xlsx");

            // load by index

            Net.SourceForge.Koogra.Excel2007.Worksheet ws2007 = wb2007.GetWorksheet(0);

            // load by name

            ws2007 = wb2007.GetWorksheetByName("Sheet1");

            // cell stuff
            for (uint r = ws2007.CellMap.FirstRow; r <= ws2007.CellMap.LastRow; ++r)
            {
                Net.SourceForge.Koogra.Excel2007.Row row = ws2007.GetRow(r);

                for (uint c = ws2007.CellMap.FirstCol; c <= ws2007.CellMap.LastCol; ++c)
                {
                    // raw value
                    Console.WriteLine(row.GetCell(c).Value);

                    // formatted value
                    Console.WriteLine(row.GetCell(c).GetFormattedValue());
                }
            }

            // generic

            Net.SourceForge.Koogra.IWorkbook genericWB = Net.SourceForge.Koogra.WorkbookFactory.GetExcel2007Reader("some.xlsx");
            genericWB = Net.SourceForge.Koogra.WorkbookFactory.GetExcelBIFFReader("some.xls");

            Net.SourceForge.Koogra.IWorksheet genericWS = genericWB.Worksheets.GetWorksheetByIndex(0);

            for (uint r = genericWS.FirstRow; r <= genericWS.LastRow; ++r)
            {
                Net.SourceForge.Koogra.IRow row = genericWS.Rows.GetRow(r);

                for (uint c = genericWS.FirstCol; c <= genericWS.LastCol; ++c)
                {
                    // raw value
                    Console.WriteLine(row.GetCell(c).Value);

                    // formatted value
                    Console.WriteLine(row.GetCell(c).GetFormattedValue());
                }
            }
        }
        
    }
}
