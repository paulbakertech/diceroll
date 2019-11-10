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

    public void UpdateScoreText(int score)
    {
        this.ScoreText.text = string.Format("SCORE: {0}", score);
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
        this.HighScoreText.text = string.Format("HIGH SCORE: {0}", score);
    }

    public void PlayAgainButtonClick()
    {
        GameManager.Instance.ResetGame();
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

}
