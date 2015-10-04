using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PosicionarSateliteEnActitudOrbital : DecisionCompleja
{
    public override string Descripcion
    {
        get { return "Posicionar Satelite en Actitud Orbital"; }
    }
    public override string AccionEnCurso
    {
        get { return "Maniobrando"; }
    }
    public PosicionarSateliteEnActitudOrbital(SateliteData data) : base(data) { 
        DefinirPaso(new PasoEnfoqueOrbital(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.Orbital));
    }

    public override bool DebeActuar()
    {
        return Data.Actitud != ActitudRotacion.Orbital;
    }
}