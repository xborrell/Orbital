using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CalcularSentidoDeLaOrbita : DecisionCompleja
{
    public override string Descripcion
    {
        get { return "Calculando el sentido de la orbita."; }
    }

    float alturaAnterior;

    public override bool DebeActuar()
    {
        return (((Data.Periapsis < 0) || (Data.Apoapsis < 0)) && (Data.OrbitaSubiendo == null));
    }

    public CalcularSentidoDeLaOrbita(SateliteData data)
        : base(data)
    {
        DefinirPaso(SolicitarEnfoqueATierra);
        DefinirPaso(ComprobarEnfoqueCorrecto);
        DefinirPaso(TomarAltura);
        DefinirPaso(EsperarIntervaloParaCompararDatos);
        DefinirPaso(ComprobarSiLaOrbitaSubeOBaja);
    }

    void SolicitarEnfoqueATierra(float deltaTime)
    {
        Data.ActitudSolicitada = ActitudRotacion.EnfocadoATierra;
        PasoCompletado();
    }

    void ComprobarEnfoqueCorrecto(float deltaTime)
    {
        if (Data.Actitud == ActitudRotacion.EnfocadoATierra)
        {
            PasoCompletado();
        }
    }

    void TomarAltura(float deltaTime)
    {
        alturaAnterior = Data.Altura;
        PasoCompletado();
    }

    void EsperarIntervaloParaCompararDatos(float deltaTime)
    {
        PasoCompletado();
        SolicitarEspera(5);
    }

    void ComprobarSiLaOrbitaSubeOBaja(float deltaTime)
    {
        if (alturaAnterior > Data.Altura)
        {
            PasoCompletado();
            Data.OrbitaSubiendo = false;
        }
        else if (alturaAnterior < Data.Altura)
        {
            PasoCompletado();
            Data.OrbitaSubiendo = true;
        }
        else
        {
            SolicitarEspera(5);
        }
    }
}