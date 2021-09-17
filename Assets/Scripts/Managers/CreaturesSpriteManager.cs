using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesSpriteManager : MonoBehaviour
{
    //спрайты созданий
    //синий
    public Sprite BlueCommon;
    public Sprite BlueInLove;
    public Sprite BlueFeeding;

    //спрайты действий
    public Sprite ActionDancing;
    public Sprite ActionDancingWith;
    public Sprite ActionPlaying;
    public Sprite ActionPlayingWith;
    public Sprite ActionSinging;
    public Sprite ActionSingingWith;
    public Sprite ActionFighting;
    public Sprite ActionSleeping;

    //умер, погиб
    public Sprite Dead1;
    public Sprite Dead2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetCommonSprite(CreatureBase creature)
    {
        if (creature == null) return BlueCommon;

        switch(creature.Color)
        {
            case CreatureColor.blue:
                return BlueCommon;
            default:
                return BlueCommon;
            //TODO: для других цветов
        }
    }

    public Sprite GetInloveSprite(CreatureBase creature)
    {
        if (creature == null) return BlueInLove;

        switch (creature.Color)
        {
            case CreatureColor.blue:
                return BlueInLove;
            default:
                return BlueInLove;
                //TODO: для других цветов
        }
    }

    public Sprite GetFeedingSprite(CreatureBase creature)
    {
        if (creature == null) return BlueFeeding;

        switch (creature.Color)
        {
            case CreatureColor.blue:
                return BlueFeeding;
            default:
                return BlueFeeding;
                //TODO: для других цветов
        }
    }
}
