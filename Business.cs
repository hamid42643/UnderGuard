using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    class Business
    {
        Customer c;

        internal Customer C
        {
            get { return c;}
        }

        public Business()
        {
            //create the customers/orders/suppliers.... objects here

            
         

        }

        public void createCustomer()
        {
            c = new Customer();

        }
    }
}
