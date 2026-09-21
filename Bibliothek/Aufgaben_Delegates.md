# Übungsaufgaben: Delegates im Projekt "Bibliothek"

Diese Aufgaben bauen auf dem bestehenden Projekt auf (`Buch.cs`, `BuchMethoden.cs`, `Buchverwaltung.cs`, `Program.cs`). Du lernst dabei:

1. **Eigene Delegates deklarieren**
2. **Standard-Delegate-Typen verwenden** (`Action`, `Func`, `Predicate`, `Comparison`)
3. **Lambda-Funktionen** – als Delegate-Variable und direkt "inline" ohne Zwischenvariable

---

## Organisatorischer Hinweis (bitte zuerst lesen)

Damit du den bestehenden Code in `Program.cs` nicht kaputt machst, leg dir für jeden Teil eine **eigene Klasse mit eigener Datei** an, z. B.:

```
UebungTeil1.cs
UebungTeil2.cs
UebungTeil3.cs
UebungTeil4.cs
```

Jede Klasse bekommt eine statische Methode `Run()`, die du am Ende aus `Main` heraus aufrufst:

```csharp
// in Program.cs, innerhalb von Main, ganz unten ergänzen:
UebungTeil1.Run();
UebungTeil2.Run();
UebungTeil3.Run();
UebungTeil4.Run();
```

So bleibt jede Aufgabe sauber getrennt und der bestehende Demo-Code oben in `Main` bleibt unangetastet. Neue Methoden für Bücher kannst du wahlweise direkt in `BuchMethoden.cs` / `Buchverwaltung.cs` ergänzen oder in deine neuen Klassen schreiben – beides ist erlaubt und in den Aufgaben vermerkt.

---

## Teil 1: Eigene Delegates deklarieren

Ein eigener Delegate-Typ wird mit dem Schlüsselwort `delegate` außerhalb einer Klasse (oder als `public delegate ...` innerhalb einer Klasse) definiert, z. B.:

```csharp
public delegate void MeinDelegate(int zahl);
```

### Aufgabe 1.1 ⭐ – Eigener Delegate ohne Rückgabewert

Deklariere einen Delegate-Typ `BuchAktion`, der zu Methoden mit der Signatur `void Methodenname(Buch buch)` passt.

Ergänze in `BuchMethoden.cs` eine neue Methode `BuchDrucken(Buch buch)`, die Titel, Autor und Jahr des Buches auf der Konsole ausgibt.

Weise in `UebungTeil1.Run()` eine Variable vom Typ `BuchAktion` dieser Methode zu und rufe sie für ein selbst erstelltes `Buch`-Objekt auf.

<details>
<summary>💡 Hinweis</summary>

- Delegate-Deklaration z. B. direkt über der `Program`-Klasse oder in einer eigenen Datei `BuchAktion.cs`:
  `public delegate void BuchAktion(Buch buch);`
- Zuweisung: `BuchAktion aktion = BuchMethoden.BuchDrucken;`
- Aufruf: `aktion(meinBuch);`
</details>

### Aufgabe 1.2 ⭐ – Eigener Delegate mit Rückgabewert

Deklariere einen Delegate-Typ `BuchAlterBerechnung`, der zu Methoden mit der Signatur `int Methodenname(Buch buch)` passt.

Ergänze in `BuchMethoden.cs` eine Methode `BerechneAlter(Buch buch)`, die das Alter des Buches in Jahren zurückgibt (aktuelles Jahr minus `buch.Jahr`).

Rufe die Methode über eine Delegate-Variable auf und gib das Ergebnis aus.

<details>
<summary>💡 Hinweis</summary>

- `public delegate int BuchAlterBerechnung(Buch buch);`
- Aktuelles Jahr bekommst du über `DateTime.Now.Year`.
- `int alter = alterDelegate(meinBuch);`
</details>

### Aufgabe 1.3 ⭐⭐ – Multicast mit eigenem Delegate

Nutze den Delegate `BuchAktion` aus Aufgabe 1.1. Hänge mit `+=` **mindestens drei** Aktionen aneinander:

1. `BuchMethoden.BuchDrucken`
2. Eine zweite selbstgeschriebene Methode (z. B. `BuchMethoden.BuchTrennlinieDrucken`, die einfach eine Linie `"------"` ausgibt)
3. Eine **Lambda-Funktion**, die direkt `+=` angehängt wird und einen beliebigen Text ausgibt

Rufe den kombinierten Delegate einmal auf und beobachte, dass alle drei Aktionen nacheinander ausgeführt werden.

<details>
<summary>💡 Hinweis</summary>

```csharp
BuchAktion aktion = BuchMethoden.BuchDrucken;
aktion += BuchMethoden.BuchTrennlinieDrucken;
aktion += (Buch b) => Console.WriteLine("Ende der Ausgabe");
aktion(meinBuch);
```
</details>

### Aufgabe 1.4 ⭐⭐ – Delegate als Parameter einer eigenen Methode

Schreibe in `Buchverwaltung.cs` eine neue Methode:

```csharp
public static void FuehreAktionAus(Buch buch, BuchAktion aktion)
```

Diese Methode soll den übergebenen Delegate innerhalb der Methode aufrufen (ggf. mit etwas Text davor/danach, z. B. `"Starte Aktion..."`).

Rufe `FuehreAktionAus` in `UebungTeil1.Run()` **zweimal** mit unterschiedlichen Delegates auf (einmal mit einer Methode, einmal mit einer Lambda-Funktion).

<details>
<summary>💡 Hinweis</summary>

Das ist dasselbe Prinzip wie `Buchverwaltung.Prozess(...)` im bestehenden Code, nur einfacher (nur ein Delegate-Parameter statt drei).
</details>

---

## Teil 2: Standard-Delegate-Typen verwenden (`Action`, `Func`, `Predicate`, `Comparison`)

Statt eigene Delegate-Typen zu deklarieren, kannst du die vordefinierten generischen Delegates aus dem `System`-Namespace verwenden:

- `Action` / `Action<T>` → kein Rückgabewert
- `Func<T, TResult>` → mit Rückgabewert
- `Predicate<T>` → gibt immer `bool` zurück
- `Comparison<T>` → vergleicht zwei Objekte, gibt `int` zurück (wie im Projekt schon bei `cbuch` verwendet)

### Aufgabe 2.1 ⭐ – Action ohne Parameter

Erstelle eine Variable vom Typ `Action` (ohne generischen Typ), die eine Begrüßung auf der Konsole ausgibt (z. B. `"Willkommen in der Bibliotheksverwaltung!"`). Rufe sie auf.

<details>
<summary>💡 Hinweis</summary>

`Action begruessung = () => Console.WriteLine("Willkommen in der Bibliotheksverwaltung!");`
</details>

### Aufgabe 2.2 ⭐ – Action&lt;Buch&gt;

Ergänze in `BuchMethoden.cs` eine Methode `BuchDetailsAnzeigen(Buch buch)`, die alle Eigenschaften des Buches (Titel, Autor, Jahr, ISBN, Preis pro Tag, Verliehen) ausgibt.

Weise diese Methode einer `Action<Buch>`-Variable zu und rufe sie für **zwei** unterschiedliche Bücher auf.

<details>
<summary>💡 Hinweis</summary>

`Action<Buch> detailsAnzeigen = BuchMethoden.BuchDetailsAnzeigen;`
</details>

### Aufgabe 2.3 ⭐⭐ – Func&lt;Buch, string&gt;

Ergänze eine Methode `BuchZusammenfassung(Buch buch)`, die **keinen** `Console.WriteLine` enthält, sondern einen zusammengesetzten `string` **zurückgibt**, z. B. `"Harry Potter (1997) von J.K. Rowling"`.

Weise die Methode einer `Func<Buch, string>`-Variable zu, rufe sie auf und gib das Ergebnis erst danach mit `Console.WriteLine` aus.

<details>
<summary>💡 Hinweis</summary>

Der Unterschied zu `Action` ist: `Func` gibt immer etwas zurück, das du in einer Variable speichern kannst, bevor du es ausgibst.
</details>

### Aufgabe 2.4 ⭐⭐ – Predicate&lt;Buch&gt; mit einer Liste

Lege in `UebungTeil2.Run()` eine `List<Buch>` mit **mindestens 4 Büchern** an (unterschiedliche Erscheinungsjahre, z. B. auch vor 1990).

Erstelle ein `Predicate<Buch>`, das `true` liefert, wenn `buch.Jahr < 1990` ist.

Nutze `buecherListe.FindAll(deinPredicate)`, um eine gefilterte Liste zu erhalten, und gib die Titel der gefundenen Bücher aus.

<details>
<summary>💡 Hinweis</summary>

- `Predicate<Buch> altesBuch = (Buch b) => b.Jahr < 1990;`
- `List<Buch> alteBuecher = buecherListe.FindAll(altesBuch);`
- `FindAll` gibt es bei `List<T>` bereits fertig eingebaut – kein Grund, sie selbst zu schreiben.
</details>

### Aufgabe 2.5 ⭐⭐ – Comparison&lt;Buch&gt; mit einer Liste

Nutze dieselbe (oder eine neue) `List<Buch>` und sortiere sie **nach Erscheinungsjahr aufsteigend**, indem du `buecherListe.Sort(deinComparison)` verwendest.

<details>
<summary>💡 Hinweis</summary>

- `Comparison<Buch> nachJahrSortieren = (Buch b1, Buch b2) => b1.Jahr.CompareTo(b2.Jahr);`
- `buecherListe.Sort(nachJahrSortieren);`
- Schau dir zum Vergleich `cbuch` in `Program.cs` an – dort wird `Comparison<Buch>` bereits verwendet, allerdings nur zum Vergleichen von Titeln, nicht zum Sortieren einer Liste.
</details>

### Aufgabe 2.6 ⭐⭐⭐ – Alles kombinieren

Baue dir eine `List<Buch>` mit mindestens 5 Büchern auf. Führe **nacheinander** aus:

1. Filtere mit einem `Predicate<Buch>` alle Bücher, die verliehen werden können (`!buch.Verliehen`)
2. Sortiere das Ergebnis mit einem `Comparison<Buch>` nach `PreisProTag` (aufsteigend)
3. Gib die sortierte, gefilterte Liste mit einer `Action<Buch>` über `buecherListe.ForEach(deineAction)` aus

<details>
<summary>💡 Hinweis</summary>

`List<T>` hat bereits die Methoden `FindAll(Predicate<T>)`, `Sort(Comparison<T>)` und `ForEach(Action<T>)` eingebaut – du musst sie nicht selbst schreiben, nur die passenden Delegates übergeben.
</details>

---

## Teil 3: Lambda-Funktionen

Lambda-Funktionen sind eine Kurzschreibweise für anonyme Methoden. Man unterscheidet:

- **Lambda einer Delegate-Variable zugewiesen**: `Func<int,int> quadrat = x => x * x;`
- **Lambda direkt "inline" übergeben**, ohne Zwischenvariable: `zahlen.Where(x => x > 5)`

### Aufgabe 3.1 ⭐ – Lambda einer Variable zuweisen

Erstelle eine Variable `Func<Buch, bool> istNeu`, die `true` liefert, wenn `buch.Jahr >= 2000` ist – als Lambda-Ausdruck. Rufe sie für ein Buch auf und gib das Ergebnis aus.

<details>
<summary>💡 Hinweis</summary>

`Func<Buch, bool> istNeu = buch => buch.Jahr >= 2000;`
</details>

### Aufgabe 3.2 ⭐⭐ – Lambda direkt als Argument (ohne Zwischenvariable)

Nimm deine `List<Buch>` aus Teil 2 und rufe **ohne** vorher eine Delegate-Variable zu deklarieren:

```csharp
var neueBuecher = buecherListe.FindAll(buch => buch.Jahr >= 2000);
buecherListe.Sort((b1, b2) => b1.Titel.CompareTo(b2.Titel));
buecherListe.ForEach(buch => Console.WriteLine(buch.Titel));
```

Schreibe diese drei Zeilen selbst (mit eigenen Bedingungen) und erkläre dir in einem Kommentar den Unterschied zu Aufgabe 2.4–2.6: Hier gibt es **keine benannte Delegate-Variable** – die Lambda-Funktion wird direkt "inline" an die Methode übergeben.

<details>
<summary>💡 Hinweis</summary>

Beide Varianten (mit und ohne Zwischenvariable) erzeugen am Ende denselben Delegate-Typ im Hintergrund. Der Compiler leitet den passenden Typ (`Predicate<Buch>`, `Comparison<Buch>`, `Action<Buch>`) automatisch aus der Methodensignatur von `FindAll`, `Sort` bzw. `ForEach` ab.
</details>

### Aufgabe 3.3 ⭐⭐ – Block-Lambda mit mehreren Anweisungen

Bisher waren alle Lambdas einzeilige Ausdrücke (`x => x * 2`). Lambdas können aber auch einen **Codeblock mit `{}`** und `return` enthalten.

Schreibe ein `Func<Buch, string>` namens `statusText`, das:

- `"Verliehen"` zurückgibt, wenn `buch.Verliehen == true`
- sonst `"Verfügbar"` zurückgibt

... und zwar **als Block-Lambda mit `if`/`else`**, nicht als einzeiliger Ausdruck.

<details>
<summary>💡 Hinweis</summary>

```csharp
Func<Buch, string> statusText = (Buch buch) =>
{
    if (buch.Verliehen)
    {
        return "Verliehen";
    }
    else
    {
        return "Verfügbar";
    }
};
```
</details>

### Aufgabe 3.4 ⭐⭐⭐ – Lambda + eigener Delegate kombinieren

Verwende deinen eigenen Delegate-Typ `BuchAktion` aus Teil 1 zusammen mit der Methode `FuehreAktionAus` aus Aufgabe 1.4.

Rufe `Buchverwaltung.FuehreAktionAus(meinBuch, ...)` auf und übergib **direkt eine Lambda-Funktion** (keine Variable, kein Methodenname) als zweites Argument, die z. B. prüft ob das Buch verliehen ist und einen passenden Text ausgibt.

<details>
<summary>💡 Hinweis</summary>

```csharp
Buchverwaltung.FuehreAktionAus(meinBuch, (Buch buch) =>
{
    Console.WriteLine(buch.Verliehen ? $"{buch.Titel} ist verliehen." : $"{buch.Titel} ist verfügbar.");
});
```
</details>

---

## Teil 4: Erweiterung – eigene Klassen (Herausforderung)

In diesem Teil erweiterst du das Projekt um eigene Klassen. Das ist bewusst offener gehalten – nutze alles, was du in Teil 1–3 gelernt hast.

### Aufgabe 4.1 ⭐⭐ – Buch-Klasse erweitern

Erweitere die Klasse `Buch` um eine neue Eigenschaft `Genre` (`string`, z. B. `"Fantasy"`, `"Sachbuch"`). Passe den Konstruktor entsprechend an (neuer Parameter `genre`).

Achtung: Wenn du den Konstruktor änderst, musst du die bestehenden Aufrufe in `Program.cs` (`HarryPotter`, `HerrDerRinge`) ebenfalls um das neue Argument ergänzen, damit das Projekt weiter kompiliert.

<details>
<summary>💡 Hinweis</summary>

Denk daran, `Genre` auch im Konstruktor-Body zuzuweisen (`Genre = genre;`), so wie es bei den anderen Properties gemacht wird.
</details>

### Aufgabe 4.2 ⭐⭐⭐ – Neue Klasse `Bibliothek`

Erstelle eine neue Klasse `Bibliothek` mit:

- Einer Eigenschaft `List<Buch> Buecher`
- Einer Methode `BuchHinzufuegen(Buch buch)`
- Einer Methode

```csharp
public void BuecherVerarbeiten(Predicate<Buch> filter, Comparison<Buch> sortierung, Action<Buch> ausgabe)
```

Diese Methode soll: die Liste mit `filter` filtern, das Ergebnis mit `sortierung` sortieren, und danach jedes Buch mit `ausgabe` ausgeben.

Rufe `BuecherVerarbeiten` aus deiner `UebungTeil4.Run()` mit unterschiedlichen Kombinationen aus Predicate/Comparison/Action auf (einmal mit Methodennamen, einmal mit Lambda-Funktionen).

<details>
<summary>💡 Hinweis</summary>

Das ist vom Prinzip her sehr ähnlich zu `Buchverwaltung.Prozess(...)`, nur dass hier mit einer ganzen `List<Buch>` statt mit einem einzelnen `Buch` gearbeitet wird. `FindAll`, `Sort` und `ForEach` von `List<T>` helfen dir dabei.
</details>

### Aufgabe 4.3 ⭐⭐⭐ – Statistik-Funktion

Ergänze (in `Bibliothek` oder einer neuen Klasse `Statistik`) eine Methode oder Delegate-Variable vom Typ `Func<List<Buch>, decimal>`, die den **Gesamtwert** aller `PreisProTag`-Werte in der Liste berechnet (Summe).

<details>
<summary>💡 Hinweis</summary>

```csharp
Func<List<Buch>, decimal> gesamtPreisProTag = (List<Buch> buecher) =>
{
    decimal summe = 0;
    foreach (var buch in buecher)
    {
        summe += buch.PreisProTag;
    }
    return summe;
};
```

Alternativ (fortgeschritten, falls bereits bekannt): LINQ mit `buecher.Sum(b => b.PreisProTag)`.
</details>

### Aufgabe 4.4 ⭐⭐⭐⭐ – Bonus: Delegate als Callback/Benachrichtigung

Deklariere einen Delegate `BenachrichtigungsDelegate`, der zu `void Methodenname(string nachricht)` passt.

Erweitere `BuchMethoden.Ausleihen` um einen **optionalen** Parameter:

```csharp
public static void Ausleihen(Buch buch, BenachrichtigungsDelegate? benachrichtigung = null)
```

Wenn `benachrichtigung` nicht `null` ist, soll sie am Ende der Methode mit einer passenden Nachricht aufgerufen werden (z. B. `$"{buch.Titel} wurde soeben ausgeliehen."`).

Rufe `Ausleihen` einmal **ohne** Benachrichtigung und einmal **mit** einer Lambda-Funktion als Benachrichtigung auf.

<details>
<summary>💡 Hinweis</summary>

- Der `?` nach dem Delegate-Typ macht den Parameter nullable, `= null` macht ihn optional.
- Prüfe vor dem Aufruf: `if (benachrichtigung != null) { benachrichtigung(nachricht); }`
- Das ist genau das Prinzip, nach dem in C# auch Events funktionieren (Events sind im Kern nichts anderes als speziell abgesicherte Multicast-Delegates).
</details>

---

## Reihenfolge-Empfehlung

Bearbeite die Teile in der Reihenfolge 1 → 2 → 3 → 4. Innerhalb eines Teils sind die Aufgaben aufeinander aufbauend sortiert (Sterne steigen an). Teil 4 ist optional/Bonus, falls noch Zeit und Lust vorhanden ist.

Viel Erfolg!
