using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScoreInfo : MonoBehaviour
{
    public GameObject playerScoreObj;

    private Text _playerScoreText;
    // Start is called before the first frame update
    void Start()
    {
        _playerScoreText = playerScoreObj.GetComponent<Text>();
        _playerScoreText.text = "Score: 0";
    }

    public void UpdateScoreText(int score)
    {
        _playerScoreText.text = "Score: " + score;
    }
}
