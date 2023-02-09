using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoosePlayersNumberModal : MonoBehaviour
{
    public MenuManager menuManager;
    public Slider slider;
    public TextMeshProUGUI playersNumberText;

    void Start()
    {
        UpdatePlayersNumber();
    }

    public void ShowModal()
    {
        this.gameObject.SetActive(true);        
    }

    public void HideModal()
    {
        this.gameObject.SetActive(false);        
    }

    public void OnNextButtonClick()
    {
        menuManager.ReceiveEvent(MenuEventType.ShowEnterPlayerNameModal);
        HideModal();
    }

    public void UpdatePlayersNumber()
    {
        float sliderValue = slider.value;
        playersNumberText.text = sliderValue.ToString();
        GlobalGameContext.playersNum = (sbyte)sliderValue;
    }
}
