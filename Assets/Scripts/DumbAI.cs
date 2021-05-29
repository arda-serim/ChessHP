using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbAI : MonoBehaviour
{
    [SerializeField] GameManager.Teams team;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.whosTurn == team && GameManager.Instance.canMove)
        {
            GetPossibleMoves();
        }   
    }

    void GetPossibleMoves()
    {
        List<Vector2> tempList = new List<Vector2>();
        List<Piece> tempPieces = new List<Piece>();

        foreach (var item in GameManager.Instance.pieces)
        {
            if (item && item.team == team)
            {
                foreach (var vec in item.CalculateMovement().MakeOneList())
                {
                    tempList.Add(vec);
                    tempPieces.Add(item);
                }
            }
        }

        int rnd = Random.Range(0, tempList.Count);

        tempPieces[rnd].GoTo(tempList[rnd]);
    }
}
