using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICurrencyButton : MonoBehaviour
{
    public Button button;
    public Text text; //TMPro.TextMeshProUGUI 

    public void Set(string textStr)
    {
        text.text = textStr;
    }

    public void SetListener(System.Action onClick)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (onClick != null)
            {
                onClick.Invoke();
            }
        });
    }
}
