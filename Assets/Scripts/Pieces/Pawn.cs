using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Pawn : Piece
{
    private void Awake()
    {
        this.GetComponent<Renderer>().material.color = new Color32(101, 67, 33 , 255);
    }

    private void Start()
    {
        this.name = "Pawn";
    }

    public override List<Tile> PossibleMoves()
    {
        var moves = new List<Tile>();

        if (Game.Instance.Turn == this.Team)
        {
            if (Team == Team.Black)
            {
                for (int i = Forward(1); i <= Forward(CurrentTile.IsStarting() ? 2 : 1); i++)
                {
                    if (i > 8 || i < 1)
                        continue;

                    var tile = Board.Tiles[i, CurrentTile.Column];

                    if (tile.Piece != null)
                        break;

                    moves.Add(tile);
                }
            }
            else
            {
                for (int i = Forward(1); i >= Forward(CurrentTile.IsStarting() ? 2 : 1); i--)
                {
                    if (i > 8 || i < 1)
                        continue;

                    var tile = Board.Tiles[i, CurrentTile.Column];

                    if (tile.Piece != null)
                        break;

                    moves.Add(tile);
                }
            }
        }

        for (int i = CurrentTile.Column - 1; i <= CurrentTile.Column + 1; i++)
        {
            if (i > 8 || i < 1 || Forward(1) < 1 || Forward(1) > 8)
                continue;

            var tile = Board.Tiles[Forward(1), i];

            if (CurrentTile.IsDiagonally(tile))
            {
                if (Game.Instance.Turn != this.Team || tile.Piece != null && tile.IsEnemy(CurrentTile))
                {
                    moves.Add(tile);
                }
            }
        }

        return moves;
    }
}
