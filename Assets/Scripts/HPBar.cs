using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image mask;
    public TextMeshProUGUI healthText;
    private float maxSize;
    
    // Start is called before the first frame update
    void Start()
    {
        maxSize = mask.rectTransform.rect.width;
    }

    public void SetValue(float curr, float max)
    {
        float percent = curr / max;
        Debug.Log(percent);
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxSize*percent);
        Debug.Log(mask.rectTransform.rect.width);
        healthText.text = curr.ToString() + "/" + max.ToString();
    }
}
