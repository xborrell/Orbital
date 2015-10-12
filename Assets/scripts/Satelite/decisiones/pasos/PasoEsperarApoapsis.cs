using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoEsperarApoapsis : Paso
{
    public PasoEsperarApoapsis(SateliteData data) : base(data) 
    {
        LogItem = new LogItem( 1, "Esperant Apoapsis");
    }

    override public void Ejecutar(float time)
    {
        if (Data.AlturaDeReferencia > Data.Altura)
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