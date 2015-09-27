using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class CamaraMaestraUpdater : AbstractUpdater
{
    void Update()
    {
        var camara = gameManager.SateliteSeleccionado.Data.Camara;
        campo.text = string.Format("[{0}|{1}|{2}]", camara.x.ToString("N0"), camara.y.ToString("N0"), camara.z.ToString("N0"));
    }
}
