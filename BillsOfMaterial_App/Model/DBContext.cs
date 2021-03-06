namespace BillsOfMaterial_App.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            //: base("name=DBContext")
        {
            this.Database.Connection.ConnectionString = Utilities.Util.GetConfig()[0];
        }

        public virtual DbSet<CS_CQBOM> CS_CQBOM { get; set; }
        public virtual DbSet<CS_CQSubBOM> CS_CQSubBOM { get; set; }
        public virtual DbSet<MA_BillOfMaterials> MA_BillOfMaterials { get; set; }
        public virtual DbSet<MA_BillOfMaterialsComp> MA_BillOfMaterialsComp { get; set; }
        public virtual DbSet<MA_BillOfMaterialsDrawings> MA_BillOfMaterialsDrawings { get; set; }
        public virtual DbSet<MA_BillOfMaterialsNotes> MA_BillOfMaterialsNotes { get; set; }
        public virtual DbSet<MA_BillOfMaterialsQnA> MA_BillOfMaterialsQnA { get; set; }
        public virtual DbSet<MA_BillOfMaterialsRouting> MA_BillOfMaterialsRouting { get; set; }
        public virtual DbSet<MA_BillOfMaterialsTools> MA_BillOfMaterialsTools { get; set; }
        public virtual DbSet<MA_BOMLabour> MA_BOMLabour { get; set; }
        public virtual DbSet<MA_CostCenters> MA_CostCenters { get; set; }
        public virtual DbSet<MA_CustQuotas> MA_CustQuotas { get; set; }
        public virtual DbSet<MA_CustQuotasDetail> MA_CustQuotasDetail { get; set; }
        public virtual DbSet<MA_CustQuotasNote> MA_CustQuotasNote { get; set; }
        public virtual DbSet<MA_CustQuotasReference> MA_CustQuotasReference { get; set; }
        public virtual DbSet<MA_CustQuotasShipping> MA_CustQuotasShipping { get; set; }
        public virtual DbSet<MA_CustQuotasSummary> MA_CustQuotasSummary { get; set; }
        public virtual DbSet<MA_CustQuotasTaxSummary> MA_CustQuotasTaxSummary { get; set; }
        public virtual DbSet<MA_CustSupp> MA_CustSupp { get; set; }
        public virtual DbSet<MA_Items> MA_Items { get; set; }
        public virtual DbSet<MA_ItemsBalances> MA_ItemsBalances { get; set; }
        public virtual DbSet<MA_ItemsBRTaxes> MA_ItemsBRTaxes { get; set; }
        public virtual DbSet<MA_ItemsFiscalYearData> MA_ItemsFiscalYearData { get; set; }
        public virtual DbSet<MA_ItemsPriceLists> MA_ItemsPriceLists { get; set; }
        public virtual DbSet<MA_ItemTypes> MA_ItemTypes { get; set; }
        public virtual DbSet<MA_Operations> MA_Operations { get; set; }
        public virtual DbSet<MA_OperationsLabour> MA_OperationsLabour { get; set; }
        public virtual DbSet<MA_OperationsTools> MA_OperationsTools { get; set; }
        public virtual DbSet<MA_PriceLists> MA_PriceLists { get; set; }
        public virtual DbSet<RM_Workers> RM_Workers { get; set; }
        public virtual DbSet<RM_WorkersAbsences> RM_WorkersAbsences { get; set; }
        public virtual DbSet<RM_WorkersArrangements> RM_WorkersArrangements { get; set; }
        public virtual DbSet<RM_WorkersDetails> RM_WorkersDetails { get; set; }
        public virtual DbSet<RM_WorkersFields> RM_WorkersFields { get; set; }
        public virtual DbSet<CS_CustQuotasComponent> CS_CustQuotasComponent { get; set; }
        public virtual DbSet<CS_CustQuotasOperation> CS_CustQuotasOperation { get; set; }
        public virtual DbSet<MA_WorkCenters> MA_WorkCenters { get; set; }
        public virtual DbSet<MA_WCFamilies> MA_WCFamilies { get; set; }
        public virtual DbSet<MA_WCFamiliesDetails> MA_WCFamiliesDetails { get; set; }
        public virtual DbSet<MA_Drawings> MA_Drawings { get; set; }
        public virtual DbSet<MA_DrawingsDescription> MA_DrawingsDescription { get; set; }
        public virtual DbSet<MA_BRNCM> MA_BRNCM { get; set; }
        public virtual DbSet<MA_UnitsOfMeasure> MA_UnitsOfMeasure { get; set; }
        public virtual DbSet<MA_Company> MA_Company { get; set; }
        public virtual DbSet<CS_DBDefaultOBS> CS_DBDefaultOBS { get; set; }
        public virtual DbSet<CS_UserManager> CS_UserManager { get; set; }
        public virtual DbSet<CS_TBCostFormation> CS_TBCostFormation { get; set; }
        public virtual DbSet<CS_OnOffValidate> CS_OnOffValidate { get; set; }
        public virtual DbSet<CS_ItemsAnalysisParameters> CS_ItemsAnalysisParameters { get; set; }
        public virtual DbSet<MA_QltCtrlAnalMet> MA_QltCtrlAnalMet { get; set; }
        public virtual DbSet<MA_QltCtrlParameters> MA_QltCtrlParameters { get; set; }
        public virtual DbSet<CS_TimeSheetPROD> CS_TimeSheetPROD { get; set; }
        public virtual DbSet<CS_CustQuotasCompAttach> CS_CustQuotasCompAttach { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CS_CustQuotasCompAttach>()
                .Property(e => e.Attatchment)
                .IsFixedLength();

            modelBuilder.Entity<CS_TimeSheetPROD>()
                .Property(e => e.Phase)
                .IsFixedLength();

            modelBuilder.Entity<CS_TimeSheetPROD>()
                .Property(e => e.IsFinalized)
                .IsFixedLength();

            modelBuilder.Entity<MA_QltCtrlParameters>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<MA_QltCtrlParameters>()
                .Property(e => e.UoM)
                .IsFixedLength();

            modelBuilder.Entity<MA_QltCtrlParameters>()
                .Property(e => e.AnalysisMethod)
                .IsFixedLength();

            modelBuilder.Entity<MA_QltCtrlAnalMet>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.UoM)
                .IsFixedLength();


            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.AnalysisMethod)
                .IsFixedLength();

            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.UpperBound)
                .IsFixedLength();

            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.LowerBound)
                .IsFixedLength();

            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.DetectableBound)
                .IsFixedLength();

            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.ExpectedResult)
                .IsFixedLength();

            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.Customer)
                .IsFixedLength();

            modelBuilder.Entity<CS_ItemsAnalysisParameters>()
                .Property(e => e.Notes)
                .IsFixedLength();

            modelBuilder.Entity<CS_OnOffValidate>()
                .Property(e => e.OfferNo)
                .IsFixedLength();

            modelBuilder.Entity<CS_OnOffValidate>()
                .Property(e => e.Item)
                .IsFixedLength();

            modelBuilder.Entity<CS_DBDefaultOBS>()
                .Property(e => e.Title)
                .IsFixedLength();

            modelBuilder.Entity<CS_DBDefaultOBS>()
                .Property(e => e.Observation)
                .IsFixedLength();

            modelBuilder.Entity<CS_TBCostFormation>()
               .Property(e => e.OfferNo)
               .IsFixedLength();

            modelBuilder.Entity<CS_TBCostFormation>()
               .Property(e => e.SumIpiIcms)
               .IsFixedLength();

            modelBuilder.Entity<CS_TBCostFormation>()
               .Property(e => e.Item)
               .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.UsePercQty)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.SF)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.InProduction)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.Configurable)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.Disabled)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.SalesDocOnly)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.TecConclusion)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.TecConclusion2)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .Property(e => e.ImageReceivePath)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterials>()
                .HasMany(e => e.MA_BillOfMaterialsComp)
                .WithRequired(e => e.MA_BillOfMaterials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_BillOfMaterials>()
                .HasMany(e => e.MA_BillOfMaterialsRouting)
                .WithRequired(e => e.MA_BillOfMaterials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_BillOfMaterials>()
                .HasOptional(e => e.MA_BillOfMaterialsDrawings)
                .WithRequired(e => e.MA_BillOfMaterials);

            modelBuilder.Entity<MA_BillOfMaterials>()
                .HasMany(e => e.MA_BillOfMaterialsTools)
                .WithRequired(e => e.MA_BillOfMaterials)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.FixedComponent)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
               .Property(e => e.TempDrawing)
               .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
              .Property(e => e.DrawingComp)
              .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
               .Property(e => e.PathFile)
               .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
               .Property(e => e.CompTecConclusion)
               .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.ToExplode)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.Configurable)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.NotPostable)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.IsKanban)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.FixedQty)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.Valorize)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsComp>()
                .Property(e => e.SetFixedQtyOnMO)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsNotes>()
                .Property(e => e.Editable)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsQnA>()
                .Property(e => e.ToExplode)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsQnA>()
                .Property(e => e.IsADefault)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsRouting>()
                .Property(e => e.TotalTime)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsRouting>()
                .Property(e => e.IsWC)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsTools>()
                .Property(e => e.IsFamily)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsTools>()
                .Property(e => e.Fixed)
                .IsFixedLength();

            modelBuilder.Entity<MA_BillOfMaterialsTools>()
                .Property(e => e.ExclusiveUse)
                .IsFixedLength();

            modelBuilder.Entity<MA_BRNCM>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<MA_BRNCM>()
               .Property(e => e.ICMSTaxRateCode)
               .IsFixedLength();

            modelBuilder.Entity<MA_CostCenters>()
                .Property(e => e.DummyCostCenter)
                .IsFixedLength();

            modelBuilder.Entity<MA_CostCenters>()
                .Property(e => e.Disabled)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.NetOfTax)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.FixingIsManual)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.Printed)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.SentByEMail)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.DueDateFromOrderDate)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.ActiveSubcontracting)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.SentByPostaLite)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.Archived)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .HasMany(e => e.MA_CustQuotasDetail)
                .WithRequired(e => e.MA_CustQuotas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_CustQuotas>()
                .HasOptional(e => e.MA_CustQuotasSummary)
                .WithRequired(e => e.MA_CustQuotas);

            modelBuilder.Entity<MA_CustQuotas>()
                .HasMany(e => e.MA_CustQuotasTaxSummary)
                .WithRequired(e => e.MA_CustQuotas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_CustQuotas>()
                .HasOptional(e => e.MA_CustQuotasNote)
                .WithRequired(e => e.MA_CustQuotas);

            modelBuilder.Entity<MA_CustQuotas>()
                .HasOptional(e => e.MA_CustQuotasShipping)
                .WithRequired(e => e.MA_CustQuotas);

            modelBuilder.Entity<MA_CustQuotas>()
               .Property(e => e.PathFile1)
               .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.PathFile2)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotas>()
                .Property(e => e.PathFile3)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasDetail>()
                .Property(e => e.NoPrint)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasDetail>()
                .Property(e => e.NoCopyOnOrder)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasDetail>()
                .Property(e => e.NetPriceIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasDetail>()
                .Property(e => e.InEI)
                .IsFixedLength();

            modelBuilder.Entity<CS_UserManager>()
                .Property(e => e.UserName)
                .IsFixedLength();

            modelBuilder.Entity<CS_UserManager>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.BOM)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.Item)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.IsItemNew)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.IsItemRetired)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.Component)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.Description)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
               .Property(e => e.TecConclusion)
               .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
               .Property(e => e.TecConclusion2)
               .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.UoM)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.Obs)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.Drawing)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.DrawingComponent)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.PathFile1)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.PathFile2)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasComponent>()
                .Property(e => e.PathFile3)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasOperation>()
                .Property(e => e.BOM)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasOperation>()
                .Property(e => e.Item)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasOperation>()
               .Property(e => e.TimeProcessStr)
               .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasOperation>()
                .Property(e => e.DescriptionOperation)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasOperation>()
                .Property(e => e.UoM)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasOperation>()
                .Property(e => e.Operation)
                .IsFixedLength();

            modelBuilder.Entity<CS_CustQuotasOperation>()
                .Property(e => e.Obs)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasReference>()
                .Property(e => e.ReferenceIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasShipping>()
                .Property(e => e.NoOfPacksIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasShipping>()
                .Property(e => e.NetWeightIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasShipping>()
                .Property(e => e.GrossWeightIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasShipping>()
                .Property(e => e.GrossVolumeIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasShipping>()
                .Property(e => e.PortAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.PackagingChargesIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.ShippingChargesIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.StampsChargesIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.CollectionChargesIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.AdditionalChargesIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.CashOnDeliveryChargesIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.DiscountsIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustQuotasSummary>()
                .Property(e => e.InsuranceChargesIsAuto)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.NaturalPerson)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.IsAnEUCustSupp)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.IBANIsManual)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.Disabled)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.PrivacyStatement)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.IsDummy)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.InTaxLists)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.InCurrency)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.NoBlackList)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.Draft)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.IsCustoms)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.NoTaxComm)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.NoSendPostaLite)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.UsedForSummaryDocuments)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.SplitTax)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.PrivacyAgreed)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.MarketingAgreed)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.ImmediateLikeAccompanying)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.FDNaturalPerson)
                .IsFixedLength();

            modelBuilder.Entity<MA_CustSupp>()
                .Property(e => e.SendByCertifiedEmail)
                .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.Description)
               .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.Revision)
               .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.DerivedFrom)
               .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.BarCode)
               .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.Notes)
               .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.ApprovalSignature)
               .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.FilePath)
               .IsFixedLength();

            modelBuilder.Entity<MA_Drawings>()
               .Property(e => e.PreferredDrawing)
               .IsFixedLength();

            modelBuilder.Entity<MA_DrawingsDescription>()
               .Property(e => e.Description)
               .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.IsGood)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.HasCustomers)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.Dimension)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.HasSuppliers)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.UseSerialNo)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.Disabled)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.IsSpecificItem)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.KitExpansion)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.PostKitComponents)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.NotPostable)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.NoUoMSearch)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.CanBeDisabled)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.BasePriceWithTax)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.SubjectToWithholdingTax)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.NoAddDiscountInSaleDoc)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.ReverseCharge)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.Draft)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.Valorize)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.NoPaymDiscountInDoc)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.ToBeDiscontinued)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .Property(e => e.AdditionalCharge)
                .IsFixedLength();

            modelBuilder.Entity<MA_Items>()
                .HasMany(e => e.MA_ItemsBalances)
                .WithRequired(e => e.MA_Items)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_Items>()
                .HasMany(e => e.MA_ItemsFiscalYearData)
                .WithRequired(e => e.MA_Items)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_Items>()
                .HasMany(e => e.MA_ItemsPriceLists)
                .WithRequired(e => e.MA_Items)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_ItemsFiscalYearData>()
                .Property(e => e.NoABCValuation)
                .IsFixedLength();

            modelBuilder.Entity<MA_ItemsFiscalYearData>()
                .Property(e => e.EvaluateByLot)
                .IsFixedLength();

            modelBuilder.Entity<MA_ItemsPriceLists>()
                .Property(e => e.WithTax)
                .IsFixedLength();

            modelBuilder.Entity<MA_ItemsPriceLists>()
                .Property(e => e.Discounted)
                .IsFixedLength();

            modelBuilder.Entity<MA_ItemsPriceLists>()
                .Property(e => e.Disabled)
                .IsFixedLength();

            modelBuilder.Entity<MA_ItemsPriceLists>()
                .Property(e => e.AlwaysShow)
                .IsFixedLength();

            modelBuilder.Entity<MA_ItemTypes>()
                .Property(e => e.HasCustomers)
                .IsFixedLength();

            modelBuilder.Entity<MA_ItemTypes>()
                .Property(e => e.HasSuppliers)
                .IsFixedLength();

            modelBuilder.Entity<MA_Operations>()
                .Property(e => e.CostsOnQty)
                .IsFixedLength();

            modelBuilder.Entity<MA_Operations>()
                .Property(e => e.TotalTime)
                .IsFixedLength();

            modelBuilder.Entity<MA_Operations>()
                .Property(e => e.IsWC)
                .IsFixedLength();

            modelBuilder.Entity<MA_Operations>()
                .HasMany(e => e.MA_OperationsLabour)
                .WithRequired(e => e.MA_Operations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_Operations>()
                .HasMany(e => e.MA_OperationsTools)
                .WithRequired(e => e.MA_Operations)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_OperationsTools>()
                .Property(e => e.IsFamily)
                .IsFixedLength();

            modelBuilder.Entity<MA_OperationsTools>()
                .Property(e => e.Fixed)
                .IsFixedLength();

            modelBuilder.Entity<MA_OperationsTools>()
                .Property(e => e.ExclusiveUse)
                .IsFixedLength();

            modelBuilder.Entity<MA_PriceLists>()
                .Property(e => e.Disabled)
                .IsFixedLength();

            modelBuilder.Entity<MA_PriceLists>()
                .Property(e => e.AlwaysShow)
                .IsFixedLength();

            modelBuilder.Entity<MA_WorkCenters>()
               .Property(e => e.Description)
               .IsFixedLength();

            modelBuilder.Entity<MA_WorkCenters>()
               .Property(e => e.Supplier)
               .IsFixedLength();

            modelBuilder.Entity<MA_WorkCenters>()
               .Property(e => e.Outsourced)
               .IsFixedLength();

            modelBuilder.Entity<MA_WorkCenters>()
               .Property(e => e.Template)
               .IsFixedLength();

            modelBuilder.Entity<MA_WorkCenters>()
               .Property(e => e.Make)
               .IsFixedLength();

            modelBuilder.Entity<MA_WorkCenters>()
               .Property(e => e.Notes)
               .IsFixedLength();

            modelBuilder.Entity<MA_WorkCenters>()
               .Property(e => e.Calendar)
               .IsFixedLength();

            modelBuilder.Entity<MA_WCFamilies>()
               .Property(e => e.Description)
               .IsFixedLength();

            modelBuilder.Entity<MA_WCFamilies>()
               .Property(e => e.Notes)
               .IsFixedLength();

            modelBuilder.Entity<MA_WCFamilies>()
                .HasMany(e => e.MA_WCFamiliesDetails)
                .WithRequired(e => e.MA_WCFamilies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MA_WCFamiliesDetails>()
              .Property(e => e.Notes)
              .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .Property(e => e.PasswordMustBeChanged)
                .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .Property(e => e.PasswordCannotChange)
                .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .Property(e => e.PasswordNeverExpire)
                .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .Property(e => e.PasswordNotRenewable)
                .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .Property(e => e.HideOnLayout)
                .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .Property(e => e.Disabled)
                .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .Property(e => e.IsRSEnabled)
                .IsFixedLength();

            modelBuilder.Entity<RM_Workers>()
                .HasMany(e => e.RM_WorkersDetails)
                .WithRequired(e => e.RM_Workers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RM_Workers>()
                .HasMany(e => e.RM_WorkersAbsences)
                .WithRequired(e => e.RM_Workers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RM_Workers>()
                .HasMany(e => e.RM_WorkersArrangements)
                .WithRequired(e => e.RM_Workers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RM_Workers>()
                .HasMany(e => e.RM_WorkersFields)
                .WithRequired(e => e.RM_Workers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RM_WorkersDetails>()
                .Property(e => e.IsWorker)
                .IsFixedLength();

            modelBuilder.Entity<RM_WorkersFields>()
                .Property(e => e.HideOnLayout)
                .IsFixedLength();

            modelBuilder.Entity<MA_UnitsOfMeasure>()
               .Property(e => e.Description)
               .IsFixedLength();

            modelBuilder.Entity<MA_UnitsOfMeasure>()
               .Property(e => e.Symbol)
               .IsFixedLength();

            modelBuilder.Entity<MA_UnitsOfMeasure>()
               .Property(e => e.Notes)
               .IsFixedLength();

            modelBuilder.Entity<MA_UnitsOfMeasure>()
               .Property(e => e.BarcodeSegment)
               .IsFixedLength();

            modelBuilder.Entity<MA_UnitsOfMeasure>()
               .Property(e => e.Disabled)
               .IsFixedLength();

            modelBuilder.Entity<MA_UnitsOfMeasure>()
               .Property(e => e.UoMForEI)
               .IsFixedLength();
        }
    }
}
