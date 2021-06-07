using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    public enum Teams
    {
        White,
        Black
    }

    [SerializeField] Color whiteColor;
    [SerializeField] Color blackColor;
    [SerializeField] GameObject squareSprite;
    [SerializeField] GameObject squareContainer;
    [SerializeField] GameObject[] piecePrefabs = new GameObject[12];
    [SerializeField] GameObject pieceContainer;
    public Piece[,] pieces = new Piece[8, 8];
    [SerializeField] GameObject possibleMoveHighlight;
    [SerializeField] GameObject possibleMoveHighlightContainer;
    public GameObject[,] possibleMoveHighlights = new GameObject[8, 8];

    public Teams whosTurn;
    public bool canMove;

    void Start()
    {
        canMove = true;
        CreateSquares();
        CreatePieces();
        CreatePossibleMoveHighlights();
    }

    /// <summary>
    /// Creates square sprites just for visuals. 
    /// Square gameobject does not have any purpose other than visual
    /// </summary>
    void CreateSquares()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                pieces[x, y] = null;

                GameObject go = Instantiate(squareSprite);
                go.transform.position = new Vector3(x, y);
                if (((x + y) & 1) == 0)
                {
                    go.GetComponent<SpriteRenderer>().material.color = blackColor;
                }
                else
                {
                    go.GetComponent<SpriteRenderer>().material.color = whiteColor;
                }

                go.transform.SetParent(squareContainer.transform);
                go.name = $"Square({x}, {y})";
            }
        }
    }

    /// <summary>
    /// Creates pieces with Piece class and its sprite. Pieces are interactable and moveable objects.
    /// </summary>
    void CreatePieces()
    {
        GameObject go = null;
        //White
        for (int i = 0; i < 8; i++)
        {
            go = Instantiate(piecePrefabs[0], new Vector3(i, 1, 0), Quaternion.identity);
            go.transform.SetParent(pieceContainer.transform);
            pieces[i, 1] = go.GetComponent<Piece>();
        }

        go = Instantiate(piecePrefabs[1], new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[0, 0] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[2], new Vector3(1, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[1, 0] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[3], new Vector3(2, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[2, 0] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[4], new Vector3(3, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[3, 0] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[5], new Vector3(4, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[4, 0] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[3], new Vector3(5, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[5, 0] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[2], new Vector3(6, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[6, 0] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[1], new Vector3(7, 0, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[7, 0] = go.GetComponent<Piece>();


        //Black
        for (int i = 0; i < 8; i++)
        {
            go = Instantiate(piecePrefabs[6], new Vector3(i, 6, 0), Quaternion.identity);
            go.transform.SetParent(pieceContainer.transform);
            pieces[i, 6] = go.GetComponent<Piece>();
        }

        go = Instantiate(piecePrefabs[7], new Vector3(0, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[0, 7] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[8], new Vector3(1, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[1, 7] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[9], new Vector3(2, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[2, 7] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[10], new Vector3(3, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[3, 7] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[11], new Vector3(4, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[4, 7] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[9], new Vector3(5, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[5, 7] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[8], new Vector3(6, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[6, 7] = go.GetComponent<Piece>();

        go = Instantiate(piecePrefabs[7], new Vector3(7, 7, 0), Quaternion.identity);
        go.transform.SetParent(pieceContainer.transform);
        pieces[7, 7] = go.GetComponent<Piece>();
    }

    /// <summary>
    /// Creates possibleMoveHighligts for player to see where he/she can go. (Just for visual.)
    /// </summary>
    void CreatePossibleMoveHighlights()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject go = Instantiate(possibleMoveHighlight, new Vector2(x, y), Quaternion.identity);
                go.SetActive(false);
                go.transform.SetParent(possibleMoveHighlightContainer.transform);
                go.name = $"PossibleMoveHighlight({x}, {y})";
                possibleMoveHighlights[x, y] = go;
            }
        }
    }

    public void EnablePossibleMoveHighlights(PossibleMoves possibleMoves)
    {
        DisablePossibleMoveHighlights();
        foreach (var item in possibleMoves.MakeOneList())
        {
            possibleMoveHighlights[(int)item.x, (int)item.y].SetActive(true);
        }
    }

    public void DisablePossibleMoveHighlights()
    {
        foreach (var item in possibleMoveHighlights)
        {
            item.SetActive(false);
        }
    }

    /// <summary>
    /// Procceds to next turn by changing whosTurn
    /// </summary>
    public void NextTurn()
    {
        switch (whosTurn)
        {
            case Teams.White:
                whosTurn = Teams.Black;
                break;
            case Teams.Black:
                whosTurn = Teams.White;
                break;
        }
    }

    public void PromotePawn(string type)
    {
        Vector2 vec = default;
        Teams team = default;
        GameObject go = null;

        foreach (var piece in pieces)
        {
            if (piece && piece.type == PieceType.Pawn 
                && (piece.transform.position.y == 0 || piece.transform.position.y == 7))
            {
                vec = new Vector2(transform.position.x, transform.position.y);
                team = piece.team;
                break;
            }
        }

        if (team == Teams.White)
        {
            if (type == "Rook")
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[1], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
            else if (type == "Knight")
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[2], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
            else if (type == "Bishop")
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[3], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
            else
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[4], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
        }
        else
        {
            if (type == "Rook")
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[7], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
            else if (type == "Knight")
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[8], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
            else if (type == "Bishop")
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[9], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
            else
            {
                Destroy(pieces[(int)vec.x, (int)vec.y].gameObject);
                pieces[(int)vec.x, (int)vec.y] = null;
                go = Instantiate(piecePrefabs[10], new Vector3(vec.x, vec.y, 0), Quaternion.identity);
                go.transform.SetParent(pieceContainer.transform);
                pieces[(int)vec.x, (int)vec.y] = go.GetComponent<Piece>();
            }
        }

        canMove = true;
    }
}
