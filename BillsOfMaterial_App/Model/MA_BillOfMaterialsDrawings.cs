namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_BillOfMaterialsDrawings
    {
        [Key]
        [StringLength(21)]
        public string BOM { get; set; }

        [StringLength(21)]
        public string Drawing { get; set; }

        [StringLength(21)]
        public string Correction { get; set; }

        [StringLength(251)]
        public string Notes { get; set; }

        [StringLength(8)]
        public string Position { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_BillOfMaterials MA_BillOfMaterials { get; set; }
    }
}
