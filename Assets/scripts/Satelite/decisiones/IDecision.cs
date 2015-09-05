using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface IDecision
{
    bool DecisionFinalizada{ get; }

    void Actua(float deltaTime);
}
