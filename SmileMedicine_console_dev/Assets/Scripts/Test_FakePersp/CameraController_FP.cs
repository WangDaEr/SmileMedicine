using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_FP : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject layerManager;
    public GameObject playerCharacter;

    private LayersManager lm;
    private PlayerController pc;

    void Start()
    {
        lm = layerManager.GetComponent<LayersManager>();
        pc = playerCharacter.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.InputLockAcquired)
        {
            float input_hor_val = pc.m_input.hor_axis_val;
            if (input_hor_val != 0.0F)
            {
                float move_hor_val = input_hor_val * pc.runningSpeed * Time.deltaTime;
                transform.Translate(Vector3.right * move_hor_val * pc.speedFactor);

                MoveLayers_relative(move_hor_val);
            }
        }
        
    }

    private void MoveLayers_relative(float player_move_hor_val)
    {
        foreach (Transform layer in lm.transform)
        {
            layer.Translate(-(Vector3.right * player_move_hor_val * layer.localScale.x));
        }
    }
}
