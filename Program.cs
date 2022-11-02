using QueryBuilder.Models;

namespace QueryBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var database = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            database += "\\Data\\Library.db";

            
            List<Author> authors;
            using (var qb = new QueryBuilder(database))
            {
                
                var sk = new Author(99, "Stephen", "King");
                qb.Create<Author>(sk);

                authors = qb.ReadAll<Author>();
                var readOne = qb.Read<Author>(99);

                sk.FirstName = "Stephen";
                sk.Surname = "King";
                qb.Update(sk);



                qb.Delete<Author>(sk);
            }
            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }
        }
    }
}