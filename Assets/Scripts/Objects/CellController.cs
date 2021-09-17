using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Uid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Текущая температура в клетке
    /// </summary>
    public float CurrentTemperature { get; set; } = 75.0f;//MaxTemp;

    /// <summary>
    /// Нормальная температура. К ней стремится текущая
    /// </summary>
    public float NormalTemperature { get; set; } = 75.0f;

    public int AllCreatureCount { get { return BlueCreatureCount + RedCreatureCount + GreenCreatureCount + OrangeCreatureCount + TurquoiseCreatureCount + PurpleCreatureCount; } }

    public List<Guid> BlueCreatureUids { get; set; } = new List<Guid>();
    public int BlueCreatureCount { get { return BlueCreatureUids.Count; } }

    public List<Guid> RedCreatureUids { get; set; } = new List<Guid>();
    public int RedCreatureCount { get { return RedCreatureUids.Count; } }

    public List<Guid> GreenCreatureUids { get; set; } = new List<Guid>();
    public int GreenCreatureCount { get { return GreenCreatureUids.Count; } }

    public List<Guid> OrangeCreatureUids { get; set; } = new List<Guid>();
    public int OrangeCreatureCount { get { return OrangeCreatureUids.Count; } }

    public List<Guid> TurquoiseCreatureUids { get; set; } = new List<Guid>();
    public int TurquoiseCreatureCount { get { return TurquoiseCreatureUids.Count; } }

    public List<Guid> PurpleCreatureUids { get; set; } = new List<Guid>();
    public int PurpleCreatureCount { get { return PurpleCreatureUids.Count; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTemperature < NormalTemperature)
            CurrentTemperature += 1 * Time.deltaTime;
        else if (CurrentTemperature > NormalTemperature)
            CurrentTemperature -= 1 * Time.deltaTime;
    }

    /// <summary>
    /// Поднять температуру
    /// </summary>
    public void TemperatureUp(float tempPerSec)
    {
        //isNormalCondition = false;
        CurrentTemperature += tempPerSec * Time.deltaTime;
        if (CurrentTemperature >= MaxTemp)
            CurrentTemperature = MaxTemp;
    }

    /// <summary>
    /// Опустить температуру
    /// </summary>
    public void TemperatureDown(float tempPerSec)
    {
        //isNormalCondition = false;
        CurrentTemperature -= tempPerSec * Time.deltaTime;
        if (CurrentTemperature <= MinTemp)
            CurrentTemperature = MinTemp;
    }

    private const float MaxTemp = 100.0f;
    private const float MinTemp = 0.0f;

    //нормальные условия для температуры (на нее ничего не влияет)
    //private bool isNormalCondition = true;
}
