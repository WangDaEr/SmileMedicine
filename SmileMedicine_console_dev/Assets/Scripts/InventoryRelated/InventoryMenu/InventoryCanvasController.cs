using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCanvasController : MonoBehaviour
{
    public GameManager gm;
    public InputSystem m_input;

    //public Dictionary<GameObject, GameObject> menus = new Dictionary<GameObject, GameObject>();
    //public List<KeyValuePair<GameObject, GameObject>> menus = new List<KeyValuePair<GameObject, GameObject>>();
    public List<GameObject> menus = new List<GameObject>();

    public int menuIndex_init;
    public pairIndex currentSelectedUIItem;
    //public GameObject currentSelectedObject;

    private int currentActiveButtonIndex;

    private List<GameObject[]> pairs;

    public bool setList = false;

    private bool hor_allow_input;
    private bool ver_allow_input;

    private Dictionary<InputSystem.AxisButtomPressed, int> button_value;

    public enum pairIndex
    {
        Button,
        Panel
    }

    private bool inputLockAcquired;

    // Start is called before the first frame update
    void Start()
    {
        inputLockAcquired = false;
        BindPanelWithButtons();

        foreach (GameObject[] pair in pairs)
        {
            Debug.Log("valid: " + pair[1]);
            pair[1].SetActive(false);
        }

        currentActiveButtonIndex = menuIndex_init;

        pairs[currentActiveButtonIndex][(int)pairIndex.Panel].SetActive(true);
        currentSelectedUIItem = pairIndex.Button;

        hor_allow_input = true;
        ver_allow_input = true;

        Debug.Log("Canvas active;");
    }

    private void Init_button_value()
    {
        button_value = new Dictionary<InputSystem.AxisButtomPressed, int>();
        button_value.Add(InputSystem.AxisButtomPressed.Positive, 1);
        button_value.Add(InputSystem.AxisButtomPressed.Negative, -1);
        button_value.Add(InputSystem.AxisButtomPressed.NoInput, 0);
    }

    private void BindPanelWithButtons()
    {
        pairs = new List<GameObject[]>();
        Transform buttons = transform.GetChild(0);

        for (int i = 0; i < menus.Count; ++i)
        {
            pairs.Add(new GameObject[2] { buttons.GetChild(i).gameObject, menus[i]});

            Color c = new Color();

            if (menus[i].GetComponent<InventoryGridMainPanel>())
            {
                menus[i].GetComponent<InventoryGridMainPanel>().focusColor = buttons.GetChild(i).GetComponent<Image>().color;
                c = menus[i].GetComponent<InventoryGridMainPanel>().focusColor;
            }
            else if (menus[i].GetComponent<InventoryVerticalMainPanel>())
            {
                menus[i].GetComponent<InventoryVerticalMainPanel>().focusColor = buttons.GetChild(i).GetComponent<Image>().color;
                c = menus[i].GetComponent<InventoryVerticalMainPanel>().focusColor;
            }
            Debug.Log("color: " + menus[i].GetComponent<InventoryMainPanel>().focusColor);
            //Debug.Log("Colour: " + c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CanvasInputLock!!!!!: " + inputLockAcquired);
        CanvasInput();
    }

    private void CanvasInput()
    {
        Debug.Log("Canvas: " + m_input.hor_axis_button);
        /*
        if (currentSelectedUIItem == pairIndex.Button)
        {
            if (m_input.hor_axis_val > 0.0F)
            {
                SwitchPanel(1);
                
            }
            else if (m_input.hor_axis_val < 0.0F)
            {
                SwitchPanel(-1);
                
            }
            else if (m_input.ver_axis_val > 0.0F)
            {

            }
            else if (m_input.ver_axis_val < 0.0F)
            {

            }
        }
        else if (currentSelectedUIItem == pairIndex.Panel)
        {
            if (m_input.hor_axis_val > 0.0F)
            {
                
            }
            else if (m_input.hor_axis_val < 0.0F)
            {
                
            }
            else if (m_input.ver_axis_val > 0.0F)
            {

            }
            else if (m_input.ver_axis_val < 0.0F)
            {

            }
        }
        */
        switch (m_input.hor_axis_button)
        {
            case InputSystem.AxisButtomPressed.Positive:
                SwitchItemHorizontal(1);
                return;
                break;

            case InputSystem.AxisButtomPressed.Negative:
                SwitchItemHorizontal(-1);
                return;
                break;

            default:
                break;
        }

        switch (m_input.ver_axis_button)
        {
            case InputSystem.AxisButtomPressed.Positive:
                SwitchItemVertical(1);
                return;
                break;

            case InputSystem.AxisButtomPressed.Negative:
                SwitchItemVertical(-1);
                return;
                break;

            default:
                break; 
        }
    }

    public void SwitchItemHorizontal(int dir)
    {
        switch (currentSelectedUIItem)
        {
            case pairIndex.Button:

                int newButtonIndex = currentActiveButtonIndex + dir;

                if (newButtonIndex < 0)
                {
                    newButtonIndex = pairs.Count - 1;
                }
                else if (newButtonIndex == pairs.Count)
                {
                    newButtonIndex = 0;
                }

                pairs[currentActiveButtonIndex][(int)pairIndex.Panel].SetActive(false);
                pairs[newButtonIndex][(int)pairIndex.Panel].SetActive(true);
                currentActiveButtonIndex = newButtonIndex;

                break;

            case pairIndex.Panel:

                pairs[currentActiveButtonIndex][(int)pairIndex.Panel].GetComponent<InventoryMainPanel>().ChangeSelectedItem(dir, InventoryMainPanel.NewIndexType.Increment);

                break;
            
            default:
                break;
        }
        
    }

    private void SwitchItemVertical(int dir)
    {
        switch (currentSelectedUIItem)
        {
            case pairIndex.Button:

                

                break;

            case pairIndex.Panel:



                break;

            default:
                break;
        }
    }


}
