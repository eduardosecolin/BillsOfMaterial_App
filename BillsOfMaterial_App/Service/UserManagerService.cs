using BillsOfMaterial_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsOfMaterial_App.Service
{
    public class UserManagerService
    {

        private readonly DBContext _context;

        public UserManagerService()
        {
            _context = new DBContext();
        }

        public bool ExistData(string user, string pwd)
        {
            try
            {
                var sql = _context.CS_UserManager.Where(x => x.UserName == user && x.Password == pwd).FirstOrDefault();
                if(sql != null)
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

        public int GetTypeUser(string user, string pwd)
        {
            try
            {
                var sql = _context.CS_UserManager.Where(x => x.UserName == user && x.Password == pwd).FirstOrDefault();
                if(sql != null)
                {
                    return Convert.ToInt32(sql.UserManagerType);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CS_UserManager GetUserObj(string user, string pwd)
        {
            return _context.CS_UserManager.Where(x => x.UserName == user && x.Password == pwd).FirstOrDefault();
        }

    }
}
