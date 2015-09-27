using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ApoapsisUpdater : AbstractUpdater
{
    void Update()
    {
        var data = gameManager.SateliteSeleccionado.Apoapsis;
        campo.text = data < 0 ? "Desconocido" : string.Format("{0} Km.", data.ToString("N3"));
    }
}
