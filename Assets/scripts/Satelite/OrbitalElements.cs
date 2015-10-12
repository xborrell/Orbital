using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OrbitalElements
{
    public float MomentoAngular { get; protected set; }
    public float Excentricity { get; protected set; }
    public float Inclination { get; protected set; }
    public float ArgumentOfPeriapsis { get; protected set; }
    public float AngleOfAscendingNode { get; protected set; }
    public float TrueAnomaly { get; protected set; }

    public float SemiejeMayor { get; protected set; }
    public float Periapsis { get; protected set; }
    public float Apoapsis { get; protected set; }
    public TimeSpan Period { get; protected set; }

    public OrbitalElements(float momentoAngular, float excentricity, float inclination, float angleOfAscendingNode, float argumentOfPeriapsis, float trueAnomaly)
    {
        MomentoAngular = momentoAngular;
        Excentricity = excentricity;
        Inclination = inclination;
        ArgumentOfPeriapsis = argumentOfPeriapsis;
        AngleOfAscendingNode = angleOfAscendingNode;
        TrueAnomaly = trueAnomaly;

        CalcularValoresDerivados();
    }

    private void CalcularValoresDerivados()
    {
        Periapsis = ((float)Math.Pow(MomentoAngular, 2) / Config.Mu) * (1 / (1 + Excentricity));

        if (Excentricity < 1) // elipse
        {
            Apoapsis = ((float)Math.Pow(MomentoAngular, 2) / Config.Mu) * (1 / (1 - Excentricity));
            SemiejeMayor = (Periapsis + Apoapsis) / 2;

            var segundos = Config.Pi2 * (float)Math.Sqrt(Math.Pow(SemiejeMayor, 3) / Config.Mu);
            Period = TimeSpan.FromSeconds(segundos);
        }
        else
        {
            Apoapsis = float.PositiveInfinity;
            SemiejeMayor = float.PositiveInfinity;
            Period = TimeSpan.MaxValue;
        }
    }
}
