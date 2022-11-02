using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class Books : IClassModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public DateTime DateOfPublication { get; set; }

        public override string ToString()
        {
            return Id + ", " + Title + ", " + Isbn + ", " + DateOfPublication;
        }
    }
}
