using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Conversor
{
    static public float ModelToRender(float modelValue)
    {
        return modelValue / Config.ScaleConversion;
    }

    static public float RenderToModel(float renderValue)
    {
        return renderValue * Config.ScaleConversion;
    }

    static public Vector3 ModelToRender(Vector3 modelValue)
    {
        return new Vector3(
            Conversor.ModelToRender( modelValue.x ),
            Conversor.ModelToRender( modelValue.y ),
            Conversor.ModelToRender( modelValue.z )
        );
    }

    static public Vector3 RenderToModel(Vector3 renderValue)
    {
        return new Vector3(
            Conversor.RenderToModel(renderValue.x),
            Conversor.RenderToModel(renderValue.y),
            Conversor.RenderToModel(renderValue.z)
        );
    }
}
