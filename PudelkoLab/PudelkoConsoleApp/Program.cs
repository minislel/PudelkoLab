using PudelkoLibrary;
namespace PudelkoConsoleApp

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pudelko pudelko1 = new Pudelko();
            Pudelko pudelko2 = new Pudelko(10, 2, 3);
            Pudelko pudelko3 = new Pudelko(23, 423, 44, UnitOfMeasure.milimeter);
            Pudelko pudelko4 = new Pudelko(1, 2);
            Pudelko pudelko5 = new Pudelko(7);
            Pudelko pudelko6 = new Pudelko(3.523, 2.233, 9.422, UnitOfMeasure.meter);
            Pudelko pudelko7 = new Pudelko(14, 43.3, 199, UnitOfMeasure.centimeter);
            Pudelko pudelko8 = new Pudelko(5, 4, 1);
            Pudelko pudelko9 = new Pudelko(10, 10, 10, UnitOfMeasure.centimeter);
            Pudelko pudelko10 = (10000, 2000, 1000);
            List<Pudelko> pudelka = new List<Pudelko>();
            pudelka.Add(pudelko1);
            pudelka.Add(pudelko2);
            pudelka.Add(pudelko3);
            pudelka.Add(pudelko4);
            pudelka.Add(pudelko5);
            pudelka.Add(pudelko6);
            pudelka.Add(pudelko7);
            pudelka.Add(pudelko8);
            pudelka.Add(pudelko9);
            pudelka.Add(pudelko10);
            

            Console.WriteLine("Lista nieposortowana: \n");
            foreach (var item in pudelka) Console.WriteLine($"{item.ToString()}, Objętość: {item.Objetosc}, Pole: {item.Pole}");
            Console.WriteLine("\n Lista posortowana:");
            pudelka.Sort(Pudelko.Compare);
            foreach (var item in pudelka) Console.WriteLine($"{item.ToString()}, Objętość: {item.Objetosc}, Pole: {item.Pole}");
            Console.WriteLine($"\nPorownanie: czy pudelko 1 jest takie samo jak pudelko 2? ");
            string arePudelkaEqual = pudelko1 == pudelko2 ? "Wychodzi na to, ze tak" : "Wychodzi na to, ze nie" ;
            Console.WriteLine(arePudelkaEqual);
            Console.WriteLine($"\nPorownanie: czy pudelko 1 jest takie samo jak pudelko 9? ");
            arePudelkaEqual = pudelko1 == pudelko9 ? "Wychodzi na to, ze tak" : "Wychodzi na to, ze nie";
            Console.WriteLine(arePudelkaEqual);
            Console.WriteLine($"\nLaczenie pudelek 6 i 4:  {pudelko6+pudelko4}");
            double[] dimensions = (double[])pudelko7;
            Console.WriteLine("\nWymiary pudelka 7 w metrach: (metoda 1)\n" + string.Join(", ", dimensions));
            Console.WriteLine("\nWymiary pudelka 7 w metrach: (metoda 2)");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Wymiar {i + 1}: {pudelko7[i]}");
            }
            Console.WriteLine("\nWymiary pudelka 7 w metrach: (metoda 3)");
            foreach (var dimension in pudelko7)
            {
                Console.WriteLine(dimension);
            }
            Console.WriteLine("\nWymiary pudelka 2 w metrach: ");
            Console.WriteLine(pudelko2.ToString("m"));
            Console.WriteLine("\nWymiary pudelka 2 w centymetrach: ");
            Console.WriteLine(pudelko2.ToString("cm"));
            Console.WriteLine("\nWymiary pudelka 2 w milimetrach: ");
            Console.WriteLine(pudelko2.ToString("mm"));
            Pudelko pudelkoKompresja8 = pudelko8.Kompresuj();
            Console.WriteLine("\nSkompresowane pudelko 8: " + pudelkoKompresja8);
            Pudelko parsedPudelko = Pudelko.Parse("3.500 m × 4.200 m × 5.600 m");
            Console.WriteLine("\nParsowanie pudelka ze stringa: \nString wejsciowy: "+ "3.5 m × 4.2 m × 5.6 m\n" + "Sparsowany obiekt.toString(): "+ parsedPudelko);



        }
    }
}
