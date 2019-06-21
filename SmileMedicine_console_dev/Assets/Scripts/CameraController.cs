using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //public Camera camera;//probably can just use Camera.mainCamera
    public Camera camera;
    public GameObject Player;
    //public float MaxZoom;
    public float ZoomRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CamToPlayer = camera.transform.position - Player.transform.position;
        CamToPlayer.Normalize();

        Vector3 pos = camera.transform.position;
        if(Input.GetKey(KeyCode.DownArrow))
        {
            pos -= CamToPlayer * ZoomRate * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            pos += CamToPlayer * ZoomRate * Time.deltaTime;
        }
        camera.transform.position = pos;
    }
}
