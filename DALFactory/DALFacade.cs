using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DALFactory
{
    public class DALFacade : DALFactory
    {
        #region PLM
        private IDAL.PLM.ILog m_Log = null;
        private IDAL.PLM.IMapping m_Mapping = null; 
        private IDAL.PLM.IApplyMaterial m_ApplyMaterial = null; 
        private IDAL.PLM.ITipTopParameter m_TipTopParameter = null; 
        private IDAL.PLM.IFlow m_Flow = null;
        private IDAL.PLM.IFlowInstance m_FlowInstance = null; 
        private IDAL.PLM.IWorkList m_WorkList = null; 
        private IDAL.PLM.IMaterial m_Material = null; 
        private IDAL.PLM.IInterfaceLog m_InterfaceLog = null;
        private IDAL.PLM.IUI m_UI = null;
        #endregion
        #region PMS
        private IDAL.PMS.IRole m_Role = null;
        private IDAL.PMS.IElement m_Element = null;
        private IDAL.PMS.IRoleRelation m_RoleRelation = null;
        #endregion
        #region Auth
        private IDAL.Auth.ISystem m_System = null;
        private IDAL.Auth.ISysMenu m_SysMenu = null;
        private IDAL.Auth.ILogin m_Login = null; 
        private IDAL.Auth.ICiphertext m_Ciphertext = null;
        #endregion
        #region HRM
        private IDAL.HRM.IDepartment m_Department = null;
        private IDAL.HRM.IEmployee m_Employee = null; 
        private IDAL.HRM.IOrganization m_Organization = null; 
        private IDAL.HRM.IPosition m_Position = null;
        #endregion
        #region CodeRule
        private IDAL.CodeRule.ICodeRule m_CodeRule = null;
        private IDAL.CodeRule.ICodeRuleParameter m_CodeRuleParameter = null;
        #endregion

        public DALFacade()
        {
            #region PLM
            m_Log = new DAL.PLM.Log();
            m_Mapping = new DAL.PLM.Mapping();
            m_ApplyMaterial = new DAL.PLM.ApplyMaterial();
            m_TipTopParameter = new DAL.PLM.TipTopParameter();
            m_Flow = new DAL.PLM.Flow();
            m_FlowInstance = new DAL.PLM.FlowInstance();
            m_WorkList = new DAL.PLM.WorkList();
            m_Material = new DAL.PLM.Material();
            m_InterfaceLog = new DAL.PLM.InterfaceLog();
            m_UI = new DAL.PLM.UI();
            #endregion        
            #region PMS
            m_Role = new DAL.PMS.Role();
            m_Element = new DAL.PMS.Element();
            m_RoleRelation = new DAL.PMS.RoleRelation();
            #endregion
            #region Auth
            m_System = new DAL.Auth.System();
            m_SysMenu = new DAL.Auth.SysMenu();
            m_Login = new DAL.Auth.Login();
            m_Ciphertext = new DAL.Auth.Ciphertext();
            #endregion
            #region HRM
            m_Department = new DAL.HRM.Department();
            m_Employee = new DAL.HRM.Employee(); 
            m_Organization = new DAL.HRM.Organization();
            m_Position = new DAL.HRM.Position();
            #endregion
            #region CodeRule
            m_CodeRule = new DAL.CodeRule.CodeRule();
            m_CodeRuleParameter = new DAL.CodeRule.CodeRuleParameter();
            #endregion
        }
        #region PLM
        public override IDAL.PLM.ILog GetLog()
        {
            return m_Log;
        }
        public override IDAL.PLM.IMapping GetMapping()
        {
            return m_Mapping;
        }        
        public override IDAL.PLM.IApplyMaterial GetApplyMaterial()
        {
            return m_ApplyMaterial;
        }
        public override IDAL.PLM.ITipTopParameter GetTipTopParameter()
        {
            return m_TipTopParameter;
        }
        public override IDAL.PLM.IFlow GetFlow()
        {
            return m_Flow;
        }
        public override IDAL.PLM.IFlowInstance GetFlowInstance()
        {
            return m_FlowInstance;
        }        
        public override IDAL.PLM.IWorkList GetWorkList()
        {
            return m_WorkList;
        }        
        public override IDAL.PLM.IMaterial GetMaterial()
        {
            return m_Material;
        }
        public override IDAL.PLM.IInterfaceLog GetInterfaceLog()
        {
            return m_InterfaceLog;
        }
        public override IDAL.PLM.IUI GetUI()
        {
            return m_UI;
        }
        #endregion
        #region Auth
        public override IDAL.PMS.IRole GetRole()
        {
            return m_Role;
        }        
        public override IDAL.PMS.IElement GetElement()
        {
            return m_Element;
        }
        public override IDAL.PMS.IRoleRelation GetRoleRelation()
        {
            return m_RoleRelation;
        }
        #endregion
        #region Auth
        public override IDAL.Auth.ISystem GetSystem()
        {
            return m_System;
        }
        public override IDAL.Auth.ISysMenu GetSysMenu()
        {
            return m_SysMenu;
        }
        public override IDAL.Auth.ILogin GetLogin()
        {
            return m_Login;
        }
        public override IDAL.Auth.ICiphertext GetCiphertext()
        {
            return m_Ciphertext;
        }
        #endregion
        #region HRM        
        public override IDAL.HRM.IDepartment GetDepartment()
        {
            return m_Department;
        }
        public override IDAL.HRM.IEmployee GetEmployee()
        {
            return m_Employee;
        }
        public override IDAL.HRM.IOrganization GetOrganization()
        {
            return m_Organization;
        }
        public override IDAL.HRM.IPosition GetPosition()
        {
            return m_Position;
        }        
        #endregion
        #region CodeRule
        public override IDAL.CodeRule.ICodeRule GetCodeRule()
        {
            return m_CodeRule;
        }
        public override IDAL.CodeRule.ICodeRuleParameter GetCodeRuleParameter()
        {
            return m_CodeRuleParameter;
        }
        #endregion
    }
}
