using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Knight : Piece
{
    private void Awake()
    {
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    private void Start()
    {
        this.name = "Knight";
    }

    public override List<Tile> PossibleMoves()
    {
        var moves = new List<Tile>();

        KnightMove(-1 , +2, ref moves);
        KnightMove(1 , +2, ref moves);
        KnightMove(2 , 1, ref moves);
        KnightMove(2 , -1, ref moves);

        KnightMove(-1 , -2, ref moves);
        KnightMove(1 , -2, ref moves);
        KnightMove(-2 , 1, ref moves);
        KnightMove(-2 , -1, ref moves);

        return moves;
    }

    private void KnightMove(int x, int y, ref List<Tile> moves)
    {
        x = CurrentTile.Row + x;
        y = CurrentTile.Column + y;

        if (x >= 1 && x <= 8 && y >= 1 && y <= 8)
        {
            var tile = Board.Tiles[x, y];

            if (tile.Piece != null && !CurrentTile.IsEnemy(tile))
                return;

            moves.Add(tile);
        }
    }
}
