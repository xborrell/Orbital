using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoEsperarPeriapsis : Paso
{
    public PasoEsperarPeriapsis(SateliteData data) : base(data) {
        LogItem = new LogItem( 1, "Esperant Periapsis");
    }

    override public void Ejecutar(float time)
    {
        if (Data.AlturaDeReferencia < Data.Altura)
        {
            PasoFinalizado = true;
        }
        else
        {
            Data.AlturaDeReferencia = Data.Altura;
            this.SegundosAEsperar = 30;
        }
    }
}