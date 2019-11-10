using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfMaterial_App.Model;

namespace BillsOfMaterial_App.Service
{
    public class NCMService
    {
        private readonly DBContext _context;

        public NCMService()
        {
            _context = new DBContext();
        }

        public List<MA_BRNCM> GetAll()
        {
            return _context.MA_BRNCM.ToList();
        }

        public List<MA_BRNCM> GetAllByParams(string param)
        {
            return _context.MA_BRNCM.Where(x => x.NCM.Contains(param) || x.Description.Contains(param)).ToList();
        }

        public string GetNCM(string item)
        {
            var sql = _context.MA_ItemsBRTaxes.Where(x => x.Item == item).FirstOrDefault();
            if(sql != null)
            {
                return sql.NCM;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
