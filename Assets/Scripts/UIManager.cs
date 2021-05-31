using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] Log whiteLog;
    [SerializeField] Log blackLog;

    public void AddLog(GameManager.Teams team, GameManager.PieceType type, Vector2 vec, bool isEmpty, bool isKilled)
    {
        switch (team)
        {
            case GameManager.Teams.White:
                whiteLog.AddLog(type, vec, isEmpty, isKilled);
                break;
            case GameManager.Teams.Black:
                blackLog.AddLog(type, vec, isEmpty, isKilled);
                break;
        }
    }
}
