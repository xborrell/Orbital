using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoEnfoqueATierra : Paso
{
    public PasoEnfoqueATierra(SateliteData data) : base(data)
    {
        LogItem = new LogItem( 1, "Orientació terra", "Demanar l'orientació a terra.");
    }

    override public void Ejecutar()
    {
        Data.ActitudSolicitada = ActitudRotacion.EnfocadoATierra;

        PasoFinalizado = true;
    }
}