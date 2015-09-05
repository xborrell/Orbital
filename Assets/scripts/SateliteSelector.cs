using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SateliteSelector : MonoBehaviour
{
    public Satelite satelite
    {
        get
        {
            return index < satelites.Count ? satelites[index] : null;
        }
    }
    List<Satelite> satelites = new List<Satelite>();
    int index = 0;

    GameObject camara;

    // Use this for initialization
    void Start()
    {
        GameObject contenedorSatelites = GameObject.Find("Satelites");
        Debug.Assert(contenedorSatelites != null, "No se ha encontrado el selector de satelites");

        foreach (Transform child in contenedorSatelites.transform)
        {
            satelites.Add(child.GetComponent<Satelite>());
        }

        camara = GameObject.Find("Main Camera");
        Debug.Assert(contenedorSatelites != null, "No se ha encontrado la camara en SateliteSelector");
    }

    public void Previus()
    {
        index--;

        if (index < 0)
            index = satelites.Count - 1;

        camara.transform.SetParent(satelite.transform, false);
    }

    public void Next()
    {
        index++;

        if (index >= satelites.Count)
            index = 0;

        camara.transform.SetParent(satelite.transform, false);
    }
}
