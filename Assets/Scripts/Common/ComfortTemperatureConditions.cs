using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComfortTemperatureConditions
{
    public static float BlueComfortPerSec(float cellTemp, float comfortTempMin, float comfortTempMax)
    {
        if (cellTemp <= comfortTempMax && cellTemp >= comfortTempMin)
            if (comfortTempMax != 0) return cellTemp / comfortTempMax; else return cellTemp;
        else
            if (cellTemp != 0) return -(comfortTempMax / cellTemp); else return -comfortTempMax;
    }
}
