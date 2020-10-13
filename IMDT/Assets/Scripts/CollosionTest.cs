using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollosionTest : MonoBehaviour
{
    public float force;
    public float friction;

    public GameObject other;

    public LayerMask collisionMask;

    //上一帧结束时的速度
    private Vector3 preV;

    Ray ray1;
    Ray ray2;
    void Start()
    {
        preV = Vector3.zero;
    }

    void Update()
    {
        //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * preV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = preV + frictionDeltaV;
        if (finalV.x * preV.x <= 0)
            frictionDeltaV.x = -preV.x;
        if (finalV.y * preV.y <= 0)
            frictionDeltaV.y = -preV.y;
        if (finalV.z * preV.z <= 0)
            frictionDeltaV.z = -preV.z;

        //计算用户用力方向
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 fDir = new Vector3(moveHorizontal, 0.0f, moveVertical);
        fDir.Normalize();


        //计算加速度
        Vector3 acceleration = force * fDir;

        Vector3 prePos = transform.position;

        //应用加速度
        Vector3 curV = preV + Time.deltaTime * acceleration + frictionDeltaV;
        transform.Translate((curV + preV) * Time.deltaTime / 2);
        preV = curV;


        //检测是否与其他球相撞
        Vector3 pos = transform.position;
        if (other != null)
        {
            OtherSphere otherSphere = other.GetComponent<OtherSphere>();
            Vector3 otherPos = other.transform.position;

            //球体间碰撞检测，判断球心距离与两球半径之和即可
            if (Vector3.Distance(pos, otherPos) < 0.5 + otherSphere.radius) //简单起见，认为自己的半径为0.5
            {
                Debug.Log("碰撞发生!");
                Vector3 v1 = preV;
                float m1 = 1.0f; // 简单起见，认为自己的质量为1
                Vector3 v2 = otherSphere.currentV;
                float m2 = otherSphere.mass;

                preV = ((m1 - m2) * v1 + 2 * m2 * v2) / (m1 + m2);
                otherSphere.currentV = ((m2 - m1) * v2 + 2 * m1 * v1) / (m1 + m2);

                //如果有碰撞，位置回退，防止穿透
                transform.position = prePos;
            }
        }

        float distance = ((curV + preV) * Time.deltaTime / 2).magnitude + 0.05f;

        Vector3 moveDirection = curV.normalized;
        CheckCollisions(distance, moveDirection);


    }

    void CheckCollisions(float moveDistance, Vector3 moveDirection)
    {
        Vector3 Vhorizontal = new Vector3(moveDirection.x, 0, 0);
        Vector3 Vupdown = new Vector3(0, 0, moveDirection.z);
        float upRatio =Mathf.Abs(moveDirection.z / moveDirection.magnitude);
        float horizontalRatio =Mathf.Abs(moveDirection.x / moveDirection.magnitude);

        //检测上下是否碰撞
        int upward = moveDirection.z > 0 ? 1 : -1;

        ray1 = new Ray(transform.position + Vupdown.normalized /2 , Vupdown);
        RaycastHit hit1;
        if (Physics.Raycast(ray1, out hit1, 1000f, collisionMask) && hit1.distance < moveDistance )
        {
            OnHitObject(hit1);
        }

        //检测左右是否碰撞
        int rightward = moveDirection.x > 0 ? 1 : -1;

        ray2 = new Ray(transform.position + Vhorizontal.normalized / 2, Vhorizontal);

        RaycastHit hit2;
        if (Physics.Raycast(ray2, out hit2, 1000f, collisionMask) && hit2.distance < moveDistance )
        {
            Debug.Log("horizontal detected..");
            OnHitObject(hit2);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray1);
        Gizmos.DrawRay(ray2);
    }

    void OnHitObject(RaycastHit hit)
    {

        Debug.Log(this.gameObject.name + "hitted:" + hit.collider.gameObject.name + " hit position:" + hit.point);
        Debug.Log("preV" + this.preV);
        Debug.Log("normal" + hit.normal);

        this.preV = Vector3.Reflect(this.preV, hit.normal);
        return;

    }
}
