using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CalcularSentidoDeLaOrbita : Decision
{
    public override bool DebeActuar()
    {
        return (((Data.Periapsis < 0) || (Data.Apoapsis < 0)) && (Data.OrbitaSubiendo == null));
    }

    public override void Inicializar()
    {
        base.Inicializar();

        Data.AlturaDeReferencia = -1;
    }

    public CalcularSentidoDeLaOrbita(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoTomarAltura(data));
        DefinirPaso(new PasoEsperar(data, 5, new LogItem( 1, "Esperar", "Esperant per evaluar el sentit")));
        DefinirPaso(new PasoGenerico(data,new LogItem( 1, "Sentit orbita", "Comprobant el sentit de l'orbita" ), ComprobarSiLaOrbitaSubeOBaja));

        LogItem = new LogItem(0, "Calc. sentit", "Calculant el sentit de l'orbita");
    }

    bool ComprobarSiLaOrbitaSubeOBaja()
    {
        if ( Data.AlturaDeReferencia > Data.Altura)
        {
            Data.OrbitaSubiendo = false;
            return true;
        }

        if (Data.AlturaDeReferencia < Data.Altura)
        {
            Data.OrbitaSubiendo = true;
            return true;
        }

        SolicitarEspera(5);
        return false;
    }
}