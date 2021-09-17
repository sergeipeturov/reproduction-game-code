using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureReproductionController : MonoBehaviour
{
    /// <summary>
    /// Для размножения нуждается в двух
    /// </summary>
    public bool NeedTwoForReproduction { get; set; } = true;

    /// <summary>
    /// Из него выйдет новый при спаривании двух
    /// </summary>
    public bool IsReproductor { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        self = this.gameObject;
        selfCreature = self.GetComponent<CreatureBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selfCreature.StateController.CurrentState == CreatureState.reproducting)
        {
            currentReproducingTime += Time.deltaTime;
            if (currentReproducingTime >= maxReproducingTime)
            {
                if (IsReproductor)
                    InstantiateChildren();
                currentReproducingTime = 0.0f;
                selfCreature.StateController.IsRejected = true;// .CurrentState = CreatureState.rejected;
                selfCreature.StateController.CurrentState = CreatureState.staying;
            }
        }
        if (selfCreature.StateController.IsRejected)// .CurrentState == CreatureState.rejected)
        {
            currentRejectingTime += Time.deltaTime;
            if (currentRejectingTime >= maxRejectingTime)
            {
                currentRejectingTime = 0.0f;
                selfCreature.StateController.IsRejected = false;// .CurrentState = CreatureState.staying;
            }
        }
    }

    private void InstantiateChildren()
    {
        switch (selfCreature.Color)
        {
            case CreatureColor.blue:
                CreatureInstantiator.InstantiateCreature(CreatureColor.blue, new Vector3(transform.position.x + 0.1f, transform.position.y + 0.1f, 0));
                break;
            //TODO: для других цветов
        }


        //if (selfCreature is CreatureBlue)
        //{
        //    CreatureInstantiator.InstantiateCreature(CreatureColor.blue, new Vector3(transform.position.x + 0.1f, transform.position.y + 0.1f, 0));
        //}
    }

    //время, затрачиваемое на размножение
    private float maxReproducingTime = 10.0f;
    //время, в течении которого создание будет переживать отвергнутое размножение
    private float maxRejectingTime = 10.0f;
    //private bool childrenWasBorn = false;
    private float currentRejectingTime = 0.0f;
    private float currentReproducingTime = 0.0f;

    private GameObject self;
    private CreatureBase selfCreature;
}
