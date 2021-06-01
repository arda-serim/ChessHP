using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Log : MonoBehaviour
{
    public string[] xToLetter = new string[8];

    int logAdded;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] RectTransform rectTransform;

    public void AddLog(GameManager.PieceType type, Vector2 vec, bool isEmpty, bool isKilled)
    {
        logAdded++;
        switch (type)
        {
            case GameManager.PieceType.Pawn:
                if (isEmpty)
                {
                    textMeshPro.text += $"\n{logAdded}) {xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else if(isKilled)
                {
                    textMeshPro.text += $"\n{logAdded}) x{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else
                {
                    textMeshPro.text += $"\n{logAdded}) -{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                break;
            case GameManager.PieceType.Rook:
                if (isEmpty)
                {
                    textMeshPro.text += $"\n{logAdded}) R{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else if (isKilled)
                {
                    textMeshPro.text += $"\n{logAdded}) Rx{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else
                {
                    textMeshPro.text += $"\n{logAdded}) R-{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                break;
            case GameManager.PieceType.Knight:
                if (isEmpty)
                {
                    textMeshPro.text += $"\n{logAdded}) N{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else if (isKilled)
                {
                    textMeshPro.text += $"\n{logAdded}) Nx{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else
                {
                    textMeshPro.text += $"\n{logAdded}) N-{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                break;
            case GameManager.PieceType.Bishop:
                if (isEmpty)
                {
                    textMeshPro.text += $"\n{logAdded}) B{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else if (isKilled)
                {
                    textMeshPro.text += $"\n{logAdded}) Bx{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else
                {
                    textMeshPro.text += $"\n{logAdded}) B-{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                break;
            case GameManager.PieceType.Queen:
                if (isEmpty)
                {
                    textMeshPro.text += $"\n{logAdded}) Q{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else if (isKilled)
                {
                    textMeshPro.text += $"\n{logAdded}) Qx{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else
                {
                    textMeshPro.text += $"\n{logAdded}) Q-{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                break;
            case GameManager.PieceType.King:
                if (isEmpty)
                {
                    textMeshPro.text += $"\n{logAdded}) K{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else if (isKilled)
                {
                    textMeshPro.text += $"\n{logAdded}) Kx{xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else
                {
                    if (vec.x == 2)
                    {
                        textMeshPro.text += $"\n{logAdded}) O-O-O";
                    }
                    else
                    {
                        textMeshPro.text += $"\n{logAdded}) O-O";
                    }
                }
                break;
        }

        if (logAdded == 1)
        {
            textMeshPro.text = textMeshPro.text.TrimStart();
        }
        ChangeRectTransform();
    }

    void ChangeRectTransform()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Mathf.Max(logAdded, 6) * 25);
        rectTransform.position = new Vector3(rectTransform.position.x, Mathf.Max(0, logAdded - 6) * 25, 0);
    }
}
