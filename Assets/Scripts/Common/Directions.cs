using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Directions
{
    /// <summary>
    /// Вправо
    /// </summary>
    public static Vector3 Right { get { return Vector3.right; } }
    /// <summary>
    /// Влево
    /// </summary>
    public static Vector3 Left { get { return Vector3.left; } }
    /// <summary>
    /// Вверх
    /// </summary>
    public static Vector3 Up { get { return Vector3.up; } }
    /// <summary>
    /// Вниз
    /// </summary>
    public static Vector3 Down { get { return Vector3.down; } }
    /// <summary>
    /// Вверх-вправо
    /// </summary>
    public static Vector3 UpRight { get { return new Vector3(1, 1, 0); } }
    /// <summary>
    /// Вниз-вправо
    /// </summary>
    public static Vector3 DownRight { get { return new Vector3(1, 1, 0); } }
    /// <summary>
    /// Вверх-влево
    /// </summary>
    public static Vector3 UpLeft { get { return new Vector3(-1, 1, 0); } }
    /// <summary>
    /// Вниз-влево
    /// </summary>
    public static Vector3 DownLeft { get { return new Vector3(-1, -1, 0); } }
    /// <summary>
    /// На месте
    /// </summary>
    public static Vector3 Stay { get { return Vector3.zero; } }

    /// <summary>
    /// Все направления, включая несколько Stay
    /// </summary>
    public static Vector3[] AllDirections { get { return new Vector3[] { Right, Left, Up, Down, UpRight, DownRight, UpLeft, DownLeft, Stay, Stay, Stay, Stay }; } }

    public static Vector3 GetRandomDirection()
    {
        int currentMoveDirection = UnityRandomHelper.RandomInt(0, AllDirections.Length);
        Vector3 direction = AllDirections[currentMoveDirection];
        return direction;
    }
}
