using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvasController : MonoBehaviour
{
    public GameManager gm;
    public InputSystem m_input;

    public int initialPanelIdx;
    public int initialButtonIdx;
    public GameObject initialButton;

    public int currentPanelIdx;
    public int currentButtonIdx;

    public float cameraPanelDis;
    public Camera mainCamera;
    public Vector3 cameraStartPos;

    private bool atIndividualPanel;
    

    private bool inputLockAcquired;
    private Dictionary<InputSystem.AxisButtomPressed, int> inputMapping;

    private void Awake()
    {
        inputLockAcquired = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        atIndividualPanel = false;

        //inputMappingSetup();
        initializeLayout();

        for (int i = 0; i < 2; i++)
        {
            foreach (Transform window in transform.GetChild(i))
            {
                window.GetComponent<InventoryButton>().icc = this;
            }
        }
        mainCamera = Camera.main;
        cameraStartPos = mainCamera.transform.position;

        mainCamera.orthographic = false; //change project mode when active canvas in GameManager

        Debug.Log("Canvas active;");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputLockAcquired)
        {
            CanvasInput();
        }
        Interaction();        
    }

    public void ChangeInputLock()
    {
        inputLockAcquired = !inputLockAcquired;
        Debug.Log("InventoryController InputLock: " + inputLockAcquired);
    }

    private void inputMappingSetup()
    {
        inputMapping = new Dictionary<InputSystem.AxisButtomPressed, int>
        {
            { InputSystem.AxisButtomPressed.Positive, 1 },
            { InputSystem.AxisButtomPressed.Negative, -1 },
            { InputSystem.AxisButtomPressed.NoInput, 0 }
        };
    }

    private void initializeLayout()
    {
        currentButtonIdx = 0;
        currentPanelIdx = 1;
    }

    private void CanvasInput()
    {
        Debug.Log("Canvas: " + m_input.hor_axis_button);

        int newButtonIdx = -1;
        int newPanelIdx = -1;
        switch (m_input.hor_axis_button)
        {
            case InputSystem.AxisButtomPressed.Positive:

                newButtonIdx = currentButtonIdx + 1;
                newButtonIdx = newButtonIdx >= transform.GetChild(currentPanelIdx).childCount ? transform.GetChild(currentPanelIdx).childCount - 1 : newButtonIdx;

                break;

            case InputSystem.AxisButtomPressed.Negative:

                newButtonIdx = currentButtonIdx - 1;
                newButtonIdx = newButtonIdx < 0 ? 0 : newButtonIdx;

                break;

            default:
                break;
        }

        switch (m_input.ver_axis_button)
        {
            case InputSystem.AxisButtomPressed.Positive:

                newPanelIdx = currentPanelIdx + 1;
                newPanelIdx = newPanelIdx >= 2 ? 1 : newPanelIdx;

                newButtonIdx = newButtonIdx < 0 ? currentButtonIdx : newButtonIdx;
                newButtonIdx = newButtonIdx >= transform.GetChild(newPanelIdx).childCount ? transform.GetChild(newPanelIdx).childCount - 1 : newButtonIdx;

                break;

            case InputSystem.AxisButtomPressed.Negative:

                newPanelIdx = currentPanelIdx - 1;
                newPanelIdx = newPanelIdx < 0 ? 0 : newPanelIdx;

                newButtonIdx = newButtonIdx < 0 ? currentButtonIdx : newButtonIdx;
                newButtonIdx = newButtonIdx >= transform.GetChild(newPanelIdx).childCount ? transform.GetChild(newPanelIdx).childCount - 1 : newButtonIdx;

                break;

            default:
                break; 
        }

        if (newButtonIdx != -1)
        {
            newPanelIdx = newPanelIdx < 0 ? currentPanelIdx : newPanelIdx;

            transform.GetChild(currentPanelIdx).GetChild(currentButtonIdx).GetComponent<InventoryButton>().LoseFocus();
            transform.GetChild(newPanelIdx).GetChild(newButtonIdx).GetComponent<InventoryButton>().GetFocus();

            Debug.Log("new Button: " + newButtonIdx + " panel: " + newPanelIdx);

            currentButtonIdx = newButtonIdx;
            currentPanelIdx = newPanelIdx;
        }
    }

    public void PanelFilter(string targetPanelTag, bool targetStatus, bool filterOthers)
    {
        foreach (Transform panel in transform)
        {
            if (panel.tag == targetPanelTag ^ filterOthers)
            {
                panel.gameObject.SetActive(targetStatus);

                Debug.Log("panel set active: " + panel.name + " " + panel.tag + " " + (panel.tag == targetPanelTag));
            }
        }
    }

    private void Interaction()
    {
        if (m_input.A_pressed && !atIndividualPanel)
        {
            transform.GetChild(currentPanelIdx).GetChild(currentButtonIdx).GetComponent<InventoryButton>().ButtonClick();

            atIndividualPanel = true;
            ChangeInputLock();
        }
        else if (m_input.B_pressed && atIndividualPanel)
        {
            transform.GetChild(currentPanelIdx).GetChild(currentButtonIdx).GetComponent<InventoryButton>().ReturnCanvas();

            atIndividualPanel = false;
            ChangeInputLock();
        }
    }
}
