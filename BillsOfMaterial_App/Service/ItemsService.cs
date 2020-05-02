using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfMaterial_App.Model;

namespace BillsOfMaterial_App.Service
{
    public class ItemsService
    {
        private readonly DBContext _context;
        SqlConnection conn;

        public ItemsService()
        {
            _context = new DBContext();
            conn = new SqlConnection(_context.Database.Connection.ConnectionString);
        }

        public List<MA_Items> GetAll()
        {
            List<MA_Items> list = new List<MA_Items>();
            //return _context.MA_Items.Where(x => x.Disabled == "0").OrderBy(x => x.Item).ToList();
            try
            {
                conn.Open();
                string sql = @"SELECT
	                            I.Item,
	                            I.Description,
	                            I.BaseUoM,
	                            I.ItemType,
	                            F.StandardCost
                            FROM
	                            MA_Items I
	                            LEFT OUTER JOIN MA_ItemsFiscalYearData F ON F.Item = I.Item
                            WHERE
	                            I.Disabled = 0
                                AND F.FiscalYear = YEAR(GETDATE())
                            ORDER BY
	                            I.Item";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MA_Items item = new MA_Items();
                    item.Item = reader["Item"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.BaseUoM = reader["BaseUoM"].ToString();
                    item.ItemType = reader["ItemType"].ToString();
                    item.BasePrice = Convert.ToDouble(reader["StandardCost"]);
                    list.Add(item);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<MA_Items> GetAll(string item)
        {
            List<MA_Items> list = new List<MA_Items>();
            //return _context.MA_Items.Where(x => x.Item.Contains(item) || x.Description.Contains(item) && x.Disabled == "0").OrderBy(x => x.Item).ToList();
            try
            {
                conn.Open();
                string sql = @"SELECT
	                            I.Item,
	                            I.Description,
	                            I.BaseUoM,
	                            I.ItemType,
	                            F.StandardCost
                            FROM
	                            MA_Items I
	                            LEFT OUTER JOIN MA_ItemsFiscalYearData F ON F.Item = I.Item
                            WHERE
	                            I.Disabled = 0
                                AND I.Description LIKE '%" + item + "%'" +
                            @"ORDER BY
	                            I.Item";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MA_Items items = new MA_Items();
                    items.Item = reader["Item"].ToString();
                    items.Description = reader["Description"].ToString();
                    items.BaseUoM = reader["BaseUoM"].ToString();
                    items.ItemType = reader["ItemType"].ToString();
                    items.BasePrice = Convert.ToDouble(reader["StandardCost"]);
                    list.Add(items);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public List<MA_Items> GetAll2(string item)
        {
            List<MA_Items> list = new List<MA_Items>();
            //return _context.MA_Items.Where(x => x.Item.Contains(item) || x.Description.Contains(item) && x.Disabled == "0").OrderBy(x => x.Item).ToList();
            try
            {
                conn.Open();
                string sql = @"SELECT
	                            I.Item,
	                            I.Description,
	                            I.BaseUoM,
	                            I.ItemType,
	                            F.StandardCost
                            FROM
	                            MA_Items I
	                            LEFT OUTER JOIN MA_ItemsFiscalYearData F ON F.Item = I.Item
                            WHERE
	                            I.Disabled = 0
                                AND I.Item LIKE '%" + item + "%'" +
                              @"AND F.FiscalYear = YEAR(GETDATE())
                            ORDER BY
	                            I.Item";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MA_Items items = new MA_Items();
                    items.Item = reader["Item"].ToString();
                    items.Description = reader["Description"].ToString();
                    items.BaseUoM = reader["BaseUoM"].ToString();
                    items.ItemType = reader["ItemType"].ToString();
                    items.BasePrice = Convert.ToDouble(reader["StandardCost"]);
                    list.Add(items);
                }

                conn.Close();
                reader.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public string GetUoMItem(string item)
        {
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
            if(sql == null)
            {
                return string.Empty;
            }
            return sql.BaseUoM;
        }

        public int? GetNatureItem(string item)
        {
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
            return sql.Nature;
        }

        public string GetDescriptionItem(string item)
        {
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
            return sql.Description;
        }

        public List<MA_Items> GetAllOldItemByParam(string oldItem)
        {
            return _context.MA_Items.Where(x => x.OldItem.Contains(oldItem)).ToList();
        }

        public List<MA_Items> GetAllOldItem()
        {
            return _context.MA_Items.Where(x => x.Disabled == "0" && x.OldItem != string.Empty).ToList();
        }

        public bool IsSpecificItem(string item)
        {
            try
            {
                var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
                if (sql != null)
                {
                    if (!string.IsNullOrEmpty(sql.IsSpecificItem))
                    {
                        return (sql.IsSpecificItem.Equals("1")) ? true : false;
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public double? GetBasePrice(string item)
        {
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return sql.BasePrice;
            }
            else
            {
                return 0;
            }
        }

        public double? GetStandardCost(string item)
        {
            var sql = _context.MA_ItemsFiscalYearData.Where(x => x.Item == item && x.FiscalYear == DateTime.Now.Year).FirstOrDefault();
            if (sql != null)
            {
                return sql.StandardCost;
            }
            else
            {
                return 0;
            }
        }

        public double? GetSpecificWeight(string item)
        {
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
            if(sql != null)
            {
                return sql.SpecificWeight;                   
            }
            else
            {
                return 0;
            }
        }

        public double?[] GetTaxValues(string item)
        {
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
            if(sql != null)
            {
                return new double?[] { sql.PIS, sql.COFINS, sql.ICMS, sql.IPI, sql.ISS, sql.IR, sql.CSLL, sql.COMISSION };
            }
            else
            {
                return null;
            }
        }

        public bool ExistItem(string item)
        {
            if(_context.MA_Items.Any(x => x.Item == item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MA_Items GetItem(string item)
        {
            return _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
        }

        public MA_ItemsFiscalYearData GetItemFiscalYearData(string item)
        {
            return _context.MA_ItemsFiscalYearData.Where(x => x.Item == item && x.FiscalYear == DateTime.Now.Year).FirstOrDefault();
        }

        public MA_ItemsBRTaxes GetItemBRTaxes(string item)
        {
            return _context.MA_ItemsBRTaxes.Where(x => x.Item == item).FirstOrDefault();
        }

        public bool AddItem(MA_Items item)
        {
            _context.MA_Items.Add(item);
            _context.SaveChanges();
            return true;
        }

        public bool EditItem(MA_Items item)
        {
            _context.MA_Items.AddOrUpdate(item);
            _context.SaveChanges();
            return true;
        }

        public bool AddItemStandardCost(MA_ItemsFiscalYearData item)
        {
            _context.MA_ItemsFiscalYearData.Add(item);
            _context.SaveChanges();
            return true;
        }

        public bool EditItemStandardCost(MA_ItemsFiscalYearData item)
        {
            _context.MA_ItemsFiscalYearData.AddOrUpdate(item);
            _context.SaveChanges();
            return true;
        }

        public bool AddItemNCM(MA_ItemsBRTaxes item)
        {
            _context.MA_ItemsBRTaxes.Add(item);
            _context.SaveChanges();
            return true;
        }

        public bool EditItemNCM(MA_ItemsBRTaxes item)
        {
            _context.MA_ItemsBRTaxes.AddOrUpdate(item);
            _context.SaveChanges();
            return true;
        }

        public double GetBaseUoMQty(string item)
        {
            try
            {
                conn.Open();
                double value = 0;
                string sql = @"SELECT TOP 1
	                                ISNULL(U.BaseUoMQty, 1) AS BaseUoMQty
                                FROM
	                                MA_Items I
	                                LEFT OUTER JOIN MA_ItemsComparableUoM U ON U.Item = I.Item
                                WHERE
	                                I.Item = '" + item.Trim() + "'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                var result = cmd.ExecuteScalar();
                if(result != null)
                {
                    value = Convert.ToDouble(result.ToString());
                }
                else
                {
                    value = 0;
                }

                conn.Close();
                return value;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
    }
}
