using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoGenerico : Paso
{
    public Func<float, bool> Accion { get; protected set; }

    public PasoGenerico(SateliteData data, LogItem logItem, Func<float, bool> paso)
        : base(data)
    {
        Accion = paso;
        LogItem = logItem;
    }

    override public void Ejecutar(float time)
    {
        if (Accion(time))
            PasoFinalizado = true;
    }
}