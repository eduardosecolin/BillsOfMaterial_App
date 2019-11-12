using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public bool UpdateCostFormationCustQuatas(int id, int position, double? costValue)
        {
            try
            {
                var sql = _context.MA_CustQuotasDetail.Where(x => x.CustQuotaId == id && x.Position == position).FirstOrDefault();
                if (sql != null)
                {
                    if (double.IsNaN(Convert.ToDouble(costValue)))
                    {
                        MessageBox.Show("O valor de formação de custo é NaN (Not a NUmber) revise o calculo!",
                            "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else
                    {
                        sql.FinalCostFormation = costValue;
                    }

                    _context.MA_CustQuotasDetail.AddOrUpdate(sql);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExistCostFormation(int id, string item)
        {
            var sql = _context.MA_CustQuotasDetail.Where(x => x.CustQuotaId == id && x.Item == item).FirstOrDefault();
            if(sql != null)
            {
                if(sql.FinalCostFormation > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int? GetPositionLine(int id, string item)
        {
            var sql = _context.MA_CustQuotasDetail.Where(x => x.CustQuotaId == id && x.Item == item.Trim()).FirstOrDefault();
            if(sql != null)
            {
                return sql.Position;
            }
            else
            {
                return 0;
            }
        }
    }
}
