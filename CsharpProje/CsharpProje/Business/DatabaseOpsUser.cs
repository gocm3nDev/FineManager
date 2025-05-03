using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpProje.Models;

namespace CsharpProje.Business
{
    class DatabaseOpsUser
    {
        private DbPPEntities _dbPPEntities;

        public DatabaseOpsUser()
        {
            _dbPPEntities = new DbPPEntities();
        }

        public void AddUser(string name, string surname, string tckn, string tel, byte auth)
        {
            var user = new TBL_USERS { Name = name, Surname = surname, TCKN = tckn, Telephone = tel, Authority = auth };
            _dbPPEntities.TBL_USERS.Add(user);
            _dbPPEntities.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var user = _dbPPEntities.TBL_USERS.Where(x => x.ID == id).FirstOrDefault();
            _dbPPEntities.TBL_USERS.Remove(user);
            _dbPPEntities.SaveChanges();
        }
        public List<TBL_USERS> ListUsers()
        {
            return _dbPPEntities.TBL_USERS.ToList();
        }
        public List<TBL_USERS> GetUserByTCKN(string tckn)
        {
            return _dbPPEntities.TBL_USERS.Where(x => x.TCKN == tckn).ToList();
        }
        public TBL_USERS GetUserById(int id)
        {
            return _dbPPEntities.TBL_USERS.Where(x => x.ID == id).FirstOrDefault();
        }
        public bool QueryToUser(string tckn, string name, string surname)
        {
            bool Query = _dbPPEntities.TBL_USERS.Any(x => x.Name == name && x.Surname == surname && x.TCKN == tckn);

            return Query;
        }
        public bool UserAuth(string tckn)
        {
            var user = _dbPPEntities.TBL_USERS.Where(x => x.TCKN == tckn).FirstOrDefault();

            return Convert.ToBoolean(user.Authority);
        }

        // İstatistik kısmı

        public int CitizenNumb()
        {
            return _dbPPEntities.TBL_USERS.Where(x => x.Authority == 0).Count();
        }
        public int AuthPersonNumb()
        {
            return _dbPPEntities.TBL_USERS.Where(x => x.Authority == 1).Count();
        }
    }
}
