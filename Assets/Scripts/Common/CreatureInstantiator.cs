using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreatureInstantiator
{
    public static PrefabManager PrefabManager { get; set; }
    public static CollisionManager CollisionManager { get; set; }

    public static void InstantiateCreature(CreatureColor color, Vector3 position)
    {
        switch(color)
        {
            case CreatureColor.blue:
                var creatureInst = GameObject.Instantiate(PrefabManager.BluePrefab, position, new Quaternion());
                creatureInst.GetComponent<CreatureBase>().CollisionEvent += CollisionManager.OnCollisionEvent;
                creatureInst.GetComponent<CreatureBase>().StateController.IsRejected = true; // .CurrentState = CreatureState.rejected;
                CellManagerStatic.AddCreatureToCell(CellManagerStatic.Cells.FirstOrDefault().gameObject, creatureInst);
                break;
        }
    }
}

public enum CreatureColor
{
    blue, red, green, orange, turquoise, purple
}
