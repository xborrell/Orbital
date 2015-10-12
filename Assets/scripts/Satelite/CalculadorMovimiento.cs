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

    public void CalcularNuevaPosicion(float deltaTime)
    {
        Vector3 fuerzaGravitatoria = CalcularFuerzaGravitatoria();
        Vector3 aceleracionGravitatoria = fuerzaGravitatoria / Config.SatellitMass;

        _data.Velocidad = _data.Velocidad + (aceleracionGravitatoria * deltaTime);
        Vector3 desplazamiento = _data.Velocidad * deltaTime;
        _data.Posicion += desplazamiento;
    }

    Vector3 CalcularFuerzaGravitatoria()
    {
        double gravitationModulus = CalcularAtraccionTerrestre(Config.SatellitMass, _data.Posicion.magnitude);
        Vector3 gravitationForce = new Vector3(_data.Posicion.x, _data.Posicion.y, _data.Posicion.z);
        gravitationForce.Normalize();
        gravitationForce = gravitationForce * (float)(gravitationModulus * -1);

        return gravitationForce;
    }

    private double CalcularAtraccionTerrestre(float masaSatelite, float distancia)
    {
        var distancia2 = Math.Pow(distancia, 2);
        var numerador = masaSatelite * Config.Mu;

        return numerador / distancia2;
    }
}
