using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class Users
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserAddress { get; set; }

        public string OtherUserDetails { get; set; }

        public double AmountOfFine { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return Id + ", " + UserName + ", " + UserAddress + ", " + OtherUserDetails + ", " + AmountOfFine + ", " + Email + ", " + PhoneNumber;
        }
    }
}
