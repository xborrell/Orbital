using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoEsperar : Paso
{
    public float TiempoAEsperar { get; protected set; }
    public float TiempoRestante { get; protected set; }

    public PasoEsperar(SateliteData data, float segundosAEsperar)
        : base(data)
    {
        TiempoRestante = TiempoAEsperar = segundosAEsperar;
    }

    public PasoEsperar(SateliteData data, float segundosAEsperar, LogItem logItem) : base(data) {
        TiempoRestante = TiempoAEsperar = segundosAEsperar;
        LogItem = logItem;
    }

    override public void Ejecutar(float time)
    {
        TiempoRestante -= time;

        PasoFinalizado = (TiempoRestante < 0);
    }
}