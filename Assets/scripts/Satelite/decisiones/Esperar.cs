using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Esperar : Decision
{
    public Esperar(SateliteData data, float segundosAEsperar)
        : base(data)
    {
        SolicitarEspera(segundosAEsperar);

        LogItem = new LogItem( 1, "Esperant", string.Format("Esperant {0} segons", segundosAEsperar));
    }

    public override bool DebeActuar()
    {
        return true; ;
    }
}