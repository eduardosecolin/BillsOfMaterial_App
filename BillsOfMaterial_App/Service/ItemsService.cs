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

        public ItemsService()
        {
            _context = new DBContext();
        }

        public List<MA_Items> GetAll()
        {
            return _context.MA_Items.Where(x => x.Disabled == "0").OrderBy(x => x.Item).ToList();
        }

        public List<MA_Items> GetAll(string item)
        {
            return _context.MA_Items.Where(x => x.Item.Contains(item) || x.Description.Contains(item) && x.Disabled == "0").OrderBy(x => x.Item).ToList();
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
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
            if (sql != null)
            {
                return (sql.IsSpecificItem.Equals("1")) ? true : false;
            }
            else
            {
                return false;
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
                return new double?[] { sql.PIS, sql.COFINS, sql.ICMS, sql.IPI, sql.ISS, sql.IR, sql.CSLL };
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
    }
}
