using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NombreUpdater : AbstractUpdater
{
    void Update()
    {
        campo.text = sateliteSelector.satelite.Nombre;
    }
}
