using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ConversorOrbital
{
    public OrbitalElements Convertir(Vector3 posicion, Vector3 velocidad)
    {
        float vr = (float)Math.Round((posicion.x * velocidad.x + posicion.y * velocidad.y + posicion.z * velocidad.z) / posicion.magnitude, 4);
        Vector3 angularMomentum = Vector3.Cross(posicion, velocidad);
        float inclination = (float)Math.Acos(angularMomentum.z / angularMomentum.magnitude);
        Vector3 nodeLine = Vector3.Cross( Vector3.forward, angularMomentum);
        float angleOfAscendingNode = NormalizarCuadrante(nodeLine.y < 0, Math.Acos(nodeLine.z / nodeLine.magnitude));
        Vector3 excentricityVector = (1 / Config.Mu) * ((posicion * (float)(Math.Pow(velocidad.magnitude, 2) - (Config.Mu / posicion.magnitude))) - (velocidad * posicion.magnitude * vr));
        double excentricity = excentricityVector.magnitude;
        float argumentOfPeriapsis = NormalizarCuadrante(excentricityVector.z < 0, Math.Acos(Vector3.Dot(nodeLine, excentricityVector) / (nodeLine.magnitude * excentricity)));
        float trueAnomaly = NormalizarCuadrante(vr < 0, Math.Acos(Vector3.Dot(excentricityVector, posicion) / (excentricity * posicion.magnitude)));

        return new OrbitalElements((float)angularMomentum.magnitude, (float)excentricity, inclination, angleOfAscendingNode, argumentOfPeriapsis, trueAnomaly);
    }

    float NormalizarCuadrante(bool invertirCuadrante, double angulo)
    {
        return NormalizarCuadrante(invertirCuadrante, (float)angulo);
    }

    float NormalizarCuadrante(bool invertirCuadrante, float angulo)
    {
        return invertirCuadrante ? Config.Pi2 - angulo : angulo;
    }
}
