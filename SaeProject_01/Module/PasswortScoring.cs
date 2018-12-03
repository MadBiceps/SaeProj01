using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SaeProject_01.Module
{
    public static class PasswortScoring
    {
        // Bewertet das Passwort
        public static int CheckStrength(string password)
        {
            var score = 0;

            if (password.Length < 1)
                return score;
            if (password.Length < 4)
                return score;
            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (Regex.Match(password, @"\d+").Success)
                score++;
            if (Regex.Match(password, @"[a-z]+").Success &&
                Regex.Match(password, @"[A-Z]+").Success)
                score++;
            if (Regex.Match(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]").Success)
                score++;
            return score;
        }
    }
}
