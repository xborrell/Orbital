using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoGenerico : Paso
{
    public Func<float, bool> Accion { get; protected set; }

    public PasoGenerico(SateliteData data, string titulo, Func<float, bool> paso) : base(data, titulo )
    {
        Accion = paso;
    }

    override public void Ejecutar(float time)
    {
        if (Accion(time))
            PasoFinalizado = true;
    }
}