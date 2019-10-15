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

        public BOMService()
        {
            _context = new DBContext();
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
            return _context.MA_BillOfMaterials.Where(x => x.BOM.Contains(bom)).ToList();
        }
    }
}
