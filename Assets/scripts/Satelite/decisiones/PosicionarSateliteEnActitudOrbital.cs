using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PosicionarSateliteEnActitudOrbital : Decision
{
    public PosicionarSateliteEnActitudOrbital(SateliteData data) : base(data) { 
        DefinirPaso(new PasoEnfoqueOrbital(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.Orbital));

        LogItem = new LogItem( 0, "Orientació orbital", "Orientar el satel·lit amb l'orbita");
    }

    public override bool DebeActuar()
    {
        return Data.Actitud != ActitudRotacion.Orbital;
    }
}