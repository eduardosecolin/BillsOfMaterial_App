using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            if (sql != null)
            {
                return sql.UnitValue;
            }
            else
            {
                return 0;
            }
        }

        public void UpdateCostFormationCustQuatas(int id, int position, double? costValue, string path1, string path2, string path3)
        {
            try
            {
                var sql = _context.MA_CustQuotasDetail.Where(x => x.CustQuotaId == id && x.Position == position).FirstOrDefault();
                if (sql != null)
                {
                    sql.FinalCostFormation = costValue;

                    _context.MA_CustQuotasDetail.AddOrUpdate(sql);
                    _context.SaveChanges();
                }

                if (path1 != string.Empty || path2 != string.Empty || path3 != string.Empty)
                {
                    var sql2 = _context.MA_CustQuotas.Where(x => x.CustQuotaId == id).FirstOrDefault();
                    if (sql2 != null)
                    {
                        sql2.PathFile1 = path1;
                        sql2.PathFile2 = path2;
                        sql2.PathFile3 = path3;

                        _context.MA_CustQuotas.AddOrUpdate(sql2);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
