using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отвечает за принятие существом решений
/// </summary>
public class CreatureDecisionController : MonoBehaviour
{
    public delegate void MadeDecision(CreatureDecision decision);
    public event MadeDecision MadeDecisionEvent;

    private void Start()
    {
        self = this.gameObject;
        selfCreature = self.GetComponent<CreatureBase>();
    }

    private void Update()
    {
        if (selfCreature.StateController.CanMove())
        {
            if (decisionTimeCount > 0)
            {
                decisionTimeCount -= Time.deltaTime;
            }
            else
            {
                //выбрать задержку времени для принятия решения
                decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
                //принимаем решение и уведомляем всех, кому надо
                MadeDecisionEvent?.Invoke(MakeSomeDecision());
            }
        }
    }

    /// <summary>
    /// Принять решение о размножении
    /// </summary>
    public bool MakeReproductionDecision()
    {
        switch (self.tag)
        {
            case "creature_blue":
                //TODO: пока что рандомное решение о размножении, потом сделать алгоритм
                int randomDecision = UnityRandomHelper.RandomInt(0, 3);
                randomDecision = 0;
                if (randomDecision == 0)
                    return true;
                else
                    return false;

            default:
                return false;
        }
    }

    /// <summary>
    /// Принять решение о танце с другим
    /// </summary>
    public bool MakeDanceWithDecision()
    {
        //TODO: пока что рандомное решение о танце, потом сделать алгоритм
        int randomDecision = UnityRandomHelper.RandomInt(0, 3);
        randomDecision = 0;
        if (randomDecision == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Принять решение об игре с другим
    /// </summary>
    public bool MakePlayWithDecision()
    {
        //TODO: пока что рандомное решение об игре, потом сделать алгоритм
        int randomDecision = UnityRandomHelper.RandomInt(0, 3);
        randomDecision = 0;
        if (randomDecision == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Принять решение о пении с другим
    /// </summary>
    public bool MakeSingWithDecision()
    {
        //TODO: пока что рандомное решение о пении, потом сделать алгоритм
        int randomDecision = UnityRandomHelper.RandomInt(0, 3);
        randomDecision = 0;
        if (randomDecision == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Принять решение о несмертельной драке
    /// </summary>
    public bool MakeFightWithDecision()
    {
        //TODO: пока что рандомное решение о драке, потом сделать алгоритм
        int randomDecision = UnityRandomHelper.RandomInt(0, 3);
        randomDecision = 0;
        if (randomDecision == 0)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Принять решение о передвижении
    /// </summary>
    private CreatureDecision MakeMoveDecision()
    {
        Vector3 direction = Directions.GetRandomDirection();
        CreatureState state;
        if (direction == Directions.Stay)
            state = CreatureState.staying;
        else
            state = CreatureState.moving;

        return new CreatureDecision(state, direction, CreatureDecisionType.move);
    }

    /// <summary>
    /// Принять решение о поиске еды
    /// </summary>
    private CreatureDecision MakeFeedSearchDecision()
    {
        CreatureState state = CreatureState.feedSearching;
        //TODO: сделать умный поиск кормушки, чтобы вычислял, что она в текущей клетке и что она ближайшая
        //+ полная или пустая. если пустая, то поиск другой. если другой нет или все пустые, то просто ходьба
        //+ если на найденной уже много кто кормится, то искать другую, посвободней
        var feeder = GameObject.FindGameObjectWithTag("feeder");
        var feederScript = feeder.GetComponent<Feeder>();
        if (feederScript != null && feederScript.IsFull)
        {
            Vector3 direction = feeder.transform.position;
            return new CreatureDecision(state, direction, CreatureDecisionType.feedSearch);
        }
        else
            return MakeMoveDecision();
    }

    /// <summary>
    /// Принять решение о танце в одиночку
    /// </summary>
    private CreatureDecision MakeDancingAloneDecision()
    {
        CreatureState state = CreatureState.dancing;
        return new CreatureDecision(state, CreatureDecisionType.dancingAlone);
    }

    /// <summary>
    /// Принять решение об игре в одиночку
    /// </summary>
    private CreatureDecision MakePlayingAloneDecision()
    {
        CreatureState state = CreatureState.playing;
        return new CreatureDecision(state, CreatureDecisionType.playingAlone);
    }

    /// <summary>
    /// Принять решение о пении в одиночку
    /// </summary>
    private CreatureDecision MakeSingingAloneDecision()
    {
        CreatureState state = CreatureState.singing;
        return new CreatureDecision(state, CreatureDecisionType.singingAlone);
    }

    /// <summary>
    /// Принять решение о сне
    /// </summary>
    private CreatureDecision MakeSleepDecision()
    {
        CreatureState state = CreatureState.sleeping;
        return new CreatureDecision(state, CreatureDecisionType.sleep);
    }

    /// <summary>
    /// Принять какое-либо решение на основе текущего состояния и положения
    /// </summary>
    private CreatureDecision MakeSomeDecision()
    {
        List<int> availableDecisions = new List<int>();
        //availableDecisions.Add((int)CreatureDecisionType.move);
        if (selfCreature.StatsController.CurrentPower > 90)
        {
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            if (selfCreature.StatsController.CanPerformFunction())
            {
                availableDecisions.Add((int)CreatureDecisionType.dancingAlone); availableDecisions.Add((int)CreatureDecisionType.dancingAlone); availableDecisions.Add((int)CreatureDecisionType.dancingAlone);
                availableDecisions.Add((int)CreatureDecisionType.playingAlone); availableDecisions.Add((int)CreatureDecisionType.playingAlone); availableDecisions.Add((int)CreatureDecisionType.playingAlone);
                availableDecisions.Add((int)CreatureDecisionType.singingAlone); availableDecisions.Add((int)CreatureDecisionType.singingAlone); availableDecisions.Add((int)CreatureDecisionType.singingAlone);
            }
        }
        else if (selfCreature.StatsController.CurrentPower > 70 && selfCreature.StatsController.CurrentPower <= 90)
        {
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.feedSearch);
            if (selfCreature.StatsController.CanPerformFunction())
            {
                availableDecisions.Add((int)CreatureDecisionType.dancingAlone); availableDecisions.Add((int)CreatureDecisionType.dancingAlone);
                availableDecisions.Add((int)CreatureDecisionType.playingAlone); availableDecisions.Add((int)CreatureDecisionType.playingAlone);
                availableDecisions.Add((int)CreatureDecisionType.singingAlone); availableDecisions.Add((int)CreatureDecisionType.singingAlone);
            }
        }
        else if (selfCreature.StatsController.CurrentPower > 50 && selfCreature.StatsController.CurrentPower <= 70)
        {
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.feedSearch); availableDecisions.Add((int)CreatureDecisionType.feedSearch);
            availableDecisions.Add((int)CreatureDecisionType.feedSearch); availableDecisions.Add((int)CreatureDecisionType.feedSearch);
            if (selfCreature.StatsController.CanPerformFunction())
            {
                availableDecisions.Add((int)CreatureDecisionType.dancingAlone); availableDecisions.Add((int)CreatureDecisionType.dancingAlone);
                availableDecisions.Add((int)CreatureDecisionType.playingAlone); availableDecisions.Add((int)CreatureDecisionType.playingAlone);
                availableDecisions.Add((int)CreatureDecisionType.singingAlone); availableDecisions.Add((int)CreatureDecisionType.singingAlone);
            }
        }
        else if (selfCreature.StatsController.CurrentPower > 30 && selfCreature.StatsController.CurrentPower <= 50)
        {
            availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move); availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.move);  availableDecisions.Add((int)CreatureDecisionType.move);
            availableDecisions.Add((int)CreatureDecisionType.feedSearch); availableDecisions.Add((int)CreatureDecisionType.feedSearch);
            availableDecisions.Add((int)CreatureDecisionType.feedSearch); availableDecisions.Add((int)CreatureDecisionType.feedSearch); availableDecisions.Add((int)CreatureDecisionType.feedSearch);
            availableDecisions.Add((int)CreatureDecisionType.feedSearch); availableDecisions.Add((int)CreatureDecisionType.feedSearch);
            availableDecisions.Add((int)CreatureDecisionType.sleep); availableDecisions.Add((int)CreatureDecisionType.sleep);
            if (selfCreature.StatsController.CanPerformFunction())
            {
                availableDecisions.Add((int)CreatureDecisionType.dancingAlone);
                availableDecisions.Add((int)CreatureDecisionType.playingAlone);
                availableDecisions.Add((int)CreatureDecisionType.singingAlone);
            }
        }
        else if (selfCreature.StatsController.CurrentPower <= 30)
        {
            availableDecisions.Add((int)CreatureDecisionType.feedSearch);
            availableDecisions.Add((int)CreatureDecisionType.sleep);
        }
        int currentAvailDecisionIndex = UnityRandomHelper.RandomInt(0, availableDecisions.Count);
        int currentAvailDecision = availableDecisions[currentAvailDecisionIndex];
        switch (currentAvailDecision)
        {
            case (int)CreatureDecisionType.move:
                return MakeMoveDecision();
            case (int)CreatureDecisionType.feedSearch:
                return MakeFeedSearchDecision();
            case (int)CreatureDecisionType.dancingAlone:
                return MakeDancingAloneDecision();
            case (int)CreatureDecisionType.playingAlone:
                return MakePlayingAloneDecision();
            case (int)CreatureDecisionType.singingAlone:
                return MakeSingingAloneDecision();
            case (int)CreatureDecisionType.sleep:
                return MakeSleepDecision();
            default:
                return MakeMoveDecision();
        }
    }

    //минимальная и максимальная задержка времени для принятия решения
    private Vector2 decisionTime = new Vector2(1, 4);
    private float decisionTimeCount = 0;
    private GameObject self;
    private CreatureBase selfCreature;
}
