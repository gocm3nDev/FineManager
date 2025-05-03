using CsharpProje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpProje.Business
{
    class DatabaseOpsLP // Class for license plate crud process
    {
        DatabaseOpsUser user = new DatabaseOpsUser();

        private DbPPEntities _db;

        public DatabaseOpsLP()
        {
            _db = new DbPPEntities();
        }

        public bool AddLicensePlate(string licensePlate, string tckn)
        {
            var isValidUser = _db.TBL_USERS.Where(x => x.TCKN == tckn).FirstOrDefault();

            if (isValidUser != null)
            {
                int userID = isValidUser.ID;

                var LicensePlate = new TBL_LPLATES { LicensePlate = licensePlate, UserID = userID };
                _db.TBL_LPLATES.Add(LicensePlate);
                _db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public TBL_USERS GetUserByLP(string licensePlate)
        {
            var lp = _db.TBL_LPLATES.Where(x => x.LicensePlate == licensePlate).FirstOrDefault();

            if (lp == null || lp.UserID == null)
                return null;

            var User = user.GetUserById((int)lp.UserID);

            return User;
        }

        public TBL_LPLATES SearchLicensePlate(string licensePlate)
        {
            return _db.TBL_LPLATES.Where(x => x.LicensePlate == licensePlate).FirstOrDefault();
        }

        // İstatistik kısmı

        public int LicensePlateNumbers()
        {
            return _db.TBL_LPLATES.Count();
        }
    }
}
