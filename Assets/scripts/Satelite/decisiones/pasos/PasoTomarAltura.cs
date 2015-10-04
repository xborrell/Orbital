using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoTomarAltura : Paso
{
    public PasoTomarAltura(SateliteData data) : base(data, "Medir Altura de Referencia") { }

    override public void Ejecutar(float time)
    {
        Data.AlturaDeReferencia = Data.Altura;
        PasoFinalizado = true;
    }
}