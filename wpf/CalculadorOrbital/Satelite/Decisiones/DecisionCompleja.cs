using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculadorOrbital
{
    public delegate void Paso(float delta);

    abstract public class DecisionCompleja : Decision
    {
        private List<Paso> pasosAEjecutar = new List<Paso>();
        private List<Paso> DefinicionDePasos = new List<Paso>();
        private float marcaDeTiempo;

        public DecisionCompleja(SateliteData data) : base(data) { }

        override public void Inicializar()
        {
            base.Inicializar();

            pasosAEjecutar.AddRange(DefinicionDePasos);
        }

        protected void DefinirPaso(Paso paso)
        {
            DefinicionDePasos.Add(paso);
        }

        override public void Actua(float deltaTime)
        {
            if (pasosAEjecutar.Count == 0)
                DecisionFinalizada = true;

            else 
                pasosAEjecutar[0](deltaTime);
        }

        protected void CambiarPaso(Paso paso)
        {
            pasosAEjecutar[0] = paso;
        }

        protected void PasoCompletado()
        {
            pasosAEjecutar.RemoveAt(0);
        }

        protected void SolicitarEspera(float segundosAEsperar)
        {
            marcaDeTiempo = segundosAEsperar;
            pasosAEjecutar.Insert( 0, Esperar);
        }

        void Esperar(float deltaTime)
        {
            marcaDeTiempo -= deltaTime;

            if (marcaDeTiempo < 0)
            {
                PasoCompletado();
            }
        }
    }
}