# Mini-Projekt 2: Multicast-Delegates & Action/Func/Predicate

Baut auf Mini-Projekt 1 auf. Voraussetzung: Grundverständnis, was ein
Delegate ist.

## Ziel dieses Projekts

1. Verstehen, dass ein Delegate **mehrere Methoden gleichzeitig** referenzieren
   kann ("Multicast-Delegate") und mit `+=` / `-=` verwaltet wird.
2. Kennenlernen der eingebauten generischen Delegate-Typen `Action<...>`,
   `Func<..., TResult>` und `Predicate<T>` – damit man in der Praxis fast nie
   einen eigenen Delegate-Typ deklarieren muss.
3. Die "Falle" bei Multicast-Delegates mit Rückgabewert verstehen.

## Aufbau

- **Teil 1 – Multicast**: An `benachrichtigen` (Typ `BenachrichtigungsHandler`)
  werden per `+=` drei Methoden gehängt. Ein einziger Aufruf
  (`benachrichtigen.Invoke(...)`) führt automatisch **alle drei** nacheinander
  aus. Das ist die technische Basis für Events (Projekt 3).

- **Teil 2 – Abmelden**: Mit `-=` kann eine bereits angehängte Methode wieder
  entfernt werden. Wichtig: Es muss exakt dieselbe Methode(nreferenz) sein.

- **Teil 3 – Action / Func / Predicate**: Statt eigene Delegate-Typen zu
  deklarieren, nutzt man in der Praxis fast immer:
  - `Action<T1, T2, ...>` – Methode ohne Rückgabewert
  - `Func<T1, ..., TResult>` – Methode mit Rückgabewert (letzter Typ-Parameter)
  - `Predicate<T>` – Kurzform für `Func<T, bool>`, häufig bei Filtern
    (z. B. `Array.FindAll`)

- **Teil 4 – Rückgabewert-Falle**: Wenn mehrere `Func<...>`-Methoden an einen
  Delegate gehängt werden, gibt `Invoke()` nur den Rückgabewert der
  **zuletzt** angehängten Methode zurück. Um alle Ergebnisse zu bekommen,
  muss man `GetInvocationList()` benutzen und jede Methode einzeln aufrufen.

## Warum ist das wichtig?

- Multicast ist die Grundlage für **Events**: Ein Button kann von mehreren
  Stellen im Code "beobachtet" werden, ohne dass die Stellen sich gegenseitig
  kennen müssen.
- `Action`/`Func`/`Predicate` sparen Code und sind in fast jeder modernen
  C#-Codebasis (insbesondere LINQ) im Einsatz.
- Die Rückgabewert-Falle ist ein beliebter Interview- bzw. Verständnisfehler –
  wer sie einmal live gesehen hat, vergisst sie nicht mehr.

## Übungsaufgaben zum Erweitern

1. **Eigene Pipeline bauen**: Erstelle einen `Func<int, int>`-Multicast-
   Delegate mit drei Schritten (z. B. `+1`, `*2`, `-3`) und finde mit
   `GetInvocationList()` heraus, welches Zwischenergebnis jeder Schritt für
   sich alleine liefern würde.
2. **Exception in der Mitte der Kette**: Was passiert, wenn die zweite von
   drei Methoden in einem Multicast-`Action`-Delegate eine Exception wirft?
   Probiere es aus und beschreibe (als Kommentar im Code), ob die dritte
   Methode noch ausgeführt wird.
3. **Predicate-Übung**: Schreibe ein `Predicate<string>`, das prüft, ob ein
   String mit einem Grossbuchstaben beginnt, und filtere damit eine Liste von
   Namen.
4. **Eigene Erweiterung**: Baue eine kleine "Ereignis-Pipeline" für ein
   Bestellsystem: Ein `Action<string>`-Delegate namens `bestellungAbgeschlossen`,
   an den du drei Methoden hängst: `RechnungErstellen`, `LagerAktualisieren`,
   `KundenEmailSenden`.

## Ausführen

```powershell
dotnet run
```
