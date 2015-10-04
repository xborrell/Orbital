using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CalcularPeriapsis : DecisionCompleja
{
    public override string Descripcion
    {
        get { return "Calc. Periapsis"; }
    }

    float alturaAnterior;

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
        DefinirPaso(new PasoGenerico(data, "Registrando Periapsis", x => { Data.Periapsis = Data.AlturaDeReferencia; return true; }));
    }
}
