using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InclinacionUpdater : AbstractUpdater
{
    void Update()
    {
        var data = gameManager.SateliteSeleccionado.Inclinacion;
        campo.text = data < 0 ? "Desconocida" : string.Format("{0}º", data.ToString("N3"));
    }
}
