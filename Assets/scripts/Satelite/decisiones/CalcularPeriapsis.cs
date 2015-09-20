using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CalcularPeriapsis : DecisionCompleja
{
    public override string Descripcion
    {
        get { return "Calculando Periapsis"; }
    }

    float alturaAnterior;

    public override bool DebeActuar()
    {
        return ((Data.Periapsis < 0) && (Data.OrbitaSubiendo == false));
    }

    public CalcularPeriapsis(SateliteData data)
        : base(data)
    {
        DefinirPaso(SolicitarEnfoqueATierra);
        DefinirPaso(ComprobarEnfoqueCorrecto);
        DefinirPaso(TomarAltura);
        DefinirPaso(Calcular);
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

    void Calcular(float deltaTime)
    {
        if (alturaAnterior < Data.Altura)
        {
            Data.Periapsis = alturaAnterior;
            Data.OrbitaSubiendo = null;
            PasoCompletado();
        }
        else
        {
            alturaAnterior = Data.Altura;
            SolicitarEspera(30);
        }
    }
}
