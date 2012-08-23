using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lektion3.Model;
using Lektion3.Model.Repository;
using Lektion3.Util;

namespace Lektion3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Saker vi gick igenom under lektion 3

            // Block 1 - Generic List

            List<int> intList = new List<int>();
            intList.Add(1);
            intList.Add(5);
            intList.Add(7);
            Console.WriteLine("intList:");
            foreach (var myInt in intList)
                Console.WriteLine("{0}", myInt);

            List<string> stringList = new List<string> { "en sträng", "en annan sträng", "en tredje sträng" };
            Console.WriteLine(@"
stringList:");
            foreach (var myString in stringList)
                Console.WriteLine("{0}", myString);

            // Block 2: Standard Linq Query

            List<string> names = new List<string>() {
                "", "Andy", "Al", "Alan", "Bert", "Bill", "Bo",
                "Carl", "Chad", "Dale", "Ken", "Elmo", "Erik",
                "Ezra", "Fred", "Gino", "Glen", "Hai", "Omar",
                "Ada", "Aida", "Alex", "Beth", "Bibi", "Cara",
                "Deb", "Dee", "Elin", "Elke", "Gail", "Hedy",
                "Jodi", "Katy", "Lana", "Mimi", "Ria", "Su"
            };

            var query = from n in names
                        where n.StartsWith("El")
                        orderby n
                        select n;

            Console.WriteLine("\nStandard Linq Query String");
            foreach (var q in query)
                Console.WriteLine(q);

            List<DateTime> dateList = new List<DateTime> {
                DateTime.Parse("2012-08-13"),
                DateTime.Parse("2012-07-15"),
                DateTime.Parse("2012-08-05"),
                DateTime.Parse("2012-06-15"),
                DateTime.Parse("2012-08-01"),
                DateTime.Parse("2012-08-15")
            };

            var dayQuery = from d in dateList
                           where d.Month == 8
                           orderby d.Day
                           select d.Day;

            Console.WriteLine("\nStandard Linq Query with DateTimes");
            foreach (var day in dayQuery.ToList())
                Console.WriteLine("Day of Month: {0}", day);

            // Block 3: Lamba och Delegates

            Console.WriteLine("\nLamba & Delegates:");
            Func<string, string> toUpper = s => s.ToUpper();
            string upperCaseString = toUpper("lowerCaseString");
            Console.WriteLine(upperCaseString);

            Func<string, bool> isMoreThan10Chars = s => s.Length > 10;
            Console.WriteLine(isMoreThan10Chars("someString"));

            // Block 4: Method-Based Linq Queries

            Repository Repo = new Repository();
            List<User> users = Repo.GetUsers();
            List<Post> posts = Repo.GetPosts();

            Console.WriteLine("\nUsers ({0}):", users.Count);
            foreach (var user in users.Where(u => u.Type == User.UserType.SuperUser))
            {
                Console.WriteLine(user.ToString(false));
            }

            Console.WriteLine("\nPosts ({0}):", posts.Count);
            foreach (var post in posts)
            {
                post.LoadUser(users.ToList());
                Console.WriteLine(post.ToString());
            }

            // Block 4: Filtered Composite Query

            var filteredQuery = posts.Where(p => p.CreateDate > DateTime.Now.AddDays(-7));
            filteredQuery = filteredQuery.Where(p => p.Tags.Any(t => t == Post.PostTags.Funny));
            Console.WriteLine("\nFunny Posts Last Week:");
            foreach (var post in filteredQuery)
            {
                post.LoadUser(users.ToList());
                Console.WriteLine("\t{0}", post.ToString());
            }

            // Block 4: Select to new type

            var postMonthQuery = posts.OrderBy(p => p.CreateDate).Select(p => new Decimal(p.CreateDate.Month));
            Console.WriteLine("\nMonth of first Post: {0}", postMonthQuery.FirstOrDefault());

            // Block 5: Extension Method

            string aMultiWordString = "This is my multi word string. Nifty!";
            Console.WriteLine("\nWordCount: {0}", aMultiWordString.WordCount());

            Console.ReadLine();
        }
    }
}
