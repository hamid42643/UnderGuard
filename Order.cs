using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public class Order
    {
        protected DataAccess da;
        private int id;
        private Decimal totalPrice;
        private DateTime date;
        private String status;
        //private DateTime dateDelivery;

        //public DateTime DateDelivery
        //{
        //    get { return dateDelivery; }
        //    set { dateDelivery = value; }
        //}

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Decimal TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public DateTime OrderDate
        {
            get { return date; }
            set { date = value; }
        }

        public String Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
