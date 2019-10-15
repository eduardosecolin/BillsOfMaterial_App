namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustQuotasNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        [StringLength(251)]
        public string Notes { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_CustQuotas MA_CustQuotas { get; set; }
    }
}
