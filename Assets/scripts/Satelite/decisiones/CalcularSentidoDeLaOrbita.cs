using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CalcularSentidoDeLaOrbita : DecisionCompleja
{
    public override string Descripcion
    {
        get { return "Calc. sentido orbita."; }
    }

    float alturaAnterior;

    public override bool DebeActuar()
    {
        return (((Data.Periapsis < 0) || (Data.Apoapsis < 0)) && (Data.OrbitaSubiendo == null));
    }

    public CalcularSentidoDeLaOrbita(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoTomarAltura(data));
        DefinirPaso(new PasoEsperar(data, 5, "Esperando Para Evaluar el Sentido"));
        DefinirPaso(new PasoGenerico(data,"Comprobando el Sentido de la Órbita", ComprobarSiLaOrbitaSubeOBaja));
    }

    bool ComprobarSiLaOrbitaSubeOBaja(float deltaTime)
    {
        if (alturaAnterior > Data.Altura)
        {
            Data.OrbitaSubiendo = false;
            return true;
        }

        if (alturaAnterior < Data.Altura)
        {
            Data.OrbitaSubiendo = true;
            return true;
        }

        SolicitarEspera(5);
        return false;
    }
}