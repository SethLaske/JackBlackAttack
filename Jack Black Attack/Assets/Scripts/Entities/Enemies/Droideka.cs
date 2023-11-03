using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Droideka : BaseEnemy
{
    private Animator anim;

    public static List<Vector3> vectorList = new List<Vector3>();

    void Start()
    {
        EnemyStart();
        anim = gameObject.GetComponentInChildren<Animator>();

        //later im going to put in the levelManager values
        // DONT GET PISSED... CHILL
        Vector3 topLeft = new Vector3(-8, 12, 0);
        Vector3 topRight = new Vector3(8, 12, 0);
        Vector3 botLeft = new Vector3(-8, -12, 0);
        Vector3 botRight = new Vector3(8, -12, 0);

        vectorList.Add(topLeft);
        vectorList.Add(topRight);
        vectorList.Add(botLeft);
        vectorList.Add(botRight);
    }


}
