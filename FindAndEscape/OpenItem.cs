using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAndEscape
{
    internal class OpenItem
    {
        public void openItem()
        {
            Console.WriteLine("Jeśli jesteś w pokoju pierwszym możesz otworzyć komodę. Jeśli jesteś w pokoju drugim możesz otworzyć okono");
            string name = Console.ReadLine();

            if (name == "otwórz komoda")
            {
                Console.WriteLine("Otwierasz komodę i znajdujesz klucz. Możesz go zabrać i użyć");
            }
            else if (name == "otwórz okno")
            {
                Console.WriteLine("UCIEKASZ!!!");
            }
            else
            {
                Console.WriteLine("Nie wybrałeś przedmiotu");
            }
        }
    }
}
