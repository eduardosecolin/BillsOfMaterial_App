using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public partial class CS_CustQuotasComponent
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 Line { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(21)]
        public string BOM { get; set; }

        [StringLength(21)]
        public string Item { get; set; }

        [StringLength(21)]
        public string Component { get; set; }

        [StringLength(128)]
        public string Description { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        public double? Qty { get; set; }

        [StringLength(1000)]
        public string Obs { get; set; }

        [StringLength(21)]
        public string Drawing { get; set; }

        [StringLength(21)]
        public string DrawingComponent { get; set; }

        public double? Costvalue { get; set; }

        public double? R1Costvalue { get; set; }

        [StringLength(1000)]
        public string PathFile1 { get; set; }

        [StringLength(1000)]
        public string PathFile2 { get; set; }

        [StringLength(1000)]
        public string PathFile3 { get; set; }

        [StringLength(50)]
        public string TecConclusion { get; set; }

        [StringLength(500)]
        public string TecConclusion2 { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
