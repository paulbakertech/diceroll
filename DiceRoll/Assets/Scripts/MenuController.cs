using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject StartScreenPanel;
    public GameObject GamePlayPanel;
    public GameObject GameOverPanel;

    public Text ScoreText;
    public Text RollCountText;

    public Text FinalScoreText;
    public Text HighScoreText;
    public Text StartHighScoreText;

    public void HideAllPanels()
    {
        this.StartScreenPanel.SetActive(false);
        this.GamePlayPanel.SetActive(false);
        this.GameOverPanel.SetActive(false);
    }

    public void ShowStartScreenPanel()
    {
        this.HideAllPanels();
        this.StartScreenPanel.SetActive(true);
        this.StartScreenPanel.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ShowGamePlayPanel()
    {
        this.HideAllPanels();
        this.GamePlayPanel.SetActive(true);
        this.GamePlayPanel.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void ShowGameOverPanel()
    {
        this.HideAllPanels();
        this.GameOverPanel.SetActive(true);
        this.GameOverPanel.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void PlayGameButtonClick()
    {
        this.ShowGamePlayPanel();
    }

    public void UpdateScoreText(int oldScore, int newScore)
    {
        if (oldScore == newScore)
        {
            this.ScoreText.text = string.Format("SCORE: {0}", newScore);
        }
        else
        {
            StartCoroutine(this.IncrementScore(oldScore, newScore));
        }
        StartCoroutine(this.ShakeScoreText());
    }

    public void UpdateRollCount(int rollCount)
    {
        this.RollCountText.text = string.Format("ROLLS: {0}", rollCount);
    }

    public void UpdateFinalScoreText(int score)
    {
        this.FinalScoreText.text = string.Format("FINAL SCORE: {0}", score);
    }

    public void UpdateHighScoreText(int score)
    {
        if (score == 0)
        {
            this.HighScoreText.text = string.Empty;
        }
        else
        {
            this.HighScoreText.text = string.Format("NEW HIGH SCORE: {0}!", score);
        }
    }

    public void PlayAgainButtonClick()
    {
        GameManager.Instance.ResetGame();
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void QuitButtonClick()
    {
        GameManager.Instance.ResetGame();
    }

    public void UpdateStartHighScoreText(int score)
    {
        this.StartHighScoreText.text = string.Format("HIGH SCORE: {0}", score);
    }

    // Let's shake the score text to make things more interesting.
    private IEnumerator ShakeScoreText()
    {
        RectTransform textPos = this.ScoreText.GetComponent<RectTransform>();
        Vector3 initialPosition = textPos.anchoredPosition;
        float shakeTime = 2.5f;
        float shakeMagnitude = 7.5f;
        while (shakeTime >= 0.0f)
        {
            Vector3 shakePosition = new Vector3(
                initialPosition.x + Random.Range(0, shakeMagnitude),
                initialPosition.y + Random.Range(0, shakeMagnitude),
                initialPosition.z);
            textPos.anchoredPosition = shakePosition;
            shakeTime -= 0.1f;
            yield return null;
        }
        textPos.anchoredPosition = initialPosition;
    }

    // Add a delay when incrementing the score.
    private IEnumerator IncrementScore(int oldScore, int newScore)
    {
        while (oldScore < newScore)
        {
            oldScore++;
            this.ScoreText.text = string.Format("SCORE: {0}", oldScore);
            yield return new WaitForSeconds(0.05f);
        }
    }

}
