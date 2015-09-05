using UnityEngine;
using System.Collections;
using System;

namespace CalculadorOrbital
{
    public class CalculadorRotacion
    {
        protected SateliteData _data;

        public CalculadorRotacion(SateliteData data)
        {
            _data = data;
        }

        public void CalcularNuevaRotacion(float deltaTime)
        {
            if (_data.ActitudSolicitada != ActitudRotacion.Ninguna)
            {
                _data.Actitud = _data.ActitudSolicitada;
                _data.ActitudSolicitada = ActitudRotacion.Ninguna;
            }
        }
    }
}