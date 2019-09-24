using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// control the information flow in the system and conduct macro tasks;
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public GameObject layerManager;
    public GameObject platformSystem;
    public GameObject inventoryCanvas;

    [HideInInspector]
    public CheckPoint cp;
    private Vector3 PlayerStartPos;

    public Camera mainCamera;

    public enum SystemUsingInput
    {
        PlayerController,
        InventoryMenu,
    }

    private InputSystem m_input;
    private SystemUsingInput sui;

    private void Awake()
    {
        SetGMReference();
        m_input = GetComponent<InputSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sui = SystemUsingInput.PlayerController;
        PlayerStartPos = playerCharacter.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_input.SELECT_pressed)
        {
            sui = (SystemUsingInput)(((int)sui + 1) % 2);

            bool showCanvas = sui == SystemUsingInput.InventoryMenu;

            inventoryCanvas.SetActive(showCanvas);
            mainCamera.orthographic = !showCanvas;
            playerCharacter.GetComponent<PlayerController>().ChangeInputLock();
            inventoryCanvas.GetComponent<InventoryCanvasController>().ChangeInputLock();
            //layerManager.SetActive(!showCanvas);
            platformSystem.SetActive(!showCanvas);
            playerCharacter.SetActive(!showCanvas);
        }
    }

    /// <summary>
    /// register the game manager to all gameobjects requiring its reference
    /// </summary>
    private void SetGMReference()
    {
        playerCharacter.GetComponent<PlayerController>().gm = this;
        playerCharacter.GetComponent<PlayerController>().m_input = GetComponent<InputSystem>();

        //layerManager.GetComponent<LayersManager>().gm = this;
        platformSystem.GetComponent<PlatformSystem>().gm = this;

        inventoryCanvas.GetComponent<InventoryCanvasController>().m_input = GetComponent<InputSystem>();
        inventoryCanvas.GetComponent<InventoryCanvasController>().m_is = inventoryCanvas.transform.parent.GetComponent<InventorySystem>();
        inventoryCanvas.transform.parent.GetComponent<InventorySystem>().gm = this;
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

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void RespawnPlayerCharacter()
    {
        if (cp)
        {
            playerCharacter.transform.position = cp.transform.position;
        }
        else
        {
            playerCharacter.transform.position = PlayerStartPos;
        }
    }

    public void ChangePlayerPosition(Vector3 des)
    {
        playerCharacter.transform.position = des;
    }
}
