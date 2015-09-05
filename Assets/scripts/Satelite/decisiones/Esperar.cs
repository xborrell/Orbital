using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Esperar : IDecision
{
    public bool DecisionFinalizada{ get; protected set;}
    SateliteData _data;
    const float tiempoDeEspera = 5.0F;
    float tiempoTranscurrido = 0.0F;

    public Esperar(SateliteData data)
    {
        _data = data;
    }

    public void Actua(float deltaTime)
    {
        tiempoTranscurrido += deltaTime;

        DecisionFinalizada = tiempoTranscurrido >= tiempoDeEspera;
    }
}
