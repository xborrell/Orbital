using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SateliteSelector
{
    public Satelite satelite
    {
        get
        {
            return index < satelites.Count ? satelites[index] : null;
        }
    }
    List<Satelite> satelites = new List<Satelite>();
    int index;

    GameObject camara;

    // Use this for initialization
    public void Start(GameManager manager)
    {
        satelites.Add(new Satelite( 
            new Vector3(-5002.099F, 24.236F, -4585.61F),
            new Vector3(2.723F, -6.502F, -3.001F),
            manager
        ));
        
        camara = GameObject.Find("Satellite Camera");
        index = 0;
    }

    public void FixedUpdate()
    {
        satelites.ForEach(sat => sat.FixedUpdate());
    }

    public void Previus()
    {
        index--;

        if (index < 0)
            index = satelites.Count - 1;

        //camara.transform.SetParent(satelite.transform, false);
    }

    public void Next()
    {
        index++;

        if (index >= satelites.Count)
            index = 0;

        //camara.transform.SetParent(satelite.transform, false);
    }
}
