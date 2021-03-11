using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    public class Hashing
    {
        public string Hash(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }

        public bool VerifyHash(string text, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
    }
}