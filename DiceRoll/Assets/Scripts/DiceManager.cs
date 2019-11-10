using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public DieController[] Dice;

    private bool WaitingForNewScore;

    private void Start()
    {
        this.WaitingForNewScore = false;
    }

    private void Update()
    {
        if (this.WaitingForNewScore)
        {
            if (this.CheckIfAllStopped())
            {
                this.WaitingForNewScore = false;
                GameManager.Instance.AddRollToScore(this.GetTotalScore());
            }
        }
    }

    private bool CheckIfAllStopped()
    {
        bool allStopped = true;
        foreach (var die in this.Dice)
        {
            if (die.DieStatus != DieStatus.Stopped)
            {
                allStopped = false;
                break;
            }
        }
        return allStopped;
    }

    public bool Roll()
    {
        bool diceRolled = false;
        if (!this.WaitingForNewScore)
        {
            this.Reset();
            this.WaitingForNewScore = true;
            foreach (var die in this.Dice)
            {
                die.Roll();
            }
            diceRolled = true;
        }
        return diceRolled;
    }

    public void Reset()
    {
        foreach (var die in this.Dice)
        {
            die.Reset();
        }
        this.WaitingForNewScore = false;
    }

    public int GetTotalScore()
    {
        int total = 0;
        foreach (var die in this.Dice)
        {
            total += die.CurrentSide;
        }
        return total;
    }
}
