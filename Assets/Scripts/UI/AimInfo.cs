using UnityEngine;
using UnityEngine.UI;

public class AimInfo : MonoBehaviour
{
    [SerializeField] Text _targetNameText;

    public void SetTargetName(string name)
    {
        _targetNameText.text = name;
    }
}
