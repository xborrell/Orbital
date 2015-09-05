using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LightManager : MonoBehaviour
{
    public Transform luz;

    void Start()
    {
        luz.LookAt(new Vector3());
    }
}
