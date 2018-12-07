using ClearChoice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearChoice.DAL
{
    public class SingletonContext
    {
        private static Context context;
       public static Context GetContext()
        {
            if (context == null)
            {
                context = new Context();
            }

            return context;
        }
    }
}