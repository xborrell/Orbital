using UnityEngine;
using System.Collections;
using System;

public class Conversor
{
    public Vector3 FromModelToRender(Vector3 modelCoordinates)
    {
        return new Vector3(modelCoordinates.x, modelCoordinates.z, modelCoordinates.y);
    }

    public Vector3 FromRenderToModel(Vector3 renderCoordinates)
    {
        return new Vector3(renderCoordinates.x, renderCoordinates.z, renderCoordinates.y);
    }
}
