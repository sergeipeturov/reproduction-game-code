using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Функции рандома юнити, чтобы удобней пользоваться
/// </summary>
public static class UnityRandomHelper
{
    /// <summary>
    /// Top exclusive
    /// </summary>
    /// <param name="bottom"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    public static int RandomInt(int bottom, int top)
    {
        return Mathf.FloorToInt(Random.Range(bottom, top));
    }
}
