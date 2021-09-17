using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStateController : MonoBehaviour
{
    private CreatureState currentState = CreatureState.staying;
    /// <summary>
    /// Текущий статус
    /// </summary>
    public CreatureState CurrentState
    { 
        get { return currentState; }
        set
        {
            OnStateChanging(currentState, value);
            currentState = value;
            switch (currentState)
            {
                case CreatureState.staying:
                case CreatureState.moving:
                //case CreatureState.rejected:
                    self.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().GetCommonSprite(selfCreature);
                    break;
                case CreatureState.reproducting:
                    self.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().GetInloveSprite(selfCreature);
                    break;
                case CreatureState.feeding:
                    self.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().GetFeedingSprite(selfCreature);
                    break;
                case CreatureState.dancing:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionDancing;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.dancingWith:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionDancingWith;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.playing:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionPlaying;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.playingWith:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionPlayingWith;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.singing:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionSinging;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.singingWith:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionSingingWith;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.fighting:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionFighting;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.sleeping:
                    selfActionSprite.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().ActionSleeping;
                    selfActionSprite.SetActive(true);
                    break;
                case CreatureState.dead1:
                    self.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().Dead1;
                    break;
                case CreatureState.dead2:
                    self.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().Dead2;
                    break;
                default:
                    self.GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameManager").GetComponent<CreaturesSpriteManager>().GetCommonSprite(selfCreature);
                    break;
            }
        } 
    }

    /// <summary>
    /// Недавно был отвергнут или недавно было размножение. Или недавно родился
    /// </summary>
    public bool IsRejected { get; set; }

    public bool CanMove()
    {
        return CurrentState == CreatureState.staying || CurrentState == CreatureState.moving || CurrentState == CreatureState.feedSearching;
    }

    public bool CanReproduce()
    {
        return (CurrentState == CreatureState.staying || CurrentState == CreatureState.moving) && !IsRejected && selfCreature.StatsController.CanReproduce();
    }

    public bool CanDance()
    {
        return (CurrentState == CreatureState.staying || CurrentState == CreatureState.moving) && selfCreature.StatsController.CanPerformFunction() && selfCreature.StatsController.CanDance() && !IsRejected;
    }

    public bool CanPlay()
    {
        return (CurrentState == CreatureState.staying || CurrentState == CreatureState.moving) && selfCreature.StatsController.CanPerformFunction() && selfCreature.StatsController.CanPlay() && !IsRejected;
    }

    public bool CanSing()
    {
        return (CurrentState == CreatureState.staying || CurrentState == CreatureState.moving) && selfCreature.StatsController.CanPerformFunction() && selfCreature.StatsController.CanSing() && !IsRejected;
    }

    public bool CanFight()
    {
        return (CurrentState == CreatureState.staying || CurrentState == CreatureState.moving) && !selfCreature.StatsController.CanPerformFunction() && selfCreature.StatsController.CanFight() && !IsRejected;
    }

    /// <summary>
    /// Смерть или гибель
    /// </summary>
    public void Death()
    {
        CurrentState = CreatureState.dead1;
        selfCreature.StatsController.CurrentComfort = 0;
        selfCreature.StatsController.CurrentHealth = 0;
        selfCreature.StatsController.CurrentMood = 0;
        selfCreature.StatsController.CurrentPower = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        selfCreature = self.GetComponent<CreatureBase>();
        selfActionSprite = selfCreature.ActionSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnStateChanging(CreatureState oldState, CreatureState newState)
    {
        if (newState == CreatureState.dead1)
        {
            if (oldState == CreatureState.fighting)
                CurrentState = CreatureState.dead2;
        }

        if (oldState == CreatureState.feeding)
            IsRejected = true;
        if (oldState == CreatureState.dancing || oldState == CreatureState.dancingWith ||
            oldState == CreatureState.playing || oldState == CreatureState.playingWith ||
            oldState == CreatureState.singing || oldState == CreatureState.singingWith ||
            oldState == CreatureState.fighting ||
            oldState == CreatureState.sleeping)
            selfActionSprite.SetActive(false);
    }

    private GameObject self;
    private CreatureBase selfCreature;
    private GameObject selfActionSprite;
}
