using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract public class Decision
{
    public bool DecisionFinalizada { get; protected set; }
    protected SateliteData Data { get; set; }

    abstract public string Descripcion { get; }
    abstract public bool DebeActuar();
    abstract public void Actua(float deltaTime);

    public Decision(SateliteData data)
    {
        Data = data;
    }

    virtual public void Inicializar()
    {
        DecisionFinalizada = false;
    }
}
