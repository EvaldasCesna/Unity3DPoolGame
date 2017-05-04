using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoringScript : MonoBehaviour {
    private int pOneScore;
    private int pTwoScore;
    private bool pOneSolids;
    private bool firstBall;
    private bool ballsStopped;
    private bool resetCueBall;

    public bool pOneTurn;
    public GameObject winScreen;
    public GameObject cueBall;
    public GameObject cue;
    public Text pOneText;
    public Text pTwoText;
    public Text pOneTurnText;
    public Text pTwoTurnText;
    public Text pOneBall;
    public Text pTwoBall;


    // Use this for initialization
    void Start () {
        pOneScore = 0;
        pTwoScore = 0;
        updateScoreText();
        firstBall = true;
        pOneTurn = false;
        changeTurn();
    }
	
	// Update is called once per frame
	void Update () {
		if (cue.GetComponent<CueBehaviour>().ballHit && GameObject.FindGameObjectWithTag("CueBall").GetComponent<Rigidbody>().velocity.magnitude > 0.001)
        {
            cue.GetComponent<CueBehaviour>().ballHit = false;
            cue.SetActive(false);

            Invoke("changeTurn", 6f);
  
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (!other.GetComponent<BallsBehaviour>().isSolid && !other.GetComponent<BallsBehaviour>().isStripe)
                eightBall();

            if (firstBall)
            {
                firstBallIn(other.GetComponent<BallsBehaviour>().isSolid, other.GetComponent<BallsBehaviour>().isStripe);
                firstBall = false;
            }
            else
            {
                scoreBalls(other.GetComponent<BallsBehaviour>().isSolid, other.GetComponent<BallsBehaviour>().isStripe);
            }

            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "CueBall")
        {
            resetCueBall = true;
            cueBall.GetComponent<BallBehaviour>().resetPos();
            cueBall.SetActive(false);
        }
    }

    public void changeTurn()
    {
        cue.SetActive(true);
        if (resetCueBall)
        {
            cueBall.SetActive(true);
        }


        if (pOneTurn == false)
        {
            pOneTurn = true;
            pOneTurnText.text = ">";
            pTwoTurnText.text = " ";
        }
        else
        {
            pOneTurn = false;
            pOneTurnText.text = " ";
            pTwoTurnText.text = ">";
        }
      
    }

    public void eightBall()
    {
        if (pOneTurn)
        {
            if (pOneScore == 7)
            {
                pOneScore++;
                updateScoreText();
                winScreen.GetComponentInChildren<Text>().text = "Player 1 Wins";
            }
            else
            {
                winScreen.GetComponentInChildren<Text>().text = "Player 1 Loss";
            }
        }
        else if(!pOneTurn)
        {
            if (pTwoScore == 7)
            {
                pTwoScore++;
                updateScoreText();
                winScreen.GetComponentInChildren<Text>().text = "Player 2 Wins";
            }
            else
            {
                winScreen.GetComponentInChildren<Text>().text = "Player 2 Loss";
            }
        }
        cue.SetActive(false);
        winScreen.SetActive(true);
    }

    public void scoreBalls(bool isSolid, bool isStripe)
    {
        if (pOneSolids)
        {
            if (isSolid)
            {
                pOneScore++;
            }
            if (isStripe)
            {
                pTwoScore++;
            }
            updateScoreText();
        }
        else
        {
            if (isStripe)
            {
                pOneScore++;
            }
            if (isSolid)
            {
                pTwoScore++;
            }
            updateScoreText();
        }
    }

    public void firstBallIn(bool isSolid, bool isStripe)
    {
        if (pOneTurn)
        {
            if (isSolid)
            {
                pOneSolids = true;
                pOneBall.text = "Solids";
                pTwoBall.text = "Stripes";
            }
            if (isStripe)
            {
                pOneSolids = false;
                pOneBall.text = "Stripes";
                pTwoBall.text = "Solids";
            }
            pOneScore = 1;
            updateScoreText();
        }
        else
        {
            if (isSolid)
            {
                pOneSolids = false;
                pTwoBall.text = "Solids";
                pOneBall.text = "Stripes";
            }
            if (isStripe)
            {
                pOneSolids = true;
                pTwoBall.text = "Stripes";
                pOneBall.text = "Solids";
            }
            pOneScore = 1;
            updateScoreText();
        }
    }

    public void updateScoreText()
    {
        pOneText.text = pOneScore.ToString();
        pTwoText.text = pTwoScore.ToString();
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
