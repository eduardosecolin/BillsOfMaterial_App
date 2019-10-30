using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class DrawingService
    {
        private readonly DBContext _context;

        public DrawingService()
        {
            _context = new DBContext();
        }

        public List<MA_Drawings> GetAll()
        {
            return _context.MA_Drawings.OrderBy(x => x.Drawing).ToList();
        }

        public List<MA_Drawings> GetAll(string item)
        {
            return _context.MA_Drawings.Where(x => x.Drawing.Contains(item) || x.Description.Contains(item)).OrderBy(x => x.Drawing).ToList();
        }
    }
}
