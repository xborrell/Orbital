using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoEnfoqueATierra : Paso
{
    public PasoEnfoqueATierra(SateliteData data) : base(data, "Solicitar el enfoque a tierra")    {    }

    override public void Ejecutar(float time)
    {
        Data.ActitudSolicitada = ActitudRotacion.EnfocadoATierra;

        PasoFinalizado = true;
    }
}