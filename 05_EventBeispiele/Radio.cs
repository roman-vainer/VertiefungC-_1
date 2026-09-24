using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBeispiele;

public class Radio
{
    public bool IstAngeschaltet { get; private set; }

    public void Anschalten()
    {
        IstAngeschaltet = true;
    }

    public void AnschaltenAlsReaktionAufKlingeln(Object? sender, EventArgs e) { 
        Anschalten();
        Console.WriteLine($"  [Radio] Radio schaltet sich ein");
    }
}
