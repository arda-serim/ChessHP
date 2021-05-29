using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCoopPlayer : MonoBehaviour
{
    Piece selectedPiece;
    Piece mouseOnPiece;
    Vector2 mouseOnVec;
    [SerializeField]GameObject highlight;

    void Update()
    {
        GetPiece();

        if (Input.GetKeyDown(KeyCode.Mouse0) && mouseOnPiece 
            && mouseOnPiece.team == GameManager.Instance.whosTurn)
        {
            SelectPiece();
        }
        else if (selectedPiece && selectedPiece.CalculateMovement().MakeOneList().Contains(mouseOnVec)
            && Input.GetKeyDown(KeyCode.Mouse0))
        {
            selectedPiece.GoTo(mouseOnVec);
            GameManager.Instance.DisablePossibleMoveHighlights();
            selectedPiece = null;
        }
    }

    void GetPiece()
    {
        Vector3 tempVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.RoundToInt(tempVec.x);
        int y = Mathf.RoundToInt(tempVec.y);

        if (x < 8 && x >= 0 && y < 8 && y >= 0)
        {
            mouseOnPiece = GameManager.Instance.pieces[x, y];
            mouseOnVec.x = x;
            mouseOnVec.y = y;
            highlight.transform.position = new Vector2(x, y);
        }
        else
        {
            mouseOnPiece = null;
            highlight.transform.position = new Vector3(1000, 1000);
        }
    }

    void SelectPiece()
    {
        if (selectedPiece == mouseOnPiece)
        {
            GameManager.Instance.DisablePossibleMoveHighlights();
            selectedPiece = null;
        }
        else
        {
            selectedPiece = mouseOnPiece;
            GameManager.Instance.EnablePossibleMoveHighlights(selectedPiece.CalculateMovement());
        }
    }
}
