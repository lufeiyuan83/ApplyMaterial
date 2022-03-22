using System;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace Template
{
    public enum TemplateType
    {
        Export,
        Import
    }
    [SuppressUnmanagedCodeSecurityAttribute]
    internal static class SafeNativeMethods
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
    }
    public class Template
    {
        private static DataTable GetData(string m_SQL)
        {
            using (DataAccess da = new DataAccess())
            {
                da.sqlParameters.Clear();
                return da.GetDataTable(m_SQL, da.sqlParameters);
            }
        }
        public static DataTable GetConfig(string m_FileName, TemplateType m_TemplateType)
        {
            return GetData("Select * From [Template].[dbo].[Excel] Where Name='" + m_FileName + "' And Type='" + m_TemplateType.ToString() + "' And IsDeleted=0");
        }
        public static DataTable GetParameter(long m_ExcelPKID)
        {
            return GetData("Select * From [Template].[dbo].[Parameter] Where ExcelPKID=" + m_ExcelPKID+" And IsDeleted=0 Order By Id");
        }
        /// <summary>
        /// 依据Excel中的Title来导出Excel
        /// </summary>
        /// <param name="m_ExcelFile">Excel模板文件</param>
        /// <param name="m_Data">导出数据</param>
        /// <param name="m_OutputPath">导出路径</param>
        /// <param name="m_FileName">导出文件名</param>
        /// <returns></returns>
        public static string ExportToExcelByTitle(string m_ExcelFile, DataTable m_Data, string m_OutputPath, string m_FileName)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Sheets sheets;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            object oMissiong = System.Reflection.Missing.Value;
            try
            {
                if (app == null)
                    return null;
                app.DisplayAlerts = false;
                string strOperateFile = m_OutputPath + m_FileName + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                if (!Directory.Exists(m_OutputPath))
                    Directory.CreateDirectory(m_OutputPath);
                File.Copy(m_ExcelFile, strOperateFile);

                workbook = app.Workbooks.Open(strOperateFile, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                sheets = workbook.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);
                if (worksheet == null)
                    return null;
                int iRowCount = worksheet.UsedRange.Rows.Count;
                int iColCount = worksheet.UsedRange.Columns.Count;

                DataTable dtConfig = GetData("select b.* from [Template].[dbo].[Excel] a,[Template].[dbo].[Parameter] b where a.id=b.ExcelPKID And a.Type='Export' and a.IsDeleted=0 and b.IsDeleted=0");

                int intStartRowIndex = 0;
                int intStartColumnIndex = 0;
                #region 获得实际开始填充数据的Title行列坐标
                for (int i = 1; i <= iRowCount; i++)
                {
                    for (int j = 1; j <= iColCount; j++)
                    {
                        DataRow[] drsConfig = dtConfig.Select("Title='" + ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i, j]).Text.ToString().Trim() + "'");
                        if (drsConfig.Length > 0)
                        {
                            if (intStartRowIndex == 0)
                            {
                                intStartRowIndex = i;
                                #region 填充需要增加的数据空白行
                                //for (int k = 0; k < m_Data.Rows.Count; k++)
                                //{
                                //    Microsoft.Office.Interop.Excel.Range xlsRows = (Microsoft.Office.Interop.Excel.Range)worksheet.Rows[intStartRowIndex + k + 3, oMissiong];
                                //    xlsRows.Insert(oMissiong, oMissiong);
                                //}
                                #endregion
                            }
                            if (intStartColumnIndex == 0)
                                intStartColumnIndex = j;
                            i = iRowCount + 1;
                            break;
                        }
                    }
                }
                #endregion
                #region 填充数据
                iRowCount = worksheet.UsedRange.Rows.Count;
                iColCount = worksheet.UsedRange.Columns.Count;
                for (int i = 0; i < m_Data.Rows.Count; i++)
                {
                    for (int j = 1; j <= iColCount; j++)
                    {
                        DataRow[] drsConfig = dtConfig.Select("Title='" + ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[intStartRowIndex, j]).Text.ToString().Trim() + "'");
                        if (drsConfig.Length > 0 && m_Data.Columns.Contains(drsConfig[0]["FieldName"].ToString()))
                        {
                            int intOffRowCount = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[intStartRowIndex, j]).MergeArea.Rows.Count;
                            int intOffColumnCount = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[intStartRowIndex, j]).MergeArea.Columns.Count;

                            Microsoft.Office.Interop.Excel.Range range = worksheet.Range[worksheet.Cells[intStartRowIndex + i + intOffRowCount, j], worksheet.Cells[intStartRowIndex + i + intOffRowCount, j + intOffColumnCount - 1]];
                            range.MergeCells = true;
                            range.Value = m_Data.Rows[i][drsConfig[0]["FieldName"].ToString()].ToString();

                            if (j == 1 && i < m_Data.Rows.Count - 2)
                            {
                                Microsoft.Office.Interop.Excel.Range xlsRows = (Microsoft.Office.Interop.Excel.Range)worksheet.Rows[intStartRowIndex + i + intOffRowCount + 1, oMissiong];
                                xlsRows.Insert(oMissiong, oMissiong);
                            }
                        }
                    }
                }
                #endregion
                #region 合并相同值
                int intStartIndex = 0;
                int intEndIndex = 0;
                for (int j = intStartColumnIndex; j <= iColCount; j++)
                {
                    for (int i = 1; i < iRowCount; i++)
                    {
                        DataRow[] drsConfig = dtConfig.Select("Title='" + ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[intStartRowIndex, j]).Text.ToString().Trim() + "'");
                        if (drsConfig.Length > 0 && Convert.ToBoolean(drsConfig[0]["NeedMerge"]))
                        {
                            if (((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i, j]).Text.ToString().Trim() != "")
                            {
                                if (((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i, j]).Text.ToString().Trim() == ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i + 1, j]).Text.ToString().Trim())
                                {
                                    if (intStartIndex == 0)
                                    {
                                        intStartIndex = i;
                                    }
                                    intEndIndex = i + 1;
                                }
                                else
                                {
                                    if (intStartIndex != 0 && intEndIndex != 0)
                                    {
                                        worksheet.Range[worksheet.Cells[intStartIndex, j], worksheet.Cells[intEndIndex, j]].Merge();
                                        intStartIndex = 0;
                                        intEndIndex = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                worksheet.Rows.AutoFit();
                return strOperateFile;
            }
            catch (Exception ex)
            {
                return "[ERROR]" + ex.Message;
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Save();
                    app.Workbooks.Close();
                    app.Quit();
                    IntPtr intPtr = new IntPtr(app.Hwnd);
                    SafeNativeMethods.GetWindowThreadProcessId(intPtr, out int k);
                    System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
                    p.Kill();
                }
            }
        }
        /// <summary>
        /// 依据Excel中的Title的坐标来导出Excel
        /// </summary>
        /// <param name="m_ExcelFile">Excel模板文件</param>
        /// <param name="m_Data">导出数据</param>
        /// <param name="m_OutputPath">导出路径</param>
        /// <param name="m_FileName">导出文件名</param>
        /// <returns></returns>
        public static string ExportToExcelByCoordinate(string m_ExcelFile, DataTable m_Data, string m_OutputPath, string m_FileName)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Sheets sheets;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            object oMissiong = System.Reflection.Missing.Value;
            try
            {
                if (app == null)
                    return null;
                app.DisplayAlerts = false;
                string strOperateFile = m_OutputPath + m_FileName + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                if (!Directory.Exists(m_OutputPath))
                    Directory.CreateDirectory(m_OutputPath);
                File.Copy(m_ExcelFile, strOperateFile);

                workbook = app.Workbooks.Open(strOperateFile, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                sheets = workbook.Worksheets;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);

                if (worksheet == null)
                    return null;
                int iRowCount = worksheet.UsedRange.Rows.Count;//Excel中数据行数
                int iColCount = worksheet.UsedRange.Columns.Count;//Excel中数据列数

                DataTable dtConfig = GetData("select b.* from [Template].[dbo].[Excel] a,[Template].[dbo].[Parameter] b where a.id=b.ExcelPKID And a.Type='Export' and a.IsDeleted=0 and b.IsDeleted=0 And b.SheetName='" + m_FileName + "'");

                #region 填充数据
                for (int i = 0; i < m_Data.Rows.Count; i++)
                {
                    for (int j = 0; j < dtConfig.Rows.Count; j++)
                    {
                        if (m_Data.Columns.Contains(dtConfig.Rows[j]["FieldName"].ToString()))
                        {
                            int intRowIndex = ((Microsoft.Office.Interop.Excel.Range)worksheet.Range[dtConfig.Rows[j]["Coordinate"].ToString()]).Row;//该Title在excel中的行坐标
                            int intColumnIndex = ((Microsoft.Office.Interop.Excel.Range)worksheet.Range[dtConfig.Rows[j]["Coordinate"].ToString()]).Column;//该Title在excel中的列坐标

                            int intOffRowCount = ((Microsoft.Office.Interop.Excel.Range)worksheet.Range[dtConfig.Rows[j]["Coordinate"].ToString()]).MergeArea.Rows.Count;
                            int intOffColumnCount = ((Microsoft.Office.Interop.Excel.Range)worksheet.Range[dtConfig.Rows[j]["Coordinate"].ToString()]).MergeArea.Columns.Count;


                            Microsoft.Office.Interop.Excel.Range range = worksheet.Range[worksheet.Cells[intRowIndex + i + intOffRowCount, intColumnIndex], worksheet.Cells[intRowIndex + i + intOffRowCount, intColumnIndex + intOffColumnCount - 1]];
                            range.MergeCells = true;
                            range.Value = m_Data.Rows[i][dtConfig.Rows[j]["FieldName"].ToString()].ToString();

                            if (j == 0 && i < m_Data.Rows.Count - 2)
                            {
                                Microsoft.Office.Interop.Excel.Range xlsRows = (Microsoft.Office.Interop.Excel.Range)worksheet.Rows[intRowIndex + i + intOffRowCount + 1, oMissiong];
                                xlsRows.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, oMissiong);
                            }
                        }
                    }
                }
                #endregion
                #region 合并相同值
                int intStartIndex = 0;//相同值的开始行索引
                int intEndIndex = 0;//相同值的结束行索引
                int intStartRowIndex = ((Microsoft.Office.Interop.Excel.Range)worksheet.Range[dtConfig.Rows[0]["Coordinate"].ToString()]).Row - 1;//实际操作Title的开始行索引
                int intStartColumnIndex = ((Microsoft.Office.Interop.Excel.Range)worksheet.Range[dtConfig.Rows[0]["Coordinate"].ToString()]).Column;//实际操作Title的开始列索引
                iRowCount = worksheet.UsedRange.Rows.Count + intStartRowIndex;//Excel中数据行数

                DataTable dtIndex = new DataTable();
                dtIndex.Columns.Add("SN");
                dtIndex.Columns.Add("StartIndex");
                dtIndex.Columns.Add("EndIndex");
                bool blFinished= false;
                for (int j = intStartColumnIndex; j <= iColCount; j++)
                {
                    if(blFinished)
                    {
                        DataRow[] drsConfig = dtConfig.Select("Coordinate='" + Convert.ToChar(((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[intStartRowIndex, j]).Column + 64) + (intStartRowIndex + 1).ToString() + "'");
                        if (drsConfig.Length > 0 && Convert.ToBoolean(drsConfig[0]["NeedMerge"]))
                        {
                            for (int k = 0; k < dtIndex.Rows.Count; k++)
                            {
                                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)worksheet.Range[worksheet.Cells[Convert.ToInt32(dtIndex.Rows[k]["StartIndex"]), j], worksheet.Cells[Convert.ToInt32(dtIndex.Rows[k]["EndIndex"]), j]];
                                range.Merge();
                            }
                        }
                    }
                    else                        
                    {
                        for (int i = intStartRowIndex; i <= iRowCount; i++)
                        {
                            DataRow[] drsConfig = dtConfig.Select("Coordinate='" + Convert.ToChar(((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[intStartRowIndex, j]).Column + 64) + (intStartRowIndex + 1).ToString() + "'");
                            if (drsConfig.Length > 0 && Convert.ToBoolean(drsConfig[0]["NeedMerge"]))
                            {
                                if (((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i, j]).Text.ToString().Trim() != "")
                                {
                                    if (((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i, j]).Text.ToString().Trim() == ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[i + 1, j]).Text.ToString().Trim())
                                    {
                                        if (intStartIndex == 0)
                                        {
                                            intStartIndex = i;
                                        }
                                        intEndIndex = i + 1;
                                    }
                                    else
                                    {
                                        if (intStartIndex != 0 || intEndIndex != 0)
                                        {
                                            Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range) worksheet.Range[worksheet.Cells[intStartIndex, j], worksheet.Cells[intEndIndex, j]];
                                            range.Merge();                                            
                                            DataRow dr = dtIndex.NewRow();
                                            dr["SN"] = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[intStartIndex, j]).Text.ToString();
                                            dr["StartIndex"]=intStartIndex;
                                            dr["EndIndex"]=intEndIndex;
                                            dtIndex.Rows.Add(dr);
                                            intStartIndex = 0;
                                            intEndIndex = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    blFinished=true;
                }
                #endregion

                worksheet.Rows.AutoFit();
                return strOperateFile;
            }
            catch (Exception ex)
            {
                return "[ERROR]" + ex.Message;
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Save();
                    app.Workbooks.Close();
                    app.Quit();
                    IntPtr intPtr = new IntPtr(app.Hwnd);
                    SafeNativeMethods.GetWindowThreadProcessId(intPtr, out int k);
                    System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);
                    p.Kill();
                }
            }
        }
    }
}