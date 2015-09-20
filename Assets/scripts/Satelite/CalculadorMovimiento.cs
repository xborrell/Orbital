using UnityEngine;
using System.Collections;
using System;

public class CalculadorMovimiento
{
    SateliteData _data;

    public CalculadorMovimiento(SateliteData data)
    {
        _data = data;
    }

    public Vector3 CalcularNuevaPosicion(float deltaTime)
    {
        Vector3 fuerzaGravitatoria = CalcularFuerzaGravitatoria();
        Vector3 aceleracionGravitatoria = fuerzaGravitatoria / Config.SatellitMass;

        _data.Velocidad = _data.Velocidad + (aceleracionGravitatoria * deltaTime);
        Vector3 desplazamiento = _data.Velocidad * deltaTime;
        _data.Posicion += desplazamiento;

        return new Vector3( _data.Posicion.x, _data.Posicion.z, _data.Posicion.y );
    }

    Vector3 CalcularFuerzaGravitatoria()
    {
        double gravitationModulus = CalcularAtraccion(Config.EarthMass, Config.SatellitMass, _data.Posicion.magnitude);
        Vector3 gravitationForce = new Vector3(_data.Posicion.x, _data.Posicion.y, _data.Posicion.z);
        gravitationForce.Normalize();
        gravitationForce = gravitationForce * (float)(gravitationModulus * -1);

        return gravitationForce;
    }

    private double CalcularAtraccion(float masa1, float masa2, float distancia)
    {
        var masas = masa1 * masa2;
        var distancia2 = Math.Pow(distancia, 2);
        var numerador = masas * Config.ConstanteGravitacionUniversal;

        return numerador / distancia2;
    }
}
