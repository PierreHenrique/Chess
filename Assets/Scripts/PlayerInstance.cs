using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerInstance : MonoBehaviour
{
    public static Player Player;
    public static Tile SelectedTile;
    public static bool InCheck = false;

    private void Awake()
    {
        Player = new Player
        {
            Name = "Konami",
            Team = Team.Black
        };
    }

    public static void ForceSelect(Tile tile)
    {
        if (tile != null)
        {
            SelectedTile = tile;
            SelectedTile.ChangeState(State.Selected);

            foreach (var possibleMove in SelectedTile.Piece.PossibleMoves())
            {
                possibleMove.ChangeState(State.Preview);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (hit.transform.tag == "Tile")
                {
                    var tile = hit.transform.GetComponent<Tile>();

                    if (SelectedTile == tile)
                        return;

                    if (SelectedTile != null && tile.State == State.Preview)
                    {
                        if (SelectedTile.Piece.Move(tile))
                        {
                            SelectedTile.Reset();
                            SelectedTile = null;
                            Game.Instance.FinishTurn();
                        }
                    }
                    else
                    {
                        if (SelectedTile != null)
                            SelectedTile.Reset();

                        if (InCheck)
                            SelectedTile = tile.Piece != null && tile.Piece.Team == Game.Instance.Turn && tile.Piece is King ? tile : null;
                        else
                            SelectedTile = tile.Piece != null && tile.Piece.Team == Game.Instance.Turn ? tile : null;

                        if (SelectedTile != null)
                        {
                            SelectedTile.ChangeState(State.Selected);

                            foreach (var possibleMove in SelectedTile.Piece.PossibleMoves())
                            {
                                possibleMove.ChangeState(State.Preview);
                            }
                        }
                    }
                }
            }
        }
    }
}
