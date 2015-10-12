using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ActitudUpdater : AbstractUpdater
{
    void Update()
    {
        campo.text = gameManager.SateliteSeleccionado.Actitud;
    }
}
