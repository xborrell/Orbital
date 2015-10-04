using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CalcularApoapsis : DecisionCompleja
{
    public override string Descripcion
    {
        get { return "Calc. Apoapsis"; }
    }

    float alturaAnterior;

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
        DefinirPaso(new PasoGenerico(data, "Registrando Apoapsis", x => { Data.Apoapsis = Data.AlturaDeReferencia; return true; }));
    }
}
