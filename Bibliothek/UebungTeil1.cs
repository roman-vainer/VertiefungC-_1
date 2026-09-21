using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bibliothek;

//### Aufgabe 1.1 ⭐ – Eigener Delegate ohne Rückgabewert

//Deklariere einen Delegate - Typ `BuchAktion`, der zu Methoden mit der Signatur `void Methodenname(Buch buch)` passt.

//Ergänze in `BuchMethoden.cs` eine neue Methode `BuchDrucken(Buch buch)`, die Titel, Autor und Jahr des Buches auf der Konsole ausgibt.

//Weise in `UebungTeil1.Run()` eine Variable vom Typ `BuchAktion` dieser Methode zu und rufe sie für ein selbst erstelltes `Buch`-Objekt auf.

//<details>
//<summary>💡 Hinweis</summary>

//- Delegate-Deklaration z. B.direkt über der `Program`-Klasse oder in einer eigenen Datei `BuchAktion.cs`:
//  `public delegate void BuchAktion(Buch buch);`
//- Zuweisung: `BuchAktion aktion = BuchMethoden.BuchDrucken;`
//- Aufruf: `aktion(meinBuch);`
//</details>

public class UebungTeil1
{
    public delegate void BuchAktion(Buch buch);
    public int i;
    public static void Run(Buch buch)
    {
        BuchAktion bAktion = BuchMethoden.BuchDrucken;
        bAktion(buch);
    }
}
