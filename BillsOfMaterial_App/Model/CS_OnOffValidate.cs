using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Model
{
    public class CS_OnOffValidate
    {
        [Key]
        public int Id { get; set; }

        public int? Id_Offer { get; set; }

        [StringLength(10)]
        public string OfferNo { get; set; }

        [StringLength(21)]
        public string Item { get; set; }

        public bool On_Off { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public Guid TBGuid { get; set; }
    }
}
