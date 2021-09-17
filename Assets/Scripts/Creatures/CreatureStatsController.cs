using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управление показателями создания
/// </summary>
public class CreatureStatsController : MonoBehaviour
{
    /// <summary>
    /// Сила. Показатель усталости. Тратится на любые действия и со временем. Восполняется сном, едой.
    /// </summary>
    public float CurrentPower { get; set; }

    /// <summary>
    /// Настроение. Тратится, если не выполняется условие настроения создания. Восполняется, если условие выполняется. Нужно, чтобы создания выполняли свои функции.
    /// </summary>
    public float CurrentMood { get; set; }

    /// <summary>
    /// Комфорт. Зависит от температуры. Влияет на то, как быстро тратится сила.
    /// </summary>
    public float CurrentComfort { get; set; }

    /// <summary>
    /// Здоровье. На него влияют текущие показания силы, настроения и комфорта, болезни, драки. По истечению умирают. Восполняется сном, едой.
    /// </summary>
    public float CurrentHealth { get; set; }

    /// <summary>
    /// Возраст
    /// </summary>
    public float CurrentAge { get; set; }






    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        selfCreature = self.GetComponent<CreatureBase>();
        CurrentPower = MaxPower;
        CurrentMood = MaxMood / 2;
        CurrentComfort = MaxComfort;
        CurrentHealth = MaxHealth;
        CurrentAge = 0;
        MaxAge = Random.Range(MaxAgeRangeBottom, MaxAgeRangeTop);
    }

    // Update is called once per frame
    void Update()
    {
        if (selfCreature.StateController.CurrentState != CreatureState.dead1 && selfCreature.StateController.CurrentState != CreatureState.dead2)
        {
            //изменение силы создания
            CurrentPower += PowerPerSecond * Time.deltaTime;
            if (CurrentPower < 0)
            {
                CurrentPower = 0;
                //TODO: дебафы от окончания силы
            }
            else if (CurrentPower > MaxPower)
            {
                CurrentPower = MaxPower;
                //selfCreature.StateController.CurrentState = CreatureState.staying;
            }

            //изменение настроения создания
            CurrentMood += MoodPerSecond * Time.deltaTime;
            if (CurrentMood < 0)
            {
                CurrentMood = 0;
                //TODO: дебафы от окончания настроения
            }
            else if (CurrentMood > MaxMood)
            {
                CurrentMood = MaxMood;
            }

            //изменение комфорта создания
            CurrentComfort += ComfortPerSecond.Invoke(CellManagerStatic.GetCellByUid(selfCreature.CurrentCellUid).CurrentTemperature, ComfortTemperatureMin, ComfortTemperatureMax) * Time.deltaTime;
            if (CurrentComfort < 0)
            {
                CurrentComfort = 0;
                //TODO: дебафы от окончания комфорта
            }
            else if (CurrentComfort > MaxComfort)
            {
                CurrentComfort = MaxComfort;
            }

            //изменение здоровья создания
            CurrentHealth += HealthPerSecond * Time.deltaTime;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
                selfCreature.StateController.Death();
            }
            else if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }

            //изменение возраста создания
            CurrentAge += AgePerSecond * Time.deltaTime;
            if (CurrentAge > MaxAge)
            {
                CurrentAge = MaxAge;
                selfCreature.StateController.Death();
            }
        }
    }

    public void InitializeParams(CreatureColor color)
    {
        switch (color)
        {
            case CreatureColor.blue:
                ComfortTemperatureMax = 70.0f;
                ComfortTemperatureMin = 40.0f;
                ComfortPerSecond = ComfortTemperatureConditions.BlueComfortPerSec;
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// Сила от одной потребленной пищи. TODO: убрать отсюда, должна зависеть от типа потребляемой пищи
    /// </summary>
    /// <returns></returns>
    public float GetPowerFromOneFood()
    {
        return PowerFromOneFood;
    }

    /// <summary>
    /// Здоровье от одной потребленной пищи. TODO: убрать отсюда, должна зависеть от типа потребляемой пищи
    /// </summary>
    /// <returns></returns>
    public float GetHealthFromOneFood()
    {
        return HealthFromOneFood;
    }

    public float GetMaxPower()
    {
        return MaxPower;
    }

    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public float GetMaxMood()
    {
        return MaxMood;
    }

    public float GetMaxComfort()
    {
        return MaxComfort;
    }

    public float GetMaxAge()
    {
        return MaxAge;
    }

    /// <summary>
    /// Позволяет ли текущее настроение исполнть свою функцию
    /// </summary>
    /// <returns></returns>
    public bool CanPerformFunction()
    {
        return CurrentMood >= (MaxMood / 2.0f);
    }

    /// <summary>
    /// Позволяют ли показатели размножаться
    /// </summary>
    public bool CanReproduce()
    {
        return CanPerformFunction() && CurrentPower > 30 && CurrentHealth > 30 && CurrentComfort > 30;
    }
    /// <summary>
    /// Позволяют ли показатели танцевать
    /// </summary>
    public bool CanDance()
    {
        return CanPerformFunction() && CurrentPower > 30;
    }
    /// <summary>
    /// Позволяют ли показатели играть
    /// </summary>
    public bool CanPlay()
    {
        return CanPerformFunction() && CurrentPower > 30;
    }
    /// <summary>
    /// Позволяют ли показатели петь
    /// </summary>
    public bool CanSing()
    {
        return CanPerformFunction() && CurrentPower > 30;
    }
    /// <summary>
    /// Позволяют ли показатели драться
    /// </summary>
    public bool CanFight()
    {
        return CanPerformFunction() && CurrentPower > 30 && CurrentHealth > 10;
    }

    /// <summary>
    /// Максимальное кол-во силы
    /// </summary>
    private float MaxPower { get; set; } = 100;
    /// <summary>
    /// Изменение силы в секунду
    /// </summary>
    private float PowerPerSecond
    {
        get
        {
            if (selfCreature == null) return 0;
            float comfortMultiplier = CurrentComfort >= MaxComfort / 3.0f ? 1.0f : 2.0f; //если текущий комфорт менее 1/3 от максимального, то силы тратится в 2 раза больше
            switch (selfCreature.StateController.CurrentState)
            {
                case CreatureState.staying:
                case CreatureState.makingReprodusingDecision:
                    return 0;
                case CreatureState.moving:
                case CreatureState.feedSearching:
                    return -1 * comfortMultiplier;
                case CreatureState.reproducting:
                case CreatureState.dancing:
                case CreatureState.dancingWith:
                case CreatureState.playing:
                case CreatureState.playingWith:
                case CreatureState.singing:
                case CreatureState.singingWith:
                case CreatureState.fighting:
                    return -2 * comfortMultiplier;
                case CreatureState.feeding:
                    return 0;
                case CreatureState.sleeping:
                    return CurrentPower < MaxPower * 0.59f ? CurrentComfort >= MaxComfort ? 2.5f : 1.5f : 0.0f; //если существу некомфортно спать, то силы восстанавливается меньше
                default:
                    return 0;
            }
        }
    }
    /// <summary>
    /// Сколько силы получает от потребления единицы еды (TODO: перенести в класс еды, когда тот будет реализован)
    /// </summary>
    private float PowerFromOneFood { get; set; } = 10.0f;

    /// <summary>
    /// Сколько здоровья получает от потребления единицы еды (TODO: перенести в класс еды, когда тот будет реализован)
    /// </summary>
    private float HealthFromOneFood { get; set; } = 10.0f;

    /// <summary>
    /// Максимальное кол-во настроения
    /// </summary>
    private float MaxMood { get; set; } = 100.0f;
    /// <summary>
    /// Изменение настроения в секунду
    /// </summary>
    private float MoodPerSecond
    {
        get
        {
            if (selfCreature == null) return 0;

            if (CurrentPower <= 0)
            {
                return -1;
            }
            else
            {
                if (selfCreature.MoodCondition())
                    return 1;
                else
                    return -1;
            }
        }
    }

    /// <summary>
    /// Комфортная температура. Нижняя граница
    /// </summary>
    private float ComfortTemperatureMin { get; set; }
    /// <summary>
    /// Комфортная температура. Верхняя граница
    /// </summary>
    private float ComfortTemperatureMax { get; set; }
    /// <summary>
    /// Максимальный комфорт
    /// </summary>
    private float MaxComfort { get; set; } = 100.0f;
    /// <summary>
    /// Изменение комфорта в секунду (делегат)
    /// </summary>
    private delegate float ComfortPerSecondDelegate(float cellTemp, float comfortTempMin, float comfortTempMax);
    /// <summary>
    /// Изменение комфорта в секунду
    /// </summary>
    private ComfortPerSecondDelegate ComfortPerSecond { get; set; }

    /// <summary>
    /// Максимальное кол-во здоровья
    /// </summary>
    private float MaxHealth { get; set; } = 100.0f;
    /// <summary>
    /// Изменение здоровья в секунду
    /// </summary>
    private float HealthPerSecond
    {
        get
        {
            if (selfCreature == null) return 0;

            if (selfCreature.StateController.CurrentState == CreatureState.sleeping)
                return CurrentHealth < MaxHealth * 0.59f ? CurrentComfort >= MaxComfort ? 1.5f : 0.0f : 0.0f; //если существу некомфортно спать, то здоровья восстанавливается меньше

            //если силы на нуле, тратится здоровье
            if (CurrentPower <= 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// Максимальный возраст (в секундах). Определяется в момент рождения случайным образом из диапазона, ограниченного MaxAgeRangeBottom и MaxAgeRangeTop
    /// </summary>
    private float MaxAge { get; set; }
    /// <summary>
    /// Нижняя граница максимального возраста
    /// </summary>
    private float MaxAgeRangeBottom { get; set; } = 300.0f; //5 минут
    /// <summary>
    /// Верхняя граница максимального возраста
    /// </summary>
    private float MaxAgeRangeTop { get; set; } = 600.0f; //10 минут
    /// <summary>
    /// Изменение возраста в секунду (1 секунда)
    /// </summary>
    private float AgePerSecond { get; set; } = 1.0f;


    private GameObject self;
    private CreatureBase selfCreature;
}
