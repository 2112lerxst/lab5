using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class BooksOutOnLoan : IClassModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public DateTime DateIssued { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime DateReturned { get; set; }

        public override string ToString()
        {
            return Id + ", " + BookId + ", " + UserId + ", " + DateIssued + ", " + DueDate + ", " + DateReturned;
        }
    }
}
