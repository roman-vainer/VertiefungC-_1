namespace Bibliothek;

public class BuchMethoden
{


    public static void Ausleihen(Buch buch)
    {
        if (!buch.Verliehen)
        {
            buch.Verliehen = true;
            Console.WriteLine($"Bitte geben Sie das Ausleihdatum für das Buch '{buch.Titel}' ein (Format: yyyy-MM-dd):");
            buch.Ausleihdatum = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine($"Das Buch '{buch.Titel}' wurde erfolgreich ausgeliehen.");
        }
        else
        {
            Console.WriteLine($"Das Buch '{buch.Titel}' ist bereits verliehen.");
        }
    }
    public static void Zurueckgeben(Buch buch)
    {
        if (buch.Verliehen)
        {
            buch.Verliehen = false;
            Console.WriteLine($"Bitte geben Sie das Rückgabedatum für das Buch '{buch.Titel}' ein (Format: yyyy-MM-dd):");
            buch.Rueckgabedatum = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine($"Das Buch '{buch.Titel}' wurde erfolgreich zurückgegeben.");
        }
        else
        {
            Console.WriteLine($"Das Buch '{buch.Titel}' war nicht verliehen.");
        }
    }

    

    
   
}