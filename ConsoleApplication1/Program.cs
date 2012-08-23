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

            // Exempel på en generisk lista av heltal, som fylls på mha .Add
            List<int> intList = new List<int>();
            intList.Add(1);
            intList.Add(5);
            intList.Add(7);
            Console.WriteLine("intList:");
            foreach (var myInt in intList)
                Console.WriteLine("{0}", myInt);

            // Exempel på en generisk lista av strängar, som initieras med värden samtidigt som den skapas.
            // Lägg också märke till att jag använder @ för att skapa en verbatim sträng, vilket i det här fallet
            // innebär att den radbrytning som finns i strängen kommer skrivas ut.
            List<string> stringList = new List<string> { "en sträng", "en annan sträng", "en tredje sträng" };
            Console.WriteLine(@"
stringList:");
            foreach (var myString in stringList)
                Console.WriteLine("{0}", myString);

            // Block 2: Standard Linq Query

            // Exempel på en grundläggande Standard Linq Query
            List<string> names = new List<string>() {
                "", "Andy", "Al", "Alan", "Bert", "Bill", "Bo",
                "Carl", "Chad", "Dale", "Ken", "Elmo", "Erik",
                "Ezra", "Fred", "Gino", "Glen", "Hai", "Omar",
                "Ada", "Aida", "Alex", "Beth", "Bibi", "Cara",
                "Deb", "Dee", "Elin", "Elke", "Gail", "Hedy",
                "Jodi", "Katy", "Lana", "Mimi", "Ria", "Su"
            };

            var query = from n in names // för alla strängar, n, i names
                        where n.StartsWith("El") // filtrera ut de som börjar med "El"
                        orderby n // sortera dem med standardsorteringen för strängar (Alfabetisk ordning)
                        select n; // välj ut de strängar, n, som stämmer in på ovanstående och tilldela den resulternde listan till query

            Console.WriteLine("\nStandard Linq Query String");
            foreach (var q in query)
                Console.WriteLine(q);

            // Exempel på en något 
            List<DateTime> dateList = new List<DateTime> {
                DateTime.Parse("2012-08-13"),
                DateTime.Parse("2012-07-15"),
                DateTime.Parse("2012-08-05"),
                DateTime.Parse("2012-06-15"),
                DateTime.Parse("2012-08-01"),
                DateTime.Parse("2012-08-15")
            };

            // Här nämde jag att DateTime.TryParse oftast är att föredra framför Parse i praktiken, 
            // eftersom Parse kastar Exception om den inte kan tolka strängen

            var dayQuery = from d in dateList // för alla DateTimes, d, i dateList
                           where d.Month == 8 // välj ut alla d i augusti
                           orderby d.Day // ordna dem efter dagen i månaden de infaller
                           select d.Day; // Välj ut dagen i månaden de infaller för alla d som stämmer in på ovanstående -> dayQuery innehålle en lista med heltal, eftersom d.Day är ett heltal

            Console.WriteLine("\nStandard Linq Query with DateTimes");
            foreach (var day in dayQuery.ToList())
                Console.WriteLine("Day of Month: {0}", day);

            // Block 3: Lamba och Delegates

            // Ett Lambda Expression är en funktion utan namn, som tar en in-parameter, bearbetar inparametern och resulterar i en utparameter
            // Lambda-uttrycket "i => i > 4" motsvarar funktionen "bool GreaterThanFour(i) {return i > 4;}"
            // En delegate är en variabel som innehåller en funktion. Vi kommer titta på Func-delegater i denna kursen
            // En Func-delegate har en typ: "Func<T1, T2>", där T1 är typen input parametern hos funktionen som skall sparas i delegaten
            // och T2 är typen på det returnerade värdet 
            //
            // Som ett exempel, Lambda-uttrycket "i => i > 4" tar en integer som input-parameter och returnerar en bool.
            // Detta innebär att ifall vi skall skapa en delegate som skall kunna hålla detta-lambda-uttryck så måste 
            // delegaten ha typen "Func<int, bool>" (int in, bool ut).
            //
            // Lägg märke till att "StringTransformationDelegate" nedan är namnet på en variabel som innehåller en funktion. Funktionen i sig saknar namn

            // Skapar ett lambda uttryck och tilldelar det till en delegate. Applicerar sedan delegaten på ett objekt som överrensstämmer med lambda-uttryckets input typ
            Console.WriteLine("\nLamba & Delegates:");
            Func<string, string> StringTransformationDelegate = s => s.ToUpper();
            string upperCaseString = StringTransformationDelegate("lowerCaseString");
            Console.WriteLine(upperCaseString);

            Func<string, bool> isMoreThan10Chars = s => s.Length > 10;
            Console.WriteLine(isMoreThan10Chars("someString"));

            // Block 4: Method-Based Linq Queries

            // Repo är ett objekt vi använder här för att "simulera" en databas
            // Ni behöver inte bry er om hur GetUsers() och GetPosts() är implementerade - låtsas som att det är data från en databas
            // 
            // Däremot kan det vara intressant att veta vad User och Post innehåller för fält och metoder
            // ifall ni vill experimentera lite med Linq
            Repository Repo = new Repository();
            List<User> users = Repo.GetUsers();
            List<Post> posts = Repo.GetPosts();

            Console.WriteLine("\nUsers ({0}):", users.Count);
            foreach (var user in users.Where(u => u.Type == User.UserType.SuperUser)) // Filtrera ut alla users som är av typen SuperUser
            {
                Console.WriteLine(user.ToString(false));
            }

            // Lista alla posts
            Console.WriteLine("\nPosts ({0}):", posts.Count);
            foreach (var post in posts)
            {
                // LoadUser laddar ett User-objekt till post.CreatedBy - om vi inte kör LoadUser kommer post endast ha ett Guid i post.CreatedByID och CreatedBy kommer vara null
                post.LoadUser(users.ToList()); // Ni behöver inte veta hur LoadUser fungerar - Vi kommer göra detta annorlunda framöver.
                Console.WriteLine(post.ToString());
            }

            // Block 4: Filtered Composite Query

            // Vi vill filtrera listan med Posts ned till de poster som är Taggade med Funny och postade den senaste veckan.

            // Vi börjar med att plocka ut poster som är skapade den senaste veckan
            var filteredQuery = posts.Where(p => p.CreateDate > DateTime.Now.AddDays(-7));
            // p.Tags är en lista av tags. 
            // p.Tags.Any(t => t == Post.PostTags.Funny) returnerar true ifall någon av taggarna i p.Tags-listan är Funny.
            // Hela raden returnerar de posts från den första filtreringen på CreateDate ovan som är taggade med Funny
            filteredQuery = filteredQuery.Where(p => p.Tags.Any(t => t == Post.PostTags.Funny)); 
            Console.WriteLine("\nFunny Posts Last Week:");
            foreach (var post in filteredQuery)
            {
                post.LoadUser(users.ToList());
                Console.WriteLine("\t{0}", post.ToString());
            }

            // Block 4: Select to new type

            // Detta exemplet visar att du kan skapa ett nytt objekt i en select-sats
            // den resulterande listan, postMonthQuery, kommer alltså innehålla objekt av typen Decimal

            var postMonthQuery = posts.OrderBy(p => p.CreateDate).Select(p => new Decimal(p.CreateDate.Month));
            Console.WriteLine("\nMonth of first Post: {0}", postMonthQuery.FirstOrDefault()); // FirstOrDefault() returnerar det första elementet, eller null om postMonthQuery skull vara tom

            // Block 5: Extension Method

            // Målet här är att skapa en extension-method för strängar som returnerar hur många ord en sträng innehåller
            // Implementationen finns i Util.ExtensionMethods.cs

            string aMultiWordString = "This is my multi word string. Nifty!";
            Console.WriteLine("\nWordCount: {0}", aMultiWordString.WordCount());

            Console.ReadLine();
        }
    }
}
