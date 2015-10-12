using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class AbstractUpdater : MonoBehaviour
{
    protected Text campo;
    protected GameManager gameManager;

    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        gameManager = (GameManager)model.GetComponent(typeof(GameManager));

        campo = gameObject.GetComponentInChildren<Text>();
        foreach (Text text in gameObject.GetComponentsInChildren<Text>())
        {
            if (text.name.ToLower().StartsWith("valor"))
            {
                campo = text;
            }
        }

        //nombre = gameObject.GetComponent<Text>();
        Debug.Assert(campo != null, "No se ha encontrado el campo valor");
    }
}
