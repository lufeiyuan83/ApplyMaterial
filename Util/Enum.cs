using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public enum ElementType
    {
        System,
        Module,
        Form,
        Control
    }
    public enum PurchaseTaskStatus
    {
        接單,
        資源開發
    }
    public enum InspectItem
    {
        Size,
        Appearance
    }
    public enum STProduction
    {
        MaterialCodeRule,
        TopClass,
        MiddleClass,
        Brand,
        SN,
        MaterialID,
        ProductName,
        Specification,
        Unit,
        SourceCode,
        ScopeCode,
        SourceCode4
    }
    public enum PrecisionMetal
    {
        MaterialCodeRule,
        TopClass,
        CustomerName,
        ProjectName,
        Procedure,
        ProductionID,
        MaterialID,
        ProductName,
        Specification,
        Unit,
        SourceCode,
        ScopeCode,
        SourceCode4
    }
    public enum SkilledMatrixRank
    {
        培训中,
        新上岗,
        熟练操作,
        师傅级
    }
    public enum TestProgramTestStage
    {
        M1_P_MBUILD,
        M3_P_CUTTER,
        M3_P_CUTTER_B,
        M3_P_TERMIN,
        M3_P_PLHCT,
        M3_P_LENSPLH
    }
    public enum M3_P_TERMINLocation
    { 
        OVEN_01,
        OVEN_02,
        OVEN_03,
        OVEN_04,
        OVEN_05,
        OVEN_06,
        OVEN_07,
        OVEN_08,
        OVEN_09,
        OVEN_10,
        OVEN_11,
        OVEN_12,
        OVEN_TC_01,
        PLATE_01,
        PLATE_02,
        PLATE_03,
        SLOT_01,
        SLOT_02,
        SLOT_03,
        SLOT_04
    }
    public enum CollectDataConfig
    {
        M1_P_MBUILD,
    }
    public enum SamplingType
    {
        Normal=0,//正常抽样
        Reduce=1,//减量抽样
        More=2//加严抽样
    }
    public enum QACheckResult
    {
        Fail=0,
        Pass=1,
        Checking=2
    }
    public enum Account
    {
        Agile,
        ME,
        HW
    }
    public enum AssetRepairResult
    {
        维修成功,
        维修失败
    }
    public enum AssetRepairType
    {
        内部维修,
        外部维修
    }
    public enum Voucher
    {
        库存转移单据 = 0,
        辅料单据 = 1,
        入库单 = 2,
        工程单据=3,
        成品隔离单据 = 4,
        原材料隔离单据 = 5,
        成品解隔离单据 = 6,
        原材料解隔离单据 = 7,
        Q71RCT = 8,
        退库单据 = 9,
        库存实物报废单据 = 10,
        生产直投发料单据 = 11,
        RWK库存转移单据 = 12,
        VMI库存转移单据 = 13,
        RTV=14,
        QCT=15,
        產品出貨檢驗報告=16
    }
    public enum CommissionStatus
    {
        Fail=0,
        Pass=1
    }
    public enum AssetStatus
    {
        ALL=0,
        资产编号审批中 = 1,
        采购中 = 2,
        在库 = 3,
        资产已发放 = 4,
        资产登记审批中 = 5,
        资产已登记 = 6,

        正常使用 = 10,
        维修中 = 11,
        维修完成 = 12,
        校准中 = 13,
        校准完成 = 14,
        报废 = 15,
        遗失 = 16,

        申请采购中 = 17,
        申请报废中=18,
        申请维修中=19,
        申请校准中 = 20,
        申请遗失中 = 21,
        申请转移中 = 22,
        调拨中=23,
        待校准=24,
        待维修=25,
        维修审批完成 = 26,
        资产维修返回 = 27,
        校准审批完成 = 28,
        资产校准返回 = 29,
        调拨审批完成 = 30,
        转移审批完成 = 31,
        报废审批完成 = 32
    }
    public enum GroupResultType
    {
        All=0,
        Any=1
    }
    public enum ViewOrApprove
    {
        Approve=0,
        View=1
    }
    public enum ApproveFlow
    {
        ApplyAssetNo = 0,//固定资产编号申请
        AssetRegister = 1, // 固定资产登记
        ApplyGoodsPGI = 2,// 成品出货出门证
        ApplyAssetTransfer = 3,//固定资产转移
        ApplyCancellingStocks = 4,//退库单
        ApplyRWKStockTransfer = 5,//RWK退库
        ApplyVMIStockTransfer = 6,//VMI库存转移
        ApplyAccessory = 7,//辅料单据
        ApplyInventoryPhysicalScrap = 8,//库存实物报废
        ApplyProject = 9,//工程单据
        ApplyFGQCTQuarantine = 10,//成品隔离单据
        ApplyFGQCTDesequestration = 11,//成品解隔离单据
        ApplyRawMaterialQCTQuarantine = 12,//原材料隔离单据
        ApplyRawMaterialQCTDesequestration = 13,//原材料解隔离单据
        ApplyDirectDisbursement = 14, // 生产直投发料单据
        ApplyQ71RCT=15,//Q71RCT
        ApplyRTV = 16,//RTV
        ApplyAssetAllot = 17,//申请资产调拨
        ApplyAssetScrap = 18,//申请资产报废
        ApplyAssetRepairForInternal = 19,//申请资产内部维修
        ApplyAssetRepairForExternal = 20,//申请资产外部维修
        ApplyAssetCalibrate = 21,//申请资产校准
        ApplyAssetLoss = 22,//申请资产遗失
        ApplyQCT=23,
        FillTrackingNumberRTVCreditNote = 24,
        FillTrackingNumberReplacements=25,
        ApplyMaterialID =26
    }
    public enum SingleVariable
    {
        GenericCode=0,
        Open=1,
        OpenSQL=2
    }
    /// <summary>
    /// 物资出门审批流程
    /// </summary>
    public enum ApproveProcess
    {
        GoodsPGI = 0//物资出门审批流程
    }
    public enum SkillLevel
    {
        Traniee=1,
        Competent=2,
        Skilled=3
    }
    public enum SkillAssessmentType
    {
        Certification = 0,
        Recertification=1,
        New=2
    }
    public enum Database
    {
        CenterSix_Pack140108=0,
        FIS=1,
        PMS=2
    }
    public enum AttendenceSite
    {
        SZ=0
    }
    public enum Location
    {
        SFGS=0,
        FGOB=1,
        LEPI=2,
        SCRS=3,
        SRMS=4,
        SFTA=5
    }
    public enum PickListStatus
    {
        New_Imported=0,
        SN_Checking_Passed=1,
        Invoice_Created=2
    }
    public enum MELabelType
    {
        PACKINGCELL=0,
        STARTLOT=1,
        FIBER=2
    }
    /// <summary>
    /// 用于SA的状态
    /// </summary>
    public enum DeviceStatus
    {
        StockIn = 42,
        Rework = 41,
        OnWay = 43,
        Error = 101
    }
    /// <summary>
    /// SN出入库时的状态
    /// </summary>
    public enum SNStatusCode
    {
        Success = 60,
        StockIn = 61,
        SNInvalid = 62,
        SNInProcess = 63,
        ONWay = 64,
        UnKownError = 65,
        NoPartId = 66,
        NotStockIn = 67,
        SNRework = 68,
        SNSample = 69,
        MEIsNotFinished=101
    }
    /// <summary>
    /// 检查后的状态
    /// </summary>
    public enum CheckResult
    {
        Pass=0,
        Fail=1
    }
    /// <summary>
    /// 借单的状态
    /// </summary>
    public enum BorrowBillStatus
    {
        Open=0,
        Closed = 1
    }
    /// <summary>
    /// SA中出入库的SN错误代码
    /// </summary>
    public enum StatusCodes
    {
        Success=0,
        ProducedGoods=1,//以前出货过
        NoPassForfinalrel_finalrelease_release = 2,//finalrel/finalrelease/release没有Pass记录
        MoreCorp_serno = 3,//Corp_serno太多
        MEIsNoFinished_PackcellIssue = 4,//ME未完成，或者Packcell问题
        Quarantine=5,//隔离产品
        NoCorp_serno = 6,//没有Corp_serno
        OnlyToTheCustomer =7,//出货给指定客户
        Location = 8,//Location不能为空
        LEPINotShipToSoldID=9,//LEPI模组不能出给该Sold-To
        LEPI = 10,//LEPI模组
        NoTrayNumber = 11,//找不到Tray Number
        NoConfig = 12,//没有Config
        NoLiveLink_Plot=13,//没有LiveLink或者Plot文件
        NetWorkIssue=14,
        PNDifferent=15,//ME和PCAS中的PN不同
        NoFoundPNInME=16,//在ME中找不到PN
        NoTest_link = 17,//在PCAS中未找到Test_link信息
        Test_StatusIsNotPass = 18,//在PCAS中该SN的Test_Link中的Test_Status状态不是PASS
        NoEDTConfiguration = 19,//该客户的EDT未配置
        DNIsNotMatchedPN = 20,//DN和PN不匹配
        NotForTheCustomer = 21,//不能出货给改客户
        BlankPN = 22,//空PN
        BlankSN=23,//空SN
        DifferentPN =24,//PN不一样
        MorePN=25,//在ME中有多个PN        
        NoTraveler = 26,//没有Traveler文件异常
        CheckFailureForPermit_ECO=27,//检查Permit/ECO不通过
        InvGTOneYear=28,//在库时间大于一年
        Exception=29//其他异常
        //Success = 10, 
        //Error1 = 11, 
        //Error2 = 12, 
        //Error3 = 13, 
        //Error4 = 14, 
        //Error5 = 15,
        //Error6 = 16, 
        //Error7 = 17, 
        //Error8 = 18, 
        //Error9 = 19, 
        //Error10 = 25, 
        //Error11 = 56, 
        //Error12 = 57, 
        //Error61 = 61, 
        //Error62 = 62, 
        //Error63 = 63, 
        //Error64 = 64, 
        //Error65 = 65
    }
    /// <summary>
    /// 调查表
    /// </summary>
    public enum Survey
    {
        Gallup=0
    }
    /// <summary>
    /// 审批类型
    /// </summary>
    public enum ApproveType
    {
        ReissueApprovalProcess = 0,
        OTApprovalProcess = 1,
        HolidayApprovalProcess = 2,
        ProductionOperator = 3,
        SalaryAmendment = 4,
        ShuttleLate = 5,
        SkillAssessmentNew =6,
        SkillAssessmentRecertification = 7,
        SkillAssessmentUpgrade=8,
        SkillAssessmentDegrade=9
    }
    /// <summary>
    /// 发送FIS通知时的类型
    /// </summary>
    public enum Notcie
    {
        Link=0,
        Picture=1,
        Table=3,
        Text=4
    }
    /// <summary>
    /// PM的方法
    /// </summary>
    public enum PerformanceAppraisalMethod
    {
        ProductionOperator = 0,
        QAOperator = 1,
        HourlyEmployeeOthers =2,
        SalariedEmployee =3
    }
    /// <summary>
    /// 补卡原因
    /// </summary>
    public enum ReissueReason
    {
        工卡遗失=0,
        工卡损坏=1,
        忘带工卡=2,
        忘记打卡=3
    }
    /// <summary>
    /// 使用期的时间
    /// </summary>
    public enum TrialPeriod
    {
        DLTrialPeriodMonth=0,
        IDLTrialPeriodMonth=1,
        TrialPeriodNoticeDay=2
    }
    /// <summary>
    /// 调查表中问题的类型
    /// </summary>
    public enum QuestionType
    {
        Radio = 0,
        Multiple = 1,
    }
    /// <summary>
    /// 特殊的审批人
    /// </summary>
    public enum Approver
    {        
        HRManager=0,
        FactoryDirector = 1,
        AttendanceAdministrator=2,
        JumpDL1OTApprover=3,
        ProductionDirector=4,
        HRPMApprovor=5,
        WareHouseManager=6,
        WareHouseLeader=7,
        FinanceManager=8,
        EngineeringManager=9,
        ImportExpManager=10,
        SiteLeader=11,
        OperatorSkillConfirm=12,
        Commission=13,
        WarehouseConfirm=14,
        AssetAdministrator=15
    }
    /// <summary>
    /// 审批结果
    /// </summary>
    public enum ApproveResult
    {
        Approved=0,
        Reject=1,
        Recall=2,
        NeedConfirm=3,
        OutDocumentNumber=4,
        ExpressNumber=5,
        TrackingNumber=6,
        InDocumentNumber=7,
        FINumber=8
    }
    public enum WorkScheduleType
    {
        A=0,
        B=1,
        C=2,
        IDL=3,
        Z=4,
        E=5,
        F=6,
        FC=7,
        FA=8,
        FB=9,
        FZ=10,
        Special=11
    }
    public enum ShiftsType
    {
        A=0,
        B=1,
        C=2,
        Z=3,
        休假=4,
        IDAL=5
    }
    /// <summary>
    /// 员工类型
    /// </summary>
    public enum DL_IDL
    {
        DL1=0,
        DL2=1,
        IDL=2,
        DL=3
    }
    /// <summary>
    /// 加班类型
    /// </summary>
    public enum OTType
    {
        Normal=0,
        Night=1,
        Weekend = 2,
        LegalHoliday = 3,
        Special=4
    }
    /// <summary>
    /// 
    /// </summary>
    public enum SendType
    {
        Message = 0,
        Name = 1
    }
    /// <summary>
    /// 工单状态
    /// </summary>
    public enum WorkOrderStatus
    {
        Fail=0,
        Pass=1,
        Finished=2,
        OK=3,
        InWareHouse=4,
        New=5,
        Shortage=6,
        Rework=8,
        Hold=9,
        TQC=10,
        QA=11,
        GR=12,
        Closed=13
    }
    /// <summary>
    /// 可选的工单状态， 用于查询工单时
    /// </summary>
    public enum OptionWorkOrderStatus
    {
        OK = 0,
        New = 1,
        Shortage = 2,
        Rework = 3,
        Hold = 4
    }
    /// <summary>
    /// 已经完成的工单状态,非在线的状态
    /// </summary>
    public enum OptionDoneWorkOrderStatus
    {
        TQC = 0,
        QA = 1,
        GR = 2,
        Closed = 3,
        Finished=4
    }
    public enum Op
    {
        EQ = 1,
        NE = 0
    }
    public enum Type
    {
        SN = 0,
        LotCode = 1,
        LotCodeAndSN = 2
    }
    public enum Lock
    {
        Lock = 1,
        UnLock = 0
    }
    public enum Select
    {
        Select = 1,
        UnSelect = 0
    }
    public enum TestResult
    {
        Pass = 1,
        Fail = 0,
        NoTest = -1
    }
    public enum StartOrPause
    {
        Start = 1,
        Pause = 0
    }
    public enum FormName
    {
        Top = 0,
        CCForUnit = 1,
        FA = 2,
        OrderStatus = 3,
        CriticalComponent = 4,
        Assembly = 5,
        ProductiionFlow = 6,
        Replenishment = 7,
        ToWarehouse = 8,
        EditDictionary = 9,
        ComponentList = 10,
        Kitting = 11,
        TQCTest = 12,
        Test = 13,
        Package = 14,
        QRCode = 15,
        PDF = 16,
        FinalTest = 17,
        Employee = 18,
        GetDataFromSAP = 19,
        BurnIn = 20,
        Mapping=21,
        Chip = 22,
        About = 23,
        Authority=24,
        ChangePassword=25,
        SAPConfirm=26,
        UnlockAndReset=27,
        Account=28,
        WOMaintain=29,
        Fixture_FET = 30,
        Chat=31,
        Report8705143=32,
        ReportResis = 33,
        Fixture_808=34,
        Configuration=35,
        Send=36,
        In=37,
        Out=38,
        Scrap=39,
        Check=40,
        Adjust=41,
        Chemical=42,
        ChemicalInventory=43,
        ChemicalUsage=44,
        ChemicalMasterSchedule = 45,
        NoticeMaintain=46,
        Intermission= 47,
        TrainingCard=48,
        WorkLicense=49,
        Certifier=50,
        BatchCopyAuthority=51,
        AVL=52,
        QA=53,
        GR=54,
        TQC=55,
        IPQA = 56,
        OBA=57,
        Shortage=58,
        NoEntryWO=59,
        UsingAccount = 60,
        BulkConfig=61,
        CheckWithEachOther=62,
        CCConfig=63,
        StatusSearch = 64,
        BOM=65,
        Training=66,
        BurnCD=67,
        AnnualTrainingPlan=68,
        UpdateComponentList=69,
        ProductionPlan=70,
        FinalTQCCheckList=71,
        RepetitiveBOM=72,
        ChangeWORev=73,
        DataMaintenance=74,
        WorkSchedule=75,
        IIVIEmail=76,
        AssetIn=77,
        AssetOut=78,
        AssetScrap=79,
        AssetCheck=80,
        AssetInfo=81,
        Owner=82,
        StaticIP=83,
        ATSFormat=84,
        CardReissue=85,
        Scheduling = 86,
        LeaderOperator=87,
        OverTime=88,
        Holiday=89,
        Leave=90,
        IDL=91,
        DateScheduleType = 92,
        ApplyHoliday = 93,
        ApproveHoliday = 94,
        SearchHoliday = 95,
        ControlHoliday = 96,
        ApplyOverTime = 97,
        ApproveOverTime = 98,
        SearchOverTime = 99,
        ControlOverTime = 100,
        ApplyResign = 101,
        ApproveResign = 102,
        SearchResign = 103,
        ControlResign = 104,
        ICCard=105,
        MealRecord=106,
        MealReport=107,
        Change=108,
        Exchange=109,
        QSetting=110,
        CheckException=111,
        ApplyReissue = 112,
        ApproveReissue = 113,
        SearchReissue = 114,
        QInput=115,
        QCheck=116,
        QAnswer=117,
        QReport=118,
        OTData=119,
        HolidayData=120,
        ReissueData=121,
        CheckCrossDateTime=122,
        IDLOTReplenishment=123,
        ReportScheduling=124,
        CheckHoliday=125,
        ReadID=126,
        SendEmail=127,
        OverTimeSurvey=128,
        PMDL1=129,
        ApplySalaryAmendment=130,
        ProductionOperator = 131,
        QAOperator = 132,
        HourlyEmployeeOthers =133,
        SalariedEmployee =134,
        MaxOTHours=135,
        ApprovePM = 136,
        SearchPM=137,
        Shipment=138,
        Gallup=139,
        ApproveSalaryAmendment=140,
        LNSDSearch=141,
        LNSDCheck=142,
        LNSD_PN_Maintain=143,
        EDTCheckParameter=144,
        ShippedSN=145,
        OTStatistics=146,
        SNScanning=147,
        BorrowBill=148,
        ReturnBill=149,
        OperatorSkillSearch=150,
        ApproveScrapBill=151,
        TrialPeriodNotice=152,
        AD=153,
        IncomingInspection=154,
        Packing=155,
        SearchScrapBill=156,
        SALabelPrint=157,
        ProductsSNCheck=158,
        GRTicketCreate=159,
        PackListCreate=160,
        MaterialRequisition=161,
        ScrapPNMaintain=162,
        ReturnBillStatusMaintain=163,
        TicketPrint=164,
        InvoicePrint=165,
        Quarantine=166,
        TicketApprove=167,
        MobileMaintain=168,
        EmployeeHistory=169,
        InventoryAging=170,
        VirtualPackCell=171,
        Recode=172,
        GRTicketSN=173,
        HC=174,
        EmployeeSearch=175,
        ProcessListAccess = 176,
        GetPN=177,
        UploadSNToSAP=178,
        PickListInfo=179,
        StoreIssue = 180,
        FGMovementBill=181,
        MasterData=182,
        PrintSN=183,
        LoadEDT=184,
        LNSQty=185,
        InboundEDT=186,
        SearchSalaryAmendment=187,
        ApplyShuttleLate=188,
        ApproveShuttleLate=189,
        SearchShuttleLate=190,
        HPLLabel=191,
        MealIDLabel=192,
        InvoiceQuery=193,
        StoreIssueSN=194,
        CustomConfigurationReport=195,
        OrganizationStructure=196,
        TurnoverRate=197,
        CustomizableReport=198,
        MaterialMatch=199,
        MaterialMatchRecord=200,
        LoadPGIData=201,
        MELog=202,
        PGIData=203,
        CollectData = 204,
        FTALabel=205,
        EICC=206,
        ProcessList=207,
        SkillConfirm=208,
        SkillAssessment=209,
        SACustomerInfo=210,
        FilterParameterForRecode=211,
        BuildConfigure=212,
        CheckME=213,
        Packcell=214,
        StoreRequisition=215,
        RawMaterialBillReport=216,
        StoreRecordingPN = 217,
        BackToWarehouse=218,
        ScrapRecord=219,
        EngineeredStoreRequisition=220,
        QuarantineRecord=221,
        ReworkFiberBackToWarehouse=222,
        Dormitory=223,
        Subsidy=224,
        FeeForDormitory=225,
        ApplyGoodsPGI = 226,
        ApproveGoodsPGI = 227,
        SearchGoodsPGI = 228,
        ReturnUnit=229,
        Coating=230,
        SpliceRecording = 231,
        DefectiveProductsFindProblem = 232,
        DefectiveProductsConfirm = 233,
        DefectiveProductsFTAOrLid = 234,
        DefectiveProductsSearch = 235,
        StoreRecordingSN=236,
        ShuttleReport=237,
        UploadCienaEDT=238,
        ApplyQCT = 239,
        ApproveQCT = 240,
        ConfirmQCT = 241,
        SearchQCT = 242,
        WIP=243,
        TPT=244,
        RGARecord = 245,
        RGADelivery = 246,
        RGAReturn = 247,
        ResetMEPassword=248,
        ApplyAssetNo=249,
        ApproveAssetNo=250,
        SearchAssetNo=251,
        ConfigApproveFlow=252,
        ConfigNode=253,
        FillDeclarationNumber=254,
        AssetRegister =255,
        ApproveAssetRegister =256,
        PrintAssetLabel =257,
        DistributionAndRecipients =258,
        Commission =259,
        SearchAssetRegister =260,
        ATSConfig =261,
        AssetMasterData =262,
        CustomerAsset =263,
        SpecificationView =264,
        SpecificationMaintain = 265,
        SpecificationCreate =266,
        SpecificationEdit =267,
        SpecificationRelease =268,
        SpecificationLinks =269,
        PumpEDT=270,
        DeclarationNumberModify =271,
        AssetRegisterModify =272,
        RGASearch =273,
        PackcellLog =274,
        ApplyAssetAllot =275,
        ApproveAssetAllot =276,
        SearchAssetAllot =277,
        ApplyAssetTransfer =278,
        ApproveAssetTransfer =279,
        SearchAssetTransfer =280,
        ApplyStockTransfer=281,
        ConfirmStockTransfer=282,
        SearchtsmiStockTransfer =283,
        ApplyWarehousingEntry=284,
        ConfirmWarehousingEntry=285,
        SearchWarehousingEntry=286,
        ApplyCancellingStocks=287,
        ApproveCancellingStocks=288,
        ConfirmCancellingStocks=289,
        SearchCancellingStocks=290,
        ApplyRWKStockTransfer=291,
        ApproveRWKStockTransfer	=292,
        ConfirmRWKStockTransfer=293,
        SearchRWKStockTransfer=294,
        ApplyVMIStockTransfer=295,
        ApproveVMIStockTransfer=296,
        ConfirmVMIStockTransfer=297,
        SearchVMIStockTransfer=298,
        ApplyDirectDisbursement=299,
        ApproveDirectDisbursement=300,
        ConfirmDirectDisbursement=301,
        SearchDirectDisbursement=302,
        ApplyAccessory=303,
        ApproveAccessory=304,
        ConfirmAccessory=305,
        SearchAccessory=306,
        ApplyInventoryPhysicalScrap=307,
        ApproveInventoryPhysicalScrap=308,
        ConfirmInventoryPhysicalScrap=309,
        SearchInventoryPhysicalScrap=310,
        ApplyProject=311,
        ApproveProject =312,
        ConfirmProject =313,
        SearchProject=314,
        ApplyQ71RCT=315,
        ApproveQ71RCT=316,
        ConfirmQ71RCT=317,
        SearchQ71RCT=318,
        ApplyRTV=319,
        ApproveRTV=320,
        FillTrackingNumberRTV=321,
        SearchRTV=322,
        VoucherPrint=323,
        EDTConfigurationViewer=324,
        EDTStatus=325,
        ApplyFGQCTQuarantine = 326,
        ApproveFGQCTQuarantine = 327,
        ConfirmFGQCTQuarantine = 328,
        SearchFGQCTQuarantine = 329,
        ApplyFGQCTDesequestration = 330,
        ApproveFGQCTDesequestration = 331,
        ConfirmFGQCTDesequestration = 332,
        SearchFGQCTDesequestration = 333,
        ApplyRawMaterialQCTQuarantine = 334,
        ApproveRawMaterialQCTQuarantine = 335,
        ConfirmRawMaterialQCTQuarantine = 336,
        SearchRawMaterialQCTQuarantine = 337,
        ApplyRawMaterialQCTDesequestration = 338,
        ApproveRawMaterialQCTDesequestration = 339,
        ConfirmRawMaterialQCTDesequestration = 340,
        SearchRawMaterialQCTDesequestration = 341,
        VoucherConfirm=342,
        VoucherSearch=343,
        KnowledgeBaseBuild=344,
        KnowledgeBaseView=345,
        ApplyAssetScrap=346,
        ApproveAssetScrap=347,
        SearchAssetScrap=348,
        Material=349,
        ApplyAssetRepair=350,
        ApproveAssetRepair=351,
        WaitingAssetRepair = 352,
        ConfirmAssetRepair = 353,
        SearchAssetRepair=354,
        ApplyAssetCalibrate=355,
        ApproveAssetCalibrate=356,
        WaitingAssetCalibrate=357,
        ConfirmAssetCalibrate=358,
        SearchAssetCalibrate=359,
        ApplyAssetLoss=360,
        ApproveAssetLoss=361,
        SearchAssetLoss=362,
        ReceiveAssetAllot=363,
        ReceiveAssetTransfer=364,
        ReceiveAssetScrap=365,
        ReceiveAssetRepair=366,
        ReceiveAssetCalibrate=367,
        LabelConfig=368,
        PO=369,
        GRInfo=370,
        CustomPrintLabel=371,
        PCASDaemonLog=372,
        QACheck=373,
        SamplingPlan=374,
        SamplingReport=375,
        SamplingSpecification=376,
        IQCInspectRTV=377,
        ASNContrast=378,
        UploadSNToHW=379,
        ApplyEDTConfig=380,
        PrintCancellingStockLabel=381,
        Permit=382,
        COCLabel=383,
        RecordCOCVisualInspection=384,
        EngineerCOCVIConfirm=385,
        QCCOCVIConfirm=386,
        SearchCOCVisualInspection=387,
        EDTConfigure=388,
        FTAPNConfigure=389,
        EngineeringSample=390,
        ModifyEDTConfigure=391,
        MaterialInformationPush=392,
        MaterialInformationCollect=393,
        MaterialAction = 394,
        MaterialMRB = 395,
        LoadSupplierTestData=396,
        Specifications=397,
        LoadInternalTestData=398,
        DeleteMEAccount=399,
        FTASFCLabel=400,
        GrapeChart=401,
        PerformanceData=402,
        PerformanceConfirm=403,
        MonthPerformanace=404,
        WorkshopAccess=405,
        POGR=406,
        NewInboundEDT=407,
        SpecificationsLink=408,
        QuarterPerformance=409,
        SkilledMatrixRankData=410,
        StageSkilledMatrixData=411,
        BatchOperateME=412,
        BurnInTimeConfig = 413,
        BurnInSN=414,
        BurnInReport=415,
        MESPNType=416,
        MESWIP=417,
        MESOutput= 418,
        MESDefectiveProducts = 419,
        MESDailyReport=420,
        FGEntryReport = 421,
        PlanEntryData=422,
        MESWIPReport=423,
        CheckSFCStatus=424,
        STProduction = 425,
        MaterialIDApporve=426,
        SearchMaterialID=427,
        PrecisionMetal=428,
        LabelPrint=429,
        ProductionWIP=430,
        MaterialInfo=431,
        FQCCheckRecord=432,
        FQCCheckReport = 433,
        WorkFlow=434,
        WFModule = 435,
        WFNodeCondition=436,
        FlowNode=437,
        Supploer,
        Customer,
        SupplierEvaluation,
        SpecailContract,
        RDTaskAllocate,
        RDResourceDevelopmentRule,
        RDResourceDevelopment,
        RDBiddingBargaining,
        RDSendSample,
        RDResourceDevelopmentWorkFlow,
        RDResourceDevelopmentSearch
    }
    public enum ShowContent
    {
        Kitting = 0,
        Assembly = 1,
        Test = 2,
        TQCTest = 3,
        FinalTest = 4,
        FA = 5,
        Package = 6,
        BurnIn = 7
    }
    public enum LogType
    {
        Exception = 0,
        Information = 1,
        SystemVersion=2,
        Holiday=3,
        OverTime=4
    }
    public enum Sex
    {
        Male = 1,
        Female = 0
    }
    public enum Position
    {
        TQC = 0,
        PE = 1,
        QE = 2,
        TE = 3,
        OPERATOR = 4,
        LINELEADER =5
    }
    public enum MappingType
    {
        SAPShowContent = 0,
        FISShowContent = 1,
        PN_OMS = 2,
        PN_PG_PL_PS = 3,
        ProductionStructure = 4,
        ProductionGroup = 5,
        ProductLine = 6,
        ProductionSeries = 7,
        Department = 8,
        FISAccess = 9,
        NeedTestCC = 11,
        Plant = 12,
        Position = 13,
        ShowContent = 14,
        WorkOrderStatus = 16,
        OptionWorkOrderStatus = 17,
        TestStep = 18,
        Operation = 19,
        Unit = 20,
        FAStatus = 21,
        ReplenishType = 22,
        NeedTQCTest = 24,
        NoticeType=25,
        OMSClass=26,
        Chat=27,
        OEEClass=28,
        ProductionGroup_WorkCenter_Type=29,
        OEEGetDataDateInterval=30,
        ProductionGroup_WorkCenter_Rack = 31,
        CriticalComponent=32,
        CertificationType = 33,
        CertificationDate=34,
        MachineTimeChecker=35,
        CertificationCheckList=36,
        FAFailureMode=37,
        Module=38,
        InspectionType=39,
        IPQAFailCode =40,
        OBAFailCode = 41,
        FISVersion = 42,
        OptionDoneWorkOrderStatus=43,
        MRPController = 44,
        SpotTest=45,
        Parallel=46,
        WorkOrderSubStatus=47,
        ShortageExceptStorageLocation=48,
        SpotTestUserID=49,
        CertificationSubmiting=50,
        PackingMaterial=51,
        TrainingType=52,
        CourseCategory=53,
        CDPartNumberPrefix=54,
        BurnCDPrinterName=55,
        FINIALTQCCheckList=56,
        WorkSchedule=57,
        AssetClass=58,
        StaticIPType=59,
        AssetStatus=60,
        IIVIDepartment=61,
        Department_CostCenter=62,
        WorkSeparator=63,
        WorkEmployeeClass=64,
        LeaderOperator=65,
        Leader=66,
        OTType=67,
        HolidayType = 68,
        BreastFeed=69,
        LeaveType=70,
        DL_IDL=71,
        IDL=72,
        SchedulingDeadline = 73,
        OTMINTime=74,
        OTBonusHours=75,
        WorkScheduleType=76,
        InOutRangeMinutes=77,
        InOutRangeHours=78,
        OTControlType=79,
        ApprovalProcess=80,
        OTApprovalProcess=81,
        HolidayApprovalProcess=82,
        ResignApprovalProcess = 83,
        Approver=84,
        DepartmentManager = 85,
        ExaminationType=86,
        ExaminationDocument=87,
        ReissueApprovalProcess=88,
        Shuttle=89,
        TrialPeriod=90,
        DLMonthMaxOTHours = 91,
        IDLMonthMaxOTHours = 92,
        SurplusAdjustableVacationHours=93,
        IDLMaxAdjustableVacationHours=94,
        MealTime=95,
        ReissueReason=96,
        CloseDownDateTime=97,
        National=98,
        Degree=99,
        MarriageStatus=100,
        LeavingCategory=101,
        ID_Address=102,
        NoExceptionNotice=103,
        HolidayMaintaince=104,
        OverTimeSurveyValidity=105,
        ProductionOperatorPerformanceLevel = 106,
        PerformanceAppraisalMethod=107,
        PMCloseDownDateTime=108,
        SystemMaintaince=109,
        SalaryAmendmentReason=110,
        SalaryAmendmentItem=111,
        GallupQuestion=112,
        LNSD_Department=113,
        LNSD_PN=114,
        TrainingDays=115,
        ScrapReturnItem=116,
        ScrapReturnResult = 117,
        Scrap_PN=118,
        ScrapBorrowBill=119,
        ScrapReturnBill=120,
        IncomingInspectionPN=121,
        ValidationFail=122,
        NoCOCData = 123,
        LiveLinkServerName = 124,
        LiveLinkArchivePath = 125,
        EDTNoticeForSA = 126,
        SignatoryForSA = 127,
        Deadline=128,
        InvoiceLogo=129,
        HW_SA_Label=130,
        HW_CustomerID=131,
        SA_Label=132,
        WaferParkingQty=133,
        NoNeedEDT=134,
        MoveType=135,
        Forwarder=136,
        Location=137,
        PaidLeaveHoursExcept=138,
        PaidLeaveHoursDistribution=139,
        PaidLeaveHoursDate=140,
        SpecialControlHolidayTypeForHR=141,
        ProductionSeries_WorkStation=142,
        SkillLevel=143,
        AssessmentType=144,
        MaterialMasterData=145,
        LNS = 146,
        LNS_CostCenter=147,
        LEPI_Sold_To = 148,
        SalaryAmendmentType=149,
        ExceptionCheckTraynumberForPN=150,
        Employee=151,
        JobGrade=152,
        MercerGrade=153,
        GILocation=154,
        ShuttleNumber=155,
        ExportPerformanceListDateForDL=156,
        ProductionSeries_WorkStation_Trainer=157,
        CustomConfigurationTableName=158,
        CustomConfigurationReport=159,
        CustomConfigurationMappingType=160,
        NotificationForQBAccountCancellation=161,
        MaterialMatchShift=162,
        LeaveNoticeForHR=163,
        SA_SNForCustomerID=165,
        CustomReportAccess=166,
        IgnoreSNCheckForSA=167,
        SkillAssessmentType=168,
        Team=169,
        FTADualPN=170,
        FilterParameterForRecode=171,
        PumpBuildSNFormat=172,
        TestStrategyType=173,
        RecertificationSwitch=174,
        WorkOrderType=175,
        MaterialType=176,
        NeedBacthNumber=177,
        Dormitory=178,
        SubsidyTypeForDormitory=179,
        Subsidy=180,
        ApproveProcess=181,
        Agency=182,
        Coating=183,
        PM_PL_PN=184,
        PG_PM_PL_PN=185,
        SingleVariable=186,
        IIVIPN_CPN=187,
        SA_SNNotForCustomerID=188,
        RGAProductionType=189,
        SpecialCarton=190,
        NoticeAfterModifyDeclarationNumber=191,
        EDTNotice=192,
        AssetOwner=193,
        CCPN=194,
        VoucherType=195,
        PaidLeaveHoursDistributionFor10Years=196,
        TravelerCheck=197,
        AssetLocation=198,
        AssetRepairType=199,
        AssetRepairResult=200,
        AssetCalibrateResult=201,
        EICCDepartment=202,
        Account=203,
        QACheckResult=204,
        NPI_PN=205,
        SamplingQtyForEachCarton=206,
        FAISampling=207,
        RTVType=208,
        CollectDataConfig=209,
        CollectDataTestStage=210,
        PN_CPN=211,
        OpeningReason=212,
        COCFailureCode=213,
        Supplier=214,
        COCVIStatus=215,
        EngineeringSample=216,
        MeasurementTools=217,
        VisualInspectionItem=218,
        VisualInspectionReason = 219,
        M3_P_TERMINLocation=220,
        M3_P_PLHCTLocation=221,
        M3_P_LENSPLHLocation=222,
        EmployeePicture=223,
        MEAccess=224,
        Machine=225,
        TestStageType=226,
        PNType=227,
        PNType_TestStage=228,
        STProduction=229,
        STProductionMaterialID = 230,
        PrecisionMetal=231,
        SampleItem=232,
        Translate=233,
        Version
    }
    public enum WorkSchedule
    {
        IDL=0,
        A=1,
        B=2,
        C=3,
        Z=4,
        E=5,
        辞职=6,
        休假=7,
        请假=8
    }
    public enum ChatConfig
    {
        IP=0,
        Port=1
    }
    /// <summary>
    /// FIS的部分权限管理角色，有PMS系统，由于时间问题，未是用该系统
    /// </summary>
    public enum FISAccess
    {
        Administrator= 0,
        LineLeader=1,
        PE=2,
        System=3,
        Replenishment=4,
        OEE=5,
        OEESetTime=6,
        Certification=7,
        FA = 8,
        BurInChecker = 9,
        CancelDA = 10,
        Unlock=11,
        TQC=12,
        QA=13,
        GR=14,
        Warehouse=15,
        Out=16,
        UsingAccount=17,
        Training=18,
        UpdateComponentList=19,
        BulkConfig=20,
        ChangeWORev=21,
        Asset=22,
        HR=23,
        IT=24,
        Administration = 25,
        HolidayData=26,
        OTData=27,
        LNSDCheck=28,
        LNSDSearch=29,
        LNSDPNMaintain=30,
        ExportProductionHours=31,
        SNScanning=32,
        Packing=33,
        ProductsSNCheck=34,
        ProductionOperator=35,
        MaterialRequisition=36,
        SearchPM=37,
        CloseReturnBill=38,
        Quarantine=39,
        EmployeeSearch=40,
        VirtualPackCell=41,
        MasterData=42,
        FGMovementBill=43,
        LoadEDT=44,
        QAOperator=45,
        MaterialMatch=46,
        CollectData = 47,
        OperatorSkill=48,
        FilterParameterForRecode=49,
        BuildConfigure=50,
        Packcell=51,
        SkillConfirm=52,
        ReturnUnit=53,
        Coating=54,
        CoatingSearch=55,
        DefectiveProductsFindProblem=56,
        DefectiveProductsFTAOrLid=57,
        WIP=58,
        TPT=59,
        DefectiveProductsSearch=60,
        EDT=61,
        RGARecord = 62,
        RGADelivery = 63,
        RGAReturn = 64,
        ApproveFlow=65,
        DeclarationNumberModify = 66,
        PrintAssetLabel=67,
        DistributionAndRecipients=68,
        Commission=69,
        CustomerAsset=70,
        ATSFormatSearch=71,
        ATSConfig=72,
        ScrapPNMaintain=73,
        SFMovementBill=74,
        VoucherConfirm=75,
        AttendanceAdministrator=76,
        UpdateOutlook=77,
        Employee=78,
        FGQCTConfirm=79,
        SFTAConfirm=80,
        PO=81,
        SamplingPlan=82,
        FillTrackingNumberRTV=83,
        IQCInspectRTV=84,
        OAOTData=85,
        COCLabel=86,
        CustomPrintLabel=87,
        FTAPNConfigure=88,
        EngineeringSample=89,
        FTA_CUTTER=90,
        GrapChart=91,
        ProductionSupervisor=92,
        MESConfig=93,
        QMS=94,
        SamplingSpecification,
        FQCCheckRecord,
        FQCCheckReport
    }
    public enum WorkTimeType
    {
        LaberTime=0,
        MachineTime=1,
        TotalTime=2,
        TestTime=3
    }
    public enum Unit
    {
        Price = 0 ,
        SafetyStock=1,
        ShelfLifeRequirement=2,
        Stock=3,
        LeadTime=4,
        Qty=5
    }
    /// <summary>
    /// 认证类型
    /// </summary>
    public enum CertificationType
    {
        Certifier = 0,
        TrainingCard = 1,
        WorkLicense = 2
    }
    /// <summary>
    /// 认证时效
    /// </summary>
    public enum CertificationDate
    {
        TrainingCardMonth =0,
        WorkLicenseYear =1
    }
    public enum FAStatus
    {
        UnFinished=0,
        Finished=1
    }
    public enum InspectionType
    {
        IPQA=0,
        OBA=1
    }
    /// <summary>
    /// 工单类型
    /// </summary>
    public enum WorkOrderType
    {
        PP01=0,
        ENG1=1,
        FART=2,
        MRB1=3,
        CHNG=4,
        ECO1=5
    }
    public enum AccessoryCheckType
    {
        TQC=0,
        QA=1
    }
    public enum WorkOrderSubStatus
    {
        SHORTAGE=0
    }
}
