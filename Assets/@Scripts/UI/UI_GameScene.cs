using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_GameScene : UI_Base
{
    [SerializeField]
    TextMeshProUGUI _killCountText;

    [SerializeField]
    Slider _gemSlider;

    public void SetGemCountRatio(float ratio)
    {
        _gemSlider.value = ratio;
    }

    public void SetKillCount(int KillCount)
    {
        _killCountText.text = $"{KillCount}";
    }
}
