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

        // Konstruktor, Username wird gesetz Passwort wird gehasht
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

        // Hash Passwort mit SHA512 für sichere Speicherung in Datenbank
        private string HashPasswort(string Passwort)
        {
            var shaEncoder = new SHA512Managed();
            var clearBytes = Encoding.ASCII.GetBytes(Passwort);
            var codedBytes = shaEncoder.ComputeHash(clearBytes);
            return codedBytes.Aggregate<byte, string>(null, (current, b) => current + $"{b:x2}");
        }

        //Checkt ob Username und PasswortHash von zwei Usern übereinstimmen
        public bool checkUser(User otherUser)
        {
            if(otherUser == null)
            {
                return false;
            }
            return otherUser.UserName.Equals(this.UserName) && otherUser.Passwort.Equals(this.Passwort);
        }
    }
}
