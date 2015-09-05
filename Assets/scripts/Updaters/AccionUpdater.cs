using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class AccionUpdater : AbstractUpdater
{
    void Update()
    {
        campo.text = sateliteSelector.satelite.Accion;
    }
}
