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
    }

    private void BindPanelWithButtons()
    {
        pairs = new List<GameObject[]>();
        Transform buttons = transform.GetChild(0);

        for (int i = 0; i < menus.Count; ++i)
        {
            pairs.Add(new GameObject[2] { buttons.GetChild(i).gameObject, menus[i]});

            if (menus[i].GetComponent<InventoryGridMainPanel>())
            {
                menus[i].GetComponent<InventoryGridMainPanel>().focusColor = buttons.GetChild(i).GetComponent<Image>().color;
            }
            else if (menus[i].GetComponent<InventoryVerticalMainPanel>())
            {
                menus[i].GetComponent<InventoryVerticalMainPanel>().focusColor = buttons.GetChild(i).GetComponent<Image>().color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputLockAcquired)
        {
            CanvasInput();
        }
    }

    public void ChangeInputLock()
    {
        inputLockAcquired = !inputLockAcquired;
    }

    private void CanvasInput()
    {
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
    }

    public void SwitchPanel(int dir)
    {
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
        /*
        GameObject preButton = pairs[currentActiveButtonIndex][(int)pairIndex.Button];
        GameObject newButton = pairs[newButtonIndex][(int)pairIndex.Button];

        preButton.GetComponent<Image>().color = preButton.GetComponent<Inventory>
        */
    }
}
