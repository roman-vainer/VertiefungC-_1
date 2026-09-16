namespace Bibliothek;

public class Program
{
    public static void Main(string[] args)
    {
        Buch HarryPotter =
        new Buch("Harry Potter und der Stein der Weisen", "J.K. Rowling", 1997, "978-3-551-35701-3", 0.45m);
        Buch HerrDerRinge =
        new Buch("Der Herr der Ringe", "J.R.R. Tolkien", 1954, "978-3-551-35701-4", 0.55m);


        Comparison<Buch> cbuch = (Buch buch1, Buch buch2) => (buch1.Titel.Equals(buch2.Titel)) ? 0 : 1;

        Console.WriteLine(cbuch(HarryPotter, HerrDerRinge));


        // Console.WriteLine(HarryPotter.Autor);

        Action<Buch> dbuch = BuchMethoden.Ausleihen;
        dbuch += BuchMethoden.Zurueckgeben;
        dbuch += Buchverwaltung.Preisberechnen;
        dbuch += (Buch buch) => Console.WriteLine($"Schnelle Lambda Funktion");

        Func<Buch, int> fbuch = (Buch buch) => buch.Rueckgabedatum.Day - buch.Ausleihdatum.Day;


        //Ergebnis muss ein bool wert rauskommen
        Predicate<Buch> pbuch = (Buch buch) => buch.IstVerliehen();
        pbuch += (Buch buch) => int.TryParse(buch.ISBN.Substring(0, 3), out int result);
        pbuch += (Buch buch) => buch.Rueckgabedatum.Day - buch.Ausleihdatum.Day > 10;
        pbuch += (Buch buch) => fbuch(buch) > 10;




        Buchverwaltung.Prozess(HarryPotter, dbuch, fbuch, pbuch);




    }



}