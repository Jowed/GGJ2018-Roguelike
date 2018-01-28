using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public Controller controller;
    public RoomController roomController;
    public GameObject Room;
    public GameObject Floor;

    public float wallWidth = 1.0f;
    public float hallWidth = 1.0f;

    public int roomCount = 0;
    public int seed;
    public bool next = true;
    public int currentLevel = 0;

    public int diffusion = 10;
    public float weightDrop = 0.1f;

    private Level[] levels = new Level[3];

    private GameObject[] rooms = new GameObject[100];
    private GameObject[] halls = new GameObject[15];

	// Use this for initialization
	void Start () {
        Random.InitState(seed);
        //constructShape(0.1f, 1.0f, 0.5f, 2.0f, 5);
        //constructHall(1.0f, 2.0f, 5.0f, 2.0f);
        //constructHall(5.0f, 1.0f, 5.0f, 5.0f);
        //constructHall(10.0f, 11.0f, 12.0f, 13.0f);
        //GameObject room = Instantiate(Room);
        //constructRoom(0.0f, 0.0f, 5.0f, 5.0f, room);
        //constructRoom(10.0f, 5.0f, 8.0f, 5.0f, room);
    }
	
	// Update is called once per frame
	void Update () {
		if (next)
        {
            next = !next;
            gen();
            //++currentLevel;
            //if (currentLevel > 3)
                //currentLevel = 0;
            //controller.cycleLevel();
        }
	}

    IEnumerator waitNSeconds()
    {
        yield return new WaitForSeconds(5);
    }

    void gen() {
        Vector2 xz;
        float dx, dz;
        RoomController r;
        if (roomCount == 0)
        {
            roomCount = (int)Random.Range(3.0f, 10.0f);
        }
        Debug.Log("Room count: " + roomCount);
        for (int i = 0; i < roomCount; ++i)
        {
            xz = Random.insideUnitCircle;
            dx = xz.x * diffusion;
            dz = xz.y * diffusion;
            rooms[i] = Instantiate(Room);
            //rooms[i].transform.parent = Floor.transform;
            genRoom(rooms[i]);
            rooms[i].transform.SetPositionAndRotation(new Vector3(dx, 0.0f, dz), Quaternion.identity);
            r = rooms[i].GetComponent<RoomController>();
            r.x1 += dx;
            r.x2 += dx;
            r.z1 += dz;
            r.z2 += dz;
        }
        separateRooms();
    }

    bool isBetween(float x, float y, float z)
    {
        if (z > y)
            return x <= z && x >= y;
        return x >= z && x <= y;
    }

    bool inside(float x, float x1, float x2, float z, float z1, float z2)
    {
        if (isBetween(x, x1, x2) && isBetween(z, z1, z2))
        {
            return true;
        }
        return false;
    }

    Vector2 conflict(RoomController r1, RoomController r2)
    {
        //r1's top-left edge is inside r2
        //bool a = inside(r1.x1, r2.x1, r2.x2, r1.z1, r2.z1, r2.z2);
        //r1's top-right edge is inside r2
        //bool b = inside(r1.x2, r2.x1, r2.x2, r1.z1, r2.z1, r2.z2);
        //r1's bottom-left edge is inside r2
        //bool c = inside(r1.x1, r2.x1, r2.x2, r1.z2, r2.z1, r2.z2);
        //r1's bottom-right edge is inside r2
        //bool d = inside(r1.x2, r2.x1, r2.x2, r1.z2, r2.z1, r2.z2);
        //left-right intersection
        //return (((r1.x1 < r2.x1 && r1.x2 > r2.x1) || (r1.x1 < r2.x2 && r1.x2 > r2.x2)) && ((r1.z1 < r2.z1 && r1.z2 > r2.z1) || (r1.z1 < r2.z2 && r1.z2 > r2.z2)));
        Vector2 result = new Vector2(0.0f, 0.0f);
        //r2 is to the right of r1 by amount result.x
        if ((r1.x1 < r2.x1 && r1.x2 > r2.x1)) //checked
        {
            ///Debug.Log("r2 is to the right of r1");
            ///Debug.Log(r1.x1 + " " + r1.z1);
            ///Debug.Log(r2.x1 + " " + r2.z1);
            result.x = r1.x2 - r2.x1; //checked
        }
        //r2 is to the left of r1 by amount result.x
        if ((r1.x1 < r2.x2 && r1.x2 > r2.x2)) //checked
        {
            ///Debug.Log("r2 is to the left of r1");
            ///Debug.Log(r1.x1 + " " + r1.z1);
            ///Debug.Log(r2.x1 + " " + r2.z1);
            result.x = Mathf.Max(result.x, r2.x2 - r1.x1); //checked
        }
        //r2 is above r1 by amount result.y
        if ((r1.z1 > r2.z2 && r2.z2 > r1.z2)) //checked
        {
            ///Debug.Log("r2 above r1");
            ///Debug.Log(r1.x1 + " " + r1.z1);
            ///Debug.Log(r2.x1 + " " + r2.z1);
            result.y = r1.z1 - r2.z2; //checked
        }
        //r2 is below r1 by amount result.y
        if ((r1.z1 > r2.z1 && r2.z1 > r1.z2)) //checked
        {
            ///Debug.Log("r2 is below r1");
            ///Debug.Log(r1.x1 + " " + r1.z1);
            ///Debug.Log(r2.x1 + " " + r2.z1);
            result.y = Mathf.Max(result.y, r2.z1 - r1.z2); //checked
        }
        return result;
    }

    void separateRooms()
    {
        RoomController r1, r2;
        Vector2 d;
        for (int i = 0; i < roomCount; ++i)
        {
            for (int j = 0; j < roomCount; ++j)
            {
                if (i == j)
                    continue;
                r1 = rooms[i].GetComponent<RoomController>();
                r2 = rooms[j].GetComponent<RoomController>();
                d = conflict(r1, r2);
                if (d.x != 0.0f && d.y != 0.0f)
                {
                    Debug.Log("Room1: " + r1.x1 + " " + r1.z1 + " " + r1.x2 + " " + r1.z2);
                    Debug.Log("Room2: " + r2.x1 + " " + r2.z1 + " " + r2.x2 + " " + r2.z2);
                    Debug.Log("Room difference: " + d.x + ", " + d.y);
                    r2.transform.position += new Vector3(2 * d.x, 0.0f, d.y);
                }
            }
        }
    }

    /*void separateRooms() {
        int inConflict = 0;
        float x, z;
        float weight = 2.0f;
        float aspect = this.GetComponent<Camera>().aspect;
        RoomController r1, r2;
        while (inConflict < 100000)
        {
            ++inConflict;
            for(int i = 0; i < roomCount - 1; ++i)
            {
                r1 = rooms[i].GetComponent<RoomController>();
                r2 = rooms[i+1].GetComponent<RoomController>();
                if (conflict(r1, r2))
                {
                    Debug.Log(r1.x1 + " " + r2.x1);
                    x = r1.x1;
                    z = r1.z1;
                    //inConflict = true;
                    if (Random.value*aspect > weight)
                    {
                        x = r1.x1 + (weight * r1.width);
                        z = r1.z1 + (weight * r1.height / 4);
                    }
                    r1.x1 = x;
                    r1.z1 = z;
                }
            }
            //weight = weight - (weight * weightDrop);
        }
    }*/

    void genRoom(GameObject room) {
        float width, height;
        width = Random.Range(5.0f, 50.0f);
        height = Random.Range(5.0f, 50.0f);
        constructRoom(0.0f, 0.0f, width, height, room);
    }

    void constructRoom(float x, float z, float width, float height, GameObject parent) {
        if (width < (2 * wallWidth) || height < (2 * wallWidth))
        {
            Debug.LogWarning("Warning: room should be large enough to accomodate wall size");
        }
        x = x - (width / 2);
        z = z + (height / 2);
        //top
        constructShape(x - (wallWidth / 2), z - (wallWidth / 2), x + width + (wallWidth / 2), z + (wallWidth / 2), 5.0f, parent);
        //bottom
        constructShape(x - (wallWidth / 2), z - height - (wallWidth / 2), x + width + (wallWidth / 2), z - height + (wallWidth / 2), 5.0f, parent);
        //left
        constructShape(x + width - (wallWidth / 2), z - height - (wallWidth / 2), x + width + (wallWidth / 2), z + (wallWidth / 2), 5.0f, parent);
        //right
        constructShape(x - (wallWidth / 2), z - height - (wallWidth / 2), x + (wallWidth / 2), z + (wallWidth / 2), 5.0f, parent);
        RoomController rc = parent.GetComponent<RoomController>();
        rc.x1 = x;
        rc.z1 = z;
        rc.x2 = x + width;
        rc.z2 = z - height;
        rc.width = width;
        rc.height = height;
    }

    void constructHall(float x1, float z1, float x2, float z2, GameObject parent) {
        if (z1 == z2)
        { //corridor is left-right
            constructShape(x1, z1 + hallWidth, x2, z2 + hallWidth + wallWidth, 5.0f, parent);
            constructShape(x1, z1 - hallWidth - wallWidth, x2, z2 - hallWidth, 5.0f, parent);
        }
        else if (x1 == x2)
        { //corridor is up-down
            constructShape(x1 + hallWidth, z1, x2 + hallWidth + wallWidth, z2, 5.0f, parent);
            constructShape(x1 - hallWidth - wallWidth, z1, x2 - hallWidth, z2, 5.0f, parent);
        }
        else //diagonal halls later
        {
            Debug.LogWarning("Cannot make a diagonal hall yet");
        }
    }

    void makeCube(float dx, float dy, float dz, float x, float y, float z)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(x, y, z);
        cube.transform.localScale = new Vector3(dx, dy, dz);
    }

    void constructShape(float x1, float z1, float x2, float z2, float height, GameObject parent){
        GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
        float sx, sz, x, z = 0.0f;
        x = (x1 + x2) / 2;
        z = (z1 + z2) / 2;
        sx = x2 - x1;
        sz = z2 - z1;
        cube.transform.position = new Vector3(x, height * 0.5f, z);
        cube.transform.localScale = new Vector3(sx, height, sz);
        cube.transform.parent = parent.transform;
	}
}
