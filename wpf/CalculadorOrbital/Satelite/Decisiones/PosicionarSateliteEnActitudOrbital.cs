using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculadorOrbital
{
    public class IniciarSatelite : Decision
    {
        public override string Descripcion { get { return "Situando en actitud orbital"; } }

        public IniciarSatelite(SateliteData data) : base(data) { }

        override public void Actua(float deltaTime)
        {
            if ((Data.Actitud != ActitudRotacion.Orbital) && (Data.Actitud != ActitudRotacion.Maniobrando))
            {
                Data.ActitudSolicitada = ActitudRotacion.Orbital;
            }

            DecisionFinalizada = Data.Actitud == ActitudRotacion.Orbital;
        }

        public override bool DebeActuar()
        {
            return Data.Actitud != ActitudRotacion.Orbital;
        }
    }
}