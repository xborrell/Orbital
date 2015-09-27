using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PeriapsisUpdater : AbstractUpdater
{
    void Update()
    {
        var periapsis = gameManager.SateliteSeleccionado.Periapsis;
        campo.text = periapsis == 0 ? "Desconocido" : periapsis.ToString("#,0 m.");
    }
}
