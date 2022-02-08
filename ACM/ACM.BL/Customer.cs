using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACM.BL
{
    public class Customer
    {
        public int CustomerId { get; private set; }

        public string EmailAdress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get 
            {
                string fullName = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                    fullName += ", ";
                fullName += FirstName;
                return fullName;
            }   
        }


    }
}
