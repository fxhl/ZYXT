using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYXT.Common;

namespace ZYXT.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string str= CommonHelper.GenerateCaptchaCode(4);
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
