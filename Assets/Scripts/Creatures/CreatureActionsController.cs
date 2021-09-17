using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление различными действиями создания (кормежка, пение и т.д.), для которых нет отдельного контроллера
/// </summary>
public class CreatureActionsController : MonoBehaviour
{
    /// <summary>
    /// С чем взаимодействует создание
    /// </summary>
    public GameObject Actor { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        selfCreature = self.GetComponent<CreatureBase>();
    }

    // Update is called once per frame
    void Update()
    {
        //питание
        if (selfCreature.StateController.CurrentState == CreatureState.feeding)
        {
            Feeding();
        }

        //танцы
        if (selfCreature.StateController.CurrentState == CreatureState.dancing || selfCreature.StateController.CurrentState == CreatureState.dancingWith)
        {
            Dancing();
        }

        //игры
        if (selfCreature.StateController.CurrentState == CreatureState.playing || selfCreature.StateController.CurrentState == CreatureState.playingWith)
        {
            Playing();
        }

        //пение
        if (selfCreature.StateController.CurrentState == CreatureState.singing || selfCreature.StateController.CurrentState == CreatureState.singingWith)
        {
            Singing();
        }

        //драки
        if (selfCreature.StateController.CurrentState == CreatureState.fighting)
        {
            Fighting();
        }

        //сон
        if (selfCreature.StateController.CurrentState == CreatureState.sleeping)
        {
            Sleeping();
        }
    }

    public void StartFeeding(GameObject feeder)
    {
        selfCreature.StateController.CurrentState = CreatureState.feeding;
        Actor = feeder;
    }

    public void StartDancing(GameObject partner = null)
    {
        if (partner == null)
        {
            selfCreature.StateController.CurrentState = CreatureState.dancing;
            maxActionTime = Random.Range(1.0f, 8.0f);
            currentActionTime = 0.0f;
        }
        else
        {
            selfCreature.StateController.CurrentState = CreatureState.dancingWith;
            maxActionTime = 4.0f;
            currentActionTime = 0.0f;
        }
        Actor = partner;
    }

    public void StartPlaying(GameObject partner = null)
    {
        if (partner == null)
        {
            selfCreature.StateController.CurrentState = CreatureState.playing;
            maxActionTime = Random.Range(1.0f, 8.0f);
            currentActionTime = 0.0f;
        }
        else
        {
            selfCreature.StateController.CurrentState = CreatureState.playingWith;
            maxActionTime = 4.0f;
            currentActionTime = 0.0f;
        }
        Actor = partner;
    }

    public void StartSinging(GameObject partner = null)
    {
        if (partner == null)
        {
            selfCreature.StateController.CurrentState = CreatureState.singing;
            maxActionTime = Random.Range(1.0f, 8.0f);
            currentActionTime = 0.0f;
        }
        else
        {
            selfCreature.StateController.CurrentState = CreatureState.singingWith;
            maxActionTime = 4.0f;
            currentActionTime = 0.0f;
        }
        Actor = partner;
    }

    public void StartFighting(GameObject partner = null)
    {
        if (partner != null)
        {
            selfCreature.StateController.CurrentState = CreatureState.fighting;
            //maxActionTime = Random.Range(1.0f, 8.0f);
            maxActionTime = 4.0f;
            currentActionTime = 0.0f;
        }
        Actor = partner;
    }

    public void StartSleeping()
    {
        selfCreature.StateController.CurrentState = CreatureState.sleeping;
        maxActionTime = Random.Range(5.0f, 9.0f);
        currentActionTime = 0.0f;
    }

    public void StopDancing()
    {
        maxActionTime = 0.0f; currentActionTime = 0.0f;
        selfCreature.StateController.CurrentState = CreatureState.staying;
    }

    public void StopPlaying()
    {
        maxActionTime = 0.0f; currentActionTime = 0.0f;
        selfCreature.StateController.CurrentState = CreatureState.staying;
    }

    public void StopSinging()
    {
        maxActionTime = 0.0f; currentActionTime = 0.0f;
        selfCreature.StateController.CurrentState = CreatureState.staying;
    }

    public void StopFighting()
    {
        maxActionTime = 0.0f; currentActionTime = 0.0f;
        selfCreature.StateController.CurrentState = CreatureState.staying;
    }

    public void StopSleeping()
    {
        maxActionTime = 0.0f; currentActionTime = 0.0f;
        selfCreature.StateController.CurrentState = CreatureState.staying;
    }

    private void Feeding()
    {
        var feeder = Actor.GetComponent<Feeder>();
        if (feeder != null)
        {
            float foodCountToConsume = 1.0f;
            float powerFromOneFood = selfCreature.StatsController.GetPowerFromOneFood();
            float healthFromOneFood = selfCreature.StatsController.GetHealthFromOneFood();
            if (feeder.IsFull)
            {
                selfCreature.StatsController.CurrentPower += powerFromOneFood * foodCountToConsume * Time.deltaTime;
                feeder.CurrentFeedCount -= foodCountToConsume * Time.deltaTime;
                if (selfCreature.StatsController.CurrentPower >= selfCreature.StatsController.GetMaxPower())
                {
                    selfCreature.StatsController.CurrentPower = selfCreature.StatsController.GetMaxPower();
                    selfCreature.StateController.CurrentState = CreatureState.staying;
                }

                if (selfCreature.StatsController.CurrentPower >= selfCreature.StatsController.GetMaxHealth())
                {
                    selfCreature.StatsController.CurrentPower = selfCreature.StatsController.GetMaxHealth();
                }
                else
                {
                    selfCreature.StatsController.CurrentHealth += healthFromOneFood * foodCountToConsume * Time.deltaTime;
                }

                if (feeder.CurrentFeedCount <= 0.0f)
                {
                    feeder.CurrentFeedCount = 0.0f;
                    selfCreature.StateController.CurrentState = CreatureState.staying;
                }
            }
            else
            {
                selfCreature.StateController.CurrentState = CreatureState.staying;
            }
        }
    }

    private void Dancing()
    {
        var partner = Actor?.GetComponent<CreatureBase>();
        currentActionTime += Time.deltaTime;
        if (currentActionTime >= maxActionTime
            || selfCreature.StatsController.CurrentPower <= selfCreature.StatsController.GetMaxPower() / 3.0f
            || selfCreature.StatsController.CurrentMood < selfCreature.StatsController.GetMaxMood() / 3.0f)
        {
            StopDancing();
            if (partner != null)
            {
                partner.ActionsController.StopDancing();
            }
        }
    }

    private void Playing()
    {
        var partner = Actor?.GetComponent<CreatureBase>();
        currentActionTime += Time.deltaTime;
        if (currentActionTime >= maxActionTime
            || selfCreature.StatsController.CurrentPower <= selfCreature.StatsController.GetMaxPower() / 3.0f
            || selfCreature.StatsController.CurrentMood < selfCreature.StatsController.GetMaxMood() / 3.0f)
        {
            StopPlaying();
            if (partner != null)
            {
                partner.ActionsController.StopPlaying();
            }
        }
    }

    private void Singing()
    {
        var partner = Actor?.GetComponent<CreatureBase>();
        currentActionTime += Time.deltaTime;
        if (currentActionTime >= maxActionTime
            || selfCreature.StatsController.CurrentPower <= selfCreature.StatsController.GetMaxPower() / 3.0f
            || selfCreature.StatsController.CurrentMood < selfCreature.StatsController.GetMaxMood() / 3.0f)
        {
            StopSinging();
            if (partner != null)
            {
                partner.ActionsController.StopSinging();
            }
        }
    }

    private void Fighting()
    {
        var partner = Actor?.GetComponent<CreatureBase>();
        if (partner != null)
        {
            currentActionTime += Time.deltaTime;
            var hittingRandomList = new List<bool>() { true, false };
            if (selfCreature.StatsController.CurrentPower > partner.StatsController.CurrentPower) hittingRandomList.Add(false); else hittingRandomList.Add(true);
            int selfHittedIndex = UnityRandomHelper.RandomInt(0, hittingRandomList.Count);
            bool selfHitted = hittingRandomList[selfHittedIndex];
            if (selfHitted)
                selfCreature.StatsController.CurrentHealth -= 1.0f * Time.deltaTime;
            else
                partner.StatsController.CurrentHealth -= 1.0f * Time.deltaTime;
            //TODO: добавить вызов анимации удара
            if (currentActionTime >= maxActionTime
                || selfCreature.StatsController.CurrentPower <= selfCreature.StatsController.GetMaxPower() / 3.0f
                || selfCreature.StatsController.CurrentHealth <= selfCreature.StatsController.GetMaxHealth() / 3.0f)
            {
                StopFighting();
                partner.ActionsController.StopFighting();
            }
        }
        else
        {
            StopFighting();
        }
    }

    private void Sleeping()
    {
        currentActionTime += Time.deltaTime;
        if (currentActionTime >= maxActionTime ||
            (selfCreature.StatsController.CurrentPower >= selfCreature.StatsController.GetMaxPower() * 0.59f && selfCreature.StatsController.CurrentHealth >= selfCreature.StatsController.GetMaxHealth() * 0.59f))
        {
            StopSleeping();
        }
    }

    private GameObject self;
    private CreatureBase selfCreature;
    private float maxActionTime = 0.0f;
    private float currentActionTime = 0.0f;
}
