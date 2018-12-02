using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SaeProject_01.Models
{
    class User
    {
        public string UserName { get; set; }
        public string Passwort { get; set; }

        public User(string UserName, string Passwort, bool Hashed)
        {
            if(Hashed)
            {
                this.UserName = UserName;
                this.Passwort = Passwort;
            }
            else
            {
                this.UserName = UserName;
                this.Passwort = this.HashPasswort(Passwort);
            }
        }

        private string HashPasswort(string Passwort)
        {
            SHA512Managed shaEncoder = new SHA512Managed();
            byte[] clearBytes = Encoding.ASCII.GetBytes(Passwort);
            byte[] codedBytes = shaEncoder.ComputeHash(clearBytes);
            string hash = null;
            foreach(byte b in codedBytes)
            {
                hash += String.Format("{0:x2}", b);
            }
            return hash;
        }

        public bool checkUser(User otherUser)
        {
            if(otherUser == null)
            {
                return false;
            }
            if(otherUser.UserName.Equals(this.UserName) && otherUser.Passwort.Equals(this.Passwort))
            {
                return true;
            }

            return false;
        }
    }
}
