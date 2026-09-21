# Mini-Projekt 5: Drei Event-Beispiele

Baut auf Mini-Projekt 3 auf. Voraussetzung: Events als "verpackte Delegates"
und das Publisher/Subscriber-Muster verstanden.

## Ziel dieses Projekts

Mini-Projekt 3 hat das komplette Muster an **einem** Beispiel (Temperatursensor)
gezeigt. Hier geht es um die Breite: **drei** bewusst unterschiedliche
Beispiele, die genau eine Sache variieren – wie viel (und welche Art von)
Daten das `EventArgs`-Objekt transportiert. Danach sollte der Unterschied
zwischen `EventHandler`, `EventHandler<T>` und eigenen `EventArgs`-Klassen
nicht mehr abstrakt sein, sondern an drei konkreten Situationen festgemacht.

## Aufbau

1. **Beispiel 1 – `Wecker`**: Event **ohne** Zusatzdaten.
   Der eingebaute, nicht-generische Typ `EventHandler` reicht, wenn die
   Nachricht komplett ist mit "es ist passiert". Ausgelöst wird mit
   `EventArgs.Empty` statt `null` – Konvention, damit Handler sich darauf
   verlassen können, dass `e` nie `null` ist.

2. **Beispiel 2 – `Warenkorb`**: Event **mit nur-lesbaren** Zusatzdaten.
   `ArtikelHinzugefuegtEventArgs` bündelt `ArtikelName`, `Preis` und
   `NeueSumme`. Zwei Subscriber (`Rabattpruefer`, `Bonuspunkteberechnung`)
   lesen dieselben Daten und reagieren unterschiedlich darauf – ohne sich
   gegenseitig zu beeinflussen, weil keiner von ihnen etwas verändern kann.

3. **Beispiel 3 – `Tuer`**: Event mit **lese- und schreibbaren** Zusatzdaten.
   `TuerSchliesstEventArgs.Abbrechen` hat einen `set`-Zugriff – die eine
   dokumentierte Ausnahme von "EventArgs ist nur lesbar" (Vorbild:
   `System.ComponentModel.CancelEventArgs`, z. B. bei `Form.Closing` in
   WinForms). Ein Subscriber (`Bewegungsmelder`) kann darüber dem Publisher
   *zurückmelden*, dass die Aktion abgebrochen werden soll.

## Warum ist das wichtig?

Der Aha-Moment liegt in Beispiel 3: `Invoke` selbst gibt `void` zurück –
trotzdem kann ein Event dem Publisher "etwas mitteilen". Das funktioniert,
weil `EventArgs` ein **Referenztyp** ist. Alle Subscriber bekommen dieselbe
Objekt-Referenz; schreibt einer eine Eigenschaft darauf, sieht der Publisher
das nach `Invoke`, weil er dasselbe Objekt in der Hand hält. Das ist exakt
der Mechanismus hinter `FormClosing`/`CancelEventArgs` in WinForms oder
`PropertyChanging` in WPF – kein Sonderfall, sondern dieselbe Sprache wie in
Beispiel 1 und 2, nur mit einer beschreibbaren statt nur-lesbaren
Eigenschaft.

## Übungsaufgaben zum Erweitern

1. **Beispiel 1 erweitern**: Füge einen dritten Subscriber `Radio` hinzu, der
   sich beim `Klingelt`-Event anmeldet und "Radio schaltet sich ein" ausgibt.
2. **Beispiel 2 erweitern**: Füge `ArtikelHinzugefuegtEventArgs` eine
   Eigenschaft `Kategorie` (string) hinzu und lasse `Rabattpruefer` nur für
   Artikel der Kategorie `"Elektronik"` prüfen.
3. **Zweites Event in Beispiel 2**: Ergänze `Warenkorb` um ein Event
   `WarenkorbGeleert` ohne Zusatzdaten (also `EventHandler`, wie in
   Beispiel 1), das beim Leeren des Warenkorbs ausgelöst wird.
4. **Reihenfolge-Falle in Beispiel 3**: Melde in `Beispiel3_Tuer` einen
   zweiten `Bewegungsmelder` an, der `erkenntBewegungImRahmen: false` hat,
   und zwar **nach** dem ersten (der `true` hat). Führe das Programm aus und
   erkläre in einem Kommentar, warum die Tür trotzdem nicht schließt –
   und was passieren würde, wenn ein sorgloser Subscriber `e.Abbrechen`
   ungefragt wieder auf `false` zurücksetzen würde.
5. **Eigenes abbrechbares Event bauen**: Baue eine Klasse `Ueberweisung` mit
   einer Methode `Ausfuehren()` und einem Event `WirdAusgefuehrt` vom Typ
   `EventHandler<UeberweisungEventArgs>`, dessen `EventArgs`-Klasse einen
   `Betrag` (nur lesbar) und ein `Abbrechen` (lese-/schreibbar) hat. Ein
   Subscriber `Betrugspruefung` soll bei Beträgen über 10.000 € abbrechen.

## Ausführen

```powershell
dotnet run
```
