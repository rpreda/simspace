using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField]
    Transform textScoreObj;

    [SerializeField]
    int scoreRate = 1;

    int currentScore;
    int startScore;
    int second = 1;
    float accTime = 0;

	// Use this for initialization
	void Start () {
        currentScore = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (moveScript.playerDead)
            return;
        accTime += Time.deltaTime;

        if (accTime > second)
        {
            accTime = 0;
            currentScore += scoreRate;
            textScoreObj.GetComponent<Text>().text = "SCORE    " + currentScore;
        }
	}
}
