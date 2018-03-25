using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class Contact
    {
        private int id;
        protected String name;
        protected String address;
        protected String phone;
        protected String email;

        private String postalCode;

        public String PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        private String province;

        public String Province
        {
            get { return province; }
            set { province = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        
        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }


    }
}
