using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public GameObject layers;

    // Start is called before the first frame update
    void Start()
    {
        SetGMReference();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LayerTransformation(int new_centre)
    {
        foreach (Transform t_child in layers.transform)
        {

        }
    }

    private void SetGMReference()
    {
        playerCharacter.GetComponent<PlayerController>().gm = this;
        playerCharacter.GetComponent<PlayerController>().m_input = GetComponent<InputSystem>();
    }
}
