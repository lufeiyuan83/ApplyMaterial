using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DALFactory
{
    public abstract class DALFactory
    {
        //private static DALFactory factory = null;

        public static DALFactory GetInstance()
        {
            return new DALFacade();
        }
        #region PLM
        public abstract IDAL.PLM.ILog GetLog();
        public abstract IDAL.PLM.IMapping GetMapping(); 
        public abstract IDAL.PLM.IApplyMaterial GetApplyMaterial(); 
        public abstract IDAL.PLM.ITipTopParameter GetTipTopParameter(); 
        public abstract IDAL.PLM.IFlow GetFlow();
        public abstract IDAL.PLM.IFlowInstance GetFlowInstance(); 
        public abstract IDAL.PLM.IWorkList GetWorkList(); 
        public abstract IDAL.PLM.IMaterial GetMaterial(); 
        public abstract IDAL.PLM.IInterfaceLog GetInterfaceLog();
        public abstract IDAL.PLM.IUI GetUI();
        #endregion
        #region PMS
        public abstract IDAL.PMS.IRole GetRole(); 
        public abstract IDAL.PMS.IElement GetElement();
        public abstract IDAL.PMS.IRoleRelation GetRoleRelation();
        #endregion
        #region Auth
        public abstract IDAL.Auth.ISystem GetSystem();
        public abstract IDAL.Auth.ISysMenu GetSysMenu(); 
        public abstract IDAL.Auth.ILogin GetLogin(); 
        public abstract IDAL.Auth.ICiphertext GetCiphertext();
        #endregion
        #region HRM
        public abstract IDAL.HRM.IDepartment GetDepartment();
        public abstract IDAL.HRM.IEmployee GetEmployee(); 
        public abstract IDAL.HRM.IOrganization GetOrganization(); 
        public abstract IDAL.HRM.IPosition GetPosition();
        #endregion
        #region CodeRule
        public abstract IDAL.CodeRule.ICodeRule GetCodeRule();
        public abstract IDAL.CodeRule.ICodeRuleParameter GetCodeRuleParameter();        
        #endregion
    }
}
