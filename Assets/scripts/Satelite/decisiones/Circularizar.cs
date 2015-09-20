using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Circularizar : DecisionCompleja
{
    const float margenDeCircularización = 0.1F;

    public override string Descripcion
    {
        get { return "Circularizar la orbita."; }
    }

    public override bool DebeActuar()
    {
        return (Data.Periapsis > 0)
            && (Data.Apoapsis > 0)
            && (Math.Abs(Data.Apoapsis - Data.Periapsis) < margenDeCircularización);
    }

    public Circularizar(SateliteData data)
        : base(data)
    {
        DefinirPaso(SolicitarEnfoqueOrbital);
        DefinirPaso(ComprobarEnfoqueCorrecto);
        DefinirPaso(CalcularValoresDelImpulso);
        DefinirPaso(CalcularMomentoDeIgnicion);
        DefinirPaso(EsperarMomentoDeIgnicion);
        DefinirPaso(EncenderMotor);
        DefinirPaso(EsperarDuracionDelImpulso);
        DefinirPaso(ApagarMotor);
        DefinirPaso(ResetearValoresOrbitales);
    }

    void SolicitarEnfoqueOrbital(float deltaTime)
    {
        Data.ActitudSolicitada = ActitudRotacion.Orbital;
        PasoCompletado();
    }

    void ComprobarEnfoqueCorrecto(float deltaTime)
    {
        if (Data.Actitud == ActitudRotacion.Orbital)
        {
            PasoCompletado();
        }
    }

    void CalcularValoresDelImpulso(float deltaTime)
    {
        PasoCompletado();
    }

    void CalcularMomentoDeIgnicion(float deltaTime)
    {
        PasoCompletado();
    }

    void EsperarMomentoDeIgnicion(float deltaTime)
    {
        PasoCompletado();
        SolicitarEspera(15);
    }

    void EncenderMotor(float deltaTime)
    {
        PasoCompletado();
    }

    void EsperarDuracionDelImpulso(float deltaTime)
    {
        PasoCompletado();
    }

    void ApagarMotor(float deltaTime)
    {
        PasoCompletado();
    }

    void ResetearValoresOrbitales(float deltaTime)
    {
        Data.InvalidateOrbitalValues();
        PasoCompletado();
    }
}