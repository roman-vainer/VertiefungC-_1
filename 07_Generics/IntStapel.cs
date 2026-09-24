namespace Generics;

// Variante A: Typparameter wird beim Erben festgelegt — IntStapel ist
// selbst keine generische Klasse mehr, sondern fertig auf int spezialisiert.
public class IntStapel : Stapel<int>
{
}
