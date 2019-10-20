using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class OperationService
    {
        private readonly DBContext _context;

        public OperationService()
        {
            _context = new DBContext();
        }

        public List<MA_Operations> GetAll()
        {
            return _context.MA_Operations.ToList();
        }

        public List<MA_Operations> GetAll(string op)
        {
            return _context.MA_Operations.Where(x => x.Item.Contains(op)).ToList();
        }

        public string GetOperationNotes(string op)
        {
            var sql = _context.MA_Operations.Where(x => x.Operation == op).FirstOrDefault();
            return (string.IsNullOrEmpty(sql.Notes)) ? string.Empty : sql.Notes;
        }

        public string GetDescriptionOp(string op)
        {
            var sql = _context.MA_Operations.Where(x => x.Operation == op).FirstOrDefault();
            return sql.Description;
        }

        public int? GetWorkTimeOperation(string op)
        {
            var sql = _context.MA_OperationsLabour.Where(x => x.Operation == op).FirstOrDefault();
            if(sql != null)
            {
                return sql.WorkingTime;
            }
            else
            {
                return 0;
            }
        }

        public List<MA_OperationsLabour> GetWorkersOperation(string op)
        {
            return _context.MA_OperationsLabour.Where(x => x.Operation == op).ToList();
 
        }
    }
}
