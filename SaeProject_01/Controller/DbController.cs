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
        // User aus Datenbank lesen und zurückgeben
        public User GetUser(string UserName)
        {
            var context = new UserTableAdapter();
            var Data = context.GetData();
            var User = Data.FirstOrDefault(x => x.Username.Equals(UserName));
            return User == null ? null : new User(User.Username, User.Passwort, true);
        }

        // User in Datenbank speichern
        public void SetUser(User user)
        {
            var context = new UserTableAdapter();
            var Data = context.InsertQuery(user.UserName, user.Passwort);
        }
    }
}
