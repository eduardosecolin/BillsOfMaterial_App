using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class CustQuotasCompAttachService
    {
        private readonly DBContext _context;

        public CustQuotasCompAttachService()
        {
            _context = new DBContext();
        }

        public void SaveOrUpdateAttachToSimulation(CS_CustQuotasCompAttach attach)
        {
            try
            {
                if (ExistAttachDataToSave(attach))
                {
                    attach.TBModified = DateTime.Now;
                    attach.TBModifiedID = attach.CustQuotasCompId;
                    _context.CS_CustQuotasCompAttach.AddOrUpdate(attach);
                }
                else
                {
                    attach.Line = GetMaxLine();
                    attach.TBCreated = DateTime.Now;
                    attach.TBModified = DateTime.Now;
                    attach.TBCreatedID = attach.CustQuotasCompId;
                    attach.TBModifiedID = attach.CustQuotasCompId;
                    _context.CS_CustQuotasCompAttach.Add(attach);
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetMaxLine()
        {
            try
            {
                return _context.CS_CustQuotasCompAttach.DefaultIfEmpty().Max(x => x == null ? 0 : x.Line) + 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ExistAttachDataToSave(CS_CustQuotasCompAttach attach)
        {
            try
            {
                if (_context.CS_CustQuotasCompAttach.Any(x => x.CustQuotasCompId == attach.CustQuotasCompId && x.Line == attach.Line))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExistAttachDataToLoad(int id)
        {
            try
            {
                if (_context.CS_CustQuotasCompAttach.Any(x => x.CustQuotasCompId == id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CS_CustQuotasCompAttach> GetAttchById(int id)
        {
            try
            {
                return _context.CS_CustQuotasCompAttach.Where(x => x.CustQuotasCompId == id).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
