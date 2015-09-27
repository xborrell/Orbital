using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class VelocitatRealUpdater : AbstractUpdater
{
    void Update()
    {
        var velocitat = gameManager.SateliteSeleccionado.Data.Velocidad;
        campo.text = string.Format("[{0}|{1}|{2}] Km/s2", velocitat.x.ToString("N1"), velocitat.y.ToString("N1"), velocitat.z.ToString("N1"));
    }
}
