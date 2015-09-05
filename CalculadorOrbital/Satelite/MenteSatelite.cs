using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CalculadorOrbital
{
    public class MenteSatelite
    {
        List<Decision> decisiones;
        Decision decisionEnCurso;

        public string Descripcion { get {
            return decisionEnCurso == null ? "Pensando" : decisionEnCurso.Descripcion;
        }
        }

        public MenteSatelite(SateliteData data)
        {
            decisiones = new List<Decision>() {
                new CalcularSentidoDeLaOrbita(data),
                new CalcularApoapsis(data),
                new CalcularPeriapsis(data),
                new CalcularInclinacion(data),
                new IniciarSatelite(data),
                new Esperar(data, 60F),
            };
        }

        internal void Update(float deltaTime)
        {
            if (decisionEnCurso == null)
                ObtieneLaSiguienteDecision();

            else if (decisionEnCurso.DecisionFinalizada)
                FinalizaDecision();

            else
                decisionEnCurso.Actua(deltaTime);
        }

        void ObtieneLaSiguienteDecision()
        {
            foreach (Decision decision in decisiones)
            {
                if (decision.DebeActuar())
                {
                    decision.Inicializar();
                    decisionEnCurso = decision;
                    return;
                }
            }
        }

        private void FinalizaDecision()
        {
            decisionEnCurso = null;
        }
    }
}