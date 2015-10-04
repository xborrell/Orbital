using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Circularizar : DecisionCompleja
{
    ActitudRotacion actitudSolicitada;
    const float margenDeCircularización = 0.1F;
    float impulsoNecesario;
    float duracionDelImpulsoEnSegundos;
    float marcaDeTiempo;
    float duracionDeMediaOrbita;
    float nuevaVelocidad;

    public override string Descripcion
    {
        get { return "Circularizar orbita."; }
    }

    public override bool DebeActuar()
    {
        return (Data.Periapsis > 0)
            && (Data.Apoapsis > 0)
            && (Math.Abs(Data.Apoapsis - Data.Periapsis) > margenDeCircularización);
    }

    public Circularizar(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoGenerico(data,"Calcular valor del Impulso", CalcularValoresDelImpulso));
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoTomarAltura(data));
        DefinirPaso(new PasoEsperarPeriapsis(data));
        DefinirPaso(new PasoEsperarApoapsis(data));
        DefinirPaso(new PasoGenerico(data, "Cambiar velocidad", CambiarVelocidad));

        //DefinirPaso(new Paso( "", EsperarPeriapsis));
        //DefinirPaso(new Paso( "", SolicitarEnfoqueOrbital));
        //DefinirPaso(new Paso( "", ComprobarEnfoqueCorrecto));
        //DefinirPaso(new Paso( "", EsperarMomentoDeIgnicion));
        //DefinirPaso(new Paso( "", EncenderMotor));
        //DefinirPaso(new Paso( "", EsperarDuracionDelImpulso));
        //DefinirPaso(new Paso( "", ApagarMotor));
        DefinirPaso(new PasoGenerico(data, "Resetear valores orbitales", ResetearValoresOrbitales));
    }

    private bool CambiarVelocidad(float delta)
    {
        Data.Velocidad.Normalize();
        Data.Velocidad *= nuevaVelocidad;

        return true;
    }

    public override void Inicializar()
    {
        base.Inicializar();

        impulsoNecesario = -1;
    }

    bool CalcularValoresDelImpulso(float deltaTime)
    {
        double radioPeriapsis = Data.Periapsis + Config.EarthRadius;
        double radioApoapsis = Data.Apoapsis + Config.EarthRadius;
        double compartido = Math.Sqrt(Config.Mu * 2);
        double momentoAngularActual = compartido * Math.Sqrt((radioApoapsis * radioPeriapsis) / (radioApoapsis + radioPeriapsis));
        double momentoAngularDeseado = compartido * Math.Sqrt((radioApoapsis * radioApoapsis) / (radioApoapsis + radioApoapsis));
        double velocidadActualenApoapsis = momentoAngularActual / radioApoapsis;
        double velocidadDeseadaEnApoapsis = momentoAngularDeseado / radioApoapsis;

        nuevaVelocidad = (float)velocidadDeseadaEnApoapsis;

        impulsoNecesario = (float)(velocidadDeseadaEnApoapsis - velocidadActualenApoapsis);
        duracionDelImpulsoEnSegundos = impulsoNecesario / Config.ImpulsoMaximo;

        if (duracionDelImpulsoEnSegundos > 5) duracionDelImpulsoEnSegundos = 5;

        return true;
    }

    bool EsperarMomentoDeIgnicion(float deltaTime)
    {
        float tiempoYaGastado = Time.time - marcaDeTiempo;
        float tiempoAntesDeApoapsisParaIgnicion = duracionDelImpulsoEnSegundos / 2;
        float tiempoAEsperar = duracionDeMediaOrbita - tiempoYaGastado - tiempoAntesDeApoapsisParaIgnicion;

        SolicitarEspera(tiempoAEsperar);

        return true;
    }

    bool EncenderMotor(float deltaTime)
    {
        Data.ImpulsoSolicitado = Config.ImpulsoMaximo;
        return true;
    }

    bool EsperarDuracionDelImpulso(float deltaTime)
    {
        SolicitarEspera(duracionDelImpulsoEnSegundos);
        return true;
    }

    bool ApagarMotor(float deltaTime)
    {
        Data.ImpulsoSolicitado = 0;

        return true;
    }

    bool ResetearValoresOrbitales(float deltaTime)
    {
        Data.InvalidateOrbitalValues();
        
        return true;
    }
}