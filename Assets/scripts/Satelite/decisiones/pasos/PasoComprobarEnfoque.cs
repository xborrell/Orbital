using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PasoComprobarEnfoque : Paso
{
    public ActitudRotacion ActitudDeseada { get; protected set; }

    public PasoComprobarEnfoque(SateliteData data, ActitudRotacion actitudDeseada)
        : base(data)
    {
        ActitudDeseada = actitudDeseada;
        LogItem = new LogItem( 1, "Esperar Orientació", "Esperar l'orientació demanada.");
    }

    override public void Ejecutar(float time)
    {
        PasoFinalizado = (Data.Actitud == ActitudDeseada);
    }
}