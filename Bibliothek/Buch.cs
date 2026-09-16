namespace Bibliothek;

public class Buch
{
    public string Titel { get; set; }
    public string Autor { get; set; }
    public int Jahr { get; set; }
    public string ISBN { get; set; }
    public DateTime Ausleihdatum { get; set; }
    public DateTime Rueckgabedatum { get; set; }

    public decimal PreisProTag { get; set; }
    public bool Verliehen { get; set; } = false;


    public Buch(string titel, string autor, int jahr, string isbn, decimal preisProTag)
    {
        Titel = titel;
        Autor = autor;
        Jahr = jahr;
        ISBN = isbn;
        PreisProTag = preisProTag;
        Verliehen = false;
    }

    public override string ToString()
    {
        return $"{Titel} von {Autor}, veröffentlicht im Jahr {Jahr}";
    }

    public bool IstVerliehen()
    {
        return Verliehen;
    }

}