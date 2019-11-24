using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillsOfMaterial_App.Model;

namespace BillsOfMaterial_App.Service
{
    public class DefaultObsService
    {
        private readonly DBContext _context;

        public DefaultObsService()
        {
            _context = new DBContext();
        }

        public List<CS_DBDefaultOBS> GetAll(string param)
        {
            if (string.IsNullOrEmpty(param))
            {
                return _context.CS_DBDefaultOBS.ToList();
            }
            else
            {
                return _context.CS_DBDefaultOBS.Where(x => x.Title.Contains(param)).ToList();
            }
        }
    }
}
