using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoTomarAltura : Paso
{
    public PasoTomarAltura(SateliteData data)
        : base(data)
    {
        LogItem = new LogItem( 1, "Alçada referencia", "Mesurar l'Alçada de Referencia");
    }

    override public void Ejecutar()
    {
        Data.AlturaDeReferencia = Data.Altura;
        PasoFinalizado = true;
    }
}