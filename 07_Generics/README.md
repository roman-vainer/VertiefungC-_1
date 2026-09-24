# Mini-Projekt 7: Generics


## Ziel dieses Projekts

Dieses Mini-Projekt ist bewusst **breiter statt tiefer**: Statt neue Theorie
zu bringen, spielt es die wichtigsten Generics-Bausteine einmal kompakt
durch — eigene generische Klasse, generische Methode, Constraints,
`default(T)`, mehrere Typparameter — und zeigt dann Vererbung mit
generischen Typen an **zwei bewusst unterschiedlichen Mustern**:

1. **`IntStapel : Stapel<int>`** — der Typparameter wird beim Erben
   **festgelegt**. `IntStapel` ist selbst keine generische Klasse mehr.
2. **`ProtokollierterStapel<T> : Stapel<T>`** — der Typparameter wird
   **weitergereicht**, die Klasse bleibt selbst generisch. Zusätzlich mit
   `new void Push(T element)` ein Praxisbeispiel für **Method Hiding**
   (kein Overriding, weil `Stapel<T>.Push` nicht `virtual` ist).

Beide Muster kommen in echtem Code vor: Variante 1, wenn ein generischer
Typ im Projekt sehr häufig mit demselben konkreten Typ gebraucht wird und
ein bequemerer Name lohnt. Variante 2, wenn man die Grundfunktionalität
erweitern will (hier: Logging), aber die Flexibilität für beliebige `T`
behalten möchte.

Beispiel 3 macht zusätzlich den Boxing-Unterschied zwischen `ArrayList`
und `List<int>` noch einmal handfest messbar.

## Aufbau (Program.cs)

- `Grundlagen_GenerischeKlasse` — eigene generische Klasse `Stapel<T>`
- `Grundlagen_GenerischeMethode` — generische Methode `Tausche<T>` 
- `Grundlagen_ConstraintsUndVergleich` — `Groesseres<T>` mit
  `where T : IComparable<T>` 
- `Grundlagen_DefaultT` — `ErsterOderStandard<T>` mit `default(T)`
- `Grundlagen_MehrereTypparameter` — `Zuordnung<TSchluessel, TWert>`
- `Beispiel1_TypparameterFestgelegt` + `IntStapel` — Muster 1.
- `Beispiel2_TypparameterWeitergereicht` + `ProtokollierterStapel<T>` —
  Muster 2 (Method Hiding).
- `Beispiel3_BoxingVermeidung` — `Stopwatch`-Messung 

## Übungsaufgaben

Die Aufgaben sind bewusst vom Einfachen zum Anspruchsvolleren geordnet. Die
ersten Aufgaben verändern nur kleine Stellen im vorhandenen Code und helfen
dabei, sich mit generischen Typen und Methodenaufrufen wieder einzufinden.
Ab Aufgabe 6 entwirfst du eigene generische Typen komplett selbst.

### Einstieg 1 — Einen weiteren Stapel benutzen

Erweitere `Grundlagen_GenerischeKlasse` um einen zweiten Stapel,
`Stapel<int> zahlenStapel`. Push drei beliebige Zahlen hinein und gib
danach `zahlenStapel.Anzahl` sowie zweimal `Pop()` aus. Beobachte: Es ist
**derselbe** `Stapel<T>` wie bei `textStapel`, nur mit einem anderen `T`.

### Einstieg 2 — Tausche mit einem weiteren Typ aufrufen

Erweitere `Grundlagen_GenerischeMethode` um zwei `bool`-Variablen
(`istAktiv1 = true`, `istAktiv2 = false`) und rufe `Werkzeuge.Tausche`
damit auf. Gib beide Werte vor und nach dem Tausch aus.

### Einstieg 3 — Groesseres mit einem dritten Typ testen

Erweitere `Grundlagen_ConstraintsUndVergleich` um einen Aufruf von
`Werkzeuge.Groesseres` mit zwei `DateTime`-Werten (z. B. `DateTime.Today`
und `DateTime.Today.AddDays(-3)`). Überlege vorher kurz: Warum kompiliert
das, obwohl `Groesseres<T>` nichts Spezifisches über `DateTime` weiß?

### Einstieg 4 — default(T) mit weiteren Typen prüfen

Erweitere `Grundlagen_DefaultT` um zwei Aufrufe von
`Werkzeuge.ErsterOderStandard`: einmal mit einer leeren `List<bool>`,
einmal mit einer leeren `List<double>`. Notiere dir (als Kommentar im
Code), welchen Wert `default(T)` für `bool` und für `double` jeweils
liefert.

### Einstieg 5 — Eine weitere Zuordnung ergänzen

Erweitere `Grundlagen_MehrereTypparameter` um ein zweites Schlüssel-Wert-
Paar (`zuordnung.Hinzufuegen("Bela", 9)`) und gib auch dieses mit
`Abrufen` wieder aus. Erstelle anschließend eine **zweite**, unabhängige
`Zuordnung<int, string>` (Schlüssel und Wert-Typ vertauscht/anders) und
befülle sie mit einem eigenen Beispiel.

### Aufgabe 6 — Eine eigene generische Klasse: Warteschlange\<T\>

Schreibe eine komplett neue generische Klasse

```csharp
public class Warteschlange<T>
{
    public void Einreihen(T element) { /* ... */ }
    public T Entnehmen() { /* ... */ }
    public int Anzahl => /* ... */;
}
```

Sie soll sich nach dem **FIFO-Prinzip** verhalten (First In, First Out —
wer zuerst eingereiht wurde, wird auch zuerst entnommen), intern basierend
auf `List<T>` oder `Queue<T>`. Teste sie sowohl mit `Warteschlange<string>`
als auch mit `Warteschlange<int>` in einer neuen Methode
`Aufgabe6_Warteschlange()`, die du aus `Main()` aufrufst.

### Aufgabe 7 — Eine generische Methode: Maximum\<T\>

Schreibe eine generische Methode

```csharp
public static T Maximum<T>(IEnumerable<T> werte) where T : IComparable<T>
```

die den größten Wert einer beliebigen Sammlung zurückgibt (Tipp: iteriere
mit `foreach` und vergleiche laufend mit `CompareTo`, analog zu
`Groesseres<T>`). Teste sie sowohl mit einer `List<int>` als auch mit
einer `List<string>`.

### Aufgabe 8 — Ein generisches Interface: IValidator\<T\>

Schreibe ein generisches Interface

```csharp
public interface IValidator<T>
{
    bool IstGueltig(T element);
}
```

und implementiere es einmal konkret als

```csharp
public class EmailValidator : IValidator<string>
```

Eine einfache Prüfregel genügt (z. B.: enthält `@` **und** enthält einen
`.` nach dem `@`). Teste `EmailValidator` mit mehreren Beispiel-Strings,
darunter mindestens eine gültige und zwei ungültige E-Mail-Adressen.

### Aufgabe 9 (Zusatz für Schnelle) — Constraint + default(T) kombinieren

Erweitere deine `Warteschlange<T>` aus Aufgabe 6 um den Constraint
`where T : class` und eine zusätzliche Methode

```csharp
public T? EntnehmenOderStandard()
```

die bei einer **leeren** Warteschlange `default(T)` zurückgibt, statt wie
`Entnehmen()` eine Exception zu werfen. Überlege: Warum ergibt diese
Methode nur mit `where T : class` wirklich Sinn? Teste sie mit einer 
leeren `Warteschlange<string>`.

## Kurze Selbstkontrolle

Beantworte in 1–2 Sätzen:

1. Warum benötigt `Maximum<T>` (Aufgabe 7) zwingend den Constraint
   `where T : IComparable<T>`, obwohl `T` beim Aufruf oft `int` oder
   `string` ist, die diese Methode ja offensichtlich haben?
2. Was würde sich ändern, wenn `ProtokollierterStapel<T>.Push` (Beispiel 2)
   ohne das Schlüsselwort `new` geschrieben würde — würde der Code noch
   kompilieren, und würde sich das Verhalten beim Aufruf über eine
   `Stapel<T>`-Variable ändern?
3. Warum ergibt `EntnehmenOderStandard` (Aufgabe 9) nur mit
   `where T : class` wirklich Sinn — was würde bei `T = int` passieren?

## Ausführen

```powershell
dotnet run
```
