using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearChoice.Utils
{
    public class GeneratePassword
    {
        public static string Generate()
        {
            Random rnd = new Random();
           

            int myRandomNo = rnd.Next(10000000, 99999999);

       return  myRandomNo.ToString();



        }

    }
}