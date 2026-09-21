using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        string s = "";
        s.Equals()
    }

}
public class BestellungArgs: EventArgs
{
    public int Bestellungnummer{ get; set; }
}

public class LagerService() { }
public class KundenService() { }