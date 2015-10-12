using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CalcularPeriapsis : Decision
{
    public override bool DebeActuar()
    {
        return ((Data.Periapsis < 0) && (Data.OrbitaSubiendo == false));
    }

    public CalcularPeriapsis(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoTomarAltura(data));
        DefinirPaso(new PasoEsperarPeriapsis(data));
        DefinirPaso(new PasoGenerico(data, new LogItem(1,"Registrant Periapsis"), 
            x => { 
                Data.Periapsis = Data.AlturaDeReferencia; 
                Data.OrbitaSubiendo = null; 
                return true; 
            }
        ));

        LogItem = new LogItem(0, "Calc. Periapsis", "Calculant Periapsis");
    }
}
