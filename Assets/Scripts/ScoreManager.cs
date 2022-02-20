using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;

    public UpdateScoreInfo scInfo;
    // Start is called before the first frame update
    void Start()
    {
        scInfo = GameObject.FindObjectOfType<UpdateScoreInfo>();
    }

    public void UpdateScore()
    {
        playerScore += 1;
        scInfo.UpdateScoreText(playerScore);
    }
}
