using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CalcularInclinacion : DecisionCompleja
{
    public override string Descripcion
    {
        get { return "Calc. Inclinacion"; }
    }

    Vector3 posicionAnterior;

    public override bool DebeActuar()
    {
        return Data.Inclinacion < 0;
    }

    public CalcularInclinacion(SateliteData data)
        : base(data)
    {
        DefinirPaso(new PasoEnfoqueATierra(data));
        DefinirPaso(new PasoComprobarEnfoque(data, ActitudRotacion.EnfocadoATierra));
        DefinirPaso(new PasoGenerico(data, "Calcular la inclinación", Calcular));
    }

    bool Calcular(float deltaTime)
    {
        var conversor = new ConversorOrbital();
        OrbitalElements elementos = conversor.Convertir(Data.Posicion, Data.Velocidad);

        Data.Inclinacion = elementos.Inclination;
        return true;
    }
}
