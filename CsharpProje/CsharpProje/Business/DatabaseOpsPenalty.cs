using CsharpProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpProje.Business
{
    class DatabaseOpsPenalty
    {
        DatabaseOpsLP lp = new DatabaseOpsLP();
        DatabaseOpsUser user = new DatabaseOpsUser();

        private DbPPEntities _db;

        public DatabaseOpsPenalty()
        {
            _db = new DbPPEntities();
        }

        public bool AddPenalty(string tckn, string licensePlate, int price)
        {
            var plate = lp.SearchLicensePlate(licensePlate);
            var newPenalty = new TBL_PENALTIES() {UserTCKN = tckn, LicensePlateID = plate.ID, Price = price };

            if (newPenalty != null)
            {
                _db.TBL_PENALTIES.Add(newPenalty);
                _db.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public int PenaltyNumb()
        {
            return _db.TBL_PENALTIES.Count();
        }

        public List<TBL_PENALTIES> GetPenaltiesByTCKN(string tckn)
        {
            var User = _db.TBL_USERS.Where(x => x.TCKN == tckn).FirstOrDefault();

            if (User != null)
                return _db.TBL_PENALTIES.Where(x => x.UserTCKN == User.TCKN).ToList();
            else
                return null;
        }

        public TBL_PENALTIES FindPenaltyById(int id)
        {
            return _db.TBL_PENALTIES.Where(x => x.ID == id).FirstOrDefault();
        }
        public void DeletePenalty(int id)
        {
            var penalty = _db.TBL_PENALTIES.Where(x => x.ID == id).FirstOrDefault();
            _db.TBL_PENALTIES.Remove(penalty);
            _db.SaveChanges();
        }
    }
}
