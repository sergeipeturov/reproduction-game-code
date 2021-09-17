using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        prefabManager = GetComponent<PrefabManager>();
        collisionManager = GetComponent<CollisionManager>();
        CreatureInstantiator.PrefabManager = prefabManager;
        CreatureInstantiator.CollisionManager = collisionManager;
        inputManager = GetComponent<InputManager>();
        uIManager = GetComponent<UIManager>();
        inputManager.UIManager = uIManager;
        CellManagerStatic.SetAllCells();
        StartGameTest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAddBlueClick()
    {
        //var creatureInst = Instantiate(prefabManager.BluePrefab, new Vector3(0, 0, 0), new Quaternion());
        //creatureInst.GetComponent<CreatureBase>().CollisionEvent += collisionManager.OnCollisionEvent;
        CreatureInstantiator.InstantiateCreature(CreatureColor.blue, new Vector3(0, 0, 0));
    }

    public void OnFillFeederClick()
    {
        var feeder = GameObject.FindGameObjectWithTag("feeder").GetComponent<Feeder>();
        feeder.AddMaxFeed();
    }

    private void StartGameTest()
    {
        //var creatureInst = Instantiate(prefabManager.BluePrefab, new Vector3(0, 0, 0), new Quaternion());
        //creatureInst.GetComponent<CreatureBase>().CollisionEvent += collisionManager.OnCollisionEvent;
        CreatureInstantiator.InstantiateCreature(CreatureColor.blue, new Vector3(0, 0, 0));
    }

    private PrefabManager prefabManager;
    private CollisionManager collisionManager;
    private InputManager inputManager;
    private UIManager uIManager;
}
