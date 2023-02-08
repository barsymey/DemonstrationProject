using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public static InterfaceController instance;

    [SerializeField] AimInfo _aimInfo;
    [SerializeField] UIDebugText _uIDebugText;

    void Start()
    {
        instance = this;
    }

    public static void SetAimTargetName(string name)
    {
        instance._aimInfo.SetTargetName(name);
    }

    public static void SetDebugText(string name)
    {
        instance._uIDebugText.SetTargetName(name);
    }
}
