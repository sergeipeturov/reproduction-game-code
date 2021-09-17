using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Обработчик событий столкновения объектов
/// </summary>
public class CollisionManager : MonoBehaviour
{
    public List<CollisionEventArgs> Collisions { get; set; } = new List<CollisionEventArgs>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Collisions.Count > 100)
            Collisions.Clear();
    }

    public void OnCollisionEvent(CollisionEventArgs e)
    {
        //Debug.Log("collision: " + e.MomentOfCollision);
        collider1 = e.Collider1; collider2 = e.Collider2;
        if (collider1 != null && collider2 != null)
        {
            collider1Creature = collider1.GetComponent<CreatureBase>(); collider2Creature = collider2.GetComponent<CreatureBase>();
            if (collider1Creature != null && collider2Creature != null)
            {
                var sameCollision = Collisions.FirstOrDefault(x => x.Collider1 == e.Collider2 && x.Collider2 == e.Collider1);
                if (sameCollision != null)
                {
                    Collisions.Remove(sameCollision);
                    if (collider1Creature.Peaceful && collider2Creature.Peaceful)
                    {
                        //TODO: при столкновении двух мирных не обязательно размножение, но можно например танцевать, драться или петь (при этом не обязательно быть одного цвета)
                        //пока просто выбираем случайно одно из парных действий и решаем, будем ли его делать
                        List<CreatureState> stateToChoose = new List<CreatureState>() { CreatureState.dancing, CreatureState.playing, CreatureState.singing, CreatureState.fighting };
                        if (IsSameColor(collider1Creature, collider2Creature)) stateToChoose.Add(CreatureState.reproducting);
                        int randChoosenStateIndex = UnityRandomHelper.RandomInt(0, stateToChoose.Count);
                        CreatureState randChoosenState = stateToChoose[randChoosenStateIndex];
                        switch (randChoosenState)
                        {
                            case CreatureState.dancing:
                                DancingCollision();
                                break;
                            case CreatureState.reproducting:
                                ReproductionCollision();
                                break;
                            case CreatureState.playing:
                                PlayingCollision();
                                break;
                            case CreatureState.singing:
                                SingingCollision();
                                break;
                            case CreatureState.fighting:
                                FightingCollision();
                                break;
                        }
                    }
                    else
                    {
                        //TODO: если один мирный, а другой агрессивный
                    }
                }
                else
                {
                    Collisions.Add(e);
                }
            }
        }   
    }

    private void ReproductionCollision()
    {
        if (collider1Creature.StateController.CanReproduce() && collider2Creature.StateController.CanReproduce())
        {
            collider1Creature.StateController.CurrentState = CreatureState.makingReprodusingDecision; collider2Creature.StateController.CurrentState = CreatureState.makingReprodusingDecision;
            collider1Creature.ReproductionController.IsReproductor = true; collider2Creature.ReproductionController.IsReproductor = false;
            bool collider1Decision = collider1.GetComponent<CreatureDecisionController>().MakeReproductionDecision();
            bool collider2Decision = collider2.GetComponent<CreatureDecisionController>().MakeReproductionDecision();
            if (collider1Decision && collider2Decision)
            {
                collider1Creature.StateController.CurrentState = CreatureState.reproducting; collider2Creature.StateController.CurrentState = CreatureState.reproducting;
            }
            else
            {
                collider1Creature.StateController.CurrentState = CreatureState.staying; collider2Creature.StateController.CurrentState = CreatureState.staying;
                collider1Creature.StateController.IsRejected = true; collider2Creature.StateController.IsRejected = true;
            }
        }
    }

    private void DancingCollision()
    {
        if (collider1Creature.StateController.CanDance() && collider2Creature.StateController.CanDance())
        {
            collider1Creature.StateController.CurrentState = CreatureState.makingSomeDecision; collider2Creature.StateController.CurrentState = CreatureState.makingSomeDecision;
            bool collider1Decision = collider1.GetComponent<CreatureDecisionController>().MakeDanceWithDecision();
            bool collider2Decision = collider2.GetComponent<CreatureDecisionController>().MakeDanceWithDecision();
            if (collider1Decision && collider2Decision)
            {
                collider1Creature.ActionsController.StartDancing(collider2Creature.gameObject);
                collider2Creature.ActionsController.StartDancing(collider1Creature.gameObject);
            }
            else
            {
                collider1Creature.StateController.CurrentState = CreatureState.staying; collider2Creature.StateController.CurrentState = CreatureState.staying;
            }
        }
    }

    private void PlayingCollision()
    {
        if (collider1Creature.StateController.CanPlay() && collider2Creature.StateController.CanPlay())
        {
            collider1Creature.StateController.CurrentState = CreatureState.makingSomeDecision; collider2Creature.StateController.CurrentState = CreatureState.makingSomeDecision;
            bool collider1Decision = collider1.GetComponent<CreatureDecisionController>().MakePlayWithDecision();
            bool collider2Decision = collider2.GetComponent<CreatureDecisionController>().MakePlayWithDecision();
            if (collider1Decision && collider2Decision)
            {
                collider1Creature.ActionsController.StartPlaying(collider2Creature.gameObject);
                collider2Creature.ActionsController.StartPlaying(collider1Creature.gameObject);
            }
            else
            {
                collider1Creature.StateController.CurrentState = CreatureState.staying; collider2Creature.StateController.CurrentState = CreatureState.staying;
            }
        }
    }

    private void SingingCollision()
    {
        if (collider1Creature.StateController.CanSing() && collider2Creature.StateController.CanSing())
        {
            collider1Creature.StateController.CurrentState = CreatureState.makingSomeDecision; collider2Creature.StateController.CurrentState = CreatureState.makingSomeDecision;
            bool collider1Decision = collider1.GetComponent<CreatureDecisionController>().MakeSingWithDecision();
            bool collider2Decision = collider2.GetComponent<CreatureDecisionController>().MakeSingWithDecision();
            if (collider1Decision && collider2Decision)
            {
                collider1Creature.ActionsController.StartSinging(collider2Creature.gameObject);
                collider2Creature.ActionsController.StartSinging(collider1Creature.gameObject);
            }
            else
            {
                collider1Creature.StateController.CurrentState = CreatureState.staying; collider2Creature.StateController.CurrentState = CreatureState.staying;
            }
        }
    }

    private void FightingCollision()
    {
        bool col1CanFight = collider1Creature.StateController.CanFight();
        bool col2CanFight = collider2Creature.StateController.CanFight();
        if (col1CanFight || col2CanFight) //здесь "или" потому что для драки не нужно обоюдное согласие
        {
            collider1Creature.StateController.CurrentState = CreatureState.makingSomeDecision; collider2Creature.StateController.CurrentState = CreatureState.makingSomeDecision;
            bool collider1Decision = col1CanFight ? collider1.GetComponent<CreatureDecisionController>().MakeFightWithDecision() : false;
            bool collider2Decision = collider1Decision ? true : col2CanFight ? collider2.GetComponent<CreatureDecisionController>().MakeFightWithDecision() : false;
            if (collider1Decision && collider2Decision)
            {
                collider1Creature.ActionsController.StartFighting(collider2Creature.gameObject);
                collider2Creature.ActionsController.StartFighting(collider1Creature.gameObject);
            }
            else
            {
                collider1Creature.StateController.CurrentState = CreatureState.staying; collider2Creature.StateController.CurrentState = CreatureState.staying;
            }
        }
    }

    private bool IsSameColor(CreatureBase c1, CreatureBase c2)
    {
        return c1.Color == c2.Color;
    }

    private GameObject collider1, collider2;
    private CreatureBase collider1Creature, collider2Creature;
}
