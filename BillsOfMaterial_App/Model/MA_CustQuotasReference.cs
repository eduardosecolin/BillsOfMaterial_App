namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustQuotasReference
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Line { get; set; }

        public int? DocumentId { get; set; }

        public int? DocumentType { get; set; }

        public DateTime? DocumentDate { get; set; }

        [StringLength(20)]
        public string DocumentNumber { get; set; }

        [StringLength(1)]
        public string ReferenceIsAuto { get; set; }

        [StringLength(32)]
        public string ReferenceNotes { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }
    }
}
