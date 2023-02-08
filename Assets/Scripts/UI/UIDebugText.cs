using UnityEngine;
using UnityEngine.UI;

public class UIDebugText : MonoBehaviour
{
    [SerializeField] Text _debugText;

    public void SetTargetName(string name)
    {
        _debugText.text = name;
    }
}
