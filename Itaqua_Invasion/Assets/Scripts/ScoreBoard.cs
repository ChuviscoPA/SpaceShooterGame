using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        ScoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore(int AmountIncrease)
    {
        score += AmountIncrease;
        ScoreText.text = score.ToString();
        
    }

}
