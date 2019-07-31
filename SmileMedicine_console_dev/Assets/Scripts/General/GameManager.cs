using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// control the information flow in the system and conduct macro tasks;
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public GameObject layerManager;
    public GameObject inventoryCanvas;

    public Camera mainCamera;

    public enum SystemUsingInput
    {
        PlayerController,
        InventoryMenu,
    }

    private InputSystem m_input;
    private SystemUsingInput sui;

    // Start is called before the first frame update
    void Start()
    {
        SetGMReference();

        m_input = GetComponent<InputSystem>();
        sui = SystemUsingInput.PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_input.SELECT_pressed)
        {
            sui = (SystemUsingInput)(((int)sui + 1) % 2);

            playerCharacter.GetComponent<PlayerController>().ChangeInputLock();
            //inventoryCanvas.GetComponent<InventoryCanvasController>().ChangeInputLock();

            inventoryCanvas.SetActive(sui == SystemUsingInput.InventoryMenu);
            mainCamera.orthographic = (sui != SystemUsingInput.InventoryMenu);
            inventoryCanvas.GetComponent<InventoryCanvasController>().ChangeInputLock();
        }
    }

    /// <summary>
    /// register the game manager to all gameobjects requiring its reference
    /// </summary>
    private void SetGMReference()
    {
        playerCharacter.GetComponent<PlayerController>().gm = this;
        playerCharacter.GetComponent<PlayerController>().m_input = GetComponent<InputSystem>();

        layerManager.GetComponent<LayersManager>().gm = this;

        inventoryCanvas.GetComponent<InventoryCanvasController>().gm = this;
        inventoryCanvas.GetComponent<InventoryCanvasController>().m_input = GetComponent<InputSystem>();
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
        inventoryCanvas.SetActive(true);
    }
}
