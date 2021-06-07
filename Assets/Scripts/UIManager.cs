using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Constant Things")]
    [SerializeField] string[] xToLetter = new string[8];
    [SerializeField] Sprite[] sprites = new Sprite[12];

    [Header("White Log")]
    [SerializeField] GameObject whiteLog;
    bool isWhiteLogOpen;
    int whiteLogAdded;
    [SerializeField] TextMeshProUGUI whiteTextMeshPro;
    [SerializeField] RectTransform whiteRectTransform;
    [SerializeField] GameObject blackLog;

    [Header("Black Log")]
    bool isBlackLogOpen;
    int blackLogAdded;
    [SerializeField] TextMeshProUGUI blackTextMeshPro;
    [SerializeField] RectTransform blackRectTransform;

    [Header("Piece Info")]
    [SerializeField] Image pieceImage;
    [SerializeField] Image realHealth;
    [SerializeField] Image healthBackground;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI attackPoint;

    [Header("Promotion")]
    [SerializeField] GameObject whitePromotion;
    [SerializeField] GameObject blackPromotion;


    public override void Init()
    {
        isWhiteLogOpen = true;
        isBlackLogOpen = true;
    }

    /// <summary>
    /// Adds log history to given values
    /// </summary>
    /// <param name="team">Team which wants to added</param>
    /// <param name="type">Type which has moved</param>
    /// <param name="vec">Vec where piece has moved</param>
    /// <param name="isEmpty">Has it gone to empty square?</param>
    /// <param name="isKilled">If not it has gone to empty square, Has it killed?</param>
    public void AddLog(GameManager.Teams team, GameManager.PieceType type, Vector2 vec, bool isEmpty, bool isKilled)
    {
        TextMeshProUGUI textMeshPro = null;
        int logAdded = 0;
        RectTransform rectTransform = null;

        switch (team)
        {
            case GameManager.Teams.White:
                textMeshPro = whiteTextMeshPro;
                whiteLogAdded++;
                logAdded = whiteLogAdded;
                rectTransform = whiteRectTransform;
                break;
            case GameManager.Teams.Black:
                textMeshPro = blackTextMeshPro;
                blackLogAdded++;
                logAdded = blackLogAdded;
                rectTransform = blackRectTransform;
                break;
        }

        switch (type)
        {
            case GameManager.PieceType.Pawn:
                if (isEmpty)
                {
                    textMeshPro.text += $"\n{logAdded}) {xToLetter[(int)vec.x]}{vec.y + 1}";
                }
                else if (isKilled)
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

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Mathf.Max(logAdded, 6) * 25);
        rectTransform.position = new Vector3(rectTransform.position.x, Mathf.Max(0, logAdded - 6) * 25, 0);
    }

    public void OpenCloseLog(string teamName)
    {
        if (teamName == "White")
        {       
            if (isWhiteLogOpen)
            {
                whiteLog.SetActive(false);
                isWhiteLogOpen = false;
            }
            else
            {
                whiteLog.SetActive(true);
                isWhiteLogOpen = true;
            }
        }
        else
        {
            if (isBlackLogOpen)
            {
                blackLog.SetActive(false);
                isBlackLogOpen = false;
            }
            else
            {
                blackLog.SetActive(true);
                isBlackLogOpen = true;
            }
        }
    }

    public void ChangePieceInfo(GameManager.PieceType type, GameManager.Teams team, int maxHP, int hp, int ap)
    {
        pieceImage.enabled = true;
        if (team == GameManager.Teams.White)
            pieceImage.sprite = sprites[type.GetHashCode()];
        else
            pieceImage.sprite = sprites[type.GetHashCode() + 6];
        realHealth.enabled = true;
        realHealth.fillAmount = (float)hp / (float)maxHP;
        healthBackground.enabled = true;
        healthText.enabled = true;
        healthText.text = $"{hp}/{maxHP}";
        attackPoint.enabled = true;
        attackPoint.text = ap.ToString();
    }

    public void ChangePieceInfo()
    {
        pieceImage.enabled = false;
        realHealth.enabled = false;
        healthBackground.enabled = false;
        healthText.enabled = false;
        attackPoint.enabled = false;
    }

    public void ShowPromotion(GameManager.Teams team)
    {
        if (team == GameManager.Teams.White)
            whitePromotion.SetActive(true);
        else
            blackPromotion.SetActive(true);
    }

    public void ShowPromotion()
    {
        whitePromotion.SetActive(false);
        blackPromotion.SetActive(false);
    }
}
