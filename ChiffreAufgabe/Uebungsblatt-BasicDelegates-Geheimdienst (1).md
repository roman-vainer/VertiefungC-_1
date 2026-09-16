<!-- Aufgabenblatt — Teilnehmerversion. Lösungen: uebung-delegates-geheimdienst-LOESUNGEN.md -->

# Übung: Operation Chiffre — eigene Delegate-Typen

**Thema:** Eigene Delegate-Typen deklarieren, zuweisen, übergeben, in Klassen speichern und zurückgeben
**Voraussetzungen:** Klassen, Konstruktoren, Felder, Methoden, `string`-Methoden, `switch`
**Nicht nötig:** Multicast (`+=`), anonyme Methoden, Lambdas, `Action`/`Func`/`Predicate`
**Bearbeitungszeit:** ca. 120 Minuten

---

## Szenario

Du baust die Funkzentrale eines Nachrichtendienstes. Jeder Agent verschlüsselt seine
Meldungen mit einem eigenen Verfahren, und die Zentrale muss Meldungen weitergeben,
ohne zu wissen, **welches** Verfahren gerade benutzt wird. Genau dafür sind Delegates da:
Du übergibst nicht das Ergebnis einer Methode, sondern die Methode selbst.

---

## Merkzettel

```csharp
public delegate string ChiffrierVerfahren(string klartext);   // Deklaration = nur die Signatur

ChiffrierVerfahren verfahren = Chiffren.Rueckwaerts;          // Zuweisung ohne Klammern
string geheim = verfahren("Hallo");                            // Aufruf
```

Ein Delegate-Typ darf überall stehen, wo ein normaler Typ stehen darf:
als **Variable**, als **Methodenparameter**, als **Feld einer Klasse** und als **Rückgabetyp**.

---

## Teil 1 — Die Verfahren und der erste Delegate 

**Aufgabe:**

1. Deklariere einen Delegate-Typ `ChiffrierVerfahren`, der einen `string` entgegennimmt und einen `string` zurückgibt.
2. Vervollständige die Klasse `Chiffren` mit drei Verfahren:
   - `Rueckwaerts` — dreht den Text um
   - `VokaleZuSterne` — ersetzt die Kleinbuchstaben `a e i o u` durch `*`
   - `Leet` — ersetzt `a→4`, `e→3`, `i→1`, `o→0` (nur Kleinbuchstaben)
3. Lege **eine** Delegate-Variable an, weise ihr die drei Verfahren nacheinander zu und gib das Ergebnis jeweils aus.

**Vorgegebener Code:**

```csharp
using System;

// TODO 1: Delegate-Typ ChiffrierVerfahren deklarieren


class Chiffren
{
    public static string Rueckwaerts(string klartext)
    {
        char[] zeichen = klartext.ToCharArray();
        Array.Reverse(zeichen);
        return new string(zeichen);
    }

    public static string VokaleZuSterne(string klartext)
    {
        // TODO 2a: a, e, i, o, u durch * ersetzen
        // Tipp: klartext.Replace("a", "*") liefert einen NEUEN string zurück
        return klartext;
    }

    public static string Leet(string klartext)
    {
        // TODO 2b: a->4, e->3, i->1, o->0
        return klartext;
    }
}

class Program
{
    static void Main()
    {
        string klartext = "Treffpunkt am Hafen";

        // TODO 3: ChiffrierVerfahren verfahren = Chiffren.Rueckwaerts;
        //         ausgeben, dann auf VokaleZuSterne und Leet umbiegen
    }
}
```

**Erwartete Ausgabe:**

```
Rueckwaerts    : nefaH ma tknupfferT
VokaleZuSterne : Tr*ffp*nkt *m H*f*n
Leet           : Tr3ffpunkt 4m H4f3n
```

**Kontrollfragen:**
- Warum steht bei der Zuweisung `Chiffren.Rueckwaerts` und nicht `Chiffren.Rueckwaerts(klartext)`?
- Warum bleibt bei `Leet` das große `A` in „Ankunft" unverändert?

---

## Teil 2 — Die Zentrale kennt das Verfahren nicht 

**Ziel:** Delegate als **Methodenparameter**.

**Aufgabe:** Schreibe in der Klasse `Funkzentrale` die Methode

```csharp
public static void Senden(string absender, string nachricht, ChiffrierVerfahren verfahren)
```

Sie verschlüsselt die Nachricht mit dem übergebenen Verfahren und gibt sie im Format
`>> [Absender] Text` aus. Die Zentrale selbst enthält **keine einzige** Verschlüsselungslogik.

**Vorgegebener Code:**

```csharp
class Funkzentrale
{
    public static void Senden(string absender, string nachricht, /* TODO: Delegate-Parameter */)
    {
        // TODO: verfahren aufrufen und ausgeben
    }
}

class Program
{
    static void Main()
    {
        Funkzentrale.Senden("Zentrale", "Lage unklar", Chiffren.Rueckwaerts);
        Funkzentrale.Senden("Zentrale", "Lage unklar", Chiffren.Leet);
    }
}
```

**Erwartete Ausgabe:**

```
>> [Zentrale] ralknu egaL
>> [Zentrale] L4g3 unkl4r
```

**Denkanstoß:** Wie viele `Senden`-Methoden bräuchtest du ohne Delegate, wenn es zehn Verfahren gäbe?

---

## Teil 3 — Jeder Agent bringt sein Verfahren mit 

**Ziel:** Delegate als **Feld einer Klasse**, gesetzt über den Konstruktor.

**Aufgabe:** Erweitere die Klasse `Agent`:

- Felder: `_codename` (string) und `_verfahren` (ChiffrierVerfahren)
- Konstruktor: `Agent(string codename, ChiffrierVerfahren verfahren)`
- Methode `Melden(string nachricht)`: verschlüsselt mit **dem eigenen** Verfahren und gibt
  `[Codename] Text` aus

Erzeuge zwei Agenten mit unterschiedlichen Verfahren.

**Vorgegebener Code:**

```csharp
class Agent
{
    private string _codename;
    // TODO: Feld für das Verfahren

    public Agent(string codename, /* TODO: Parameter */)
    {
        _codename = codename;
        // TODO: Feld zuweisen
    }

    public void Melden(string nachricht)
    {
        // TODO: eigenes Verfahren anwenden und ausgeben
    }
}

class Program
{
    static void Main()
    {
        Agent falke      = new Agent("Falke", Chiffren.Rueckwaerts);
        Agent nachtigall = new Agent("Nachtigall", Chiffren.Leet);

        falke.Melden("Der Kurier ist unterwegs");
        nachtigall.Melden("Treffen um acht");
    }
}
```

**Erwartete Ausgabe:**

```
[Falke] sgewretnu tsi reiruK reD
[Nachtigall] Tr3ff3n um 4cht
```

**Erweiterung:** Ergänze eine Methode `VerfahrenWechseln(ChiffrierVerfahren neuesVerfahren)`.
Lass Falke nach der ersten Meldung auf `VokaleZuSterne` wechseln und noch einmal melden.

---

## Teil 4 — Ein zweiter Delegate-Typ: die Zensurstelle 

**Ziel:** Zwei **verschiedene** Delegate-Typen in derselben Klasse.

**Aufgabe:**

1. Deklariere einen zweiten Delegate-Typ:
   `public delegate bool Sicherheitspruefung(string nachricht);`
2. Schreibe eine Klasse `Sicherheit` mit zwei Prüfmethoden:
   - `NichtZuLang` — `true`, wenn die Nachricht maximal 20 Zeichen hat
   - `KeinKlarname` — `true`, wenn die Nachricht **nicht** das Wort `Moskau` enthält
3. Gib `Agent` ein zweites Delegate-Feld für die Prüfung. `Melden` verschlüsselt nur,
   wenn die Prüfung `true` liefert — sonst `[Codename] ABBRUCH: nicht freigegeben`.

**Vorgegebener Code:**

```csharp
// TODO 1: delegate bool Sicherheitspruefung(string nachricht);

class Sicherheit
{
    public static bool NichtZuLang(string nachricht)
    {
        // TODO
        return true;
    }

    public static bool KeinKlarname(string nachricht)
    {
        // TODO: Tipp -> nachricht.Contains("Moskau")
        return true;
    }
}

class Agent
{
    private string _codename;
    private ChiffrierVerfahren _verfahren;
    // TODO 3a: Feld für die Prüfung

    public Agent(string codename, ChiffrierVerfahren verfahren, /* TODO 3b */)
    {
        // TODO
    }

    public void Melden(string nachricht)
    {
        // TODO 3c: erst prüfen, dann verschlüsseln oder abbrechen
    }
}

class Program
{
    static void Main()
    {
        Agent falke      = new Agent("Falke", Chiffren.Rueckwaerts, Sicherheit.NichtZuLang);
        Agent nachtigall = new Agent("Nachtigall", Chiffren.Leet, Sicherheit.KeinKlarname);

        falke.Melden("Alles ruhig");
        falke.Melden("Das Paket liegt hinter der Tuer");
        nachtigall.Melden("Ankunft heute");
        nachtigall.Melden("Ankunft in Moskau");
    }
}
```

**Erwartete Ausgabe:**

```
[Falke] gihur sellA
[Falke] ABBRUCH: nicht freigegeben
[Nachtigall] 4nkunft h3ut3
[Nachtigall] ABBRUCH: nicht freigegeben
```

**Kontrollfrage:** Warum kannst du `Sicherheit.NichtZuLang` **nicht** einem `ChiffrierVerfahren` zuweisen,
obwohl beides Methoden mit einem `string`-Parameter sind?

---

## Teil 5 — Der Tagesschlüssel: Delegate als Rückgabewert 

**Ziel:** Eine Methode, die ein Delegate **zurückgibt**.

**Aufgabe:** Schreibe in `Funkzentrale` die Methode

```csharp
public static ChiffrierVerfahren VerfahrenFuerTag(string wochentag)
```

Sie liefert je nach Tag ein anderes Verfahren zurück (per `switch`):

| Tag | Verfahren |
|---|---|
| Montag, Dienstag | `Rueckwaerts` |
| Mittwoch, Donnerstag | `Leet` |
| alle anderen | `VokaleZuSterne` |

**Vorgegebener Code:**

```csharp
class Funkzentrale
{
    public static ChiffrierVerfahren VerfahrenFuerTag(string wochentag)
    {
        // TODO: switch (wochentag) -> passende Methode ZURÜCKGEBEN (ohne Klammern!)
        return Chiffren.VokaleZuSterne;
    }
}

class Program
{
    static void Main()
    {
        string[] tage = { "Montag", "Donnerstag", "Sonntag" };

        foreach (string tag in tage)
        {
            ChiffrierVerfahren tagesschluessel = Funkzentrale.VerfahrenFuerTag(tag);
            Console.WriteLine($"{tag,-12}: {tagesschluessel("Lage stabil")}");
        }
    }
}
```

**Erwartete Ausgabe:**

```
Montag      : libats egaL
Donnerstag  : L4g3 st4b1l
Sonntag     : L*g* st*b*l
```

**Stolperfalle:** Was wäre der Unterschied zwischen `return Chiffren.Leet;` und `return Chiffren.Leet("Lage stabil");`?

---

## Teil 6 — Hin und zurück: eine Klasse mit zwei Delegate-Feldern 

**Ziel:** Zwei Delegate-Variablen desselben Typs sinnvoll kombinieren.

**Aufgabe:**

1. Ergänze in `Chiffren` die Methode `LeetZurueck` (`4→a`, `3→e`, `1→i`, `0→o`).
2. Schreibe eine Klasse `Chiffre` mit den Feldern `_name`, `_verschluesseln`, `_entschluesseln`
   (beide vom Typ `ChiffrierVerfahren`) und der Methode `Test(string klartext)`, die Klartext,
   Geheimtext und zurückentschlüsselten Text ausgibt.
3. Lege zwei Chiffren an: `"Spiegel"` (Rueckwaerts ↔ Rueckwaerts) und `"Zahlencode"` (Leet ↔ LeetZurueck).
4. Schreibe zusätzlich eine Methode, die **zwei** Verfahren hintereinander anwendet:
   `public static string Doppelt(string klartext, ChiffrierVerfahren erst, ChiffrierVerfahren dann)`

**Vorgegebener Code:**

```csharp
class Chiffre
{
    private string _name;
    private ChiffrierVerfahren _verschluesseln;
    // TODO 2a: Feld _entschluesseln

    public Chiffre(string name, ChiffrierVerfahren verschluesseln, /* TODO 2b */)
    {
        // TODO
    }

    public void Test(string klartext)
    {
        // TODO 2c:
        // string geheim  = _verschluesseln(klartext);
        // string zurueck = _entschluesseln(geheim);
        // Ausgabe: "Spiegel: 'Mission erfuellt' -> 'tlleufre noissiM' -> 'Mission erfuellt'"
    }
}

class Funkzentrale
{
    public static string Doppelt(string klartext, ChiffrierVerfahren erst, ChiffrierVerfahren dann)
    {
        // TODO 4: erst anwenden, Ergebnis in dann hineingeben
        return klartext;
    }
}

class Program
{
    static void Main()
    {
        Chiffre spiegel    = new Chiffre("Spiegel", Chiffren.Rueckwaerts, Chiffren.Rueckwaerts);
        Chiffre zahlencode = new Chiffre("Zahlencode", Chiffren.Leet, Chiffren.LeetZurueck);

        spiegel.Test("Mission erfuellt");
        zahlencode.Test("Mission erfuellt");

        Console.WriteLine("Doppelt: " + Funkzentrale.Doppelt("Basis Nord", Chiffren.Rueckwaerts, Chiffren.Leet));
    }
}
```

**Erwartete Ausgabe:**

```
Spiegel: 'Mission erfuellt' -> 'tlleufre noissiM' -> 'Mission erfuellt'
Zahlencode: 'Mission erfuellt' -> 'M1ss10n 3rfu3llt' -> 'Mission erfuellt'
Doppelt: dr0N s1s4B
```

**Abschlussfrage:** Was passiert, wenn du `new Chiffre("Kaputt", Chiffren.VokaleZuSterne, Chiffren.VokaleZuSterne)`
anlegst und testest? Warum ist das Verfahren als Chiffre unbrauchbar?


---


## Checkliste

- [ ] Ich kann einen eigenen Delegate-Typ deklarieren.
- [ ] Ich weiß, warum bei der Zuweisung keine Klammern stehen.
- [ ] Ich habe ein Delegate als Methodenparameter übergeben.
- [ ] Ich habe ein Delegate als Feld einer Klasse gespeichert und über den Konstruktor gesetzt.
- [ ] Ich habe eine Methode geschrieben, die ein Delegate zurückgibt.
- [ ] Ich kann erklären, warum zwei Delegate-Typen mit gleicher Parameterliste trotzdem nicht kompatibel sind.
