using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// control the information flow in the system and conduct macro tasks;
/// </summary>
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

    /// <summary>
    /// register the game manager to all gameobjects requiring its reference
    /// </summary>
    private void SetGMReference()
    {
        playerCharacter.GetComponent<PlayerController>().gm = this;
        playerCharacter.GetComponent<PlayerController>().m_input = GetComponent<InputSystem>();
    }

    /// <summary>
    /// start the main menu and pasue the game;
    /// </summary>
    public void StartPauseMenu()
    {

    }

    /// <summary>
    /// dispaly the inventory interface;
    /// </summary>
    public void StartInventory()
    {

    }
}
