using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace NPOIOperateExcel
{
    public static class ExcelUtility
    {
        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="m_FilePath">excel路径</param>  
        /// <param name="m_FirstRowIsColumnName">第一行是否是列名</param>  
        /// <param name="m_SheetNumber">读取第几个Sheet，从0开始</param>  
        /// <returns>返回datatable</returns>  
        public static DataTable ExcelToDataTable(string m_FilePath, bool m_FirstRowIsColumnName, int m_SheetNumber = 0)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.Open(m_FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                //using (fs = File.OpenRead(m_FilePath))
                {
                    // 2007版本  
                    if (m_FilePath.Trim().ToUpper().IndexOf(".XLSX") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (m_FilePath.Trim().ToUpper().IndexOf(".XLS") > 0)
                        workbook = new HSSFWorkbook(fs);
                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(m_SheetNumber);//读取第一个sheet，当然也可以循环读取每个sheet  
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (m_FirstRowIsColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)  
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                                case CellType.Boolean:
                                                    dataRow[j] = cell.BooleanCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 数据表转Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="m_ExportFile">导出的Excel文件</param>\
        /// <param name="m_SheetName">导出的Excel的Sheet名</param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataTable dt, string m_ExportFile, string m_SheetName = "Sheet0")
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet(m_SheetName);//创建一个名称为Sheet0的表  
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].ColumnName);
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    using (fs = File.OpenWrite(m_ExportFile))
                    {
                        workbook.Write(fs);
                        result = true;
                    }
                }
                return result;
            }
            catch
            {
                //if (fs != null)
                //{
                //    fs.Close();
                //}
                return false;
            }
        }
        /// <summary>
        /// 数据表转Excel
        /// </summary>
        /// <param name="dtData">导出数据</param>
        /// <param name="dtFieldMapping">导出TiTle映射</param>
        /// <param name="m_ExportFile">导出的Excel文件</param>\
        /// <param name="m_SheetName">导出的Excel的Sheet名</param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataTable dtData, DataTable dtFieldMapping, string m_ExportFile, string m_SheetName = "Sheet0")
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dtData != null && dtData.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet(m_SheetName);//创建一个名称为Sheet0的表  
                    int rowCount = dtData.Rows.Count;//行数  
                    int columnCount = dtFieldMapping.Rows.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dtFieldMapping.Rows[c]["Title"].ToString());
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据 
                            for (int k = 0; k < dtData.Columns.Count; k++)
                            {
                                if (dtData.Columns[k].ColumnName.ToUpper() == dtFieldMapping.Rows[j]["FieldName"].ToString().ToUpper())
                                {
                                    cell.SetCellValue(dtData.Rows[i][k].ToString());
                                    break;
                                }
                            }
                        }
                    }
                    using (fs = File.OpenWrite(m_ExportFile))
                    {
                        workbook.Write(fs);
                        result = true;
                    }
                }
                return result;
            }
            catch
            {
                //if (fs != null)
                //{
                //    fs.Close();
                //}
                return false;
            }
        }
    }
}
