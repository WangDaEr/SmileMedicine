using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindowButton : InventoryButton
{
    public float panelInitialScale;
    public float panelShowupTime;

    public float panelDistance;

    private float cameraTransformSpeed;

    private void Awake()
    {
        bindedPanel.GetComponent<InventoryMainPanel>().icc = transform.parent.parent.GetComponent<InventoryCanvasController>();
        bindedPanel.GetComponent<InventoryMainPanel>().selectedColor = GetComponent<Image>().color;
        bindedPanel.GetComponent<InventoryMainPanel>().unSelectedColor = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedAlpha = 0.3F;
        unselectedAlpha = 1.0F;
        alphaSwitchDuration = 1.0F;

        //Debug.Log("Window button: " + gameObject.name + transform.parent.parent.GetComponent<InventoryCanvasController>().initialButtonIdx + " " + transform.parent.parent.GetComponent<InventoryCanvasController>().initialPanelIdx);

        if (transform.parent.parent.GetComponent<InventoryCanvasController>().initialButtonIdx == transform.GetSiblingIndex() 
            && transform.parent.parent.GetComponent<InventoryCanvasController>().initialPanelIdx == transform.parent.GetSiblingIndex())
        {
            GetFocus();

            Debug.Log("initial button: " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void GetFocus()
    {
        GetComponent<Image>().CrossFadeAlpha(selectedAlpha, alphaSwitchDuration, false);
    }

    public override void LoseFocus()
    {
        GetComponent<Image>().CrossFadeAlpha(unselectedAlpha, alphaSwitchDuration, false);
    }

    public override void ButtonClick()
    {
        Debug.Log("Button is Clicked: " + gameObject.name);

        Vector3 des_pos = bindedPanel.transform.position;
        Vector3 off_set = icc.mainCamera.transform.position - icc.transform.position;

        cameraTransformSpeed = (des_pos + off_set - icc.mainCamera.transform.position).magnitude / panelShowupTime;

        icc.mainCamera.GetComponent<CameraController_IC>().startSpecialMove(des_pos + off_set, cameraTransformSpeed);

        bindedPanel.GetComponent<InventoryMainPanel>().ChangeInputLock();
    }

    public override void ReturnCanvas()
    {
        icc.mainCamera.GetComponent<CameraController_IC>().startSpecialMove(icc.cameraStartPos, cameraTransformSpeed);
        bindedPanel.GetComponent<InventoryMainPanel>().ChangeInputLock();
    }
}
