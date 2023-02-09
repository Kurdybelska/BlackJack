using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoModal : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerPointsText;

    public void Start()
    {
        SetCanvasAsParent();
    }

    public void HideModal()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowModal()
    {
        this.gameObject.SetActive(true);
    }

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }

    public void SetPlayerPoints(int playerPoints)
    {
        playerPointsText.text = playerPoints.ToString();
    }

    private void SetCanvasAsParent()
    {
        this.gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }
}
