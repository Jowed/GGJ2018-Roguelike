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

    private Animator anim;

    // Use this for initialization
    void Start()
    {
        //this should work but doesn't :-(
        anim = GetComponent<Animator>();
        anim.enabled = false;
        StartCoroutine(delay());
        anim.enabled = true;
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(Random.value * 5);
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
        if (Vector3.Distance(transform.position, player.position) <= maxDist &&
            Vector3.Distance(transform.position, player.position) >= minDist)
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
