using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfMaterial_App.Model;

namespace BillsOfMaterial_App.Service
{
    public class CustQuotasService
    {
        private readonly DBContext _context;

        public CustQuotasService()
        {
            _context = new DBContext();
        }

        public MA_CustQuotas GetById(int id)
        {
            return _context.MA_CustQuotas.Where(x => x.CustQuotaId == id).FirstOrDefault();
        }

        public List<MA_CustQuotasDetail> GetAll(int id)
        {
            return _context.MA_CustQuotasDetail.Where(x => x.CustQuotaId == id).OrderBy(x => x.Position).ToList();
        }

        public List<MA_CustQuotas> GetCustQuota()
        {
            return _context.MA_CustQuotas.ToList();
        }

        public List<MA_CustQuotas> GetByQuotationNo(string no)
        {
            return _context.MA_CustQuotas.Where(x => x.QuotationNo.Contains(no)).ToList();
        }

        public double? GetUnitValueItem(int id, int position)
        {
            var sql = _context.MA_CustQuotasDetail.Where(x => x.CustQuotaId == id && x.Position == position).FirstOrDefault();
            if(sql != null)
            {
                return sql.UnitValue;
            }
            else
            {
                return 0;
            }
        }
    }
}
