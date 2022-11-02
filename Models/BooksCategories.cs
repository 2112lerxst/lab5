using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class BooksCategories : IClassModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public int CategoryId { get; set; }

        public override string ToString()
        {
            return Id + ", " + BookId + ", " + CategoryId;
        }
    }
}
