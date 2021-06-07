using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public GameManager.PieceType type;
    public GameManager.Teams team;

    public int maxHP;
    public int hp;
    public int ap;

    bool movedBefore;

    public void Attack(Piece attackingPiece)
    {
        attackingPiece.hp = Mathf.Max(attackingPiece.hp - ap, 0);
    }

    public PossibleMoves CalculateMovement()
    {
        switch (type)
        {
            case GameManager.PieceType.Pawn:
                return CalculatePawnMovement();
            case GameManager.PieceType.Rook:
                return CalculateRookMovement();
            case GameManager.PieceType.Knight:
                return CalculateKnightMovement();
            case GameManager.PieceType.Bishop:
                return CalculateBishopMovement();
            case GameManager.PieceType.Queen:
                return CalculateQueenMovement();
            case GameManager.PieceType.King:
                return CalculateKingMovement();
            default:
                return default;
        }
    }

    PossibleMoves CalculatePawnMovement()
    {
        PossibleMoves tempList = new PossibleMoves();
        tempList.empty = new List<Vector2>();
        tempList.enemy = new List<Vector2>();
        tempList.castling = new List<Vector2>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        int tempIntX = 0;
        int tempIntY = 0;

        switch (team)
        {
            case GameManager.Teams.White:
                tempIntY = 1;
                break;
            case GameManager.Teams.Black:
                tempIntY = -1;
                break;
        }

        if (y + tempIntY < 8 && y + tempIntY >= 0
            && !GameManager.Instance.pieces[x, y + tempIntY])
        {
            tempList.empty.Add(new Vector2(x, y + tempIntY));
        }

        tempIntX = 1;
        if (y + tempIntY < 8 && y + tempIntY >= 0 && x + tempIntX < 8
            && GameManager.Instance.pieces[x + tempIntX, y + tempIntY]
            && GameManager.Instance.pieces[x + tempIntX, y + tempIntY].team != team)
        {
            tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
        }

        tempIntX = -1;
        if (y + tempIntY < 8 && y + tempIntY >= 0 && x + tempIntX >= 0
            && GameManager.Instance.pieces[x + tempIntX, y + tempIntY]
            && GameManager.Instance.pieces[x + tempIntX, y + tempIntY].team != team)
        {
            tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
        }

        if (!movedBefore && tempList.empty.Count > 0)
        {
            switch (team)
            {
                case GameManager.Teams.White:
                    tempIntY = 2;
                    break;
                case GameManager.Teams.Black:
                    tempIntY = -2;
                    break;
            }

            if (y + tempIntY < 8 && y + tempIntY >= 0
                && !GameManager.Instance.pieces[x, y + tempIntY])
            {
                tempList.empty.Add(new Vector2(x, y + tempIntY));
            }
        }

        return tempList;
    }

    PossibleMoves CalculateRookMovement()
    {
        PossibleMoves tempList = new PossibleMoves();
        tempList.empty = new List<Vector2>();
        tempList.enemy = new List<Vector2>();
        tempList.castling = new List<Vector2>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        int tempInt = 0;
        Piece tempPiece = null;

        tempInt = 1;
        while (x + tempInt < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempInt, y];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempInt, y));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempInt, y));
            tempInt++;
        }

        tempInt = 1;
        while (y + tempInt < 8)
        {
            tempPiece = GameManager.Instance.pieces[x, y + tempInt];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x, y + tempInt));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x, y + tempInt));
            tempInt++;
        }

        tempInt = -1;
        while (x + tempInt >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempInt, y];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempInt, y));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempInt, y));
            tempInt--;
        }

        tempInt = -1;
        while (y + tempInt >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x, y + tempInt];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x, y + tempInt));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x, y + tempInt));
            tempInt--;
        }

        return tempList;
    }

    PossibleMoves CalculateKnightMovement()
    {
        PossibleMoves tempList = new PossibleMoves();
        tempList.empty = new List<Vector2>();
        tempList.enemy = new List<Vector2>();
        tempList.castling = new List<Vector2>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        int tempIntX = 0;
        int tempIntY = 0;
        Piece tempPiece = null;

        tempIntX = -1;
        tempIntY = 2;
        if (x + tempIntX >= 0 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = -2;
        tempIntY = 1;
        if (x + tempIntX >= 0 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = -2;
        tempIntY = -1;
        if (x + tempIntX >= 0 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = -1;
        tempIntY = -2;
        if (x + tempIntX >= 0 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = 1;
        tempIntY = -2;
        if (x + tempIntX < 8 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = 2;
        tempIntY = -1;
        if (x + tempIntX < 8 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = 2;
        tempIntY = 1;
        if (x + tempIntX < 8 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = 1;
        tempIntY = 2;
        if (x + tempIntX < 8 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        return tempList;
    }

    PossibleMoves CalculateBishopMovement()
    {
        PossibleMoves tempList = new PossibleMoves();
        tempList.empty = new List<Vector2>();
        tempList.enemy = new List<Vector2>();
        tempList.castling = new List<Vector2>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        int tempIntX = 0;
        int tempIntY = 0;
        Piece tempPiece;

        tempIntX = 1;
        tempIntY = 1;
        while (x + tempIntX < 8 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX++;
            tempIntY++;
        }

        tempIntX = -1;
        tempIntY = 1;
        while (x + tempIntX >= 0 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX--;
            tempIntY++;
        }

        tempIntX = -1;
        tempIntY = -1;
        while (x + tempIntX >= 0 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX--;
            tempIntY--;
        }

        tempIntX = 1;
        tempIntY = -1;
        while (x + tempIntX < 8 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX++;
            tempIntY--;
        }

        return tempList;
    }

    PossibleMoves CalculateQueenMovement()
    {
        PossibleMoves tempList = new PossibleMoves();
        tempList.empty = new List<Vector2>();
        tempList.enemy = new List<Vector2>();
        tempList.castling = new List<Vector2>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        int tempInt = 0;
        int tempIntX = 0; 
        int tempIntY = 0; 
        Piece tempPiece = null;

        tempInt = 1;
        while (x + tempInt < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempInt, y];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempInt, y));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempInt, y));
            tempInt++;
        }

        tempInt = 1;
        while (y + tempInt < 8)
        {
            tempPiece = GameManager.Instance.pieces[x, y + tempInt];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x, y + tempInt));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x, y + tempInt));
            tempInt++;
        }

        tempInt = -1;
        while (x + tempInt >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempInt, y];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempInt, y));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempInt, y));
            tempInt--;
        }

        tempInt = -1;
        while (y + tempInt >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x, y + tempInt];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x, y + tempInt));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x, y + tempInt));
            tempInt--;
        }

        tempIntX = 1;
        tempIntY = 1;
        while (x + tempIntX < 8 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX++;
            tempIntY++;
        }

        tempIntX = -1;
        tempIntY = 1;
        while (x + tempIntX >= 0 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX--;
            tempIntY++;
        }

        tempIntX = -1;
        tempIntY = -1;
        while (x + tempIntX >= 0 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX--;
            tempIntY--;
        }

        tempIntX = 1;
        tempIntY = -1;
        while (x + tempIntX < 8 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (tempPiece)
            {
                if (tempPiece.team != team)
                {
                    tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
                }
                break;
            }
            tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            tempIntX++;
            tempIntY--;
        }

        return tempList;
    }

    PossibleMoves CalculateKingMovement()
    {
        PossibleMoves tempList = new PossibleMoves();
        tempList.empty = new List<Vector2>();
        tempList.enemy = new List<Vector2>();
        tempList.castling = new List<Vector2>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        int tempIntX = 0;
        int tempIntY = 0;
        Piece tempPiece = null;

        tempIntX = 0;
        tempIntY = 1;
        if (y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x, y + tempIntY));
            }
        }

        tempIntX = -1;
        tempIntY = 1;
        if (x + tempIntX >= 0 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = -1;
        tempIntY = 0;
        if (x + tempIntX >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y));
            }
        }

        tempIntX = -1;
        tempIntY = -1;
        if (x + tempIntX >= 0 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = 0;
        tempIntY = -1;
        if (y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x, y + tempIntY));
            }
        }

        tempIntX = 1;
        tempIntY = -1;
        if (x + tempIntX < 8 && y + tempIntY >= 0)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }

        tempIntX = 1;
        tempIntY = 0;
        if (x + tempIntX < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y));
            }
        }

        tempIntX = 1;
        tempIntY = 1;
        if (x + tempIntX < 8 && y + tempIntY < 8)
        {
            tempPiece = GameManager.Instance.pieces[x + tempIntX, y + tempIntY];
            if (!tempPiece)
            {
                tempList.empty.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
            else if (tempPiece.team != team)
            {
                tempList.enemy.Add(new Vector2(x + tempIntX, y + tempIntY));
            }
        }


        if (!movedBefore)
        {
            switch (team)
            {
                case GameManager.Teams.White:
                    if (!GameManager.Instance.pieces[3, 0]
                        && !GameManager.Instance.pieces[2, 0]
                        && !GameManager.Instance.pieces[1, 0]
                        && GameManager.Instance.pieces[0, 0]
                        && GameManager.Instance.pieces[0, 0].team == GameManager.Teams.White
                        && GameManager.Instance.pieces[0, 0].type == GameManager.PieceType.Rook
                        && !GameManager.Instance.pieces[0, 0].movedBefore)
                    {
                        tempList.castling.Add(new Vector2(2, 0));
                    }

                    if (!GameManager.Instance.pieces[5, 0]
                        && !GameManager.Instance.pieces[6, 0]
                        && GameManager.Instance.pieces[7, 0]
                        && GameManager.Instance.pieces[7, 0].team == GameManager.Teams.White
                        && GameManager.Instance.pieces[7, 0].type == GameManager.PieceType.Rook
                        && !GameManager.Instance.pieces[7, 0].movedBefore)
                    {
                        tempList.castling.Add(new Vector2(6, 0));
                    }
                    break;
                case GameManager.Teams.Black:
                    if (!GameManager.Instance.pieces[3, 7]
                         && !GameManager.Instance.pieces[2, 7]
                         && !GameManager.Instance.pieces[1, 7]
                         && GameManager.Instance.pieces[0, 7]
                         && GameManager.Instance.pieces[0, 7].team == GameManager.Teams.Black
                         && GameManager.Instance.pieces[0, 7].type == GameManager.PieceType.Rook
                         && !GameManager.Instance.pieces[0, 7].movedBefore)
                    {
                        tempList.castling.Add(new Vector2(2, 7));
                    }

                    if (!GameManager.Instance.pieces[5, 7]
                        && !GameManager.Instance.pieces[6, 7]
                        && GameManager.Instance.pieces[7, 7]
                        && GameManager.Instance.pieces[7, 7].team == GameManager.Teams.Black
                        && GameManager.Instance.pieces[7, 7].type == GameManager.PieceType.Rook
                        && !GameManager.Instance.pieces[7, 7].movedBefore)
                    {
                        tempList.castling.Add(new Vector2(6, 7));
                    }
                    break;
            }
        }

        return tempList;
    }

    public void GoTo(Vector2 vecGone)
    {
        GameManager.Instance.canMove = false;
        switch (type)
        {
            case GameManager.PieceType.Pawn:
                StartCoroutine(PawnGoTo(vecGone));
                break;
            case GameManager.PieceType.Rook:
                StartCoroutine(RookGoTo(vecGone));
                break;
            case GameManager.PieceType.Knight:
                StartCoroutine(KnightGoTo(vecGone));
                break;
            case GameManager.PieceType.Bishop:
                StartCoroutine(BishopGoTo(vecGone));
                break;
            case GameManager.PieceType.Queen:
                StartCoroutine(QueenGoTo(vecGone));
                break;
            case GameManager.PieceType.King:
                StartCoroutine(KingGoTo(vecGone));
                break;
        }
    }

    IEnumerator PawnGoTo(Vector2 vecGone)
    {
        Vector2 startingVec = new Vector2((int)transform.position.x, (int)transform.position.y);

        Piece pieceGone = GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y];

        if (pieceGone) //Goes to square with piece 
        {
            Attack(pieceGone);

            if (pieceGone.hp > 0) //Goes to square with piece and not killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;

                float t = 0;
                while (t <= 1)
                {
                    t += 0.1f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(wantedPos, startingPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, false);
            }
            else //Goes to square with piece and killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;
                float t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
                Destroy(pieceGone.gameObject);
                GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, true);
            }
        }
        else //Goes to empty square
        {
            Vector2 startingPos = transform.position;
            Vector2 wantedPos = vecGone;
            float t = 0;
            while (t <= 1)
            {
                t += 0.05f;
                transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
            GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
            GameManager.Instance.NextTurn();
            UIManager.Instance.AddLog(team, type, vecGone, true, false);
        }

        if ((transform.position.y == 0 || transform.position.y == 7))
        {
            UIManager.Instance.ShowPromotion(team);
            Destroy(this.gameObject);
            yield break;
        }

        GameManager.Instance.canMove = true;
        if (!movedBefore)
            movedBefore = true;
    }

    IEnumerator RookGoTo(Vector2 vecGone)
    {
        Vector2 startingVec = new Vector2((int)transform.position.x, (int)transform.position.y);

        Piece pieceGone = GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y];

        if (pieceGone) //Goes to square with piece 
        {
            Attack(pieceGone);

            if (pieceGone.hp > 0) //Goes to square with piece and not killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;

                float t = 0;
                while (t <= 1)
                {
                    t += 0.1f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(wantedPos, startingPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, false);
            }
            else //Goes to square with piece and killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;
                float t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
                Destroy(pieceGone.gameObject);
                GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, true);
            }
        }
        else //Goes to empty square
        {
            Vector2 startingPos = transform.position;
            Vector2 wantedPos = vecGone;
            float t = 0;
            while (t <= 1)
            {
                t += 0.05f;
                transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
            GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
            GameManager.Instance.NextTurn();
            UIManager.Instance.AddLog(team, type, vecGone, true, false);
        }

        GameManager.Instance.canMove = true;
        if (!movedBefore)
            movedBefore = true;
    }
    
    IEnumerator KnightGoTo(Vector2 vecGone)
    {
        Vector2 startingVec = new Vector2((int)transform.position.x, (int)transform.position.y);

        Piece pieceGone = GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y];

        if (pieceGone) //Goes to square with piece 
        {
            Attack(pieceGone);

            if (pieceGone.hp > 0) //Goes to square with piece and not killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;

                float t = 0;
                while (t <= 1)
                {
                    t += 0.1f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(wantedPos, startingPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, false);
            }
            else //Goes to square with piece and killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;
                float t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
                Destroy(pieceGone.gameObject);
                GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, true);
            }
        }
        else //Goes to empty square
        {
            Vector2 startingPos = transform.position;
            Vector2 wantedPos = vecGone;
            float t = 0;
            while (t <= 1)
            {
                t += 0.05f;
                transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
            GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
            GameManager.Instance.NextTurn();
            UIManager.Instance.AddLog(team, type, vecGone, true, false);
        }

        GameManager.Instance.canMove = true;
        if (!movedBefore)
            movedBefore = true;
    }
    
    IEnumerator BishopGoTo(Vector2 vecGone)
    {
        Vector2 startingVec = new Vector2((int)transform.position.x, (int)transform.position.y);

        Piece pieceGone = GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y];

        if (pieceGone) //Goes to square with piece 
        {
            Attack(pieceGone);

            if (pieceGone.hp > 0) //Goes to square with piece and not killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;

                float t = 0;
                while (t <= 1)
                {
                    t += 0.1f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(wantedPos, startingPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, false);
            }
            else //Goes to square with piece and killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;
                float t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
                Destroy(pieceGone.gameObject);
                GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, true);
            }
        }
        else //Goes to empty square
        {
            Vector2 startingPos = transform.position;
            Vector2 wantedPos = vecGone;
            float t = 0;
            while (t <= 1)
            {
                t += 0.05f;
                transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
            GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
            GameManager.Instance.NextTurn();
            UIManager.Instance.AddLog(team, type, vecGone, true, false);
        }

        GameManager.Instance.canMove = true;
        if (!movedBefore)
            movedBefore = true;
    }
    
    IEnumerator QueenGoTo(Vector2 vecGone)
    {
        Vector2 startingVec = new Vector2((int)transform.position.x, (int)transform.position.y);

        Piece pieceGone = GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y];

        if (pieceGone) //Goes to square with piece 
        {
            Attack(pieceGone);

            if (pieceGone.hp > 0) //Goes to square with piece and not killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;

                float t = 0;
                while (t <= 1)
                {
                    t += 0.1f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(wantedPos, startingPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, false);
            }
            else //Goes to square with piece and killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;
                float t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
                Destroy(pieceGone.gameObject);
                GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, true);
            }
        }
        else //Goes to empty square
        {
            Vector2 startingPos = transform.position;
            Vector2 wantedPos = vecGone;
            float t = 0;
            while (t <= 1)
            {
                t += 0.05f;
                transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
            GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
            GameManager.Instance.NextTurn();
            UIManager.Instance.AddLog(team, type, vecGone, true, false);
        }

        GameManager.Instance.canMove = true;
        if (!movedBefore)
            movedBefore = true;
    }
    
    IEnumerator KingGoTo(Vector2 vecGone)
    {
        Vector2 startingVec = new Vector2((int)transform.position.x, (int)transform.position.y);

        Piece pieceGone = GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y];

        if (CalculateMovement().castling.Contains(vecGone)) //Makes castling
        {
            Vector2 startingPos = transform.position;
            Vector2 wantedPos = vecGone;
            float t = 0;
            while (t <= 1)
            {
                t += 0.05f;
                transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
            GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;

            switch (team)
            {
                case GameManager.Teams.White:
                    if (vecGone.x == 2)
                    {
                        StartCoroutine(GameManager.Instance.pieces[0, 0].GoToForCastling(new Vector2(3, 0)));
                    }
                    else
                    {
                        StartCoroutine(GameManager.Instance.pieces[7, 0].GoToForCastling(new Vector2(5, 0)));
                    }
                    break;
                case GameManager.Teams.Black:
                    if (vecGone.x == 2)
                    {
                        StartCoroutine(GameManager.Instance.pieces[0, 7].GoToForCastling(new Vector2(3, 7)));
                    }
                    else
                    {
                        StartCoroutine(GameManager.Instance.pieces[7, 0].GoToForCastling(new Vector2(3, 7)));
                    }
                    break;
            }

            GameManager.Instance.NextTurn();
            UIManager.Instance.AddLog(team, type, vecGone, false, false);
        }
        else if (pieceGone) //Goes to square with piece 
        {
            Attack(pieceGone);

            if (pieceGone.hp > 0) //Goes to square with piece and not killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;

                float t = 0;
                while (t <= 1)
                {
                    t += 0.1f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(wantedPos, startingPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, false);
            }
            else //Goes to square with piece and killed
            {
                Vector3 startingPos = transform.position;
                Vector3 wantedPos = pieceGone.transform.position;
                float t = 0;
                while (t <= 1)
                {
                    t += 0.05f;
                    transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                    yield return new WaitForSeconds(0.01f);
                }

                GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
                Destroy(pieceGone.gameObject);
                GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
                GameManager.Instance.NextTurn();
                UIManager.Instance.AddLog(team, type, vecGone, false, true);
            }
        }
        else //Goes to empty square
        {
            Vector2 startingPos = transform.position;
            Vector2 wantedPos = vecGone;
            float t = 0;
            while (t <= 1)
            {
                t += 0.05f;
                transform.position = Vector3.Lerp(startingPos, wantedPos, t);
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
            GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
            GameManager.Instance.NextTurn();
            UIManager.Instance.AddLog(team, type, vecGone, true, false);
        }

        GameManager.Instance.canMove = true;
        if (!movedBefore)
            movedBefore = true;
    }

    IEnumerator GoToForCastling(Vector2 vecGone)
    {
        Vector2 startingVec = new Vector2((int)transform.position.x, (int)transform.position.y);

        Vector2 startingPos = transform.position;
        Vector2 wantedPos = vecGone;
        float t = 0;
        while (t <= 1)
        {
            t += 0.05f;
            transform.position = Vector3.Lerp(startingPos, wantedPos, t);
            yield return new WaitForSeconds(0.01f);
        }

        GameManager.Instance.pieces[(int)startingVec.x, (int)startingVec.y] = null;
        GameManager.Instance.pieces[(int)vecGone.x, (int)vecGone.y] = this;
    }
}

public struct PossibleMoves
{
    public List<Vector2> empty { get; set; }
    public List<Vector2> enemy { get; set; }
    public List<Vector2> castling { get; set; }

    public List<Vector2> MakeOneList()
    {
        List<Vector2> tempList = new List<Vector2>();

        foreach (var item in empty)
        {
            tempList.Add(item);
        }        
        foreach (var item in enemy)
        {
            tempList.Add(item);
        }        
        foreach (var item in castling)
        {
            tempList.Add(item);
        }

        return tempList;
    }
}
