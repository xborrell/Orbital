using UnityEngine;
using System.Collections;

public class CamaraATierra : MonoBehaviour
{
    GameObject target;
    Camera camera;
    float distanciaAPintar = Config.EarthRadius + 50;

    void Start()
    {
        target = GameObject.Find("Satellite Camera");
        Debug.Assert(target != null, "No se ha encontrado la cámara de satellite.");

        camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;

        camera.farClipPlane = transform.position.magnitude + distanciaAPintar;
    }
}
