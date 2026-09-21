using static ChiffreAufgabe.Chiffren;

namespace ChiffreAufgabe
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
        
            string klartext = "Treffpunkt am Hafen";

            ChiffrierVerfahren verfahren = Rueckwaerts;

            Console.WriteLine($"Rueckwaerts: {verfahren(klartext)}");

            verfahren = Chiffren.VokaleZuSterne;
            Console.WriteLine($"VokaleZuSterne: {verfahren(klartext)}");

            verfahren = Chiffren.Leet;
            Console.WriteLine($"Leet: {verfahren(klartext)}");

            Funkzentrale.Senden("Zentrale", "Lage unklar", Chiffren.Rueckwaerts);
            Funkzentrale.Senden("Zentrale", "Lage unklar", Chiffren.Leet);

            Agent falke = new Agent("Falke", Chiffren.Rueckwaerts, Sicherheit.NichtZulang);
            Agent nachtigall = new Agent("Nachtigall", Chiffren.Leet, Sicherheit.KeinKlarname);

            falke.Melden("Alles ruhig");
            falke.Melden("Das Paket liegt hinter der Tuer");
            nachtigall.Melden("Ankunft heute");
            nachtigall.Melden("Ankunft in Moskau");

            string[] tage = ["Montag", "Donnerstag", "Sonntag"];

            foreach (var tag in tage)
            {
                ChiffrierVerfahren tagesschluessel = Funkzentrale.VerfahrenFuerTag(tag);
                Console.WriteLine($"{tag:-12} {tagesschluessel("Lage stabil")}");
            }

            Chiffre spiegel = new Chiffre("Spielen", Chiffren.Rueckwaerts, Chiffren.Rueckwaerts);
            Chiffre zahlencode = new Chiffre("Zahlencode", Chiffren.Leet, Chiffren.LeetZurueck);

            spiegel.Test("Mission erfuellt");
            zahlencode.Test("Mission erfuellt");

            Console.WriteLine("Doppelt: " + Funkzentrale.Doppelt("Basis Nord", Chiffren.Rueckwaerts, Chiffren.Leet));
        }
    }
}
