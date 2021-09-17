using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Принятое созданием решение
/// </summary>
public class CreatureDecision
{
    /// <summary>
    /// Состояние, на которое должно измениться текущее состояние
    /// </summary>
    public CreatureState State { get; set; }

    /// <summary>
    /// Направление (если решение подразумевает движение)
    /// </summary>
    public Vector3 Direction { get; set; }

    /// <summary>
    /// Тип принятого решения
    /// </summary>
    public CreatureDecisionType Type { get; set; }

    public CreatureDecision(CreatureState state, CreatureDecisionType type)
    {
        State = state; Direction = Vector3.zero; Type = type;
    }

    public CreatureDecision(CreatureState state, Vector3 direction, CreatureDecisionType type)
    {
        State = state; Direction = direction; Type = type;
    }
}

public enum CreatureDecisionType : int
{
    /// <summary>
    /// Решение о передвижении
    /// </summary>
    move,
    /// <summary>
    /// Решение о поиске еды
    /// </summary>
    feedSearch,
    /// <summary>
    /// Решение о питании
    /// </summary>
    feed,
    /// <summary>
    /// Решение о танце в одиночку
    /// </summary>
    dancingAlone,
    /// <summary>
    /// Решение об игре в одиночку
    /// </summary>
    playingAlone,
    /// <summary>
    /// Решение о пении в одиночку
    /// </summary>
    singingAlone,
    /// <summary>
    /// Решение о сне
    /// </summary>
    sleep,
    /// <summary>
    /// Другое решение
    /// </summary>
    //other
}
