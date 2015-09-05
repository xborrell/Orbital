using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CalculadorOrbital
{
    public class CalcularInclinacion : DecisionCompleja
    {
        public override string Descripcion
        {
            get { return "Calculando Inclinacion"; }
        }

        Vector3 posicionAnterior;

        public override bool DebeActuar()
        {
            return Data.Inclinacion < 0;
        }

        public CalcularInclinacion(SateliteData data)
            : base(data)
        {
            DefinirPaso(SolicitarEnfoqueATierra);
            DefinirPaso(ComprobarEnfoqueCorrecto);
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

        void Calcular(float deltaTime)
        {
            var conversor = new ConversorOrbital();
            OrbitalElements elementos = conversor.Convertir(Data.Posicion, Data.Velocidad);

            Data.Inclinacion = elementos.Inclination;
            PasoCompletado();
        }
    }
}