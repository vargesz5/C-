using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emelterettsegifeladat
{
    internal class Feladatok
    {
        public List<beszelgetes> adatok;
        private List<string> tagok;
        public Feladatok(string filename)
        {
            adatok = new();
            foreach (var item in File.ReadAllLines(filename, Encoding.UTF8).Skip(1))
            {
                string[] parts = item.Split(";");
                DateTime kezdet = DateTime.ParseExact(parts[0], "yy.MM.dd-HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime vege = DateTime.ParseExact(parts[1], "yy.MM.dd-HH:mm:ss", CultureInfo.InvariantCulture);
                string kezdemenyezo = parts[2];
                string fogado = parts[3];
                beszelgetes uj = new(kezdet, vege, kezdemenyezo, fogado);
                adatok.Add(uj);
            }
            tagok = File.ReadAllLines("tagok.txt", Encoding.UTF8).ToList();
        }
        public void Feladat4()
        {
            int tagokSzama = tagok.Count();
            Console.WriteLine($"4. feladat: Tagok Száma: {tagokSzama} fő Beszélgetések: {adatok.Count}db");
        }
        public void Feladat5()
        {
            var leghosszab = adatok.OrderByDescending(x => x.Eltelt).First();
            Console.WriteLine("5. feladat: Leghosszabb beszélgetés adatai:");
            Console.WriteLine($"\t Kezdeményző: {leghosszab.Kezdemenyezo}");
            Console.WriteLine($"\t Fogadó: {leghosszab.Fogado}");
            Console.WriteLine($"\t Kezdet: {leghosszab.Kezdet}");
            Console.WriteLine($"\t Vége: {leghosszab.Veg}");
            Console.WriteLine($"\t Hossz: {leghosszab.Eltelt.TotalSeconds}mp");
        }
        public void Feladat7()
        {
            var uj = tagok.Except(adatok.Select(x => x.Fogado).Union(adatok.Select(x => x.Kezdemenyezo))).ToList();
            Console.WriteLine("7. feladat: Nem beszélgettek senkivel");
            foreach (var item in uj)
            {
                Console.WriteLine($"\t {item}");
            }
        }
        public void Feladat6()
        {
            Console.Write("Adj meg egy tag nevét: ");
            string tag = Console.ReadLine();
            var osszes = adatok.Where(x=>x.Kezdemenyezo == tag || x.Fogado ==tag).ToList();
             TimeSpan osszesBeszelgetes = new(0,0,0);
            foreach (var item in osszes)
            {
                osszesBeszelgetes += item.Eltelt;
            }
            Console.WriteLine($"{osszesBeszelgetes.Hours:D2} {osszesBeszelgetes.Minutes:D2} {osszesBeszelgetes.Seconds:D2}");
        }
    }
}
