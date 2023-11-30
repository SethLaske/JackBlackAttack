using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
    public GameObject target;
    public float offScreenThreshhold;
    private Camera mainCamera;
    private float distanceToTarget;
    public static bool enemyOnScreen;
    public bool isEnemy;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    Debug.Log(enemyOnScreen);
       Vector3 targetViewportPosition = mainCamera.WorldToViewportPoint(target.transform.position);

       if(targetViewportPosition.z > 0 && targetViewportPosition.x > 0 && targetViewportPosition.x < 1 && targetViewportPosition.y > 0 && targetViewportPosition.y < 1)
        {
         //target is on screen. Hide indicator
            enemyOnScreen = true;
            
        }
        else
        {
            enemyOnScreen = false;
            Vector3 screenEdge = mainCamera.ViewportToWorldPoint(new Vector3(Mathf.Clamp(targetViewportPosition.x, 0.1f, 0.9f), Mathf.Clamp(targetViewportPosition.y, 0.1f, 0.9f), mainCamera.nearClipPlane));
            transform.position = new Vector3(screenEdge.x, screenEdge.y, 0);
            Vector3 direction = target.transform.position - transform.position;
            float angle = GetAngleFromVectorFloat(direction) + 270;
            transform.rotation = Quaternion.Euler(0,0,angle);
        }
        if (enemyOnScreen || (!LevelManager.PointerOn && isEnemy))
            {sr.enabled = false;}
        else
            {sr.enabled = true;}


           
    }
    public float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) 
            n += 360;
        return n;
    }
}
//Could put arrow under enemy prefabs and set active when 3 are left
//put one under gate, turn on when gate open. 