using UnityEngine;
using System.Collections;
using System;

namespace CalculadorOrbital
{

    public class CalculadorMovimiento
    {
        SateliteData _data;
        const float incremento = 0.1F;

        public CalculadorMovimiento(SateliteData data)
        {
            _data = data;
        }

        public void CalcularNuevaPosicion(float deltaTime)
        {
            while (deltaTime > incremento)
            {
                InternalCalcularNuevaPosicion(incremento);
                deltaTime -= incremento;
            }

            if (deltaTime > 0)
            {
                InternalCalcularNuevaPosicion(deltaTime);
            }
        }

        public void InternalCalcularNuevaPosicion(float deltaTime)
        {

            Vector3 fuerzaGravitatoria = CalcularFuerzaGravitatoria();
            Vector3 aceleracionGravitatoria = fuerzaGravitatoria / Constantes.SatellitMass;

            _data.Velocidad = _data.Velocidad + (aceleracionGravitatoria * deltaTime);
            Vector3 desplazamiento = _data.Velocidad * deltaTime;
            _data.Posicion += desplazamiento;
        }

        Vector3 CalcularFuerzaGravitatoria()
        {
            double gravitationModulus = CalcularAtraccionGravitatoria(Constantes.SatellitMass, _data.Posicion.magnitude);
            Vector3 gravitationForce = new Vector3(_data.Posicion.x, _data.Posicion.y, _data.Posicion.z);
            gravitationForce.Normalize();
            gravitationForce = gravitationForce * (float)(gravitationModulus * -1);

            return gravitationForce;
        }

        private double CalcularAtraccionGravitatoria(float masa2, float distancia)
        {
            return Constantes.Mu * masa2 / Math.Pow(distancia, 2);
        }
    }
}