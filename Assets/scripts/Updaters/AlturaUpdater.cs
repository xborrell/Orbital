using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class AlturaUpdater : AbstractUpdater
{
    void Update()
    {
        var altura = gameManager.SateliteSeleccionado.Altura;
        campo.text = altura == 0 ? "Desconocido" : altura.ToString("#,0 m.");
    }
}
