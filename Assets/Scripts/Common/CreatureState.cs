using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureState
{
    /// <summary>
    /// Ничего не делает
    /// </summary>
    staying = 0,
    /// <summary>
    /// Передвигается
    /// </summary>
    moving,
    /// <summary>
    /// Размножается
    /// </summary>
    reproducting,
    /// <summary>
    /// Принимает решение о размножении
    /// </summary>
    makingReprodusingDecision,
    /// <summary>
    /// Принимает некое парное решение
    /// </summary>
    makingSomeDecision,
    /// <summary>
    /// Отвергнутая попытка размножения
    /// </summary>
    //rejected,
    /// <summary>
    /// Питается
    /// </summary>
    feeding,
    /// <summary>
    /// Ищет еду
    /// </summary>
    feedSearching,
    /// <summary>
    /// Танцует
    /// </summary>
    dancing,
    /// <summary>
    /// Танцует с кем-то
    /// </summary>
    dancingWith,
    /// <summary>
    /// Играет
    /// </summary>
    playing,
    /// <summary>
    /// Играет с кем-то
    /// </summary>
    playingWith,
    /// <summary>
    /// Дерется
    /// </summary>
    fighting,
    /// <summary>
    /// Поет
    /// </summary>
    singing,
    /// <summary>
    /// Поет с кем-то
    /// </summary>
    singingWith,
    /// <summary>
    /// Спит
    /// </summary>
    sleeping,
    /// <summary>
    /// Терморегулирует
    /// </summary>
    termoregulating,
    /// <summary>
    /// Самолечится
    /// </summary>
    selfcureing,
    /// <summary>
    /// Спасается бегством
    /// </summary>
    fleeing,
    /// <summary>
    /// Охотится
    /// </summary>
    hunting,
    /// <summary>
    /// Умер
    /// </summary>
    dead1,
    /// <summary>
    /// Погиб
    /// </summary>
    dead2
}

public static class CreatureStateString
{
    public static string Get(CreatureState state)
    {
        switch (state)
        {
            case CreatureState.staying:
                return "Ничего не делает";
            case CreatureState.moving:
                return "Гуляет";
            case CreatureState.reproducting:
                return "Размножается";
            case CreatureState.makingReprodusingDecision:
                return "Ухаживает";
            case CreatureState.feeding:
                return "Питается";
            case CreatureState.feedSearching:
                return "Ищет еду";
            case CreatureState.dancing:
                return "Танцует";
            case CreatureState.dancingWith:
                return "Танцует в паре";
            case CreatureState.playing:
                return "Играет";
            case CreatureState.playingWith:
                return "Играет в паре";
            case CreatureState.fighting:
                return "Дерется";
            case CreatureState.singing:
                return "Поет";
            case CreatureState.singingWith:
                return "Поет в паре";
            case CreatureState.makingSomeDecision:
                return "Договариваются";
            case CreatureState.sleeping:
                return "Спит";
            case CreatureState.dead1:
                return "Умер";
            case CreatureState.dead2:
                return "Погиб";
            //TODO: реализовать остальное! + для парных действий добавить указание, с кем именно совершается действие
            default:
                return "";
        }
    }
}
