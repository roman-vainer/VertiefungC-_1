using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EventsMitDelegates;

public class Bestellung
{
    public List<int> Bestellungen { get; }

    public event EventHandler<BestellungArgs>? BestellungStorniert;

    public Bestellung(List<int> bestellungen)

    {
        Bestellungen = bestellungen;
    }
    public void AddBestellung(int bestellunNummer)

    {
        Bestellungen.Add(bestellunNummer);
    }
    public void BestellungStornieren(int bestellungsNummer)
    {
        Bestellungen.Remove(bestellungsNummer);
        OnBestellungStornieren(new BestellungArgs(bestellungsNummer));
    }

    protected void OnBestellungStornieren(BestellungArgs bestellungArgs)
    {
        BestellungStorniert?.Invoke(this, bestellungArgs);
    }
}
public class BestellungArgs : EventArgs
{
    public int Bestellungnummer { get; set; }

    public BestellungArgs(int bestellungnummer)
    {
        Bestellungnummer = bestellungnummer;
    }
}

public class LagerService()
{
    public static void BestandAktualisieren(Object? sender, BestellungArgs args)
    {
        Console.WriteLine($"[LagerService] Die Artikel aus der Bestellung Nr. {args.Bestellungnummer} wurden wieder ins Lager zurückgebucht");
    }
}
public class KundenService() { 
    public static void NachrichtSenden(Object? sender, BestellungArgs args)
    {
        Console.WriteLine($"[KundenService] Ihre Bestellung Nr. {args.Bestellungnummer} wurde storniert");
    }
}