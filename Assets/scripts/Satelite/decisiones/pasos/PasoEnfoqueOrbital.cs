using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoEnfoqueOrbital : Paso
{
    public PasoEnfoqueOrbital(SateliteData data) : base(data, "Solicitar el enfoque orbital") { }

    override public void Ejecutar(float time)
    {
        Data.ActitudSolicitada = ActitudRotacion.Orbital;

        PasoFinalizado = true;
    }
}