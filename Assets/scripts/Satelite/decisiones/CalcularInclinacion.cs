using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CalcularInclinacion : Decision
{
    public override bool DebeActuar()
    {
        return Data.Inclinacion < 0;
    }

    public CalcularInclinacion(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoGenerico(data, new LogItem(1, "Calc. inclinació", "Calcular l'inclinació"), Calcular));

        LogItem = new LogItem(0, "Calc. Inclinació", "Calculant Inclinació");
    }

    bool Calcular()
    {
        var conversor = new ConversorOrbital();
        OrbitalElements elementos = conversor.Convertir(Data.Posicion, Data.Velocidad);

        Data.Inclinacion = elementos.Inclination;
        return true;
    }
}
