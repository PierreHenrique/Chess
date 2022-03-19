using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

class King : Piece
{
    private void Awake()
    {
        this.GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void Start()
    {
        this.name = "King";
    }

    private void CleanMoves(List<Tile> c, out List<Tile> ex)
    {
        var p = Board.Pieces.Where(x => x.Team != this.Team).ToList();
        ex = new List<Tile>();

        for (int y = 0; y < c.Count; y++)
        {
            var tile = c[y];

            for (var i = 0; i < p.Count; i++)
            {
                var e = p[i];
                var possibleMoves = e.PossibleMoves();
                var restrictedTiles = new List<Tile>();

                for (var j = 0; j < possibleMoves.Count; j++)
                {
                    var possibleTile = possibleMoves[j];

                    if (possibleTile.Piece != null && possibleTile.Piece.Team != this.Team)
                        restrictedTiles.Add(possibleTile);

                    if (possibleTile.Row == tile.Row && possibleTile.Column == tile.Column)
                    {
                        if (tile.IsHorizontally(e.CurrentTile) && restrictedTiles.Count(x => e.CurrentTile.IsBetween(tile, x, 1)) == 0)
                            ex.Add(tile);
                        else if (tile.IsVertically(e.CurrentTile) && restrictedTiles.Count(x => e.CurrentTile.IsVertically(tile) && e.CurrentTile.IsBetween(tile, x)) == 0)
                            ex.Add(tile);
                        else if (tile.IsDiagonally(e.CurrentTile) && restrictedTiles.Count(x => e.CurrentTile.IsDiagonally(tile) && e.CurrentTile.IsBetween(tile, x, 2)) == 0)
                            ex.Add(tile);

                        foreach (var restricted in restrictedTiles)
                        {
                            if (restricted.IsHorizontally(e.CurrentTile) && restricted == tile)
                                ex.Add(tile);

                            if (restricted.IsVertically(e.CurrentTile) && restricted == tile)
                                ex.Add(tile);

                            if (restricted.IsDiagonally(e.CurrentTile) && restricted == tile)
                                ex.Add(tile);
                        }
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (Game.Instance.Turn == this.Team)
        {
            var p = Board.Pieces.Where(x => x.Team != this.Team).ToList();
            var ex = new List<Tile>();
            var inCheck = false;
            var tile = this.CurrentTile;

            for (var i = 0; i < p.Count; i++)
            {
                var e = p[i];
                var possibleMoves = e.PossibleMoves();
                var restrictedTiles = new List<Tile>();

                for (var j = 0; j < possibleMoves.Count; j++)
                {
                    var possibleTile = possibleMoves[j];

                    if (possibleTile.Piece != null && possibleTile.Piece.Team != this.Team)
                        restrictedTiles.Add(possibleTile);

                    if (possibleTile.Row == tile.Row && possibleTile.Column == tile.Column)
                    {
                        if (tile.IsHorizontally(e.CurrentTile) && restrictedTiles.Count(x => e.CurrentTile.IsBetween(tile, x, 1)) == 0)
                            inCheck = true;
                        if (tile.IsVertically(e.CurrentTile) && restrictedTiles.Count(x => e.CurrentTile.IsVertically(tile) && e.CurrentTile.IsBetween(tile, x)) == 0)
                            inCheck = true;
                        if (tile.IsDiagonally(e.CurrentTile) && restrictedTiles.Count(x =>
                            e.CurrentTile.IsDiagonally(tile) && e.CurrentTile.IsBetween(tile, x, 2)) == 0)
                            inCheck = true;

                        foreach (var restricted in restrictedTiles)
                        {
                            if (restricted.IsHorizontally(e.CurrentTile) && restricted == tile)
                                inCheck = true;

                            if (restricted.IsVertically(e.CurrentTile) && restricted == tile)
                                inCheck = true;

                            if (restricted.IsDiagonally(e.CurrentTile) && restricted == tile)
                                inCheck = true;
                        }
                    }
                }
            }

            PlayerInstance.InCheck = inCheck;

            if (inCheck)
                PlayerInstance.ForceSelect(this.CurrentTile);
        }
    }

    public override List<Tile> PossibleMoves()
    {
        var h = this.Horizontally(1);
        var v = this.Vertically(1);
        var d = this.Diagonally(1);

        var c = h.Concat(v).Concat(d).ToList();

        if (Game.Instance.Turn == this.Team)
        {
            CleanMoves(c, out List<Tile> ex);

            //Debug.LogError(c.Except(ex).ToList().Count);

            return c.Except(ex).ToList();
        }

        return c;
    }
}
