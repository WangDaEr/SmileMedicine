using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : MonoBehaviour
{
    public enum MobileInputSwipeDirection
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public struct MobileInputData
    {
        public bool fingerClick;
        public bool fingerClickLeft;
        public bool fingerHold;
        public Vector2 fingerPosition;
        public bool hasSwipe;
        public Vector2 movement;
        public MobileInputSwipeDirection swipeDirection; 
    }

    public float minimumSwipeDistance = 20F;
    public Text dispaly;

    private InputSystem inputSystem;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isSwipe;

    //mobile input debug message
    private int a = 0;
    private int f = -1;
    private string t = "";

    private void Awake()
    {
        inputSystem = GetComponent<InputSystem>();
        inputSystem.mi = this;
    }

    // Update is called once per frame
    void Update()
    {
        MobileInputData data = new MobileInputData()
        {
            fingerClick = false,
            fingerClickLeft = Input.mousePosition.x < Screen.width / 2,
            fingerHold = false,
            hasSwipe = false,
            movement = new Vector2(),
            swipeDirection = MobileInputSwipeDirection.None
        };

        

        Debug.Log("touches: " + Input.touches.Length);

        //mobile input debug message
        string t0 = "touch" + f + " has ended\n";

        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                startPos = t.position;
                endPos = t.position;
                data.fingerPosition = t.position;
            }

            if (t.phase == TouchPhase.Ended)
            {
                f = t.fingerId;
                endPos = t.position;
                checkSwipe(ref data);
            }

            data.fingerClick |= !data.hasSwipe && t.phase == TouchPhase.Began;
            data.fingerHold |= !data.hasSwipe && t.phase == TouchPhase.Stationary;
        }

        //mobile input debug message
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            ++a;
        }
        string t1 = "touches: " + Input.touches.Length + "\n";
        string t2 = "touch end count: " + a + "\n";
        string t3 = "swipe detection: " + data.hasSwipe;
        //Debug.Log("clicked: " + data.fingerClick);
        if (dispaly)
        {
            dispaly.text = t0 + t1 + t2 + t3 + t;
        }

        inputSystem.mid = data;
    }

    private bool IsValidSwipe()
    {
        return Mathf.Abs(startPos.x - endPos.x) > minimumSwipeDistance || Mathf.Abs(startPos.y - endPos.y) > minimumSwipeDistance;
    }

    private void checkSwipe(ref MobileInputData data)
    {
        bool ans = IsValidSwipe();
        //mobile input debug message
        if (dispaly)
        {
            t = "\nis valid swipe: " + ans + " " + Mathf.Abs(startPos.x - endPos.x) + " " + Mathf.Abs(startPos.y - endPos.y);
        }

        if (ans)
        {
            data.movement = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);
            if (Mathf.Abs(startPos.x - endPos.x) > Mathf.Abs(startPos.y - endPos.y))
            {
                if (startPos.x < endPos.x) { data.swipeDirection = MobileInputSwipeDirection.Right; }
                else { data.swipeDirection = MobileInputSwipeDirection.Left; }
            }
            else
            {
                if (startPos.y < endPos.y) { data.swipeDirection = MobileInputSwipeDirection.Up; }
                else { data.swipeDirection = MobileInputSwipeDirection.Down; }
            }

            Debug.Log("is swipe");
        }

        data.hasSwipe = ans;
    }
}
