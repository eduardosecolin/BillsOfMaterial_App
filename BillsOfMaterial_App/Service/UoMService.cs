using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class UoMService
    {
        private readonly DBContext _context;

        public UoMService()
        {
            _context = new DBContext();
        }

        public List<MA_UnitsOfMeasure> GetAll()
        {
            return _context.MA_UnitsOfMeasure.ToList();
        }

        public List<MA_UnitsOfMeasure> GetAllByParams(string param)
        {
            return _context.MA_UnitsOfMeasure.Where(x => x.BaseUoM.Contains(param) || x.Description.Contains(param)).ToList();
        }
    }
}
