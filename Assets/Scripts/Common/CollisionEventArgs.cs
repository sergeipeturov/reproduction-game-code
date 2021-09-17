using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEventArgs
{
    //public DateTime MomentOfCollision { get; set; }
    //public string MomentOfCollision { get; set; }
    public GameObject Collider1 { get; set; }
    public GameObject Collider2 { get; set; }

    public CollisionEventArgs(GameObject collider1, GameObject collider2)
    {
        Collider1 = collider1; Collider2 = collider2;
        //MomentOfCollision = DateTime.Now.ToString("HH.mm");
    }
}
