# Mini-Projekt 3: Events mit eigenem Delegate

Baut auf Mini-Projekt 1 und 2 auf. Voraussetzung: Delegate-Grundlagen und
Multicast-Delegates verstanden.

## Ziel dieses Projekts

Verstehen, dass ein **Event** technisch nichts anderes ist als ein
**Multicast-Delegate mit eingeschränktem Zugriff** – und das komplette
Publisher/Subscriber-Muster in C# selbst gebaut haben.

## Aufbau

1. **`TemperaturEventArgs`**: eigene Datenklasse, die von `EventArgs` erbt.
   Konvention in .NET: Event-Daten werden gebündelt statt als einzelne
   Parameter übergeben, damit man später erweitern kann, ohne alle
   Aufrufer anzupassen.

2. **`TemperaturUeberschrittenHandler`**: ein eigener Delegate-Typ mit der
   .NET-üblichen Event-Signatur `(object? sender, TEventArgs e)`.

3. **`Temperatursensor`** ist der **Publisher**:
   ```csharp
   public event TemperaturUeberschrittenHandler? TemperaturUeberschritten;
   ```
   Das Schlüsselwort `event` "verpackt" den Delegate:
   - Von **aussen** darf man nur `+=` (anmelden) und `-=` (abmelden).
   - Von **aussen** kann man das Event NICHT aufrufen oder mit `=`
     überschreiben – nur die Klasse selbst darf das (über
     `OnTemperaturUeberschritten`).
   - Das verhindert typische Fehler, z. B. dass ein Subscriber versehentlich
     alle anderen Subscriber löscht.

4. **`Alarmanlage`** und **`BenachrichtigungsDienst`** sind **Subscriber**.
   Sie kennen den Sensor, der Sensor kennt sie aber nicht als Klassen –
   er kennt nur "Methoden mit passender Signatur, die sich angemeldet haben".
   Das nennt man **lose Kopplung**.

5. Am Ende zeigt `StandardTemperatursensor`, dass man in der Praxis fast nie
   einen eigenen Delegate-Typ für Events schreibt, sondern das eingebaute
   generische `EventHandler<TEventArgs>` verwendet.

## Warum ist das wichtig?

Events sind das Rückgrat von:
- UI-Frameworks (Button-Klicks, WPF/WinForms/Blazor)
- Beobachtbare Zustandsänderungen (z. B. `INotifyPropertyChanged`)
- Lose gekoppelten Architekturen, in denen der "Sender" nicht wissen muss,
  wer alles zuhört

Wer das Publisher/Subscriber-Muster einmal selbst gebaut hat, versteht
sofort, warum `button.Click += ...` in jeder UI-Anwendung so funktioniert,
wie es funktioniert.

## Übungsaufgaben zum Erweitern

1. **Neues Event hinzufügen**: Füge dem `Temperatursensor` ein zweites Event
   `TemperaturNormalisiert` hinzu, das ausgelöst wird, wenn die Temperatur
   nach einer Überschreitung wieder unter den Schwellenwert fällt.
2. **Mehrfaches Abonnieren verhindern**: Was passiert, wenn du
   `sensor.TemperaturUeberschritten += alarmanlage.AufTemperatur_Reagieren;`
   zweimal ausführst? Probiere es aus und beschreibe das Verhalten als
   Kommentar.
3. **Eigenes EventArgs erweitern**: Füge `TemperaturEventArgs` eine
   `DateTime`-Eigenschaft `Zeitpunkt` hinzu und gib sie bei jeder Reaktion mit
   aus.
4. **Eigenes Mini-System bauen**: Baue eine Klasse `Bestellung` mit einem
   Event `BestellungStorniert` (nutze `EventHandler<EventArgs>` oder
   `EventHandler`), auf das sich ein `LagerService` und ein `KundenService`
   anmelden.
5. **Unterschied event vs. normaler Delegate**: Entferne testweise das
   Schlüsselwort `event` vor `TemperaturUeberschritten` in
   `Temperatursensor` und versuche in `Main`,
   `sensor.TemperaturUeberschritten = null;` aufzurufen. Was ändert sich?
   Notiere den Unterschied.

## Ausführen

```powershell
dotnet run
```
