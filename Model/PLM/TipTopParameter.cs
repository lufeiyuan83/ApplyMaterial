using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.PLM
{
    public class TipTopParameter
    {
        public string MaterialId
        { get; set; }
        public string StartSN
        { get; set; }
        public string SN
        { get; set; }        
        public string SourceCode
        { get; set; }
        public string SourceCode2
        { get; set; }
        public string GroupCode
        { get; set; }
        public string FourthGroupCode
        { get; set; }
        public string ABCCode
        { get; set; }
        public string IsBonded
        { get; set; }
        public string MaterialClass
        { get; set; }
        public string MainUnit
        { get; set; }
        public string BuCode
        { get; set; }
        public string IsConsumed
        { get; set; }
        public string ReplenishmentStrategyCode
        { get; set; }
        public string IsSoftwareObject
        { get; set; }
        public string MultiLocation
        { get; set; }
        public string MPSMRPInventoryQty
        { get; set; }
        public string InavailableInventoryQty
        { get; set; }
        public string AvailableInventoryQty
        { get; set; }
        public string IsEngineeringMaterial
        { get; set; }
        public int LowCode
        { get; set; }
        public string UnitWeight
        { get; set; }
        public int CostLevel
        { get; set; }
        public int AveragePurchaseQty
        { get; set; }
        public int Ima110
        { get; set; }
        public string Ima137 
        { get; set; }
        public string SNContorol
        { get; set; }
        public string Ima146
        { get; set; }
        public string IsMassProductionMaterial
        { get; set; }
        public DateTime Ima901
        { get; set; }
        public string Ima908
        { get; set; }
        public bool IsPostSuccess
        { get; set; }
        public bool IsDeleted
        { get; set; }
        public string CreateUserID
        { get; set; }
        public DateTime CreateDateTime
        { get; set; }
        public string LastUpdateUserID
        { get; set; }
        public DateTime LastUpdateDateTime
        { get; set; }
        public string Remark
        { get; set; }
    }
}
