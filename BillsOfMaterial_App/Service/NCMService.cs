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
    }
}
