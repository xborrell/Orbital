using UnityEngine;
using System.Collections;
using System;

public class SateliteRender : MonoBehaviour
{
    protected GameManager gameManager;
    Conversor conversor = new Conversor();

    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        gameManager = (GameManager)model.GetComponent(typeof(GameManager));
    }

    void Update()
    {
        var satelite = gameManager.SateliteSeleccionado;

        transform.position = conversor.FromModelToRender(satelite.Data.Posicion);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, conversor.FromModelToRender(satelite.Data.Orientacion));
    }
}
