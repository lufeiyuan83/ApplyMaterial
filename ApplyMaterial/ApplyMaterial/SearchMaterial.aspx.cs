using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ERP.ApplyMaterial
{
    public partial class SearchMaterial : BasePage
    {
        private static string strFlowName = "ApplyMaterial";
        //private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name+".aspx";
        private static DataTable dtData;
        //private void LoadData()
        //{
        //    try
        //    {
        //        BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
        //        dtData = m_ApplyMaterial.Select();
        //        dgvData.DataSource = dtData;
        //        dgvData.DataBind();

        //        dgvWorkFlow.DataSource = null;
        //        dgvWorkFlow.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
        //        Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
        //    }
        //}
        private void LoadData(string m_MaterialId)
        {
            try
            {
                BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                dtData = m_ApplyMaterial.Select(m_MaterialId);
                dgvData.DataSource = dtData;
                dgvData.DataBind();

                dgvWorkFlow.DataSource = null;
                dgvWorkFlow.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void LoadWorkFLow(string m_FlowName, string m_FormId)
        {
            try
            {
                BLL.PLM.FlowInstance m_FlowInstance = new BLL.PLM.FlowInstance();
                dgvWorkFlow.DataSource = m_FlowInstance.Select(m_FlowName, m_FormId);
                dgvWorkFlow.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void SetDefaultMappingValue(DataTable dtMapping, FineUI.DropDownList ddl)
        {
            if (dtMapping != null)
            {
                DataRow[] drs = dtMapping.Select("IsDefault=1");
                if (drs.Length > 0)
                {
                    List<string> lstMapping = new List<string>();
                    for (int i = 0; i < drs.Length; i++)
                        lstMapping.Add(drs[i]["MappingValue1"].ToString());
                    ddl.SelectedValueArray = lstMapping.ToArray();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.PLM.Mapping m_Mapping = new BLL.PLM.Mapping();
                DataTable dtMapping = m_Mapping.Select("Database", "TipTop");
                cmbBuCode.DataSource = dtMapping;
                cmbBuCode.DataBind();
                SetDefaultMappingValue(dtMapping, cmbBuCode);

                if (Request.QueryString["MaterialId"] != null)
                    LoadData(Request.QueryString["MaterialId"].ToString());
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                dtData = m_ApplyMaterial.Select(cmbBuCode.SelectedValue, txtMaterialId.Text.Trim(), txtProductionName.Text.Trim(),txtSpecification.Text.Trim(),txtCreateUserId.Text.Trim());
                dgvData.DataSource = dtData;
                dgvData.DataBind();

                dgvWorkFlow.DataSource = null;
                dgvWorkFlow.DataBind();
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                dtData = m_ApplyMaterial.Select(cmbBuCode.SelectedValue, txtMaterialId.Text.Trim(), txtCreateUserId.Text.Trim());
                DataTable dtExcelConfig = Template.Template.GetConfig("Material", Template.TemplateType.Export);
                if (dtExcelConfig != null && dtExcelConfig.Rows.Count > 0)
                {
                    string strOutputPath = @"C:\Output\";
                    if (!Directory.Exists(strOutputPath))
                        Directory.CreateDirectory(strOutputPath);
                    string strOutFile = strOutputPath + dtExcelConfig.Rows[0]["Name"].ToString() + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";

                    DataTable dtExcelParameter = Template.Template.GetParameter(Convert.ToInt64(dtExcelConfig.Rows[0]["Id"]));

                    if (NPOIOperateExcel.ExcelUtility.DataTableToExcel(dtData, dtExcelParameter, strOutFile, dtExcelConfig.Rows[0]["Name"].ToString()))
                    {
                        FileInfo fileInfo = new FileInfo(strOutFile);

                        Response.ClearContent();
                        Response.AddHeader("content-disposition", "attachment; filename=" + fileInfo.Name.ToString());
                        Response.ContentType = "application/excel";
                        Response.ContentEncoding = System.Text.Encoding.UTF8;
                        Response.WriteFile(strOutFile);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void DgvData_RowDoubleClick(object sender, FineUI.GridRowClickEventArgs e)
        {
            txtMaterialClass.Text = "";
            txtMainUnit.Text = "";
            txtSourceCode.Text = "";
            txtGroupCode.Text = "";
            txtFourthGroupCode.Text = "";
            txtABCCode.Text = "";
            cbIsBonded.Checked = false;
            txtRemark.Text = "";

            try
            {
                if (dgvData.SelectedRow != null)
                {
                    BLL.PLM.TipTopParameter m_TipTopParameter = new BLL.PLM.TipTopParameter();
                    DataTable dtTipTopParameter = m_TipTopParameter.Select(dgvData.SelectedRow.Values[1].ToString());
                    if (dtTipTopParameter != null && dtTipTopParameter.Rows.Count > 0)
                    {
                        BLL.PLM.Mapping m_Mapping = new BLL.PLM.Mapping();
                        DataTable dtMapping = m_Mapping.Select("Material");
                        if (dtMapping != null && dtMapping.Rows.Count > 0)
                        {
                            DataRow[] drs = dtMapping.Select("MapCode='MaterialClass' And MappingValue1='" + dtTipTopParameter.Rows[0]["MaterialClass"].ToString() + "'");
                            if (drs.Length > 0)
                                txtMaterialClass.Text = drs[0]["MappingValue2"].ToString();
                            drs = dtMapping.Select("MapCode='MainUnit' And MappingValue1='" + dtTipTopParameter.Rows[0]["MainUnit"].ToString() + "'");
                            if (drs.Length > 0)
                                txtMainUnit.Text = drs[0]["MappingValue2"].ToString();
                            drs = dtMapping.Select("MapCode='SourceCode' And MappingValue1='" + dtTipTopParameter.Rows[0]["SourceCode"].ToString() + "'");
                            if (drs.Length > 0)
                                txtSourceCode.Text = drs[0]["MappingValue2"].ToString();
                            drs = dtMapping.Select("MapCode='FourthGroupCode' And MappingValue1='" + dtTipTopParameter.Rows[0]["FourthGroupCode"].ToString() + "'");
                            if (drs.Length > 0)
                                txtFourthGroupCode.Text = drs[0]["MappingValue2"].ToString();
                        }
                        txtGroupCode.Text = dtTipTopParameter.Rows[0]["GroupCode"].ToString();
                        txtABCCode.Text = dtTipTopParameter.Rows[0]["ABCCode"].ToString();
                        cbIsBonded.Checked = dtTipTopParameter.Rows[0]["IsBonded"].ToString() == "Y";
                        txtRemark.Text = dtTipTopParameter.Rows[0]["Remark"].ToString();

                        pEdit.Hidden = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void DgvData_RowSelect(object sender, FineUI.GridRowSelectEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LoadWorkFLow(strFlowName, dgvData.SelectedRow.Values[1].ToString());
            }
            else
            {
                dgvWorkFlow.DataSource = null;
                dgvWorkFlow.DataBind();
            }
        }
        protected void DgvData_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            string sortField = dgvData.SortField;
            string sortDirection = dgvData.SortDirection;

            DataView dv = dtData.DefaultView;
            if (!string.IsNullOrEmpty(sortField))
            {
                dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }

            dgvData.DataSource = dv;
            dgvData.DataBind();
        }
        protected void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvData.SelectedRowIndexArray.Length; i++)
                {
                    #region 抛送料号到TipTop
                    BLL.PLM.TipTopParameter m_TipTopParameter = new BLL.PLM.TipTopParameter();
                    bool isExistPostPostSuccessData = false;
                    #region 记录抛送TipTop的报文
                    TipTopMaterial.ArrayOfString lstContent = new TipTopMaterial.ArrayOfString();
                    string strContent = "";
                    DataTable dtTipTopParameter = m_TipTopParameter.Select(dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[1].ToString(), dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[6].ToString());
                    if (dtTipTopParameter != null && dtTipTopParameter.Rows.Count > 0)
                    {
                        lstContent.Add("<ima_file><Item><ima01>" + dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[1].ToString() + "</ima01><ima02>" + dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[4].ToString() + "</ima02><ima021>" + dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[3].ToString() + "</ima021><ima06>" + dtTipTopParameter.Rows[0]["GroupCode"].ToString() + "</ima06><ima07>" + dtTipTopParameter.Rows[0]["ABCCode"].ToString() + "</ima07><ima08>" + dtTipTopParameter.Rows[0]["SourceCode"].ToString() + "</ima08><ima12>" + dtTipTopParameter.Rows[0]["FourthGroupCode"].ToString() + "</ima12><ima15>" + dtTipTopParameter.Rows[0]["IsBonded"].ToString() + "</ima15><ima25>" + dtTipTopParameter.Rows[0]["MainUnit"].ToString() + "</ima25><ima37>" + dtTipTopParameter.Rows[0]["ReplenishmentStrategyCode"].ToString() + "</ima37><ima70>" + dtTipTopParameter.Rows[0]["IsConsumed"].ToString() + "</ima70><ima105>" + dtTipTopParameter.Rows[0]["IsSoftwareObject"].ToString() + "</ima105><ima107>" + dtTipTopParameter.Rows[0]["MultiLocation"].ToString() + "</ima107><ima110>" + dtTipTopParameter.Rows[0]["MaterialClass"].ToString() + "</ima110></Item></ima_file>");

                        strContent = "<ima_file>";
                        strContent += " <Item>";
                        strContent += "     <ima01>" + dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[1].ToString() + "</ima01>";//物料编码
                        strContent += "     <ima02>" + dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[4].ToString() + "</ima02>";//品名
                        strContent += "     <ima021>" + dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[3].ToString() + "</ima021>";//规格型号
                        strContent += "     <ima06>" + dtTipTopParameter.Rows[0]["GroupCode"].ToString() + "</ima06>";//分群码
                        strContent += "     <ima07>" + dtTipTopParameter.Rows[0]["ABCCode"].ToString() + "</ima07>";//ABC码
                        strContent += "     <ima08>" + dtTipTopParameter.Rows[0]["SourceCode"].ToString() + "</ima08>";//来源码
                        strContent += "     <ima12>" + dtTipTopParameter.Rows[0]["FourthGroupCode"].ToString() + "</ima12>";//其他分群码四
                        strContent += "     <ima15>" + dtTipTopParameter.Rows[0]["IsBonded"].ToString() + "</ima15>";//保税否
                        strContent += "     <ima25>" + dtTipTopParameter.Rows[0]["MainUnit"].ToString() + "</ima25>";//库存单位
                        strContent += "     <ima37>" + dtTipTopParameter.Rows[0]["ReplenishmentStrategyCode"].ToString() + "</ima37>";//补货策略码
                        strContent += "     <ima70>" + dtTipTopParameter.Rows[0]["IsConsumed"].ToString() + "</ima70>";//是否消耗性物料
                        strContent += "     <ima105>" + dtTipTopParameter.Rows[0]["IsSoftwareObject"].ToString() + "</ima105>";//是否软件物料
                        strContent += "     <ima107>" + dtTipTopParameter.Rows[0]["MultiLocation"].ToString() + "</ima107>";//是否多插件位置管理
                        strContent += "     <ima110>" + dtTipTopParameter.Rows[0]["MaterialClass"].ToString() + "</ima110>";//物料类型
                        strContent += " </Item>";
                        strContent += "</ima_file>";
                    }
                    #endregion
                    TipTopMaterial.CostDataWSSoapClient tipTopMaterial = new TipTopMaterial.CostDataWSSoapClient();
                    string strResult = tipTopMaterial.CostDataIn("PN", dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[6].ToString(), "Insert", lstContent);
                    DataTable dt = new DataTable();
                    System.IO.StringReader sr = new System.IO.StringReader(strResult);
                    System.Xml.XmlTextReader xtr = new System.Xml.XmlTextReader(sr);
                    DataSet ds = new DataSet();
                    ds.ReadXml(xtr);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        #region 记录抛送TipTop的日志
                        BLL.PLM.InterfaceLog m_InterfaceLog = new BLL.PLM.InterfaceLog();
                        Model.PLM.InterfaceLog interfaceLog = new Model.PLM.InterfaceLog
                        { 
                            LogType = "TipTop",
                            BuCode = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[6].ToString(),
                            Message = ds.Tables[0].Rows[0]["msg"].ToString(),
                            Status = (ds.Tables[0].Rows[0]["flag"].ToString() == "Y" || ds.Tables[0].Rows[0]["msg"].ToString().Contains("料號已經存在")) ? "1" : "0",
                            MaterialId = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[1].ToString(),
                            PostParameter = strContent,
                            CreateUserID = Util.Util.UserName,
                            LastUpdateUserID = Util.Util.UserName,
                            Remark = ""
                        };
                        isExistPostPostSuccessData =(interfaceLog.Status == "1");

                        if (m_InterfaceLog.Add(interfaceLog))
                        {
                            Model.PLM.TipTopParameter tipTopParameter = new Model.PLM.TipTopParameter
                            {
                                MaterialId = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[1].ToString(),
                                BuCode = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[6].ToString(),
                                IsPostSuccess = interfaceLog.Status == "1",
                                LastUpdateUserID = Util.Util.UserName
                            };
                            m_TipTopParameter.Update(tipTopParameter);
                        }
                        #endregion
                    }
                    #region 将抛送成功的料号转入正式料号表中
                    if (isExistPostPostSuccessData)
                    {
                        BLL.PLM.Material m_Material = new BLL.PLM.Material();

                        if (!m_Material.IsExist(dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[1].ToString()))
                        {
                            Model.PLM.Material material = new Model.PLM.Material
                            {
                                MaterialId = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[1].ToString(),
                                Rev = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[2].ToString(),
                                ProductionName = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[4].ToString(),
                                Specification = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[3].ToString(),
                                CreateUserID = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[8].ToString(),
                                LastUpdateUserID = Util.Util.UserName,
                                Remark = dgvData.Rows[dgvData.SelectedRowIndexArray[i]].Values[10].ToString()
                            };
                            m_Material.Add(material);
                        }
                    }
                    #endregion
                    #endregion
                    BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                    dtData = m_ApplyMaterial.Select(cmbBuCode.SelectedValue, txtMaterialId.Text.Trim(), txtCreateUserId.Text.Trim());
                    dgvData.DataSource = dtData;
                    dgvData.DataBind();

                    dgvWorkFlow.DataSource = null;
                    dgvWorkFlow.DataBind();
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
    }
}