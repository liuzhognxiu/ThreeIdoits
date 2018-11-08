using UnityEngine;
using System.Collections;

public enum ControlState
{
    Idle,
    JoyStick,
    Mouse,
    Key,
}

public class UIJoystickPanel : MonoBehaviour
{
    [SerializeField]
    private Transform mJoystick;
    UIWidget mJoystickWidget;
    private Transform mTouch;
    private UISprite mArea;
    private UIWidget mZone;
    private Vector3 mMouseLocalPos;
    private float mAreaRadius;
    private bool mFixJoystick = false;

    public GameObject Player;

    public bool FixJoystick
    {
        get
        {
            return mFixJoystick;
        }
        set
        {
            mFixJoystick = value;
        }
    }
    private Vector3 mJoystickDefaultLocalPosition;

    static float[] DirThresholds;
    static float DirOffset;

    CSDirection mLastDirection = CSDirection.None;
    public CSDirection LastDirection
    {
        get { return mLastDirection; }
        set
        {
            mLastDirection = value;
            if (mLastDirection != CSDirection.None)
            {
            }
        }
    }

    private float mPixelSizeAdjustment = 0;
    private float PixelSizeAdjustment
    {
        get
        {
            if (mPixelSizeAdjustment == 0)
            {
                mPixelSizeAdjustment = NGUITools.FindInParents<UIRoot>(this.transform).pixelSizeAdjustment;
            }
            return mPixelSizeAdjustment;
        }
    }

    private static UIJoystickPanel mSingleton;
    public static UIJoystickPanel Singleton { get { return mSingleton; } }

    public void Reset()
    {
        LastDirection = CSDirection.None;
    }

    void Awake()
    {
        if (mSingleton == null) mSingleton = this;
        mJoystick = this.transform.Find("joystick");
        mJoystickWidget = mJoystick.GetComponent<UIWidget>();
        mTouch = this.transform.Find("joystick/touch");
        mArea = this.transform.Find("joystick/area").GetComponent<UISprite>();//Utility.Get<UISprite>(this.transform, "joystick/area");
        mZone = this.transform.Find("zone").GetComponent<UIWidget>(); //Utility.Get<UIWidget>(this.transform, "zone");

        //摇杆参数配置逆时针
        string[] splitStr = "315#270#225#180#135#90#45#0".Split('#');
        DirThresholds = new float[splitStr.Length];
        float value;
        for (int i = 0; i < DirThresholds.Length; i++)
        {
            if (float.TryParse(splitStr[i], out value))
            {
                DirThresholds[i] = value;
            }
        }

        if (DirThresholds.Length > 0)
        {
            DirOffset = 90f + (360f - DirThresholds[0]) / 2f;
        }

        //DirThresholds = new float[]{315f,270f,225f,180f,135f,90f,45f,0f};
        //DirOffset = 90f + (360f - DirThresholds[0]) / 2f;
    }

    void Start()
    {
        mJoystick.gameObject.SetActive(true);
        mJoystickWidget.alpha = 0.4f;
        StartCoroutine(SaveDefaultPos());
        FixJoystick = true;
        mAreaRadius = mArea.width / 2;

        //mClientHandler.AddEvent(CEvent.Scene_Change, SceneChange);

        UIEventListener.Get(mZone.gameObject).onPress = OnPressTouch;
        UIEventListener.Get(mZone.gameObject).onDrag = OnDragTouch;
    }

    bool mHasSaveDefaultPos = false;
    IEnumerator SaveDefaultPos()
    {
        yield return null;
        mJoystickDefaultLocalPosition = mJoystick.localPosition;
        mHasSaveDefaultPos = true;
    }

    Vector3 mKeyOffset = Vector3.zero;
    Vector3 mLastKeyOffest = Vector3.zero;
    void Update()
    {
        if (Application.isEditor)
        {
            mKeyOffset.y = Input.GetKey(KeyCode.W) ? 51 : (Input.GetKey(KeyCode.S) ? -51 : 0);
            mKeyOffset.x = Input.GetKey(KeyCode.D) ? 51 : (Input.GetKey(KeyCode.A) ? -51 : 0);

            if (mKeyOffset.x != 0 || mKeyOffset.y != 0)
            {
                if (mLastKeyOffest.x == 0f && mLastKeyOffest.y == 0)
                {
                    if (FixJoystick)
                    {
                        OnPressTouch(true, (mKeyOffset + mJoystick.localPosition) / PixelSizeAdjustment);
                        OnDragTouch(null, Vector2.zero);
                    }
                    else
                    {
                        OnPressTouch(true, (new Vector3(Random.Range(-40f, 40f), Random.Range(-40f, 40f), 0f) + mJoystick.localPosition) / PixelSizeAdjustment);
                        OnDragTouch(null, mKeyOffset / PixelSizeAdjustment);
                    }
                }
                else if (mKeyOffset != mLastKeyOffest)
                {
                    OnDragTouch(null, (mKeyOffset - mLastKeyOffest) / PixelSizeAdjustment);
                }
            }
            else if (mLastKeyOffest.x != 0 || mLastKeyOffest.y != 0)
            {
                StopMove();
            }

            mLastKeyOffest = mKeyOffset;
        }

        Move(GetDirection(mTouch.localPosition));

    }

    private void OnPressTouch(GameObject go, bool state)
    {
        if (state)
        {
            OnPressTouch(state, ((Vector3)UICamera.currentTouch.pos - new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        }
        else
        {
            OnPressTouch(state);
        }
    }

    void OnPressTouch(bool state, Vector3 pos = default(Vector3))
    {
        if (!mHasSaveDefaultPos) return;
        if (state)
        {
            if (mFixJoystick)
            {
                mJoystick.localPosition = mJoystickDefaultLocalPosition;
                mMouseLocalPos = pos * PixelSizeAdjustment - mJoystick.localPosition;
                if (mMouseLocalPos.sqrMagnitude > 40000)
                {
                    return;
                }
                else if (mMouseLocalPos.sqrMagnitude > 1600)
                {
                    SetTouchPos(mMouseLocalPos);
                }
            }
            else
            {
                mJoystick.localPosition = pos * PixelSizeAdjustment;
                mMouseLocalPos = Vector3.zero;
            }
        }
        else
        {
            if (mFixJoystick == false) mJoystick.localPosition = mJoystickDefaultLocalPosition;

            mTouch.localPosition = Vector3.zero;
            Move(CSDirection.None);
        }

        ShowJoystic(state);

    }

    void ShowJoystic(bool value)
    {
        mJoystickWidget.alpha = value ? 1f : 0.4f;
    }

    private void OnDragTouch(GameObject go, Vector2 delta)
    {
        mMouseLocalPos += (Vector3)delta * PixelSizeAdjustment;
        SetTouchPos(mMouseLocalPos);
    }

    private void SetTouchPos(Vector3 mouseLocalPos)
    {
        if (mouseLocalPos.magnitude > mAreaRadius) mTouch.localPosition = mouseLocalPos.normalized * mAreaRadius;
        else mTouch.localPosition = mouseLocalPos;
    }

    public void Move(CSDirection direction)
    {
    }

    public static CSDirection GetDirection(Vector3 offsetPos)
    {
        if (DirThresholds == null || offsetPos == Vector3.zero || offsetPos.magnitude < 1) return CSDirection.None;

        float angle = Mathf.Atan2(offsetPos.y, offsetPos.x) * Mathf.Rad2Deg;
        float oldAngle = angle;
        angle -= DirOffset;
        if (angle < 0)
        {
            angle += 360f;
        }

        //Debug.Log(oldAngle + "," + angle);
        for (int i = 0; i < DirThresholds.Length; i++)
        {
            if (angle >= DirThresholds[i])
            {
                //Debug.Log((CSDirection)i);
                return (CSDirection)i;
            }
        }
        return CSDirection.None;
        //if (angle > -22.5f && angle <= 22.5f) return CSDirection.Right;
        //else if (angle > 22.5f && angle <= 67.5f) return CSDirection.Right_Up;
        //else if (angle > 67.5f && angle <= 112.5f) return CSDirection.Up;
        //else if (angle > 112.5f && angle <= 157.5f) return CSDirection.Left_Up;
        //else if ((angle < -157.5f && angle >= -180f) ||
        //         (angle > 157.5f && angle <= 180f)) return CSDirection.Left;
        //else if (angle < -112.5f && angle >= -157.5f) return CSDirection.Left_Down;
        //else if (angle < -67.5f && angle >= -112.5f) return CSDirection.Down;
        //else if (angle < -22.5f && angle >= -67.5f) return CSDirection.Right_Down;
        //else return CSDirection.None;
    }

    public void Move(Vector3 targetPos, Vector3 originalPos)
    {
        Move(GetMouseDirection(targetPos, originalPos));
    }

    public void StopMove()
    {
        OnPressTouch(false);
    }

    public static CSDirection GetMouseDirection(Vector3 target, Vector3 original)
    {
        return GetDirection(target - original);
    }

    void OnDestroy()
    {
      
    }
}
