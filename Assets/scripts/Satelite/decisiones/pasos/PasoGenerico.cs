using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoGenerico : Paso
{
    public Func<bool> Accion { get; protected set; }

    public PasoGenerico(SateliteData data, LogItem logItem, Func<bool> paso)
        : base(data)
    {
        Accion = paso;
        LogItem = logItem;
    }

    override public void Ejecutar()
    {
        if (Accion())
            PasoFinalizado = true;
    }
}