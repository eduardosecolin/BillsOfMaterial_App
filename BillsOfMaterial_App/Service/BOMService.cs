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

        public BOMService()
        {
            _context = new DBContext();
            serviceCompOp = new CustQuotaCompOpService();
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
            List<string> listStr = serviceCompOp.GetDistinctItemBOM();
            if (listStr.Count > 0)
            {
                foreach (var item in listStr)
                {
                    MA_BillOfMaterials bom = new MA_BillOfMaterials();
                    bom.BOM = item.Trim();
                    bom.Description = GetDescriptionBOM(item.Trim());
                    bom.UoM = GetUoMBOM(item.Trim());
                    bom.LastModificationDate = serviceCompOp.GetTBModifiedFromSimulationCompBOM(item.Trim());
                    bom.CreationDate = serviceCompOp.GetTBCreatedFromSimulationCompBOM(item.Trim());
                    listBOM.Add(bom);
                }
            }

            return listBOM;
        }

        public List<MA_BillOfMaterialsComp> GetComponentsBOMFromSimulation(string bom, string itemComp)
        {
            int? id = serviceCompOp.GetIdFromSimulationCompBOM(bom);
            List<MA_BillOfMaterialsComp> listComp = new List<MA_BillOfMaterialsComp>();
            List<CS_CustQuotasComponent> compList = serviceCompOp.GetSimulationComponents2(Convert.ToInt32(id), itemComp);
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
                    if(item.Obs == null)
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

        public List<MA_BillOfMaterialsRouting> GetOperationsBOMFromSimulation(string bom, string itemOp)
        {
            int? id = serviceCompOp.GetIdFromSimulationOpBOM(bom);
            List<MA_BillOfMaterialsRouting> listOp = new List<MA_BillOfMaterialsRouting>();
            List<CS_CustQuotasOperation> opList = serviceCompOp.GetSimulationOperations2(Convert.ToInt32(id), itemOp);
            if (opList.Count > 0)
            {
                foreach (var item in opList)
                {
                    MA_BillOfMaterialsRouting op = new MA_BillOfMaterialsRouting();
                    op.BOM = bom.Trim();
                    op.Operation = item.Operation.Trim();
                    string time = Convert.ToDateTime(item.TimeProcess).ToShortTimeString();
                    string[] vTime = time.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    TimeSpan ts = new TimeSpan(Convert.ToInt32(vTime[0]), Convert.ToInt32(vTime[1]), 0);
                    double result = ts.TotalMilliseconds;
                    op.ProcessingTime = Convert.ToInt32(result.ToString().Substring(0, result.ToString().Length - 3)); ;
                    if (item.Obs == null)
                    {
                        op.Notes = string.Empty;
                    }
                    else
                    {
                        op.Notes = item.Obs.Trim();
                    }
                    listOp.Add(op);
                }
            }

            return listOp;
        }


    }
}
