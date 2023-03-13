using System;
using System.Diagnostics;

namespace FindAndEscape
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Room currentRoom = InitializeRooms(); // Utworzenie początkowego pokoju
            List<Item> inventory = new List<Item>(); // Utworzenie pustej listy przedmiotów w ekwipunku
            Console.WriteLine("Witaj w grze tekstowej!");
            Console.WriteLine("Wpisz 'pomoc' aby zobaczyć dostępne komendy.");
            Console.WriteLine(currentRoom.Description); // Opis początkowego pokoju
            while (true) // Główna pętla gry
            {
                Console.Write("> ");
                string input = Console.ReadLine().ToLower(); // Odczytanie wejścia użytkownika i zamiana na małe litery

                if (input.StartsWith("użyj ")) // Użycie przedmiotu
                {
                    string itemName = input.Substring(5); // Pobranie nazwy przedmiotu po 'użyj '
                    Item item = inventory.Find(i => i.Name.ToLower() == itemName); // Wyszukanie przedmiotu w ekwipunku

                    if (item == null)
                    {
                        Console.WriteLine("Nie masz takiego przedmiotu w ekwipunku.");
                    }
                    else
                    {
                        Console.WriteLine("Używasz {0}.", item.Name);
                        item.Use(currentRoom, inventory); // Użycie przedmiotu na aktualnym pokoju i/lub w ekwipunku
                    }
                }
                else if (input.StartsWith("zabierz ")) // Zabranie przedmiotu
                {
                    string itemName = input.Substring(8); // Pobranie nazwy przedmiotu po 'zabierz '
                    Item item = currentRoom.Items.Find(i => i.Name.ToLower() == itemName); // Wyszukanie przedmiotu w pokoju

                    if (item == null)
                    {
                        Console.WriteLine("Nie ma takiego przedmiotu w pokoju.");
                    }
                    else
                    {
                        Console.WriteLine("Zabierasz {0}.", item.Name);
                        currentRoom.Items.Remove(item); // Usunięcie przedmiotu z pokoju
                        inventory.Add(item); // Dodanie przedmiotu do ekwipunku
                    }
                }
                else if (input == "przedmioty") // Wyświetlenie listy przedmiotów w ekwipunku
                {
                    Console.WriteLine("Ekwipunek:");
                    if (inventory.Count == 0)
                    {
                        Console.WriteLine("- pusty -");
                    }
                    else
                    {
                        foreach (Item item in inventory)
                        {
                            Console.WriteLine("- {0}", item.Name);
                        }
                    }
                }
                else if (input == "pomoc") // Wyświetlenie listy dostępnych komend
                {
                    Console.WriteLine("Dostępne komendy:");
                    Console.WriteLine("- użyj PRZEDMIOT");
                    Console.WriteLine("- zabierz PRZEDMIOT");
                    Console.WriteLine("- przedmioty");
                    Console.WriteLine("- pomoc");
                    Console.WriteLine("- opisz");
                }
                else if (input == "opisz") // Opisanie aktualnego pokoju
                {
                    Console.WriteLine(currentRoom.Description);
                 }
                else // Komenda nieznana
                {
                    Console.WriteLine("Nieznana komenda. Wpisz 'pomoc' aby zobaczyć dostępne komendy.");
                }
            }
        }
        static Room InitializeRooms() // Inicjalizacja początkowego pokoju i przedmiotów
        {
            Room room1 = new Room("Jesteś w pokoju. Widzisz stół, krzesło i drzwi.", null);
            Room room2 = new Room("Jesteś w drugim pokoju. Widzisz kanapę, fotel i okno.", null);

            Item item1 = new Item("Klucz", "Złoty klucz.");
            Item item3 = new Item("Stół. ", "Drewniany stoł");
            Item item2 = new Item("Miecz", "Ostry miecz.");

            room1.Items.Add(item1);
            room1.Items.Add(item3);
            room2.Items.Add(item2);

            room1.ConnectedRooms.Add("wschód", room2);
            room2.ConnectedRooms.Add("zachód", room1);

            return room1;
        }
        class Room // Klasa reprezentująca pokój
        {
            public string Description { get; set; } // Opis pokoju
            public List<Item> Items { get; set; } // Lista przedmiotów w pokoju
            public Dictionary<string, Room> ConnectedRooms { get; set; } // Słownik połączonych pokojów

            public Room(string description, List<Item> items)
            {
                Description = description;
                Items = items ?? new List<Item>();
                ConnectedRooms = new Dictionary<string, Room>();
            }
        }

        class Item // Klasa reprezentująca przedmiot
        {
            public string Name { get; set; } // Nazwa przedmiotu
            public string Description { get; set; } // Opis przedmiotu

            public Item(string name, string description)
            {
                Name = name;
                Description = description;
            }

            public void Use(Room room, List<Item> inventory)
            {
                if (Name == "Klucz" && room.ConnectedRooms.ContainsKey("wschód")) // Użycie klucza do otwarcia drzwi
                {
                    Room nextRoom = room.ConnectedRooms["wschód"];
                    Console.WriteLine("Używasz klucz do otwarcia drzwi.");
                    Console.WriteLine(nextRoom.Description);
                    room = nextRoom;
                }
                else if (Name == "Miecz") // Użycie miecza
                {
                    Console.WriteLine("Używasz miecza. Nic się nie dzieje.");
                }
                else // Nie można użyć tego przedmiotu
                {
                    Console.WriteLine("Nie możesz użyć {0} w tym miejscu.", Name);
                }
            }
        }
    }

    //W powyższej grze gracz znajduje się w początkowym pokoju, który opisany jest jako "Jesteś w pokoju. Widzisz stół, krzesło i drzwi.".
    //Gracz może poruszać się między pokojami, używać przedmiotów i zabierać przedmioty z pokoju.
    //W grze znajdują się dwa pokoje połączone ze sobą drzwiami. W pokoju 1 znajduje się złoty klucz, który może być użyty do otwarcia drzwi prowadzących do
    //drugiego pokoju. W drugim pokoju znajduje się ostry miecz, który jednak nie może być użyty w żaden sposób.

    //Gracz może użyć komendy "opisz" aby otrzymać opis aktualnego pokoju, "przedmioty" aby wyświetlić listę przedmiotów w jego posiadaniu, "zabierz [nazwa przedmiotu]" aby zdobyć przedmiot znajdujący się w aktualnym pokoju i "użyj [nazwa przedmiotu]" aby użyć przedmiotu.

    //Gracz może poruszać się między pokojami za pomocą komendy "idź [kierunek]", gdzie kierunek to "wschód", "zachód", "północ" lub "południe", jeśli taki kierunek istnieje w aktualnym pokoju.

    //Gracz może również użyć komendy "pomoc" aby otrzymać listę dostępnych komend oraz ich opis.
}
