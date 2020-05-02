using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class BOMService
    {
        private readonly DBContext _context;
        CustQuotaCompOpService serviceCompOp;
        CustQuotasService cqService;

        public BOMService()
        {
            _context = new DBContext();
            serviceCompOp = new CustQuotaCompOpService();
            cqService = new CustQuotasService();
        }

        public List<MA_BillOfMaterialsComp> GetComponentsBOM(string bom)
        {
            return _context.MA_BillOfMaterialsComp.Where(x => x.BOM == bom).ToList();
        }

        public List<MA_BillOfMaterialsRouting> GetOperationsBOM(string bom)
        {
            return _context.MA_BillOfMaterialsRouting.Where(x => x.BOM == bom).ToList();
        }

        public List<MA_BillOfMaterials> GetBOM()
        {
            return _context.MA_BillOfMaterials.ToList();
        }

        public List<MA_BillOfMaterials> GetBOMByParams(string bom)
        {
            return _context.MA_BillOfMaterials.Where(x => x.BOM.Contains(bom) || x.Description.Contains(bom)).ToList();
        }

        public List<MA_BillOfMaterials> GetBOMByParams2(string bom)
        {
            return _context.MA_BillOfMaterials.Where(x => x.TecConclusion.Contains(bom)).ToList();
        }

        public List<MA_BillOfMaterialsDrawings> GetAllDrawing(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return _context.MA_BillOfMaterialsDrawings.ToList();
            }
            else
            {
                return _context.MA_BillOfMaterialsDrawings.Where(x => x.Drawing.Contains(param)).ToList();
            }
        }

        public string GetDescriptionBOM(string bom)
        {
            var sql = _context.MA_BillOfMaterials.Where(x => x.BOM == bom).FirstOrDefault();
            if(sql != null)
            {
                return sql.Description;
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetUoMBOM(string bom)
        {
            var sql = _context.MA_BillOfMaterials.Where(x => x.BOM == bom).FirstOrDefault();
            if (sql != null)
            {
                return sql.UoM;
            }
            else
            {
                return string.Empty;
            }
        }

        public List<MA_BillOfMaterials> GetBOMFromSImulation()
        {
            List<MA_BillOfMaterials> listBOM = new List<MA_BillOfMaterials>();
            var listStr = serviceCompOp.GetAllSimulations();
            if (listStr.Count > 0)
            {
                foreach (var item in listStr)
                {
                    MA_BillOfMaterials bom = new MA_BillOfMaterials();
                    bom.BOM = item.BOM.Trim();
                    bom.Description = GetDescriptionBOM(item.BOM.Trim());
                    bom.UoM = GetUoMBOM(item.BOM.Trim());
                    bom.LastModificationDate = serviceCompOp.GetTBModifiedFromSimulationCompBOM(item.BOM.Trim());
                    bom.CreationDate = serviceCompOp.GetTBCreatedFromSimulationCompBOM(item.BOM.Trim());
                    bom.LastSubId = item.Id;
                    bom.TecConclusion = cqService.GetQuotationNo(item.Id);
                    listBOM.Add(bom);
                }
            }

            return listBOM;
        }

        public List<MA_BillOfMaterialsComp> GetComponentsBOMFromSimulation(string bom, string itemComp, int? Id, bool isAnalize)
        {
            int? id = Id;
            List<MA_BillOfMaterialsComp> listComp = new List<MA_BillOfMaterialsComp>();
            List<CS_CustQuotasComponent> compList = new List<CS_CustQuotasComponent>();
            if (isAnalize)
            {
                compList = serviceCompOp.GetSimulationComponents2(Convert.ToInt32(id), itemComp);
            }
            else
            {
                compList = serviceCompOp.GetSimulationComponents2_2(Convert.ToInt32(id), itemComp);
            }
            if(compList.Count > 0)
            {
                foreach (var item in compList)
                {
                    MA_BillOfMaterialsComp comp = new MA_BillOfMaterialsComp();
                    comp.BOM = bom.Trim();
                    comp.Component = item.Component.Trim();
                    comp.Description = item.Description.Trim();
                    comp.UoM = item.UoM.Trim();
                    comp.Qty = item.Qty;
                    comp.ScrapQty = item.Costvalue == null ? 0 : item.Costvalue;
                    comp.Drawing = item.PathFile1 == null ? string.Empty : item.PathFile1.Trim();
                    comp.DrawingComp = item.DrawingComponent == null ? string.Empty : item.DrawingComponent.Trim();
                    comp.TempDrawing = item.Drawing == null ? string.Empty : item.Drawing.Trim();
                    comp.PathFile = item.PathFile2 == null ? string.Empty : item.PathFile2.Trim();
                    comp.CompTecConclusion = item.TecConclusion2 == null ? string.Empty : item.TecConclusion2.Trim();
                    comp.WastePerc = item.R1Costvalue;
                    if (item.Obs == null)
                    {
                        comp.Notes = string.Empty;
                    }
                    else
                    {
                        comp.Notes = item.Obs.Trim();
                    }
                    listComp.Add(comp);
                }
            }

            return listComp;
        }

        public List<MA_BillOfMaterialsRouting> GetOperationsBOMFromSimulation(string bom, string itemOp, int? Id, bool isAnalize)
        {
            try
            {
                int? id = Id;
                List<MA_BillOfMaterialsRouting> listOp = new List<MA_BillOfMaterialsRouting>();
                List<CS_CustQuotasOperation> opList = new List<CS_CustQuotasOperation>();
                if (isAnalize)
                {
                    opList = serviceCompOp.GetSimulationOperations2(Convert.ToInt32(id), itemOp);
                }
                else
                {
                    opList = serviceCompOp.GetSimulationOperations2_2(Convert.ToInt32(id), itemOp);
                }
                
                if (opList.Count > 0)
                {
                    foreach (var item in opList)
                    {
                        MA_BillOfMaterialsRouting op = new MA_BillOfMaterialsRouting();
                        op.BOM = bom.Trim();
                        double result = 0;
                        op.Operation = item.Operation.Trim();
                        string time = item.TimeProcessStr;
                        if (time != string.Empty)
                        {
                            string[] vTime = time.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            TimeSpan ts = new TimeSpan(Convert.ToInt32(vTime[0]), Convert.ToInt32(vTime[1]), 0);
                            result = ts.TotalMilliseconds;
                            if (result == 0)
                            {
                                op.ProcessingTime = 0;
                            }
                            else
                            {
                                op.ProcessingTime = Convert.ToInt32(result.ToString().Substring(0, result.ToString().Length - 3));
                            }
                        }
                        else
                        {
                            result = 0;
                            op.ProcessingTime = null;
                        }

                        if (string.IsNullOrEmpty(item.Obs))
                        {
                            op.Notes = string.Empty;
                        }
                        else
                        {
                            op.Notes = item.Obs.Trim();
                        }
                        op.SetupAttendancePerc = item.CostOperation;
                        op.WC = item.UoM;
                        op.Qty = item.Qty;
                        listOp.Add(op);
                    }
                }

                return listOp;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
