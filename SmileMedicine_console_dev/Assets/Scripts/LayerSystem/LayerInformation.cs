using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// store distinct attributes of an individual layer and perform tasks related to itself;
/// </summary>
public class LayerInformation : MonoBehaviour
{
    public int layerIndex;
    private LayersManager lm;
    private float switchSpeed;
    private Vector3 lt_des_pos;
    private float lt_des_degree;
    private float degreeCount = 0.0F;
    private bool lt_rotate_dir;

    private bool startTransformation;

    public LayersManager LayerManager
    {
        get { return lm; }
        set { lm = value; this.switchSpeed = lm.switchSpeed; }
    }

    // Start is called before the first frame update
    void Start()
    {
        startTransformation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTransformation)
        {
            startTransformation = !LayerTransformation(lt_des_pos, lt_des_degree, lt_rotate_dir);
        }
        else
        {
            degreeCount = 0.0F;
        }
    }

    /// <summary>
    /// only called once to start the transformation;
    /// </summary>
    /// <param name="des_pos"></param>
    public void StartLayerTransformation(Vector3 des_pos, int rotateDir)
    {
        if (startTransformation)
        {
            return;
        }

        lt_des_pos = des_pos;
        lt_des_degree = lm.CylinderIntervalDegree * rotateDir;
        lt_rotate_dir = rotateDir > 0;

        startTransformation = true;
    }

    /// <summary>
    /// move individual layer and check whether it reach the destination;
    /// </summary>
    /// <param name="des_pos"></param>
    /// <returns></returns>
    public bool LayerTransformation(Vector3 des_pos, float degree, bool rotateDir)
    {

        if (lm.layerTranformationOption == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, des_pos, switchSpeed * Time.deltaTime);
        }
        else if (lm.layerTranformationOption == 1)
        {
            transform.RotateAround(new Vector3(0.0F, -lm.CylinderRadius, 0.0F), Vector3.right, rotateDir ? switchSpeed * Time.deltaTime : -switchSpeed * Time.deltaTime);
            degreeCount += rotateDir ? switchSpeed * Time.deltaTime : -switchSpeed * Time.deltaTime;
            //Debug.Log("roatting!!!!!");
        }

        bool reach_end = lm.layerTranformationOption == 0 ? des_pos.Equals(transform.position) : (degreeCount - lt_des_degree) / lt_des_degree >= 0;

        return reach_end;
    }
}
