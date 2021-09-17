using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отвечает за движение существа (пока не понятно, надо ли)
/// </summary>
public class CreatureMoveController : MonoBehaviour
{
    //public Animator animator;

    /// <summary>
    /// Направление движения
    /// </summary>
    public Vector3 MoveDirection { get; set; }

    /// <summary>
    /// Скорость движения
    /// </summary>
    public float MoveSpeed { get; set; } = 0.5f;

    void Start()
    {
        self = this.gameObject;
        selfCreature = self.GetComponent<CreatureBase>();
    }

    void Update()
    {
        //движение
        if (selfCreature.StateController.CanMove())
        {
            //поиск кормушки
            if (selfCreature.StateController.CurrentState == CreatureState.feedSearching)
            {
                //TODO: сейчас двигается напрямую. сделать огибание препятствий
                transform.position += (MoveDirection - transform.position).normalized * Time.deltaTime * MoveSpeed;
            }
            //просто движение
            else
            {
                transform.position += MoveDirection * Time.deltaTime * MoveSpeed;
            }
        }
    }

    
    private GameObject self;
    private CreatureBase selfCreature;
}
