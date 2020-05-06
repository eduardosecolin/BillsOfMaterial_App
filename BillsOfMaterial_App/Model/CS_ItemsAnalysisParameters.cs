using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public class CS_ItemsAnalysisParameters
    {
        [Key]
        [StringLength(21)]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Item { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Line { get; set; }

        [StringLength(8)]
        public string UoM { get; set; }

        [StringLength(64)]
        public string Parameter { get; set; }

        [StringLength(32)]
        public string AnalysisMethod { get; set; }

        private int? analysisArea;

        public int? AnalysisArea { get { if (analysisArea == null) return 0; else return analysisArea; } set { analysisArea = value; } }

        [StringLength(21)]
        public string UpperBound { get; set; }

        [StringLength(21)]
        public string LowerBound { get; set; }

        [StringLength(21)]
        public string DetectableBound { get; set; }

        private double? expectedNumResult;

        public double? ExpectedNumResult { get { if (expectedNumResult == null) return 0; else return expectedNumResult; } set { expectedNumResult = value; } }

        [StringLength(32)]
        public string ExpectedResult { get; set; }

        [StringLength(1)]
        public string ToBePrinted { get; set; }

        private double? revision;

        public double? Revision { get { if (revision == null) return 0; else return revision; } set { revision = value; } }

        public DateTime? DisabledDate { get; set; }

        private DateTime? insertionDate;

        public DateTime? InsertionDate { get { if (insertionDate == null) return DateTime.Now; else return insertionDate; } set { insertionDate = value; } }

        [StringLength(12)]
        public string Customer { get; set; }

        [StringLength(2000)]
        public string Notes { get; set; }

        public int? IdSimulation { get; set; }

        [StringLength(1)]
        public string Disabled { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }
    }
}
