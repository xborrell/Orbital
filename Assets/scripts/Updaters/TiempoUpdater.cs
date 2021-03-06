﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TiempoUpdater : MonoBehaviour
{
    Text tiempo;
    GameManager gameManager;

    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        gameManager = (GameManager)model.GetComponent(typeof(GameManager));

        tiempo = gameObject.GetComponentInChildren<Text>();

        Debug.Assert(tiempo != null, "No se ha encontrado el campo del tiempo");
    }

    // Update is called once per frame
    void Update()
    {
        tiempo.text = string.Format("X{0}", Time.timeScale); 
    }
}
