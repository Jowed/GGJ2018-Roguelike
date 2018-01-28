using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public float movementSpeed = 1.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float mouseSpeed = 0.0f;
    public bool fogOn = true;
    public Color fogColor = Color.black;
    public float fogDensity = 0.2f;

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

        CharacterController controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(dx, 0, dz);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed;
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
        float x = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSpeed;
        //float z = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSpeed;
        
        //transform.position += new Vector3(dx * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad), 0.0f, dz * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        transform.Rotate(0, x, 0);
        if ((x != 0.0f || moveDirection.x != 0.0f || moveDirection.z != 0.0f) && !audio.isPlaying)
        {
            audio.clip = walking[i];
            audio.Play();
        }
        //transform.Rotate(-z, 0, 0);
    }
}
