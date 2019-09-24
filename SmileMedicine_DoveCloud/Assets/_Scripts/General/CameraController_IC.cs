using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_IC : MonoBehaviour
{
    private bool startMove;
    private Vector3 des_pos;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        startMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startMove)
        {
            startMove = !specialMoveUpdate();
        }
    }

    public void startSpecialMove(Vector3 des_pos, float speed)
    {
        this.des_pos = des_pos;
        this.speed = speed;
        startMove = true;

        //Debug.Log("speed: " + speed * Time.deltaTime);
    }

    private bool specialMoveUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, des_pos, speed * Time.deltaTime);

        return transform.position == des_pos;
    }
}
