using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject parent;
    public Vector3 child;
    private float x, z;
    public float far;
    public float speed;
    public float count;
    public int num;
    public WallType wallType;
    private Renderer renderer;
    public float hight;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wallType == WallType.Player1)
        {
            renderer.material.color = Color.blue;
        }
        if (wallType == WallType.Player2)
        {
            renderer.material.color = Color.red;
        }
        transform.position = ((parent.transform.position + (child * count)));
    }
}
