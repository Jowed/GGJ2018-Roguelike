using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Move : MonoBehaviour {

    public float movementSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float mouseSpeed = 0.0f;
    public bool fogOn = true;
    public Color fogColor = Color.black;
    public float fogDensity = 0.2f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public CharacterController controller;
    public Transform t;
    public bool resetOnDeath = true;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private bool inRange = false;

    public AudioClip[] walking;
    private int i = 0;
    public AudioSource audio;

    // Use this for initialization
    void Start () {
        RenderSettings.fogColor = fogColor;
        RenderSettings.fog = true;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogMode = FogMode.Exponential;
    }

    private void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.gameObject.tag);
        if(c.CompareTag("Enemy"))
        {
            if (resetOnDeath)
            {
                t.position = new Vector3(-10.08f, 1.0f, -17.64f);
                SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
            }
            else
            {
                t.position = new Vector3(-10.08f, 1.0f, -17.64f);
            }
        }
        if (c.CompareTag("Finish"))
        {
            SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update () {
        ++i;
        if (i >= walking.Length)
            i = 0;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fog = fogOn;
        RenderSettings.fogDensity = fogDensity;
        RenderSettings.fogMode = FogMode.Exponential;

        float dx = 0.0f;
        float dz = 0.0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            dz = 1.0f;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            dx = -1.0f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            dz = -1.0f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dx = 1.0f;

        moveDirection = new Vector3(dx, 0, dz);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;
        moveDirection += Physics.gravity;
        controller.Move(moveDirection * Time.deltaTime);
       
        //float CharacterSpeed = 10.0f;

        /*if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            dir.z += 1.0f;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            dir.x -= 1.0f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            dir.z -= 1.0f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            dir.x += 1.0f;
        else if (Input.GetKey(KeyCode.Tab))
            dir.y -= 1.0f;
        else if (Input.GetKey(KeyCode.Space))
            dir.y += 1.0f;
            */
        //moveDirection.Normalize();
        //transform.Translate(moveDirection * movementSpeed * Time.deltaTime);
        //float x = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
        //float z = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;

        float x = yaw;
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (yaw > 360)
            yaw -= 360;
        if (yaw < 0)
            yaw += 360;
        if (pitch > 70)
            pitch = 70;
        if (pitch < -70)
            pitch = -70;
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        //Debug.Log(pitch + " " + yaw);
        //transform.position += new Vector3(dx * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad), 0.0f, dz * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        //transform.Rotate(z, x, 0);
        if ((yawRange(yaw) || moveDirection.x != 0.0f || moveDirection.z != 0.0f) && !audio.isPlaying)
        {
            audio.clip = walking[i];
            audio.Play();
        }
        //transform.Rotate(-z, 0, 0);
    }

    //return true if recently rotated by about ~45
    bool yawRange(float yaw)
    {
        if (yaw % 45 == 0.0f)
        {
            if (inRange)
                return false;
            inRange = true;
            return true;
        }
        float x = 45.0f / (yaw % 45);
        if (x > 9.0f || x < 1.125f)
        {
            if (inRange)
                return false;
            inRange = true;
            return true;
        }
        inRange = false;
        return false;
    }
}
