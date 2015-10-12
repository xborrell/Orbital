using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PosicioRealUpdater : AbstractUpdater
{
    void Update()
    {
        var posicion = gameManager.SateliteSeleccionado.Data.Posicion;
        campo.text = string.Format("[{0}|{1}|{2}] Km", posicion.x.ToString("N0"), posicion.y.ToString("N0"), posicion.z.ToString("N0"));
    }
}
