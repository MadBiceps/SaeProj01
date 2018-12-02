using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaeProject_01.Models;
using System.Data.OleDb;
using SaeProject_01.Datasets;
using SaeProject_01.Datasets.UserDataSetTableAdapters;

namespace SaeProject_01.Controller
{
    class DbController
    {
        public User GetUser(string UserName)
        {
            UserTableAdapter context = new UserTableAdapter();
            var Data = context.GetData();
            var User = Data.Where(x => x.Username.Equals(UserName)).FirstOrDefault();
            if(User == null)
            {
                return null;
            }
            return new User(User.Username, User.Passwort, true);
        }

        public void SetUser(User user)
        {
            UserTableAdapter context = new UserTableAdapter();
            var Data = context.InsertQuery(user.UserName, user.Passwort);
        }
    }
}
