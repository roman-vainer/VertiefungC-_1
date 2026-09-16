# Mini-Projekt 4: Action, Func, Predicate & Comparison an einem Beispiel

Baut auf Mini-Projekt 1–3 auf. Voraussetzung: Delegate-Grundlagen,
Multicast-Delegates, `Action`/`Func`/`Predicate` bereits aus Mini-Projekt 2
bekannt.

## Ziel dieses Projekts

Die vier wichtigsten **eingebauten, generischen Delegate-Typen** von .NET
nicht isoliert, sondern **gemeinsam an einem durchgängigen Beispiel**
(einer kleinen Kontaktverwaltung) sehen – damit klar wird, wofür man welchen
Typ konkret einsetzt.

| Delegate-Typ      | Frage, die er beantwortet | Rückgabewert | Typisches Einsatzgebiet |
|--------------------|----------------------------|---------------|---------------------------|
| `Action<T>`        | "Tu etwas mit T"           | keiner (`void`) | Ausgeben, Loggen, `List<T>.ForEach` |
| `Func<T, TResult>` | "Berechne/wandle T um"     | `TResult`      | Projektionen, `Select`, Formatierung |
| `Predicate<T>`     | "Erfüllt T eine Bedingung?"| immer `bool`   | Filtern/Suchen, `FindAll`, `RemoveAll` |
| `Comparison<T>`    | "Was kommt zuerst, a oder b?" | `int` (<0, 0, >0) | Eigene Sortierlogik, `List<T>.Sort` |

## Aufbau

Datenbasis ist eine Liste von `Kontakt`-Objekten (Name, Alter, Stadt).

1. **`Action<Kontakt>`**: `kontaktAusgeben` gibt einen Kontakt formatiert aus.
   Wird an `List<T>.ForEach(Action<T>)` übergeben – für jedes Element der
   Liste wird die Action einmal ausgeführt.

2. **`Predicate<Kontakt>`**: `istVolljaehrig` und `wohntInBerlin` prüfen je
   eine Bedingung. Beide werden zu `volljaehrigUndBerlin` kombiniert und an
   `List<T>.FindAll(Predicate<T>)` übergeben, um nur passende Kontakte zu
   erhalten.

3. **`Func<Kontakt, string>`**: `anzeigetextErzeugen` wandelt einen Kontakt
   in einen Anzeigetext um. Wird mit LINQ `.Select(Func<T, TResult>)`
   verwendet. Zusätzlich zeigt `Func<Kontakt, int, string>`, dass `Func` auch
   mit mehreren Eingabeparametern funktioniert (der letzte Typ-Parameter ist
   immer der Rückgabetyp).

4. **`Comparison<Kontakt>`**: `nachAlterAufsteigend` vergleicht zwei
   Kontakte anhand des Alters und wird an `List<T>.Sort(Comparison<T>)`
   übergeben. `nachStadtDannName` zeigt eine mehrstufige Sortierung
   (zuerst Stadt, bei Gleichstand Name) – eine sehr verbreitete
   Sortier-Anforderung in der Praxis.

5. **Alle vier zusammen**: `BerichtErstellen(...)` nimmt bewusst alle vier
   Delegate-Arten als Parameter entgegen und zeigt, wie sie sich zu einer
   kleinen Verarbeitungs-Pipeline zusammensetzen lassen:
   **filtern (Predicate) → sortieren (Comparison) → umwandeln (Func) →
   ausgeben (Action)**.

## Warum ist das wichtig?

Diese vier Typen sind die "Werkzeugkiste", die man in nahezu jedem
C#-Projekt täglich benutzt – meist ohne es bewusst "Delegate" zu nennen,
weil sie über Lambda-Ausdrücke sehr natürlich wirken. Wer weiss, welcher
Typ wofür gedacht ist, liest fremden Code (und LINQ-Ketten) deutlich
schneller.

Wichtige Beobachtung: `Predicate<T>` ist letztlich nichts anderes als ein
Spezialfall von `Func<T, bool>` – .NET bietet `Predicate<T>` trotzdem
separat an, weil viele ältere `List<T>`-Methoden (`FindAll`, `RemoveAll`,
`Find`, `Exists`, `TrueForAll`) historisch genau diesen Typ erwarten.

## Übungsaufgaben zum Erweitern

1. **Eigenes Predicate**: Schreibe ein `Predicate<Kontakt>`, das prüft, ob
   der Name mit einem bestimmten Buchstaben beginnt, und filtere damit.
2. **Eigenes Func**: Schreibe ein `Func<Kontakt, int>`, das die
   verbleibenden Jahre bis zum 67. Lebensjahr (Renteneintritt) berechnet,
   und gib das Ergebnis für jeden Kontakt aus.
3. **Eigene Comparison**: Schreibe ein `Comparison<Kontakt>`, das absteigend
   nach Alter sortiert (Tipp: Vergleich einfach umdrehen oder mit `-1`
   multiplizieren).
4. **RemoveAll statt FindAll**: Nutze `List<T>.RemoveAll(Predicate<T>)`, um
   alle minderjährigen Kontakte direkt aus einer Kopie der Liste zu
   entfernen, statt sie nur herauszufiltern.
5. **Eigene Pipeline zusammenstellen**: Rufe `BerichtErstellen` ein zweites
   Mal mit anderen Delegates auf: Filter = "wohnt in Hamburg oder München",
   Sortierung = nach Name, Formatierung = nur Name in Grossbuchstaben,
   Ausgabe = mit einer laufenden Nummer davor.

## Ausführen

```powershell
dotnet run
```
