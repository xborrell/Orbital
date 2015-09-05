using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InclinacionUpdater : AbstractUpdater
{
    void Update()
    {
        var inclinacion = sateliteSelector.satelite.Inclinacion;
        campo.text = inclinacion == 0 ? "Desconocido" : inclinacion.ToString("#,2 m.");
    }
}
