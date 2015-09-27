using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private List<float> tiempos = new List<float> { 0, 1, 2, 5, 10, 25, 50, 100 };
    private int tiempoSeleccionado = 1;
    private SateliteSelector selector;

    public Satelite SateliteSeleccionado { get { return selector.satelite; } }

    void Awake()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("ca-ES");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ca-ES");

        Time.fixedDeltaTime = 0.1F;
        Time.timeScale = tiempos[tiempoSeleccionado];

        selector = new SateliteSelector();
    }

    void Start()
    {
        selector.Start();
    }

    void FixedUpdate()
    {
        selector.FixedUpdate();
    }

    public void Frenar()
    {
        if (tiempoSeleccionado > 0)
        {
            tiempoSeleccionado--;
            Time.timeScale = tiempos[tiempoSeleccionado];
        }
    }

    public void Acelerar()
    {
        if (tiempoSeleccionado < tiempos.Count - 1)
        {
            tiempoSeleccionado++;
            Time.timeScale = tiempos[tiempoSeleccionado];
        }
    }
}
