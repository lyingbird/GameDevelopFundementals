using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSphere : MonoBehaviour
{
    public float friction;
    public float mass;
    public float radius;

    Ray ray1;
    Ray ray2;
    public LayerMask collisionMask;

    [NonSerialized]
    public Vector3 currentV;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * currentV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = currentV + frictionDeltaV;
        if (finalV.x * currentV.x <= 0)
            frictionDeltaV.x = -currentV.x;
        if (finalV.y * currentV.y <= 0)
            frictionDeltaV.y = -currentV.y;
        if (finalV.z * currentV.z <= 0)
            frictionDeltaV.z = -currentV.z;

        //应用加速度
        Vector3 curV = currentV + frictionDeltaV;


        

        transform.Translate((curV + currentV) * Time.deltaTime / 2);
        currentV = curV;

        float distance = ((curV + currentV) * Time.deltaTime / 2).magnitude + 0.05f;
        Vector3 moveDirection = currentV.normalized;
        CheckCollisions(distance, moveDirection);
        


    }

    void CheckCollisions(float moveDistance, Vector3 moveDirection)
    {
        Vector3 Vhorizontal = new Vector3(moveDirection.x, 0, 0);
        Vector3 Vupdown = new Vector3(0, 0, moveDirection.z);
        float upRatio = Mathf.Abs(moveDirection.z / moveDirection.magnitude);
        float horizontalRatio = Mathf.Abs(moveDirection.x / moveDirection.magnitude);

        //检测上下是否碰撞
        int upward = moveDirection.z > 0 ? 1 : -1;

        ray1 = new Ray(transform.position + Vupdown.normalized / 2, Vupdown);
        RaycastHit hit1;
        if (Physics.Raycast(ray1, out hit1, 1000f, collisionMask) && hit1.distance < moveDistance)
        {
            OnHitObject(hit1);
        }

        //检测左右是否碰撞
        int rightward = moveDirection.x > 0 ? 1 : -1;

        ray2 = new Ray(transform.position + Vhorizontal.normalized / 2, Vhorizontal);

        RaycastHit hit2;
        if (Physics.Raycast(ray2, out hit2, 1000f, collisionMask) && hit2.distance < moveDistance)
        {
            Debug.Log("horizontal detected..");
            OnHitObject(hit2);
        }
    }


    void OnHitObject(RaycastHit hit)
    {
        
            Debug.Log(this.gameObject.name +  "hitted:" + hit.collider.gameObject.name + " hit position:" + hit.point);
            this.currentV = Vector3.Reflect(this.currentV, hit.normal);
            return;

    }
}
