using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoEnfoqueOrbital : Paso
{
    public PasoEnfoqueOrbital(SateliteData data)
        : base(data)
    {
        LogItem = new LogItem( 1, "Orientació orbital", "Demanar l'orientació orbital.");
    }

    override public void Ejecutar(float time)
    {
        Data.ActitudSolicitada = ActitudRotacion.Orbital;

        PasoFinalizado = true;
    }
}