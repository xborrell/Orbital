using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Circularizar : Decision
{
    const float margenDeCircularización = 0.1F;
    float impulsoNecesario;
    float duracionDelImpulsoEnSegundos;
    float nuevaVelocidad;

    public override bool DebeActuar()
    {
        return (Data.Periapsis > 0)
            && (Data.Apoapsis > 0)
            && (Math.Abs(Data.Apoapsis - Data.Periapsis) > margenDeCircularización);
    }

    public Circularizar(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoGenerico(data,new LogItem( 1, "Calcular impuls", "Calcular el valor del impuls."), CalcularValoresDelImpulso));
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoTomarAltura(data));
        DefinirPaso(new PasoEsperarPeriapsis(data));
        DefinirPaso(new PasoEsperarApoapsis(data));
        DefinirPaso(new PasoGenerico(data, new LogItem( 1, "Cambiar velocitat"), CambiarVelocidad));

        //DefinirPaso(new Paso( "", EsperarPeriapsis));
        //DefinirPaso(new Paso( "", SolicitarEnfoqueOrbital));
        //DefinirPaso(new Paso( "", ComprobarEnfoqueCorrecto));
        //DefinirPaso(new Paso( "", EsperarMomentoDeIgnicion));
        //DefinirPaso(new Paso( "", EncenderMotor));
        //DefinirPaso(new Paso( "", EsperarDuracionDelImpulso));
        //DefinirPaso(new Paso( "", ApagarMotor));
        DefinirPaso(new PasoGenerico(data, new LogItem( 1, "Resetejar", "Resetejar valors orbitals" ), ResetearValoresOrbitales));

        LogItem = new LogItem( 0, "Orbita circular", "Fer l'orbita circular");
    }

    private bool CambiarVelocidad()
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

    bool CalcularValoresDelImpulso()
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

    //bool EsperarMomentoDeIgnicion(float deltaTime)
    //{
    //    float tiempoYaGastado = Time.time - marcaDeTiempo;
    //    float tiempoAntesDeApoapsisParaIgnicion = duracionDelImpulsoEnSegundos / 2;
    //    float tiempoAEsperar = duracionDeMediaOrbita - tiempoYaGastado - tiempoAntesDeApoapsisParaIgnicion;

    //    SolicitarEspera(tiempoAEsperar);

    //    return true;
    //}

    //bool EncenderMotor(float deltaTime)
    //{
    //    Data.ImpulsoSolicitado = Config.ImpulsoMaximo;
    //    return true;
    //}

    //bool EsperarDuracionDelImpulso(float deltaTime)
    //{
    //    SolicitarEspera(duracionDelImpulsoEnSegundos);
    //    return true;
    //}

    //bool ApagarMotor(float deltaTime)
    //{
    //    Data.ImpulsoSolicitado = 0;

    //    return true;
    //}

    bool ResetearValoresOrbitales()
    {
        Data.InvalidateOrbitalValues();
        
        return true;
    }
}