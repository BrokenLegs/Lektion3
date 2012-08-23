using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Oftast så lägger man ExtensionMethods samlat på ett specifikt ställe i projektet - Som här i Lektion3.Util
namespace Lektion3.Util
{
    // ExtensionMethods måste definieras i statiska klasser
    public static class ExtensionMethods
    {
        // En ExtensionMethod är statisk och första argumentet är alltid 'this [typen man vill använda metoden på]' - här 'this string'
        // Det går bra att använda ytterligare in-parametrar - de skall inte ha parametern 'this'
        // Anledningen till nyckelordet 'this' är att det som skickas in som första parameter är objektet som metoden anropas från

        // För att denna metod skall vara tillgänglig så behöver "using Lektion3.Util" användas där du vill utnyttja metoden eftersom den är definierad i det namespacet - se namespace ovan
        public static int WordCount(this string myString)
        {
            // Split splittar en sträng till en array av strängar, baserat på den lista av tecken (separatorer) man skickar in - här splittar vi på mellanslag, ' '
            // StringSplitOptions.RemoveEmptyEntries tar bort eventuella tomma strängar, "".
            // Eftersom resultatet av split är en array av ord kan vi köra Length på arrayen för att få vår WordCount
            // Extension-metoden kan användas på en sträng-literal: "hur många ord är detta?".WordCount() eller på en strängvariabel: myString.WordCount()
            return myString.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ).Length;
        }
    }
}
