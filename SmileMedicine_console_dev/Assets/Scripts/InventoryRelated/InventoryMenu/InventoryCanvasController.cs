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
    public List<GameObject[]> menus = new List<GameObject[]>();

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

    }
}
