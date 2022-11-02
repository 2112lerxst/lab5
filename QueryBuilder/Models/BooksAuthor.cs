using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class BooksAuthor : IClassModel
    {
        public int Id { get; set; }

        public int BookID { get; set; }

        public int AuthorID { get; set; }

        public override string ToString()
        {
            return Id + ", " + BookID + ", " + AuthorID;
        }
    }
}
