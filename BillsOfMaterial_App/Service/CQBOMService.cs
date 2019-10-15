using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class CQBOMService
    {
        private readonly DBContext _context;

        public CQBOMService()
        {
            _context = new DBContext();
        }

        public List<CS_CQBOM> GetNullList()
        {
            List<CS_CQBOM> list = new List<CS_CQBOM>();
            int cont = 0;
            while (cont < 50)
            {
                CS_CQBOM entity = new CS_CQBOM();
                entity.BOM = string.Empty;
                entity.CustQuotaId = 0;
                entity.Description = string.Empty;
                entity.DescriptionOperation = string.Empty;
                entity.Line = 0;
                entity.OBSOperation = string.Empty;
                entity.OBSSemiFinished = string.Empty;
                entity.Operation = string.Empty;
                entity.Qty = 0;
                entity.SemiFinished = string.Empty;
                entity.TimeProcess = null;
                entity.UoM = string.Empty;

                list.Add(entity);
                cont++;
            }

            return list;           
        }

        public void Save(List<CS_CQBOM> cqBoms)
        {
            foreach (var item in cqBoms)
            {
                _context.CS_CQBOM.Add(item);
            }
            _context.SaveChanges();
        }
    }
}
