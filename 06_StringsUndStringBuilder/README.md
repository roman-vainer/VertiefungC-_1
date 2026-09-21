# Mini-Projekt 6: Strings und StringBuilder im Zusammenspiel

Baut auf **Foliensatz_Strings** auf. Voraussetzung: Unveränderlichkeit
von `string` und die Grundidee von `StringBuilder` verstanden.

## Ziel dieses Projekts

Dieses Mini-Projekt ist bewusst **breiter statt tiefer**: Statt neue Theorie
zu bringen, spielt es die wichtigsten String-Operationen einmal kompakt
durch — Erzeugen, Vergleichen, Suchen, Teilen/Verbinden, „Verändern" — und
zeigt dann `StringBuilder` an **zwei bewusst unterschiedlichen Mustern**:

1. **`ArtikellisteAnhaengen(StringBuilder sb, ...)`** — der StringBuilder
   wird von **außen übergeben** (vorgegeben). Die Methode erzeugt selbst
   keinen, sondern baut nur an einem bereits vorhandenen Puffer weiter.
2. **`ErstelleKassenbon(...)`** — der StringBuilder wird **innerhalb der
   Methode selbst erzeugt** (`new StringBuilder()`). Die Methode ist von
   Kopf bis Summenzeile allein für ihren Text zuständig.

Beide Muster kommen in echtem Code vor: Variante 1 typischerweise, wenn
mehrere Methoden nacheinander an **einem gemeinsamen** Report schreiben
(z. B. Kopf, Positionen, Fußzeile aus getrennten Methoden). Variante 2,
wenn eine Methode ein **abgeschlossenes** Ergebnis liefern soll, das der
Aufrufer gar nicht kennen muss (er sieht nur den fertigen `string`).

Beispiel 3 macht zusätzlich Performance `+=` vs.
`StringBuilder` noch einmal handfest messbar.

## Aufbau (Program.cs)

- `Grundlagen_Erzeugung` / `_Vergleich` / `_Suchen` / `_TeilenUndVerbinden` /
  `_Veraendern` — je eine kurze, lauffähige Erinnerung an die
  String-Methoden aus Folie 2–10.
- `Grundlagen_StringBuilder` — `Append`, `AppendLine`, `Insert`, `Replace`,
  Method Chaining, `Length` (Folie 16–17).
- `Beispiel1_StringBuilderVorgegeben` + `ArtikellisteAnhaengen` — Muster 1.
- `Beispiel2_StringBuilderSelbstErstellt` + `ErstelleKassenbon` — Muster 2.
- `Beispiel3_PerformanceVergleich` — `Stopwatch`-Messung wie in Folie 15.

## Übungsaufgaben

Die Aufgaben sind bewusst vom Einfachen zum Anspruchsvolleren geordnet. Die
ersten Aufgaben verändern nur kleine Stellen im vorhandenen Code und helfen
dabei, sich mit Variablen, Methodenaufrufen und den wichtigsten Operationen
wieder einzufinden.

### Einstieg 1 — Eine weitere Variable deklarieren

Erweitere `Grundlagen_Erzeugung` um eine Variable `string kategorie` und gib
sie zusammen mit dem Namen und der Artikelanzahl aus. Verwende zum Beispiel
die Ausgabeform:

```csharp
Console.WriteLine($"{name}: {anzahl} Stück ({kategorie})");
```

### Einstieg 2 — Eine vorhandene Variable anpassen

Ändere in `Grundlagen_Vergleich` den Wert von `eingabe` zunächst in
`"  JA  "`. Ergänze eine Ausgabe, die vor dem Vergleich `Trim()` verwendet.
Beobachte, warum der direkte Vergleich und der bereinigte Vergleich zu
unterschiedlichen Ergebnissen führen können.

### Einstieg 3 — Eine kleine Methode ergänzen

Schreibe unterhalb von `Grundlagen_Suchen` die Methode

```csharp
static bool IstArtikelVorhanden(string artikel, string gesucht)
```

Sie soll mit `Contains` prüfen, ob `gesucht` im Artikel vorkommt. Rufe die
Methode in `Grundlagen_Suchen` mit `"Bio-Apfel 1kg"` und `"Apfel"` auf und
gib das Ergebnis aus.

### Einstieg 4 — Strings in kleinen Schritten bearbeiten

Erweitere `Grundlagen_Veraendern` um eine Variable `string kurzname`, die aus
`roh` entsteht und anschließend

1. mit `Trim()` bereinigt,
2. mit `Replace("-", " ")` angepasst und
3. mit `ToLower()` kleingeschrieben wird.

Gib `kurzname` aus. Die vorhandene Variable `angezeigt` soll dabei erhalten
bleiben.

### Einstieg 5 — Einen StringBuilder schrittweise erweitern

Ergänze `Grundlagen_StringBuilder` um eine weitere Zeile mit `AppendLine`.
Füge anschließend mit `Insert` am Anfang ein passendes Präfix ein und gib
zusätzlich `sb.Length` vor und nach der Änderung aus. Verwende dabei nur den
bereits vorhandenen `StringBuilder`; erstelle keinen zweiten.

### Aufgabe 6 — StringBuilder ist vorgegeben

Ergänze in `Program.cs` eine neue Methode

```csharp
static void EreignisseAnhaengen(StringBuilder sb, List<(DateTime Zeitpunkt, string Ereignis)> eintraege)
```

Sie bekommt den `StringBuilder` **fertig übergeben** — erzeuge in dieser
Methode **keinen eigenen** `new StringBuilder()`. Hänge pro Eintrag genau
eine Zeile im Format `"[dd.MM.yyyy HH:mm] Ereignis"` an (Tipp: Format-
Spezifizierer aus Folie 12–13, z. B. `$"[{zeitpunkt:dd.MM.yyyy HH:mm}] ..."`).

Rufe die Methode dann so auf (analog zu `Beispiel1_StringBuilderVorgegeben`):

```csharp
var eintraege = new List<(DateTime, string)>
{
    (new DateTime(2026, 9, 21, 8, 5, 0), "Kasse geöffnet"),
    (new DateTime(2026, 9, 21, 8, 47, 0), "Warenlieferung angenommen"),
    (new DateTime(2026, 9, 21, 12, 15, 0), "Kassensturz"),
};

var protokoll = new StringBuilder();          // wird HIER erzeugt ...
protokoll.AppendLine("=== Ereignisprotokoll ===");
EreignisseAnhaengen(protokoll, eintraege);    // ... und WEITERGEGEBEN

Console.WriteLine(protokoll.ToString());
```

### Aufgabe 7 — StringBuilder muss selbst erstellt werden

Schreibe eine komplett neue Methode

```csharp
static string ErstelleVisitenkarte(string name, string beruf, string telefon, string email)
```

Sie bekommt **keinen** `StringBuilder` als Parameter — du musst innerhalb
der Methode selbst einen erzeugen (`var sb = new StringBuilder();`), einen
mehrzeiligen Text zusammenbauen und am Ende `sb.ToString()` zurückgeben.

Vorgaben für den Aufbau (Beispielbreite: 30 Zeichen):

- oben und unten je eine Rahmenzeile aus `'*'` (Tipp: `new string('*', 30)`)
- eine Zeile mit dem Namen, zentriert oder mit `PadLeft`/`PadRight` auf
  30 Zeichen ausgerichtet
- je eine Zeile für Beruf, Telefon und E-Mail

Teste die Methode mit eigenen Werten und gib das Ergebnis mit
`Console.WriteLine` aus.

### Aufgabe 8 (Zusatz, reine Strings — kein StringBuilder)

Schreibe eine Methode `string NormalisiereArtikelEingabe(string eingabe)`,
die:

1. mit `string.IsNullOrWhiteSpace` prüft, ob die Eingabe leer ist, und in
   diesem Fall `"(unbekannt)"` zurückgibt,
2. sonst die Eingabe trimmt und in Kleinbuchstaben mit großem Anfangs-
   buchstaben umwandelt (z. B. `"  BIO APFEL  "` → `"Bio apfel"`).

Teste sie mit mehreren Eingaben, darunter `null`, `""`, `"   "` und
`"  BIO APFEL  "`.

### Aufgabe 9 

Erweitere `Beispiel3_PerformanceVergleich` um eine dritte Variante: baue
denselben Text mit `StringBuilder`, aber diesmal mit **vorab gesetzter
Kapazität** (`new StringBuilder(500_000)`, Folie 18). Vergleiche alle drei
Laufzeiten bei `anzahl = 500_000`.

## Kurze Selbstkontrolle

Beantworte in 1–2 Sätzen:

1. Warum darf `EreignisseAnhaengen` (Aufgabe 6) keinen eigenen
   `StringBuilder` erzeugen, ohne dass die Aufgabe ihren Sinn verliert?
2. Was wäre der Unterschied, wenn `ErstelleKassenbon` (Beispiel 2)
   stattdessen einen `StringBuilder` als Parameter erwarten würde?
3. Warum liefert `ErstelleVisitenkarte` (Aufgabe 7) am Ende `sb.ToString()`
   zurück und nicht den `StringBuilder` selbst?

## Ausführen

```powershell
dotnet run
```
