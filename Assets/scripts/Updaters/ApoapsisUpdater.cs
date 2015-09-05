using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ApoapsisUpdater : AbstractUpdater
{
    void Update()
    {
        var apoapsis = sateliteSelector.satelite.Apoapsis;
        campo.text = apoapsis == 0 ? "Desconocido" : apoapsis.ToString("#,0 m.");
    }
}
