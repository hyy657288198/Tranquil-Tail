using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHPbar : MonoBehaviour
{
    public Slider slider;

    public void updateEnemyHP(float curr, float max)
    {
        Debug.Log("enemy hp");
        slider.value = curr / max;
    }
}
