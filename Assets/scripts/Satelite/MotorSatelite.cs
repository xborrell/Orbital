using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MotorSatelite
{
    SateliteData data;
    Action<float> variacionImpulso = null;

    public MotorSatelite(SateliteData data)
    {
        this.data = data;
    }

    public void FullPower()
    {
        CambioDeImpulso(Config.ImpulsoMaximo);
    }
    public void Stop()
    {
        CambioDeImpulso(0);
    }

    public void CambioDeImpulso(float cambioDeImpulsoPedido)
    {
        data.ImpulsoSolicitado = -1;

        if (cambioDeImpulsoPedido > Config.ImpulsoMaximo)
            cambioDeImpulsoPedido = Config.ImpulsoMaximo;

        else if (cambioDeImpulsoPedido < 0)
            cambioDeImpulsoPedido = 0;

        if (cambioDeImpulsoPedido > data.Impulso)
        {
            variacionImpulso = x => Acelerar(x, cambioDeImpulsoPedido);
        }

        if (cambioDeImpulsoPedido < data.Impulso)
        {
            variacionImpulso = x => Frenar(x, cambioDeImpulsoPedido);
        }
    }

    internal void CalcularImpulso(float deltaTime)
    {
        if (data.ImpulsoSolicitado >= 0)
        {
            CambioDeImpulso(data.ImpulsoSolicitado);
        }

        if (variacionImpulso != null)
        {
            variacionImpulso(deltaTime);
        }
    }

    void Acelerar(float deltaTime, float aceleracionSolicitada)
    {
        var variacion = Config.VariacionMaximaDelImpulso * deltaTime;

        data.Impulso += variacion;

        if (data.Impulso >= aceleracionSolicitada)
        {
            data.Impulso = aceleracionSolicitada;
            variacionImpulso = null;
        }
    }

    void Frenar(float deltaTime, float frenadoSolicitado)
    {
        var variacion = Config.VariacionMaximaDelImpulso * deltaTime;

        data.Impulso -= variacion;

        if (data.Impulso <= frenadoSolicitado)
        {
            data.Impulso = frenadoSolicitado;
            variacionImpulso = null;
        }
    }
}