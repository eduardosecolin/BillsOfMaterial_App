namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RM_Workers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RM_Workers()
        {
            RM_WorkersDetails = new HashSet<RM_WorkersDetails>();
            RM_WorkersAbsences = new HashSet<RM_WorkersAbsences>();
            RM_WorkersArrangements = new HashSet<RM_WorkersArrangements>();
            RM_WorkersFields = new HashSet<RM_WorkersFields>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkerID { get; set; }

        [StringLength(128)]
        public string Password { get; set; }

        [StringLength(1)]
        public string PasswordMustBeChanged { get; set; }

        [StringLength(1)]
        public string PasswordCannotChange { get; set; }

        [StringLength(1)]
        public string PasswordNeverExpire { get; set; }

        [StringLength(1)]
        public string PasswordNotRenewable { get; set; }

        public DateTime? PasswordExpirationDate { get; set; }

        public short? PasswordAttemptsNumber { get; set; }

        [StringLength(8)]
        public string Title { get; set; }

        [StringLength(32)]
        public string Name { get; set; }

        [StringLength(64)]
        public string LastName { get; set; }

        public int? Gender { get; set; }

        [StringLength(128)]
        public string CompanyLogin { get; set; }

        [StringLength(128)]
        public string DomicilyAddress { get; set; }

        [StringLength(64)]
        public string DomicilyCity { get; set; }

        [StringLength(3)]
        public string DomicilyCounty { get; set; }

        [StringLength(10)]
        public string DomicilyZip { get; set; }

        [StringLength(64)]
        public string DomicilyCountry { get; set; }

        [StringLength(20)]
        public string DomicilyFC { get; set; }

        [StringLength(2)]
        public string DomicilyISOCode { get; set; }

        [StringLength(20)]
        public string Telephone1 { get; set; }

        [StringLength(20)]
        public string Telephone2 { get; set; }

        [StringLength(20)]
        public string Telephone3 { get; set; }

        [StringLength(20)]
        public string Telephone4 { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(64)]
        public string URL { get; set; }

        [StringLength(64)]
        public string SkypeID { get; set; }

        [StringLength(8)]
        public string CostCenter { get; set; }

        public double? HourlyCost { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(32)]
        public string CityOfBirth { get; set; }

        [StringLength(16)]
        public string CivilStatus { get; set; }

        [StringLength(16)]
        public string RegisterNumber { get; set; }

        public DateTime? EmploymentDate { get; set; }

        public DateTime? ResignationDate { get; set; }

        [StringLength(128)]
        public string ImagePath { get; set; }

        [StringLength(1)]
        public string HideOnLayout { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(16)]
        public string Latitude { get; set; }

        [StringLength(16)]
        public string Longitude { get; set; }

        [StringLength(8)]
        public string PIN { get; set; }

        [StringLength(8)]
        public string Branch { get; set; }

        [StringLength(64)]
        public string Address2 { get; set; }

        [StringLength(10)]
        public string StreetNo { get; set; }

        [StringLength(64)]
        public string District { get; set; }

        [StringLength(2)]
        public string FederalState { get; set; }

        [StringLength(1)]
        public string IsRSEnabled { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RM_WorkersDetails> RM_WorkersDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RM_WorkersAbsences> RM_WorkersAbsences { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RM_WorkersArrangements> RM_WorkersArrangements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RM_WorkersFields> RM_WorkersFields { get; set; }
    }
}
