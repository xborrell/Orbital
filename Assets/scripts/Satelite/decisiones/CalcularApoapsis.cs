using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CalcularApoapsis : Decision
{
    public override bool DebeActuar()
    {
        return ((Data.Apoapsis < 0) && (Data.OrbitaSubiendo == true));
    }

    public CalcularApoapsis(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoTomarAltura(data));
        DefinirPaso(new PasoEsperarApoapsis(data));
        DefinirPaso(new PasoGenerico(data, new LogItem( 1, "Registrant Apoapsis" ),
            x =>
            {
                Data.Apoapsis = Data.AlturaDeReferencia;
                Data.OrbitaSubiendo = null;
                return true;
            }
        ));

        LogItem = new LogItem(0, "Calc. Apoapsis", "Calculant Apoapsis");
    }
}
