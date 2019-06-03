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
            startTransformation = LayerTransformation(lt_des_pos);
        }
    }

    /// <summary>
    /// only called once to start the transformation;
    /// </summary>
    /// <param name="des_pos"></param>
    public void StartLayerTransformation(Vector3 des_pos)
    {
        lt_des_pos = des_pos;
        startTransformation = true;
    }

    /// <summary>
    /// move individual layer and check whether it reach the destination;
    /// </summary>
    /// <param name="des_pos"></param>
    /// <returns></returns>
    public bool LayerTransformation(Vector3 des_pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, des_pos, switchSpeed * Time.deltaTime);
        bool not_reach_end = des_pos.Equals(transform.position);

        return not_reach_end;
    }
}
