using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TiempoTotalUpdater : MonoBehaviour
{
    Text tiempo;

    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        tiempo = gameObject.GetComponentInChildren<Text>();

        Debug.Assert(tiempo != null, "No se ha encontrado el campo del tiempo total");
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan tiempoTranscurrido = TimeSpan.FromSeconds(Time.time);
        tiempo.text = string.Format("{0} {1}:{2}:{3}", tiempoTranscurrido.Days, tiempoTranscurrido.Hours, tiempoTranscurrido.Minutes, tiempoTranscurrido.Seconds);
    }
}
