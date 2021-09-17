using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CellManagerStatic
{
    public static List<CellController> Cells { get; set; } = new List<CellController>();

    public static int CreatureCountInAllCells { get { return Cells.Sum(x => x.AllCreatureCount); } }

    public static void SetAllCells()
    {
        var cellObjs = GameObject.FindGameObjectsWithTag("cell");
        foreach(var item in cellObjs)
        {
            var cellScript = item.GetComponent<CellController>();
            if (cellScript != null)
                Cells.Add(cellScript);
        }
    }

    public static CellController GetCellByUid(Guid uid)
    {
        return Cells.FirstOrDefault(x => x.Uid == uid);
    }

    public static void AddCreatureToCell(GameObject cell, GameObject creature)
    {
        var cellScript = cell.GetComponent<CellController>();
        var creatureScript = creature.GetComponent<CreatureBase>();
        if (cellScript != null && creatureScript != null)
        {
            var currentCellScript = Cells.FirstOrDefault(x => x.Uid == cellScript.Uid);
            if (currentCellScript != null)
            {
                switch (creatureScript.Color)
                {
                    case CreatureColor.blue:
                        if (!currentCellScript.BlueCreatureUids.Any(x => x == creatureScript.Uid))
                        {
                            currentCellScript.BlueCreatureUids.Add(creatureScript.Uid);
                            creatureScript.CurrentCellUid = currentCellScript.Uid;
                        }
                        break;
                    //TODO: добавить для остальных цветов
                }
            }
        }
    }

    public static int CreatureCountInCurrentCell(Guid cellUid)
    {
        var cell = Cells.FirstOrDefault(x => x.Uid == cellUid);
        if (cell != null)
        {
            return cell.AllCreatureCount;
        }
        else
        {
            return 0;
        }    
    }
}
