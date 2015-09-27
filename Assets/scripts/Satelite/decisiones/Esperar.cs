using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Esperar : Decision
{
    public override string Descripcion
    {
        get { return "Esperando"; }
    }

    float tiempoDeEspera = 5.0F;
    float tiempoTranscurrido = 0.0F;

    public Esperar(SateliteData data, float tiempoAEsperar)
        : base(data)
    {
        tiempoDeEspera = tiempoAEsperar;
    }

    public Esperar(SateliteData data) : base(data) { }

    override public void Inicializar()
    {
        base.Inicializar();
        tiempoTranscurrido = 0;
    }


    override public void Actua(float deltaTime)
    {
        tiempoTranscurrido += deltaTime;

        DecisionFinalizada = tiempoTranscurrido >= tiempoDeEspera;
    }

    public override bool DebeActuar()
    {
        return true; ;
    }
}