using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResultController : MonoBehaviour
{
    [SerializeField] Text rankText;
    [SerializeField] Text nameText;
    [SerializeField] Text eatenCountText;
    string[] ordinals = new string[] { "st", "nd", "rd" };

    RectTransform rectTransform;
    public void OnStart(float posY)
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, posY);
    }

    public void ShowParam(int rank, string name, int eatenCount)
    {
        string ordinal = (rank > ordinals.Length) ? "th" : ordinals[rank - 1];
        rankText.text = rank + ordinal;
        nameText.text = name;
        eatenCountText.text = "★ " + eatenCount;
    }
}
