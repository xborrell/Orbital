using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IniciarSatelite : IDecision
{
    public bool DecisionFinalizada{ get; protected set;}
    SateliteData _data;

    public IniciarSatelite(SateliteData data)
    {
        _data = data;
    }

    public void Actua(float deltaTime)
    {
        if ((_data.Actitud != ActitudRotacion.Orbital) && (_data.Actitud != ActitudRotacion.Maniobrando))
        {
            _data.ActitudSolicitada = ActitudRotacion.Orbital;
        }

        DecisionFinalizada = _data.Actitud == ActitudRotacion.Orbital;
    }
}
