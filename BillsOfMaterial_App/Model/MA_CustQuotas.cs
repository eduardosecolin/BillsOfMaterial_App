namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustQuotas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MA_CustQuotas()
        {
            MA_CustQuotasDetail = new HashSet<MA_CustQuotasDetail>();
            MA_CustQuotasTaxSummary = new HashSet<MA_CustQuotasTaxSummary>();
        }

        public int? SaleOrdId { get; set; }

        [StringLength(10)]
        public string QuotationNo { get; set; }

        public DateTime? QuotationDate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public int? UseCustomerOrContact { get; set; }

        [StringLength(12)]
        public string Customer { get; set; }

        [StringLength(12)]
        public string Contact { get; set; }

        [StringLength(8)]
        public string Language { get; set; }

        [StringLength(32)]
        public string OurReference { get; set; }

        [StringLength(32)]
        public string YourReference { get; set; }

        [StringLength(8)]
        public string Payment { get; set; }

        [StringLength(8)]
        public string PriceList { get; set; }

        [StringLength(11)]
        public string CustomerBank { get; set; }

        [StringLength(11)]
        public string CompanyBank { get; set; }

        [StringLength(8)]
        public string SendQuotationTo { get; set; }

        [StringLength(8)]
        public string PaymentAddress { get; set; }

        [StringLength(1)]
        public string NetOfTax { get; set; }

        [StringLength(8)]
        public string Currency { get; set; }

        public DateTime? FixingDate { get; set; }

        [StringLength(1)]
        public string FixingIsManual { get; set; }

        public double? Fixing { get; set; }

        [StringLength(8)]
        public string Salesperson { get; set; }

        [StringLength(8)]
        public string AreaManager { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        [StringLength(1)]
        public string Printed { get; set; }

        [StringLength(1)]
        public string SentByEMail { get; set; }

        [StringLength(18)]
        public string CompanyCA { get; set; }

        public int? Presentation { get; set; }

        public DateTime? ValidityEndingDate { get; set; }

        public short? ValidityDays { get; set; }

        [StringLength(1)]
        public string DueDateFromOrderDate { get; set; }

        public int? LastSubId { get; set; }

        [StringLength(10)]
        public string Job { get; set; }

        [StringLength(8)]
        public string CostCenter { get; set; }

        [StringLength(8)]
        public string ProductLine { get; set; }

        [StringLength(1)]
        public string ActiveSubcontracting { get; set; }

        [StringLength(32)]
        public string RequestNo { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? CustSuppType { get; set; }

        [StringLength(8)]
        public string TaxJournal { get; set; }

        [StringLength(8)]
        public string StubBook { get; set; }

        [StringLength(8)]
        public string InvRsn { get; set; }

        [StringLength(8)]
        public string StoragePhase1 { get; set; }

        [StringLength(12)]
        public string SpecificatorPhase1 { get; set; }

        [StringLength(8)]
        public string StoragePhase2 { get; set; }

        [StringLength(12)]
        public string SpecificatorPhase2 { get; set; }

        public int? Specificator1Type { get; set; }

        public int? Specificator2Type { get; set; }

        [StringLength(15)]
        public string ContractCode { get; set; }

        [StringLength(16)]
        public string ProjectCode { get; set; }

        [StringLength(16)]
        public string TaxCommunicationGroup { get; set; }

        [StringLength(18)]
        public string CompanyPymtCA { get; set; }

        [StringLength(1)]
        public string SentByPostaLite { get; set; }

        [StringLength(1)]
        public string Archived { get; set; }

        public int? FromExternalProgram { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_CustQuotasDetail> MA_CustQuotasDetail { get; set; }

        public virtual MA_CustQuotasSummary MA_CustQuotasSummary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_CustQuotasTaxSummary> MA_CustQuotasTaxSummary { get; set; }

        public virtual MA_CustQuotasNote MA_CustQuotasNote { get; set; }

        public virtual MA_CustQuotasShipping MA_CustQuotasShipping { get; set; }
    }
}
