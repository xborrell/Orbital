using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class InclinacionUpdater : AbstractUpdater
{
    void Update()
    {
        var inclinacion = gameManager.SateliteSeleccionado.Inclinacion;
        campo.text = inclinacion == 0 ? "Desconocido" : inclinacion.ToString("#,2 m.");
    }
}
