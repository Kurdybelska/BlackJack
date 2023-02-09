using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoBox : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerPointsText;

    void Start()
    {
        SetPlayerInfosAsParent();    
    }

    public void Init(string playerName, int playerPoints)
    {
        playerNameText.text = playerName + ":";
        playerPointsText.text = playerPoints.ToString();
    }

    private void SetPlayerInfosAsParent()
    {
        this.gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("PlayersInfos").transform, false);
    }
}
