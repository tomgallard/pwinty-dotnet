using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Pwinty.Client
{
    public class Md5HashCalculator
    {
        public static string MD5HashFile(Stream fileData)
        {
            byte[] hash = MD5.Create().ComputeHash(fileData);
            fileData.Position = 0;
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
