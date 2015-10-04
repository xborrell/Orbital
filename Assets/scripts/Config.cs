using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Config
{
    public const float Mu = 398600F;                                     // Km3 / segundo2
    public const float EarthRadius = 6378.280F;                          // KM
    public const float EarthMass = 5.974E24F;                            // KG
    public const float SatellitMass = 750F;                              // KG
    public const float RadianToDegreeCoeficient = 180 / (float)Math.PI;
    public const float DegreeToRadianCoeficient = (float)Math.PI / 180;
    public const float Pi2 = (float)(2 * Math.PI);
    public const float ImpulsoMaximo = .01F;                             // Km/s2
    public const float VariacionMaximaDelImpulso = .014F;                // Km/s2

    static Random randomGenerator = new Random();
    public static float GetRandomValue(int minValue, int maxValue)
    {
        return randomGenerator.Next(minValue, maxValue);
    }
}
