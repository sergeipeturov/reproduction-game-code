using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBlue : CreatureBase
{
    /// <summary>
    /// Кол-во созданий, не меньше которого должно быть в клетке для повышения настроения
    /// </summary>
    public int CreatureCountForGoodMood { get; set; } = 10;

    public override void OnAwake()
    {
        Color = CreatureColor.blue;
    }

    public override void CollisionDetect(GameObject collider)
    {
        base.CollisionDetect(collider);
    }

    public override bool MoodCondition()
    {
        //чем больше любых созданий в клетке с синими, тем лучше
        var creatureCountInCell = CellManagerStatic.CreatureCountInCurrentCell(CurrentCellUid);
        if (creatureCountInCell > CreatureCountForGoodMood)
            return true;
        else
            return false;
    }

    public override void PerformFunction()
    {
        CellManagerStatic.GetCellByUid(CurrentCellUid).TemperatureDown(0.1f);
    }
}
