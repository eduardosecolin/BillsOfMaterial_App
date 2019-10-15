using System;
using System.Collections.Generic;
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
            return _context.MA_Items.Where(x => x.Disabled == "0").ToList();
        }

        public List<MA_Items> GetAll(string item)
        {
            return _context.MA_Items.Where(x => x.Item.Contains(item) && x.Disabled == "0").ToList();
        }

        public string GetUoMItem(string item)
        {
            var sql = _context.MA_Items.Where(x => x.Item == item).FirstOrDefault();
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
            if(sql != null)
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
            if(sql != null)
            {
                return sql.BasePrice;
            }
            else
            {
                return 0;
            }
        }
    }
}
