namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MA_CustQuotasShipping
    {
        [StringLength(8)]
        public string Port { get; set; }

        [StringLength(16)]
        public string Shipping { get; set; }

        [StringLength(8)]
        public string Package { get; set; }

        public short? NoOfPacks { get; set; }

        public double? NetWeight { get; set; }

        public double? GrossWeight { get; set; }

        public double? GrossVolume { get; set; }

        [StringLength(1)]
        public string NoOfPacksIsAuto { get; set; }

        [StringLength(1)]
        public string NetWeightIsAuto { get; set; }

        [StringLength(1)]
        public string GrossWeightIsAuto { get; set; }

        [StringLength(1)]
        public string GrossVolumeIsAuto { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustQuotaId { get; set; }

        [StringLength(8)]
        public string ShipToAddress { get; set; }

        [StringLength(1)]
        public string PortAuto { get; set; }

        public DateTime TBCreated { get; set; }

        public DateTime TBModified { get; set; }

        public int TBCreatedID { get; set; }

        public int TBModifiedID { get; set; }

        public virtual MA_CustQuotas MA_CustQuotas { get; set; }
    }
}
