using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class AlturaRealUpdater : AbstractUpdater
{
    void Update()
    {
        var altura = gameManager.SateliteSeleccionado.Data.Posicion.magnitude - Config.EarthRadius;
        campo.text = string.Format("{0} Km.", altura.ToString("N3"));
    }
}
