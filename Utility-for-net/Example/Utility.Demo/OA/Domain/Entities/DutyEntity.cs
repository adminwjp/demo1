using NHibernate.Mapping.Attributes;
using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Entities
{
    /// <summary>
    /// 职位信息
    /// </summary>
    [Class(Table = "duty_info", Name = "DutyInfo", NameType = typeof(DutyEntity), Lazy = false)]
    public class DutyEntity:BaseEntity
    {
        private DateTime _accessionDate;//加入时间
        private DateTime _dimissionDate;//离开时间
        private string _dimissionReason;//离职原因
        private DateTime _firstPactDate;//第一次工作(签条约)时间
        private DateTime _pactStartDate;//第一次工作(签条约)开始时间
        private DateTime _pactEndDate;//第一次工作(签条约)结束时间
        private string _bankName;
        private string _bandId;
        private string _societySafefyNo;//社会安全()编号
        private string _annuitySafefyNo;//年保险金(养老金)编号
        private string _doleSafefyNo;//失业救助金编号
        private string _medicareSafefyNo;//医疗保险金编号
        private string _compoSafefyNo;//工伤赔偿费编号
        private string _acoumulationFundNo;// 公积金 编号

        [Property(Column = "accession_date")]
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime AccessionDate
        {
            get { return this._accessionDate; }
            set { Set(ref _accessionDate, value, "AccessionDate"); }
        }
        /// <summary>
        /// 离开时间
        /// </summary>
        [Property(Column = "dimission_date")]
        public DateTime DimissionDate
        {
            get { return this._dimissionDate; }
            set { Set(ref _dimissionDate, value, "DimissionDate"); }
        }
        /// <summary>
        /// 离职原因
        /// </summary>
        [Property(Column = "dimission_reason")]
        public string DimissionReason
        {
            get { return this._dimissionReason; }
            set { Set(ref _dimissionReason, value, "DimissionReason"); }
        }
        /// <summary>
        /// 第一次工作(签条约)时间
        /// </summary>
        [Property(Column = "first_pact_date")]
        public DateTime FirstPactDate
        {
            get { return this._firstPactDate; }
            set { Set(ref _firstPactDate, value, "FirstPactDate"); }
        }
        /// <summary>
        /// 第一次工作(签条约)开始时间
        /// </summary>
        [Property(Column = "pact_start_date")]
        public DateTime PactStartDate
        {
            get { return this._pactStartDate; }
            set { Set(ref _pactStartDate, value, "PactStartDate"); }
        }
        /// <summary>
        /// 第一次工作(签条约)结束时间
        /// </summary>
        [Property(Column = "pact_end_date")]
        public DateTime PactEndDate
        {
            get { return this._pactEndDate; }
            set { Set(ref _pactEndDate, value, "PactEndDate"); }
        }
        [Property(Column = "bank_name", Length =50)]
        public string BankName
        {
            get { return this._bankName; }
            set { Set(ref _bankName, value, "BankName"); }
        }
        [Property(Column = "bank_id", Length = 20)]
        public string BandId
        {
            get { return this._bandId; }
            set { Set(ref _bandId, value, "BandId"); }
        }
        /// <summary>
        /// 社会安全()编号
        /// </summary>
        [Property(Column = "society_safefy_no", Length = 20)]
        public string SocietySafefyNo
        {
            get { return this._societySafefyNo; }
            set { Set(ref _societySafefyNo, value, "SocietySafefyNo"); }
        }
        /// <summary>
        /// 年保险金(养老金)编号
        /// </summary>
        [Property(Column = "annuity_safefy_no", Length = 20)]
        public string AnnuitySafefyNo
        {
            get { return this._annuitySafefyNo; }
            set { Set(ref _annuitySafefyNo, value, "AnnuitySafefyNo"); }
        }
        /// <summary>
        /// 失业救助金编号
        /// </summary>
        [Property(Column = "dole_safefy_no", Length = 20)]
        public string DoleSafefyNo
        {
            get { return this._doleSafefyNo; }
            set { Set(ref _doleSafefyNo, value, "DoleSafefyNo"); }
        }
        /// <summary>
        /// 医疗保险金编号
        /// </summary>
        [Property(Column = "medicare_safefy_no", Length = 20)]
        public string MedicareSafefyNo
        {
            get { return this._medicareSafefyNo; }
            set { Set(ref _medicareSafefyNo, value, "MedicareSafefyNo"); }
        }
        /// <summary>
        /// 工伤赔偿费编号
        /// </summary>
        [Property(Column = "compo_safefy_no", Length = 20)]
        public string CompoSafefyNo
        {
            get { return this._compoSafefyNo; }
            set { Set(ref _compoSafefyNo, value, "CompoSafefyNo"); }
        }
        /// <summary>
        /// 公积金 编号
        /// </summary>
        [Property(Column = "acoumulation_fund_no", Length = 20)]
        public string AcoumulationFundNo
        {
            get { return this._acoumulationFundNo; }
            set { Set(ref _acoumulationFundNo, value, "AcoumulationFundNo"); }
        }
    }
}
