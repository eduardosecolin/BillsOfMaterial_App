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
        SqlConnection conn;

        public CustQuotasService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
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

        public bool UpdateCostFormationCustQuatas(int id, double? costValue)
        {
            try
            {
                int position = GetPositionOffer(id);
                string costValueStr = costValue.ToString().Replace(",", ".");
                conn.Open();
                string sql = $"UPDATE MA_CustQuotasDetail SET FinalCostFormation = { costValueStr } WHERE CustQuotaId = { id } AND Position = { position }";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
                
            }
            catch (Exception)
            {
                return false;
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

        private int GetPositionOffer(int id)
        {
            try
            {
                int valor = 0;
                conn.Open();
                string sql = $"SELECT Position FROM MA_CustQuotasDetail WHERE CustQuotaId = { id }";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    valor = Convert.ToInt32(result.ToString());
                }
                else
                {
                    valor = 0;
                }
                conn.Close();
                return valor;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetQuotationNo(int id)
        {
            try
            {
                string str = string.Empty;
                conn.Open();
                string sql = $"SELECT QuotationNo FROM MA_CustQuotas WHERE CustQuotaId = { id }";
                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    str = result.ToString();
                }
                conn.Close();
                return str;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
    }
}
