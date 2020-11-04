using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : MonoBehaviour
{
    public Transform kPlayer;
    public float fDistance = 5f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float distance = Mathf.Sqrt(h * h + v * v);
        float speed = distance / Time.deltaTime;
        animator.SetFloat("MoveSpeed", speed);

        bool fire = Input.GetButtonDown("Fire1");
        if (fire)
        {
            animator.SetTrigger("Die");
        }
    }

    private void FixedUpdate()
    {
        Vector3 kNewPos = Camera.main.transform.position + fDistance * Camera.main.transform.forward;
        kNewPos.y = 0;

        kPlayer.position = kNewPos;
    }
}
