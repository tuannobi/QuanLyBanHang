using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace QuanLyBanHang.Utils
{
    public class TokenGenerator
    {
        public static string GenerateToken(int size = 6)
        {
            var crypto = new RNGCryptoServiceProvider();
            byte[] rbytes = new byte[size / 2];
            crypto.GetNonZeroBytes(rbytes);

            return ToHexString(rbytes, true);
        }

        private static string ToHexString(byte[] bytes, bool useLowerCase = false)
        {
            var hex = string.Concat(bytes.Select(b => b.ToString(useLowerCase ? "x2" : "X2")));

            return hex;
        }
    }
}
