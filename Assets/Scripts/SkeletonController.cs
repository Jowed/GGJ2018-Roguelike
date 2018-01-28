using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{

    public Transform player;
    public float speed = 4.0f;
    public float maxDist = 10.0f;
    public float minDist = 5.0f;

    public CharacterController controller;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //close
        //if (Vector3.Distance(transform.position, player.position) >= minDist)
        //{
        //play attack animation
        //}
        //near
        Vector3 playerxz = new Vector3(player.transform.position.x, 0.475f, player.transform.position.z);
        if (Vector3.Distance(transform.position, player.position) <= maxDist)
        {
            transform.LookAt(playerxz);
            //transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            controller.Move((transform.forward + Physics.gravity) * speed * Time.deltaTime);
        }
        //far
        //else
        //{
            //play idle animation
        //}
    }
}
