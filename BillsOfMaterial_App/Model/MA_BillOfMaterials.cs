namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_BillOfMaterials
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MA_BillOfMaterials()
        {
            MA_BillOfMaterialsComp = new HashSet<MA_BillOfMaterialsComp>();
            MA_BillOfMaterialsRouting = new HashSet<MA_BillOfMaterialsRouting>();
            MA_BillOfMaterialsTools = new HashSet<MA_BillOfMaterialsTools>();
        }

        [Key]
        [StringLength(21)]
        public string BOM { get; set; }

        public int? CodeType { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        [StringLength(50)]
        public string TecConclusion { get; set; }

        [StringLength(500)]
        public string TecConclusion2 { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModificationDate { get; set; }

        [StringLength(1)]
        public string UsePercQty { get; set; }

        [StringLength(1)]
        public string SF { get; set; }

        [StringLength(64)]
        public string Notes { get; set; }

        [StringLength(2000)]
        public string ImageReceivePath { get; set; }

        [StringLength(1)]
        public string InProduction { get; set; }

        public int? LastSubId { get; set; }

        [StringLength(1)]
        public string Configurable { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        [StringLength(1)]
        public string SalesDocOnly { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_BillOfMaterialsComp> MA_BillOfMaterialsComp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_BillOfMaterialsRouting> MA_BillOfMaterialsRouting { get; set; }

        public virtual MA_BillOfMaterialsDrawings MA_BillOfMaterialsDrawings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MA_BillOfMaterialsTools> MA_BillOfMaterialsTools { get; set; }
    }
}
