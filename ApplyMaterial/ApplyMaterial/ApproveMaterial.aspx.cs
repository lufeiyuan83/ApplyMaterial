using FineUI;
using System;
using System.Collections.Generic;
using System.Data;

namespace ERP.ApplyMaterial
{
    public partial class ApproveMaterial : BasePage
    {
        private static string strFlowName = "ApplyMaterial";
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static DataTable dtData;
        private void LoadData(string m_FlowName, string m_EmployeeID, string m_Status)
        {
            try
            {
                DataTable dtApplyMaterial = null;
                BLL.PLM.FlowInstance m_FlowInstance = new BLL.PLM.FlowInstance();
                DataTable dtApprovalWorkflow = m_FlowInstance.GetCurrentWorkFlow(m_FlowName, m_EmployeeID);
                if (dtApprovalWorkflow != null && dtApprovalWorkflow.Rows.Count > 0)
                {
                    for (int i = 0; i < dtApprovalWorkflow.Rows.Count; i++)
                    {
                        BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                        DataTable dtDataTemp = m_ApplyMaterial.GetStatusData(dtApprovalWorkflow.Rows[i]["FormID"].ToString(), m_Status);
                        if (dtApplyMaterial == null)
                            dtApplyMaterial = dtDataTemp;
                        else
                            dtApplyMaterial.Merge(dtDataTemp);
                    }
                }
                dtData = dtApplyMaterial;
                DgvData.DataSource = dtApplyMaterial;
                DgvData.DataBind();
                if (DgvData.Rows.Count == 0)
                {
                    BtnApprove.Enabled = false;
                    BtnReject.Enabled = false;
                }
                else
                {
                    BtnApprove.Enabled = true;
                    BtnReject.Enabled = true;
                }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["d"] != null)
                {
                    if (Request.QueryString["u"] != null)
                        Util.Util.UserName = Request.QueryString["u"].ToString();
                }
                LoadData(strFlowName, Util.Util.UserName, "Processing");
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
                if (DgvData.SelectedRow != null)
                {
                    BLL.PLM.TipTopParameter m_TipTopParameter = new BLL.PLM.TipTopParameter();
                    DataTable dtTipTopParameter = m_TipTopParameter.Select(DgvData.SelectedRow.Values[1].ToString());
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
                LoadWorkFLow(strFlowName, DgvData.SelectedRow.Values[1].ToString());
            }
            else
            {
                dgvWorkFlow.DataSource = null;
                dgvWorkFlow.DataBind();
            }
        }
        protected void DgvData_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            string sortField = DgvData.SortField;
            string sortDirection = DgvData.SortDirection;

            DataView dv = dtData.DefaultView;
            if (!string.IsNullOrEmpty(sortField))
            {
                dv.Sort = String.Format("{0} {1}", sortField, sortDirection);
            }

            DgvData.DataSource = dv;
            DgvData.DataBind();
        }
        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (DgvData.SelectedRow != null)
                {
                    List<string> lstRequestNO = new List<string>();
                    for (int i = 0; i < DgvData.SelectedRowIndexArray.Length; i++)
                        lstRequestNO.Add(DgvData.Rows[DgvData.SelectedRowIndexArray[i]].Values[1].ToString());
                    BLL.PLM.FlowInstance m_FlowInstance = new BLL.PLM.FlowInstance();
                    if (m_FlowInstance.Approve(strFlowName, lstRequestNO, Util.Util.UserName, txtComments.Text, out string strMessage))
                    {
                        BLL.PLM.WorkList m_WorkList = new BLL.PLM.WorkList();
                        m_WorkList.Update(lstRequestNO, "~/ApplyMaterial/SearchMaterial.aspx", "1");

                        if (!m_FlowInstance.FlowIsFinished(strFlowName, lstRequestNO))
                        {
                            DataTable dtCurrentWorkFlow = m_FlowInstance.GetCurrentWorkFlow(strFlowName, lstRequestNO, "");
                            if (dtCurrentWorkFlow != null && dtCurrentWorkFlow.Rows.Count > 0)
                            {
                                BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                                Model.HRM.Employee employee = m_Employee.Select(Util.Util.applicationOrg, dtCurrentWorkFlow.Rows[0]["Approver"].ToString());
                                if (employee != null)
                                {
                                    List<Model.PLM.WorkList> lstWorkList = new List<Model.PLM.WorkList>();
                                    for (int i = 0; i < lstRequestNO.Count; i++)
                                    {
                                        Model.PLM.WorkList workList = new Model.PLM.WorkList
                                        {
                                            TableName = "PLM.dbo.ApplyMaterial",
                                            FormId = lstRequestNO[i],
                                            SystemCode = Util.Util.SystemCode,
                                            Title = GetGlobalResourceObject(strClassName, "ApplyMaterial").ToString() + "(" + lstRequestNO[i] + ")",
                                            URL = "~/ApplyMaterial/ApproveMaterial.aspx",
                                            Approver = employee.EmployeeID,
                                            FlowName = dtCurrentWorkFlow.Rows[0]["FlowName"].ToString(),
                                            Status = false,
                                            CreateUserID = Util.Util.UserName
                                        };
                                        lstWorkList.Add(workList);
                                    }
                                    if (m_WorkList.Add(lstWorkList))
                                        Util.MailSending.Send(employee.EmployeeEmail, "", GetGlobalResourceObject(strClassName, "ApplyMaterial").ToString(), string.Format(GetGlobalResourceObject(strClassName, "EmailBody").ToString(), employee.EmployeeName, lstRequestNO, Util.Util.webURL));
                                }
                            }
                            LoadData(strFlowName, Util.Util.UserName, "Processing");
                            Alert.Show(GetGlobalResourceObject(strClassName, "ApproveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                            if (DgvData.SelectedRow != null)
                            {
                                LoadData(strFlowName, Util.Util.UserName, "Processing");
                            }
                            else
                            {
                                dgvWorkFlow.DataSource = null;
                                dgvWorkFlow.DataBind();
                            }
                        }
                        else
                        {
                            BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                            List<Model.PLM.ApplyMaterial> lstApplyMaterial = m_ApplyMaterial.Select(lstRequestNO);
                            for (int i = 0; i < lstApplyMaterial.Count; i++)
                            {
                                lstApplyMaterial[i].Status = "Approved";
                                lstApplyMaterial[i].LastUpdateUserID = Util.Util.UserName;
                            }

                            if (m_ApplyMaterial.Update(lstApplyMaterial))
                            {
                                for (int i = 0; i < lstApplyMaterial.Count; i++)
                                {
                                    #region 抛送料号到TipTop
                                    TipTopMaterial.CostDataWSSoapClient tipTopMaterial = new TipTopMaterial.CostDataWSSoapClient();
                                    string[] arrBuCode = lstApplyMaterial[i].BuCode.Split(',');
                                    BLL.PLM.TipTopParameter m_TipTopParameter = new BLL.PLM.TipTopParameter();
                                    bool isExistPostPostSuccessData = false;
                                    for (int j = 0; j < arrBuCode.Length; j++)
                                    {
                                        #region 记录抛送TipTop的报文
                                        TipTopMaterial.ArrayOfString lstContent = new TipTopMaterial.ArrayOfString();
                                        string strContent = "";
                                        DataTable dtTipTopParameter = m_TipTopParameter.Select(lstApplyMaterial[i].MaterialId, arrBuCode[j]);
                                        if (dtTipTopParameter != null && dtTipTopParameter.Rows.Count > 0)
                                        {
                                            lstContent.Add("<ima_file><Item><ima01>" + lstApplyMaterial[i].MaterialId + "</ima01><ima02>" + lstApplyMaterial[i].ProductionName + "</ima02><ima021>" + lstApplyMaterial[i].Specification + "</ima021><ima06>" + dtTipTopParameter.Rows[0]["GroupCode"].ToString() + "</ima06><ima07>" + dtTipTopParameter.Rows[0]["ABCCode"].ToString() + "</ima07><ima08>" + dtTipTopParameter.Rows[0]["SourceCode"].ToString() + "</ima08><ima12>" + dtTipTopParameter.Rows[0]["FourthGroupCode"].ToString() + "</ima12><ima15>" + dtTipTopParameter.Rows[0]["IsBonded"].ToString() + "</ima15><ima25>" + dtTipTopParameter.Rows[0]["MainUnit"].ToString() + "</ima25><ima37>" + dtTipTopParameter.Rows[0]["ReplenishmentStrategyCode"].ToString() + "</ima37><ima70>" + dtTipTopParameter.Rows[0]["IsConsumed"].ToString() + "</ima70><ima105>" + dtTipTopParameter.Rows[0]["IsSoftwareObject"].ToString() + "</ima105><ima107>" + dtTipTopParameter.Rows[0]["MultiLocation"].ToString() + "</ima107><ima110>" + dtTipTopParameter.Rows[0]["MaterialClass"].ToString() + "</ima110></Item></ima_file>");

                                            strContent = "<ima_file>";
                                            strContent += " <Item>";
                                            strContent += "     <ima01>" + lstApplyMaterial[i].MaterialId + "</ima01>";//物料编码
                                            strContent += "     <ima02>" + lstApplyMaterial[i].ProductionName + "</ima02>";//品名
                                            strContent += "     <ima021>" + lstApplyMaterial[i].Specification + "</ima021>";//规格型号
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
                                        string strResult = tipTopMaterial.CostDataIn("PN", arrBuCode[j], "Insert", lstContent);
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
                                                BuCode = arrBuCode[j],
                                                Message = ds.Tables[0].Rows[0]["msg"].ToString(),
                                                Status = (ds.Tables[0].Rows[0]["flag"].ToString() == "Y" || ds.Tables[0].Rows[0]["msg"].ToString().Contains("料號已經存在"))?"1":"0",
                                                MaterialId = lstApplyMaterial[i].MaterialId,
                                                PostParameter = strContent,
                                                CreateUserID = Util.Util.UserName,
                                                LastUpdateUserID = Util.Util.UserName,
                                                Remark = ""
                                            };
                                            isExistPostPostSuccessData = (interfaceLog.Status == "1");
                                            if (m_InterfaceLog.Add(interfaceLog))
                                            {
                                                Model.PLM.TipTopParameter tipTopParameter = new Model.PLM.TipTopParameter
                                                {
                                                    MaterialId = lstRequestNO[i],
                                                    BuCode = arrBuCode[j],
                                                    IsPostSuccess = interfaceLog.Status == "1",
                                                    LastUpdateUserID = Util.Util.UserName
                                                };
                                                m_TipTopParameter.Update(tipTopParameter);
                                            }
                                            #endregion
                                        }
                                    }
                                    #region 将抛送成功的料号转入正式料号表中
                                    if (isExistPostPostSuccessData)
                                    {
                                        BLL.PLM.Material m_Material = new BLL.PLM.Material();
                                        Model.PLM.Material material = new Model.PLM.Material
                                        {
                                            MaterialId = lstApplyMaterial[i].MaterialId,
                                            Rev = lstApplyMaterial[i].Rev,
                                            ProductionName = lstApplyMaterial[i].ProductionName,
                                            Specification = lstApplyMaterial[i].Specification,
                                            CreateUserID = lstApplyMaterial[i].CreateUserID,
                                            LastUpdateUserID = lstApplyMaterial[i].LastUpdateUserID,
                                            Remark = lstApplyMaterial[i].Remark
                                        };
                                        m_Material.Add(material);
                                    }
                                    #endregion
                                    #endregion
                                    #region 更新审批列表
                                    Model.PLM.WorkList workList = m_WorkList.Search("ERP", "PLM.dbo.ApplyMaterial", lstApplyMaterial[i].MaterialId);
                                    workList.FormId = lstApplyMaterial[i].MaterialId;
                                    workList.Title = "(" + lstRequestNO[i] + ")" + GetGlobalResourceObject(strClassName, "MaterialApproveOK").ToString();
                                    m_WorkList.Update(workList);
                                    #endregion
                                    LoadData(strFlowName, Util.Util.UserName, "Processing");
                                    Alert.Show(GetGlobalResourceObject(strClassName, "ApproveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                                    if (DgvData.SelectedRow != null)
                                    {
                                        LoadWorkFLow(strFlowName, DgvData.SelectedRow.Values[1].ToString());
                                    }
                                    else
                                    {
                                        dgvWorkFlow.DataSource = null;
                                        dgvWorkFlow.DataBind();
                                    }
                                }
                            }
                        }
                    }
                    else
                        Alert.Show(strMessage, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
                else
                    Alert.Show(GetGlobalResourceObject(strClassName, "SelectData").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void BtnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtComments.Text.Trim().Length==0)
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "InputComments").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                    return;
                }
                if (DgvData.SelectedRow != null)
                {
                    List<string> lstRequestNO = new List<string>();
                    string strRequestNO = "";
                    for (int i = 0; i < DgvData.SelectedRowIndexArray.Length; i++)
                    {
                        strRequestNO += DgvData.Rows[DgvData.SelectedRowIndexArray[i]].Values[1].ToString() + ",";
                        lstRequestNO.Add(DgvData.Rows[DgvData.SelectedRowIndexArray[i]].Values[1].ToString());
                    }
                    if (strRequestNO.Length > 0)
                        strRequestNO = strRequestNO.Substring(0, strRequestNO.Length - 1);
                    BLL.PLM.FlowInstance m_FlowInstance = new BLL.PLM.FlowInstance();
                    int intStep = -1;
                    if (m_FlowInstance.Reject(strFlowName, lstRequestNO, Util.Util.UserName, txtComments.Text, out string strMessage, out intStep))
                    {
                        BLL.PLM.WorkList m_WorkList = new BLL.PLM.WorkList();
                        for (int i = 0; i < lstRequestNO.Count; i++)
                        {
                            Model.PLM.WorkList workList = m_WorkList.Search("ERP", "PLM.dbo.ApplyMaterial", lstRequestNO[i]);
                            workList.FormId = lstRequestNO[i];
                            workList.Title = "(" + lstRequestNO[i] + ")"+ GetGlobalResourceObject(strClassName, "Rejected").ToString();
                            workList.URL = "~/ApplyMaterial/SearchMaterial.aspx";
                            workList.Status = true;
                            m_WorkList.Update(workList);
                        }
                        DataTable dtRejectWorkFlow = m_FlowInstance.Select(strFlowName, lstRequestNO, intStep);
                        if (dtRejectWorkFlow != null && dtRejectWorkFlow.Rows.Count > 0)
                        {
                            BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                            Model.HRM.Employee employee = m_Employee.Select(Util.Util.applicationOrg, dtRejectWorkFlow.Rows[0]["Approver"].ToString());
                            if (employee != null)
                                Util.MailSending.Send(employee.EmployeeEmail, "", GetGlobalResourceObject(strClassName, "MaterialRejectNotice").ToString(),string.Format(GetGlobalResourceObject(strClassName, "MaterialRejectNotice").ToString(),employee.EmployeeName , strRequestNO, Util.Util.webURL));
                        }
                        BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                        List<Model.PLM.ApplyMaterial> lstApplyMaterial = m_ApplyMaterial.Select(lstRequestNO);
                        for (int i = 0; i < lstApplyMaterial.Count; i++)
                        {
                            lstApplyMaterial[i].Status = "Reject";
                            lstApplyMaterial[i].LastUpdateUserID = Util.Util.UserName;
                        }
                        if (m_ApplyMaterial.Update(lstApplyMaterial))
                        {
                            LoadData(strFlowName, Util.Util.UserName, "Processing");
                            Alert.Show(GetGlobalResourceObject(strClassName, "RejectOK").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                            if (DgvData.SelectedRow != null)
                            {
                                LoadWorkFLow(strFlowName, DgvData.SelectedRow.Values[1].ToString());
                            }
                            else
                            {
                                dgvWorkFlow.DataSource = null;
                                dgvWorkFlow.DataBind();
                            }
                        }
                    }
                    else
                        Alert.Show(strMessage, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
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