using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;

namespace ERP.ApplyMaterial
{
    public partial class ApplyMaterial : BasePage
    {
        private string strFlowName = "ApplyMaterial";
        private readonly string strClassName = new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType.Name + ".aspx";
        private static string strCodeRule = "";

        private static DataTable dtData = new DataTable();
        private static Panel p;

        /// <summary>
        /// 解析编码规则
        /// </summary>
        /// <param name="rule">编码规则，例如[CodeRule][TopClass]-[MiddleClass]-[SubClass][SN]</param>
        /// <returns></returns>
        private void ParseRule(string rule)
        {
            dtData.Clear();
            List<string> lstRule = new List<string>();
            while (rule.Contains("["))
            {
                int intStartIndex = rule.IndexOf('[');
                int intEndIndex = rule.IndexOf(']');
                lstRule.Add(rule.Substring(intStartIndex + 1, intEndIndex - intStartIndex - 1).Trim());
                rule = rule.Substring(intEndIndex + 1);
            }
            for (int i = 0; i < lstRule.Count; i++)
            {
                foreach (Control con in p.Controls[0].Controls)
                {
                    foreach (Control control in con.Controls)
                    {
                        if (control is FineUI.DropDownList)
                        {
                            FineUI.DropDownList ddl = (control as FineUI.DropDownList);
                            if (lstRule[i] == "CodeRule")
                            {
                                BLL.PLM.Mapping m_Mapping = new BLL.PLM.Mapping();
                                DataRow dr = dtData.NewRow();
                                dr["Id"] = dtData.Rows.Count;
                                dr["ParameterCode"] = lstRule[i];
                                dr["IsDone"] = true;
                                dr["DataTable"] = m_Mapping.Select("Material", "MaterialCodeRule");
                                dr["Type"] = CmbCodeRule.GetType().Name;
                                dr["ControlId"] = CmbCodeRule.ID;
                                dr["Value"] = CmbCodeRule.SelectedValue;
                                dtData.Rows.Add(dr);
                                break;
                            }
                            else if (ddl.EmptyText == lstRule[i])
                            {
                                DataRow dr = dtData.NewRow();
                                dr["Id"] = dtData.Rows.Count;
                                dr["ParameterCode"] = lstRule[i];
                                dr["IsDone"] = false;
                                dr["DataTable"] = null;
                                dr["Type"] = control.GetType().Name;
                                dr["ControlId"] = ddl.ID;
                                dr["Value"] = "";
                                dtData.Rows.Add(dr);
                                break;
                            }
                        }
                        else if (control is TextBox)
                        {
                            TextBox txt = (control as TextBox);
                            if (txt.EmptyText == lstRule[i])
                            {
                                DataRow dr = dtData.NewRow();
                                dr["Id"] = dtData.Rows.Count;
                                dr["ParameterCode"] = lstRule[i];
                                dr["IsDone"] = false;
                                dr["DataTable"] = null;
                                dr["Type"] = control.GetType().Name;
                                dr["ControlId"] = txt.ID;
                                dr["Value"] = "";
                                dtData.Rows.Add(dr);
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="codeRule">编码原则</param>
        private void InitData(string codeRule)
        {
            try
            {
                txtMaterialId.Text = "";
                #region 设置所有动态参数Panel不可见
                foreach (Control control in pData.Controls)
                {
                    if (control is Panel)
                    {
                        (control as Panel).Hidden = true;
                    }
                }
                #endregion
                BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                DataTable dtCodeRule = m_CodeRule.Select("MaterialCodeRule", codeRule);
                if (dtCodeRule != null && dtCodeRule.Rows.Count > 0)
                {
                    p = (FineUI.Panel)ControlUtil.FindControl(dtCodeRule.Rows[0]["BindPanelId"].ToString());
                    if (p != null)
                    {
                        p.Hidden = false;//这是当前选择的编码原则对应的参数Panel可见
                        #region 根据CodeRule表中配置的规则，动态判断每个下拉控件的SelectedIndexChanged事件执行动作和顺序
                        strCodeRule = dtCodeRule.Rows[0]["RuleEN"].ToString().Trim();
                        txtMaterialId.Text = strCodeRule;
                        ParseRule(strCodeRule);
                        if (dtData.Rows.Count > 0)
                        {
                            LoadData(codeRule);
                        }
                        #endregion
                    }
                }
                else
                {
                    Alert.Show(string.Format(GetGlobalResourceObject(strClassName, "NOTFoundCodeRule").ToString(), codeRule), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 级联关系数据动态绑定
        /// </summary>
        /// <param name="codeRule">编码规则</param>
        private void LoadData(string codeRule)
        {
            try
            {
                int intStartIndex = -1;//每次级联关系只级联下一级DropDownList的绑定
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (intStartIndex == -1)
                    {
                        BLL.PLM.Mapping m_Mapping = new BLL.PLM.Mapping();
                        if (!Convert.ToBoolean(dtData.Rows[i]["IsDone"]))
                        {
                            if (i < 1)
                            {
                                #region dtData第一行是CodeRule，当初进行处理
                                FineUI.DropDownList ddl = (FineUI.DropDownList)ControlUtil.FindControl(dtData.Rows[i]["ControlId"].ToString());
                                if (dtData.Rows[i]["Type"].ToString() == "DropDownList")
                                {
                                    ddl.DataSource = m_Mapping.GetExtendItems("Material", "MaterialCodeRule", codeRule);
                                    ddl.DataBind();
                                    dtData.Rows[i]["IsDone"] = true;
                                    dtData.Rows[i]["DataTable"] = (DataTable)ddl.DataSource;
                                    dtData.Rows[i]["Value"] = ""; 
                                }
                                #endregion
                            }
                            else
                            {
                                if (dtData.Rows[i]["Type"].ToString() == "DropDownList")
                                {
                                    #region 如果当前级联控件是DropDownList，则依据上一级的选择值获得当前级联关系的数据，进行数据绑定
                                    DataRow[] drs;
                                    if (dtData.Rows[i - 1]["ParameterCode"].ToString() == "SN")
                                        drs = (dtData.Rows[i - 1]["DataTable"] as DataTable).Select("MappingValue2='" + dtData.Rows[i - 1]["Value"].ToString() + "'");
                                    else
                                        drs = (dtData.Rows[i - 1]["DataTable"] as DataTable).Select("MappingValue1='" + dtData.Rows[i - 1]["Value"].ToString() + "'");
                                    FineUI.DropDownList ddl = (FineUI.DropDownList)ControlUtil.FindControl(dtData.Rows[i]["ControlId"].ToString());
                                    if (drs.Length > 0)
                                    {
                                        if (drs[0]["ExtendID"].ToString() == "0")
                                        {
                                            #region 当ExtendID为0时，则断开级联关系，从下一级开始继续判断级联情况，如无，直接加载即可
                                            if (ddl.Items.Count == 0)
                                            {
                                                DataTable dt = m_Mapping.Search("Material", dtData.Rows[i]["ParameterCode"].ToString(), CmbCodeRule.SelectedValue);
                                                ddl.DataSource = dt;
                                                ddl.DataBind();
                                                dtData.Rows[i]["IsDone"] = true;
                                                dtData.Rows[i]["DataTable"] = dt;
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            DataTable dt = m_Mapping.GetExtendItems(Convert.ToInt64(drs[0]["MappingId"]));
                                            ddl.DataSource = dt;
                                            ddl.DataBind();
                                            dtData.Rows[i]["IsDone"] = true;
                                            dtData.Rows[i]["DataTable"] = dt;
                                        }
                                    }
                                    else
                                    {
                                        ddl.DataSource = null;
                                        ddl.DataBind();
                                        dtData.Rows[i]["IsDone"] = true;
                                        dtData.Rows[i]["DataTable"] = null;
                                        dtData.Rows[i]["Value"] = "";
                                    }
                                    #endregion
                                }
                                else if (dtData.Rows[i]["Type"].ToString() == "TextBox")
                                {
                                    #region TextBox，则依据上一级的选择值获得当前级联关系的数据，进行数据赋值
                                    DataRow[] drs = (dtData.Rows[i - 1]["DataTable"] as DataTable).Select("MappingValue1='" + dtData.Rows[i - 1]["Value"].ToString() + "'");
                                    if (drs.Length > 0)
                                    {
                                        FineUI.TextBox txt = (FineUI.TextBox)ControlUtil.FindControl(dtData.Rows[i]["ControlId"].ToString());
                                        txt.Text = "";
                                        DataTable dt = m_Mapping.GetExtendItems(Convert.ToInt64(drs[0]["MappingId"]));
                                        dtData.Rows[i]["IsDone"] = true;
                                        dtData.Rows[i]["DataTable"] = dt;
                                        if (dt != null && dt.Rows.Count > 0)
                                        {
                                            dtData.Rows[i]["Value"] = dt.Rows[0]["MappingValue2"].ToString();
                                            dtData.Rows[i]["Text"] = dt.Rows[0]["MappingValue1"].ToString();
                                            txt.Text = dt.Rows[0]["MappingValue2"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        dtData.Rows[i]["IsDone"] = true;
                                        dtData.Rows[i]["DataTable"] = null;
                                        dtData.Rows[i]["Value"] = "";
                                        dtData.Rows[i]["Text"] = "";
                                    }
                                    #endregion
                                }
                            }
                            intStartIndex = i;
                        }
                    }
                    else
                    {
                        #region 清空当前DropDownList下级所有级联关系的数据绑定
                        if (dtData.Rows[i]["Type"].ToString() == "DropDownList")
                        {
                            FineUI.DropDownList ddl = (FineUI.DropDownList)ControlUtil.FindControl(dtData.Rows[i]["ControlId"].ToString());
                            ddl.DataSource = null;
                            ddl.DataBind();
                        }
                        #endregion
                        #region 清空当前TextBox的内容
                        else if (dtData.Rows[i]["Type"].ToString() == "TextBox")
                        {
                            FineUI.TextBox txt = (FineUI.TextBox)ControlUtil.FindControl(dtData.Rows[i]["ControlId"].ToString());
                            txt.Text = "";
                        }
                        #endregion
                    }
                }
                #region 实时依据当前DropDownList选择的值进行物料编码替换
                string strResult = "";
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (String.IsNullOrEmpty(strResult))
                        strResult = strCodeRule.Replace("[" + dtData.Rows[i]["ParameterCode"].ToString() + "]", dtData.Rows[i]["Value"].ToString() == "" ? "[" + dtData.Rows[i]["ParameterCode"].ToString() + "]" : (dtData.Rows[i]["Value"].ToString() == "~" ? "" : dtData.Rows[i]["Value"].ToString()));
                    else
                        strResult = strResult.Replace("[" + dtData.Rows[i]["ParameterCode"].ToString() + "]", dtData.Rows[i]["Value"].ToString() == "" ? "[" + dtData.Rows[i]["ParameterCode"].ToString() + "]" : (dtData.Rows[i]["Value"].ToString() == "~" ? "" : dtData.Rows[i]["Value"].ToString()));
                }
                txtMaterialId.Text = strResult;
                #endregion
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 设定DropDownList的默认值
        /// </summary>
        /// <param name="dtMapping">数据表</param>
        /// <param name="ddl">需要设定默认中的DropDownList</param>
        private void SetDefaultMappingValue(DataTable dtMapping, FineUI.DropDownList ddl)
        {
            if (dtMapping != null)
            {
                DataRow[] drs = dtMapping.Select("IsDefault=1");
                if (drs.Length > 0)
                {
                    ddl.SelectedValue = drs[0]["MappingValue1"].ToString();
                }
            }
        }
        /// <summary>
        /// 加载动态页面
        /// </summary>
        /// <param name="uiForm">页面名称</param>
        private void LoadUI(string uiForm)
        {
            BLL.PLM.UI ui = new BLL.PLM.UI();
            DataTable dtUI = ui.Select(uiForm);
            if (dtUI != null && dtUI.Rows.Count > 0)
            {
                DataRow[] drsPanel = dtUI.Select("Type='Panel'", "SortNumber");
                for (int i = 0; i < drsPanel.Length; i++)
                {
                    Panel panel = new Panel();
                    panel.ID = drsPanel[i]["ID"].ToString().Trim();
                    panel.Layout = (Layout)Enum.Parse(typeof(Layout), drsPanel[i]["Layout"].ToString().Trim());
                    panel.ShowHeader = Convert.ToBoolean(drsPanel[i]["ShowHeader"].ToString().Trim());
                    panel.Title = drsPanel[i]["Title"].ToString().Trim(); 
                    pData.Items.Add(panel);
                    DataRow[] drsForm = dtUI.Select("UIId='" + drsPanel[i]["ChildId"].ToString().Trim() + "'", "SortNumber");
                    if (drsForm.Length > 0)
                    {
                        Form form = new Form();
                        form.ID = drsForm[0]["ID"].ToString().Trim();
                        form.ShowBorder = Convert.ToBoolean(drsForm[0]["ShowBorder"].ToString().Trim());
                        form.ShowHeader = Convert.ToBoolean(drsForm[0]["ShowHeader"].ToString().Trim());
                        form.Width = Convert.ToInt32(drsForm[0]["Width"].ToString().Trim());
                        form.BodyPadding = drsForm[0]["BodyPadding"].ToString().Trim();
                        panel.Items.Add(form);
                        string[] arrFormRow = drsForm[0]["ChildId"].ToString().Trim().Split(',');
                        for (int j = 0; j < arrFormRow.Length; j++)
                        {
                            DataRow[] drsFormRow = dtUI.Select("UIId='" + arrFormRow[j].Trim() + "'", "SortNumber");
                            if (drsFormRow.Length > 0)
                            {
                                for (int k = 0; k < drsFormRow.Length; k++)
                                {
                                    FormRow formRow = new FormRow();
                                    form.Items.Add(formRow);
                                    string[] arrControl = drsFormRow[k]["ChildId"].ToString().Trim().Split(',');
                                    for (int m = 0; m < arrControl.Length; m++)
                                    {
                                        DataRow[] drsControl = dtUI.Select("UIId='" + arrControl[m].Trim() + "'", "SortNumber");
                                        if (drsControl.Length > 0)
                                        {
                                            if (drsControl[0]["Type"].ToString().Trim() == "DropDownList")
                                            {
                                                DropDownList ddl = new DropDownList();
                                                ddl.ID = drsControl[0]["ID"].ToString().Trim();
                                                ddl.EmptyText = drsControl[0]["EmptyText"].ToString().Trim();
                                                ddl.Label = GetGlobalResourceObject(strClassName, drsControl[0]["Label"].ToString().Trim()).ToString();
                                                ddl.LabelAlign = (LabelAlign)Enum.Parse(typeof(LabelAlign), drsControl[0]["LabelAlign"].ToString().Trim());
                                                ddl.DataValueField = drsControl[0]["DataValueField"].ToString().Trim();
                                                ddl.DataTextField = drsControl[0]["DataTextField"].ToString().Trim();
                                                ddl.Required = Convert.ToBoolean(drsControl[0]["Required"].ToString().Trim());
                                                ddl.ShowRedStar = Convert.ToBoolean(drsControl[0]["ShowRedStar"].ToString().Trim());
                                                ddl.AutoPostBack = Convert.ToBoolean(drsControl[0]["AutoPostBack"].ToString().Trim());
                                                ddl.AutoSelectFirstItem = Convert.ToBoolean(drsControl[0]["AutoSelectFirstItem"].ToString().Trim());
                                                ddl.SelectedIndexChanged += Cmb_SelectedIndexChanged;
                                                formRow.Items.Add(ddl);
                                            }
                                            else if (drsControl[0]["Type"].ToString() == "TextBox")
                                            {
                                                TextBox txt = new TextBox();
                                                txt.ID = drsControl[0]["ID"].ToString().Trim();
                                                txt.EmptyText = drsControl[0]["EmptyText"].ToString().Trim();
                                                txt.Label = GetGlobalResourceObject(strClassName, drsControl[0]["Label"].ToString().Trim()).ToString();
                                                txt.LabelAlign = (LabelAlign)Enum.Parse(typeof(LabelAlign), drsControl[0]["LabelAlign"].ToString().Trim());
                                                txt.Enabled = Convert.ToBoolean(drsControl[0]["Enabled"].ToString().Trim());
                                                formRow.Items.Add(txt);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            string path = Request.Url.ToString();
            string name = path.Substring(path.LastIndexOf("/") + 1);
            LoadUI(name);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!dtData.Columns.Contains("Id"))
                    {
                        dtData.Columns.Add("Id", typeof(int));
                        dtData.Columns.Add("ParameterCode");
                        dtData.Columns.Add("IsDone", typeof(bool));
                        dtData.Columns.Add("DataTable", typeof(DataTable));
                        dtData.Columns.Add("Type");
                        dtData.Columns.Add("ControlId");
                        dtData.Columns.Add("Value");
                        dtData.Columns.Add("Text"); 
                        dtData.Columns.Add("ExtendID");
                    }

                    BLL.PLM.Mapping m_Mapping = new BLL.PLM.Mapping();
                    DataTable dtMapping = m_Mapping.Select("Database", "TipTop");
                    cmbBuCode.DataSource = dtMapping;
                    cmbBuCode.DataBind();

                    DataTable dtBuCode = m_Mapping.Select("Material", "BuCode");
                    if (dtBuCode != null && dtBuCode.Rows.Count > 0)
                        cmbBuCode.SelectedValueArray = dtBuCode.Rows[0]["MappingValue2"].ToString().Split(',');
                    else
                        SetDefaultMappingValue(dtMapping, cmbBuCode);

                    List<string> lstMapCode = new List<string>() { "MaterialCodeRule", "MaterialClass", "MainUnit", "SourceCode", "FourthGroupCode", "ABCCode" };
                    DataSet dsMapping = m_Mapping.Select("Material", lstMapCode);
                    CmbCodeRule.DataSource = dsMapping.Tables["MaterialCodeRule"];
                    CmbCodeRule.DataBind();
                    SetDefaultMappingValue(dsMapping.Tables["MaterialCodeRule"], CmbCodeRule);

                    InitData(CmbCodeRule.SelectedValue);

                    cmbMaterialClass.DataSource = dsMapping.Tables["MaterialClass"];
                    cmbMaterialClass.DataBind();
                    SetDefaultMappingValue(dsMapping.Tables["MaterialClass"], cmbMaterialClass);

                    cmbMainUnit.DataSource = dsMapping.Tables["MainUnit"];
                    cmbMainUnit.DataBind();
                    SetDefaultMappingValue(dsMapping.Tables["MainUnit"], cmbMainUnit);

                    cmbSourceCode.DataSource = dsMapping.Tables["SourceCode"];
                    cmbSourceCode.DataBind();
                    SetDefaultMappingValue(dsMapping.Tables["SourceCode"], cmbSourceCode);

                    cmbFourthGroupCode.DataSource = dsMapping.Tables["FourthGroupCode"];
                    cmbFourthGroupCode.DataBind();
                    SetDefaultMappingValue(dsMapping.Tables["FourthGroupCode"], cmbFourthGroupCode);

                    cmbABCCode.DataSource = dsMapping.Tables["ABCCode"];
                    cmbABCCode.DataBind();
                    SetDefaultMappingValue(dsMapping.Tables["ABCCode"], cmbABCCode);
                }
                catch (Exception ex)
                {
                    BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                    Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
            }
        }
        protected void CmbCodeRule_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProductionName.Text = "";
            txtSpecification.Text = "";
            InitData(CmbCodeRule.SelectedValue);
        }
        protected void Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((FineUI.DropDownList)sender).SelectedValue != null)
                {
                    DataRow[] drs = dtData.Select("ParameterCode='" + ((FineUI.DropDownList)sender).EmptyText + "'");
                    if (drs.Length > 0)
                    {
                        if (((FineUI.DropDownList)sender).EmptyText == "SN")
                        {
                            drs[0]["Value"] = ((FineUI.DropDownList)sender).SelectedText;
                            drs[0]["Text"] = ((FineUI.DropDownList)sender).SelectedValue;
                            DataRow[] drsExtendID = ((DataTable)drs[0]["DataTable"]).Select("MappingValue1='"+ drs[0]["Text"].ToString() + "'");
                            if (drsExtendID.Length > 0)
                                drs[0]["ExtendID"] = drsExtendID[0]["ExtendID"].ToString();
                        }
                        else
                        {
                            drs[0]["Value"] = ((FineUI.DropDownList)sender).SelectedValue;
                            drs[0]["Text"] = ((FineUI.DropDownList)sender).SelectedText;
                            DataRow[] drsExtendID = ((DataTable)drs[0]["DataTable"]).Select("MappingValue1='" + drs[0]["Value"].ToString() + "'");
                            if (drsExtendID.Length > 0)
                                drs[0]["ExtendID"] = drsExtendID[0]["ExtendID"].ToString();

                            DataRow[] drsMiddleClass2 = dtData.Select("ControlID='CmbMiddleClass2'");
                            if (drsMiddleClass2.Length > 0)
                            {
                                if(drsMiddleClass2[0]["Text"].ToString() == "修磨刀具")
                                {
                                    cmbRepairMoldType.Hidden = false;
                                    BLL.PLM.Mapping m_Mapping = new BLL.PLM.Mapping();
                                    cmbRepairMoldType.DataSource = m_Mapping.Select("Material", "RepairMoldType");
                                    cmbRepairMoldType.DataBind();
                                }
                                else
                                    cmbRepairMoldType.Hidden = true;
                            }
                            else
                                cmbRepairMoldType.Hidden = true;
                        }
                        #region 清空当前DropDownList的下级级联关系的IsDone、DataTable和Value值
                        for (int i = Convert.ToInt32(drs[0]["Id"]) + 1; i < dtData.Rows.Count; i++)
                        {
                            if (i > 0 && dtData.Rows[i - 1]["ExtendID"].ToString() != "0")
                            {
                                if (dtData.Rows[i]["ExtendID"].ToString() != "0")
                                {
                                    dtData.Rows[i]["IsDone"] = false;
                                    dtData.Rows[i]["DataTable"] = null;
                                    dtData.Rows[i]["Value"] = "";
                                    dtData.Rows[i]["Text"] = "";
                                    dtData.Rows[i]["ExtendID"] = "";
                                    break;
                                }
                            }
                        }
                        dtData.Rows[dtData.Rows.Count-1]["ExtendID"] = "0";
                        #endregion
                        LoadData(strCodeRule);
                        ReplaceProductionName(CmbCodeRule.SelectedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        private void ReplaceProductionName(string _CodeRule)
        {
            try
            {
                BLL.CodeRule.CodeRule m_CodeRule = new BLL.CodeRule.CodeRule();
                DataTable dtCodeRule = m_CodeRule.Select("ProductionNameCodeRule", _CodeRule);
                if (dtCodeRule != null && dtCodeRule.Rows.Count > 0)
                {
                    #region 实时依据当前DropDownList选择的值进行物料编码替换
                    string strResult = "";
                    for (int i = 0; i < dtData.Rows.Count; i++)
                    {
                        if (String.IsNullOrEmpty(strResult))
                            strResult = dtCodeRule.Rows[0]["RuleEN"].ToString().Replace("[" + dtData.Rows[i]["ParameterCode"].ToString() + "]", dtData.Rows[i]["Text"].ToString() == "" ? "[" + dtData.Rows[i]["ParameterCode"].ToString() + "]" : dtData.Rows[i]["Text"].ToString());
                        else
                            strResult = strResult.Replace("[" + dtData.Rows[i]["ParameterCode"].ToString() + "]", dtData.Rows[i]["Text"].ToString() == "" ? "[" + dtData.Rows[i]["ParameterCode"].ToString() + "]" : dtData.Rows[i]["Text"].ToString());
                    }
                    strResult = strResult.Replace("[Specification]", txtSpecification.Text.Trim() == "" ? "[Specification]" : txtSpecification.Text.Trim());
                    strResult = strResult.Replace("[SourceCode]", cmbSourceCode.SelectedText);
                    strResult = strResult.Replace("[Rev]", txtRev.Text.Trim() == "" ? "[Rev]" : txtRev.Text.Trim());
                    if (!cmbRepairMoldType.Hidden)
                        strResult = strResult.Replace("[RepairMoldType]", String.IsNullOrEmpty(cmbRepairMoldType.SelectedText)? "[RepairMoldType]" : cmbRepairMoldType.SelectedText.Trim());
                    else
                        strResult = strResult.Replace("[RepairMoldType]", "");
                    txtProductionName.Text = strResult;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
        protected void txtSpecification_TextChanged(object sender, EventArgs e)
        {
            ReplaceProductionName(CmbCodeRule.SelectedValue);
        }
        protected void txtRev_TextChanged(object sender, EventArgs e)
        {
            ReplaceProductionName(CmbCodeRule.SelectedValue);
        }
        protected void cmbRepairMoldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReplaceProductionName(CmbCodeRule.SelectedValue);
        }
        protected void BtnSaveAndCreate_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据校准
                if (txtProductionName.Text.Trim().Length == 0)
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "ProductionNameCanNotBlank").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                    return;
                }
                if (txtSpecification.Text.Trim().Length == 0)
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "SpecificationCanNotBlank").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                    return;
                }
                if (txtGroupCode.Text.Trim().Length == 0)
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "GroupCodeCanNotBlank").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                    return;
                }
                #endregion
                #region 获得当前审批流
                BLL.PLM.Flow m_Flow = new BLL.PLM.Flow();
                DataTable dtFlow = m_Flow.Select(strFlowName, CmbCodeRule.SelectedText, Util.Util.UserName);
                int intFlowGroup = -1;
                if (dtFlow == null || dtFlow.Rows.Count == 0)
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "NoAuth").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                    return;
                }
                else if (dtFlow != null && dtFlow.Rows.Count > 0)
                {
                    DataRow[] drs = dtFlow.Select("FlowNode='Applier'");
                    if (drs.Length == 0)
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "NoAuth").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                        return;
                    }
                    else
                        intFlowGroup = Convert.ToInt32(drs[0]["FlowGroup"]);
                }
                #endregion
                #region 数据准备
                string strBuCode = "";
                for (int i = 0; i < cmbBuCode.SelectedValueArray.Length; i++)
                {
                    strBuCode += cmbBuCode.SelectedValueArray[i] + ",";
                }
                if (strBuCode.Length > 0)
                    strBuCode = strBuCode.Substring(0, strBuCode.Length - 1);
                Model.PLM.ApplyMaterial applyMaterial = new Model.PLM.ApplyMaterial
                {
                    MaterialId = txtMaterialId.Text,
                    Rev=txtRev.Text.Trim(),
                    ProductionName = txtProductionName.Text.Trim(),
                    Specification = txtSpecification.Text.Trim(),
                    BuCode = strBuCode,
                    Status = "Processing",
                    IsDeleted = false,
                    CreateUserID = Util.Util.UserName,
                    LastUpdateUserID = Util.Util.UserName,
                    Remark = txtRemark.Text.Trim()
                };

                List<Model.PLM.TipTopParameter> lstTipTopParameter = new List<Model.PLM.TipTopParameter>();
                for (int i = 0; i < cmbBuCode.SelectedValueArray.Length; i++)
                {
                    DataRow[] drsSN = dtData.Select("ParameterCode='SN'");
                    string strStartSN = "";
                    string strSN = "";
                    if (drsSN.Length > 0)
                    {
                        strStartSN = drsSN[0]["Text"].ToString();
                        strSN = drsSN[0]["Value"].ToString();
                    }
                    Model.PLM.TipTopParameter tipTopParameter = new Model.PLM.TipTopParameter
                    {
                        StartSN = strStartSN,
                        SN = strSN,
                        SourceCode = cmbSourceCode.SelectedValue,
                        SourceCode2 = "R",
                        GroupCode = txtGroupCode.Text.Trim(),
                        FourthGroupCode = cmbFourthGroupCode.SelectedValue,
                        ABCCode = cmbABCCode.SelectedValue,
                        IsBonded = cbIsBonded.Checked ? "Y" : "N",
                        MaterialClass = cmbMaterialClass.SelectedValue,
                        MainUnit = cmbMainUnit.SelectedValue,
                        BuCode = cmbBuCode.SelectedValueArray[i],

                        IsConsumed = "N",
                        ReplenishmentStrategyCode = "0",
                        IsSoftwareObject = "N",
                        MultiLocation = "N",
                        MPSMRPInventoryQty = "0.000",
                        InavailableInventoryQty = "0.000",
                        AvailableInventoryQty = "0.000",
                        IsEngineeringMaterial = "N",
                        LowCode = 99,
                        UnitWeight = "0E-9",
                        CostLevel = 1,
                        AveragePurchaseQty = 1,
                        Ima110 = 1,
                        Ima137 = "N",
                        SNContorol = "0",
                        Ima146 = "R",
                        IsMassProductionMaterial = "N",
                        Ima901 = DateTime.Now,
                        Ima908 = "Y"
                    };
                    lstTipTopParameter.Add(tipTopParameter);
                }
                #endregion
                #region 检查是否有审批权限
                if (!m_Flow.CheckApproveAccess(strFlowName, CmbCodeRule.SelectedText, Util.Util.UserName))
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "NoAccess").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                    return;
                }
                #endregion
                #region 添加申请料号信息，提交审批和添加代办信息
                BLL.PLM.ApplyMaterial m_ApplyMaterial = new BLL.PLM.ApplyMaterial();
                #region 检查是否存在料号
                if (!txtMaterialId.Text.Contains("*"))
                {
                    DataTable dtApplyMaterial = m_ApplyMaterial.Select(txtMaterialId.Text);
                    if (dtApplyMaterial != null && dtApplyMaterial.Rows.Count > 0)
                    {
                        Alert.Show(GetGlobalResourceObject(strClassName, "ExistMaterial").ToString(), GetGlobalResourceObject("Language", "Warning").ToString(), MessageBoxIcon.Warning);
                        return;
                    }
                }
                #endregion
                if (m_ApplyMaterial.Add(applyMaterial, lstTipTopParameter, out string strMaterialId))
                {
                    #region 提交审批
                    if (m_Flow.StartFlow(strFlowName, CmbCodeRule.SelectedText, strMaterialId, Util.Util.UserName, out string strMessage))
                    {
                        BLL.HRM.Employee m_Employee = new BLL.HRM.Employee();
                        DataTable dtEmployee = m_Employee.Select(Util.Util.applicationOrg, strMessage.Split(','));
                        if (dtEmployee != null && dtEmployee.Rows.Count > 0)
                        {

                            BLL.PLM.WorkList m_WorkList = new BLL.PLM.WorkList();
                            m_WorkList.Update(strMaterialId, "1");

                            #region 添加审批流待办信息
                            List<Model.PLM.WorkList> lstWorkList = new List<Model.PLM.WorkList>();
                            string strAllEmail = "";
                            for (int i = 0; i < dtEmployee.Rows.Count; i++)
                            {
                                Model.PLM.WorkList workList = new Model.PLM.WorkList
                                {
                                    TableName = "PLM.dbo.ApplyMaterial",
                                    FormId = strMaterialId,
                                    SystemCode = Util.Util.SystemCode,
                                    Title = GetGlobalResourceObject(strClassName, "ApplyMaterial").ToString() + "(" + strMaterialId + ")",
                                    URL = "~/ApplyMaterial/ApproveMaterial.aspx",
                                    Approver = dtEmployee.Rows[i]["EmployeeID"].ToString(),
                                    FlowName = strFlowName,
                                    FlowGroup = intFlowGroup,
                                    Status = false,
                                    CreateUserID = Util.Util.UserName
                                };
                                lstWorkList.Add(workList);
                                strAllEmail += dtEmployee.Rows[i]["EmployeeEmail"].ToString() + ";";
                            }
                            if (m_WorkList.Add(lstWorkList))
                            {
                                if (dtEmployee.Rows.Count > 1)
                                    Util.MailSending.Send(strAllEmail, "", GetGlobalResourceObject(strClassName, "ApplyMaterial").ToString(), string.Format(GetGlobalResourceObject(strClassName, "EmailBody").ToString(), "All", Util.Util.webURL));
                                else if (dtEmployee.Rows.Count == 1)
                                    Util.MailSending.Send(strAllEmail, "", GetGlobalResourceObject(strClassName, "ApplyMaterial").ToString(), string.Format(GetGlobalResourceObject(strClassName, "EmailBody").ToString(), dtEmployee.Rows[0]["EmployeeName"].ToString(), Util.Util.webURL));
                            }
                            #endregion
                        }
                        #region 提交审批成功后，清空原有数据
                        LblMessage.Text = "  ("+ strMaterialId + ")   (" + txtSpecification.Text + ")" + GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString();
                        txtProductionName.Text = "";
                        txtSpecification.Text = "";
                        #endregion
                        //Alert.ShowInTop(GetGlobalResourceObject(strClassName, "SaveSuccessfully").ToString(), GetGlobalResourceObject("Language", "Information").ToString(), MessageBoxIcon.Information);
                    }
                    #endregion
                }
                else
                {
                    Alert.Show(GetGlobalResourceObject(strClassName, "SaveFailed").ToString(), GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
                }
                #endregion
            }
            catch (Exception ex)
            {
                BLL.Global.WriteLog(ex, this.ClientTarget, Util.LogType.Exception.ToString(), new System.Diagnostics.StackTrace(true).GetFrame(0).GetMethod().ToString(), Util.Util.UserName, Environment.MachineName, "Line:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileLineNumber().ToString() + " Column:" + new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileColumnNumber().ToString());
                Alert.ShowInTop(ex.Message, GetGlobalResourceObject("Language", "Error").ToString(), MessageBoxIcon.Error);
            }
        }
    }
}