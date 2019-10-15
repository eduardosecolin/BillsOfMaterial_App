namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustSupp
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustSuppType { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(12)]
        public string CustSupp { get; set; }

        [StringLength(128)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string TaxIdNumber { get; set; }

        [StringLength(20)]
        public string FiscalCode { get; set; }

        [StringLength(20)]
        public string ChambOfCommRegistrNo { get; set; }

        [StringLength(16)]
        public string Account { get; set; }

        [StringLength(128)]
        public string Address { get; set; }

        [StringLength(10)]
        public string ZIPCode { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        [StringLength(3)]
        public string County { get; set; }

        [StringLength(64)]
        public string Country { get; set; }

        [StringLength(20)]
        public string Telephone1 { get; set; }

        [StringLength(20)]
        public string Telephone2 { get; set; }

        [StringLength(16)]
        public string Telex { get; set; }

        [StringLength(16)]
        public string Fax { get; set; }

        [StringLength(64)]
        public string Internet { get; set; }

        [StringLength(128)]
        public string EMail { get; set; }

        [StringLength(2)]
        public string ISOCountryCode { get; set; }

        [StringLength(5)]
        public string SIACode { get; set; }

        [StringLength(64)]
        public string ContactPerson { get; set; }

        [StringLength(8)]
        public string TitleCode { get; set; }

        [StringLength(1)]
        public string NaturalPerson { get; set; }

        [StringLength(1)]
        public string IsAnEUCustSupp { get; set; }

        [StringLength(8)]
        public string Language { get; set; }

        [StringLength(8)]
        public string PriceList { get; set; }

        [StringLength(11)]
        public string CustSuppBank { get; set; }

        [StringLength(8)]
        public string Payment { get; set; }

        [StringLength(2)]
        public string CACheck { get; set; }

        [StringLength(34)]
        public string IBAN { get; set; }

        [StringLength(1)]
        public string IBANIsManual { get; set; }

        [StringLength(18)]
        public string CA { get; set; }

        [StringLength(1)]
        public string CIN { get; set; }

        [StringLength(8)]
        public string Currency { get; set; }

        [StringLength(8)]
        public string SendDocumentsTo { get; set; }

        [StringLength(8)]
        public string PaymentAddress { get; set; }

        [StringLength(8)]
        public string ShipToAddress { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(1024)]
        public string Notes { get; set; }

        [StringLength(24)]
        public string WorkingTime { get; set; }

        [StringLength(11)]
        public string CompanyBank { get; set; }

        public double? Discount1 { get; set; }

        public double? Discount2 { get; set; }

        [StringLength(16)]
        public string DiscountFormula { get; set; }

        [StringLength(16)]
        public string ExternalCode { get; set; }

        [StringLength(18)]
        public string CompanyCA { get; set; }

        public int? Presentation { get; set; }

        [StringLength(18)]
        public string CustomerCompanyCA { get; set; }

        [StringLength(16)]
        public string DDCustSupp { get; set; }

        [StringLength(1)]
        public string PrivacyStatement { get; set; }

        [StringLength(12)]
        public string LinkedCustSupp { get; set; }

        public int? DocumentSendingType { get; set; }

        [StringLength(1)]
        public string IsDummy { get; set; }

        [StringLength(1)]
        public string InTaxLists { get; set; }

        [StringLength(32)]
        public string WorkingPosition { get; set; }

        public int? CustSuppKind { get; set; }

        [StringLength(32)]
        public string TaxOffice { get; set; }

        [StringLength(8)]
        public string Storage { get; set; }

        [StringLength(8)]
        public string CostCenter { get; set; }

        [StringLength(10)]
        public string Job { get; set; }

        public DateTime? InsertionDate { get; set; }

        public DateTime? PrivacyStatementPrintDate { get; set; }

        [StringLength(32)]
        public string Region { get; set; }

        public int? MailSendingType { get; set; }

        [StringLength(12)]
        public string OldCustSupp { get; set; }

        [StringLength(10)]
        public string CompanyRegistrNo { get; set; }

        [StringLength(18)]
        public string FactoringCA { get; set; }

        [StringLength(1)]
        public string InCurrency { get; set; }

        [StringLength(1)]
        public string NoBlackList { get; set; }

        [StringLength(12)]
        public string BlackListCustSupp { get; set; }

        [StringLength(64)]
        public string SkypeID { get; set; }

        [StringLength(10)]
        public string CBICode { get; set; }

        [StringLength(8)]
        public string InvoiceAccTpl { get; set; }

        [StringLength(8)]
        public string CreditNoteAccTpl { get; set; }

        [StringLength(1)]
        public string Draft { get; set; }

        [StringLength(16)]
        public string Latitude { get; set; }

        [StringLength(16)]
        public string Longitude { get; set; }

        [StringLength(1)]
        public string IsCustoms { get; set; }

        [StringLength(64)]
        public string CertifiedEMail { get; set; }

        [StringLength(1)]
        public string NoTaxComm { get; set; }

        [StringLength(1)]
        public string NoSendPostaLite { get; set; }

        [StringLength(20)]
        public string GenRegNo { get; set; }

        [StringLength(10)]
        public string GenRegEntity { get; set; }

        [StringLength(20)]
        public string FedStateReg { get; set; }

        public int? TaxpayerType { get; set; }

        [StringLength(20)]
        public string MunicipalityReg { get; set; }

        [StringLength(9)]
        public string SUFRAMA { get; set; }

        [StringLength(64)]
        public string Address2 { get; set; }

        [StringLength(10)]
        public string StreetNo { get; set; }

        [StringLength(64)]
        public string District { get; set; }

        [StringLength(2)]
        public string FederalState { get; set; }

        [StringLength(8)]
        public string PaymentPeriShablesWithin60 { get; set; }

        [StringLength(8)]
        public string PaymentPeriShablesOver60 { get; set; }

        [StringLength(1)]
        public string UsedForSummaryDocuments { get; set; }

        [StringLength(8)]
        public string FiscalCtg { get; set; }

        [StringLength(10)]
        public string ActivityCode { get; set; }

        [StringLength(128)]
        public string FantasyName { get; set; }

        [StringLength(16)]
        public string PymtAccount { get; set; }

        [StringLength(1)]
        public string LeasingLetter { get; set; }

        [StringLength(3)]
        public string ChambOfCommCounty { get; set; }

        [StringLength(1)]
        public string SplitTax { get; set; }

        [StringLength(128)]
        public string FiscalName { get; set; }

        public int? TaxIdType { get; set; }

        [StringLength(1)]
        public string PrivacyAgreed { get; set; }

        [StringLength(1)]
        public string MarketingAgreed { get; set; }

        [StringLength(34)]
        public string SplitTaxIBAN { get; set; }

        [StringLength(7)]
        public string IPACode { get; set; }

        [StringLength(17)]
        public string EORICode { get; set; }

        [StringLength(20)]
        public string AdministrationReference { get; set; }

        [StringLength(1)]
        public string ImmediateLikeAccompanying { get; set; }

        [StringLength(1)]
        public string ElectronicInvoicing { get; set; }

        [StringLength(8)]
        public string PermanentBranchCode { get; set; }

        [StringLength(2)]
        public string FDISOCountryCode { get; set; }

        [StringLength(16)]
        public string FDFiscalCode { get; set; }

        [StringLength(1)]
        public string FDNaturalPerson { get; set; }

        [StringLength(80)]
        public string FDCompanyName { get; set; }

        [StringLength(60)]
        public string FDName { get; set; }

        [StringLength(60)]
        public string FDLastName { get; set; }

        [StringLength(8)]
        public string FiscalRegime { get; set; }

        [StringLength(28)]
        public string FDFiscalCodeID { get; set; }

        [StringLength(17)]
        public string FDEORICode { get; set; }

        [StringLength(8)]
        public string FDTitleCode { get; set; }

        [StringLength(1)]
        public string SendByCertifiedEmail { get; set; }

        public int? AswStandard { get; set; }

        public int? RegisterReceivedEI { get; set; }

        [StringLength(64)]
        public string EICertifiedEMail { get; set; }

        [StringLength(35)]
        public string EITypeCodeItemCustomer { get; set; }

        [StringLength(35)]
        public string EITypeCodeItemBarcode { get; set; }

        [StringLength(35)]
        public string EITypeCodeItem { get; set; }

        public int? EIUnitValue { get; set; }

        [StringLength(6)]
        public string OMNIASubAccount { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
