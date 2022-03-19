using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Burst;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Tile CurrentTile;
    public Team Team;

    private void Update()
    {
        Physics.Raycast(this.transform.position, Vector3.down, out RaycastHit hit, 1);

        if (hit.transform.tag == "Tile")
        {
            var tile = hit.transform.GetComponent<Tile>();

            if (CurrentTile != null && CurrentTile != tile)
                CurrentTile.Piece = null;

            tile.Piece = this;
            CurrentTile = tile;
        }
    }

    public int Forward(int value)
    {
        if (Team == Team.White)
            return CurrentTile.Row - value;

        return CurrentTile.Row + value;
    }

    public bool Move(Tile tile)
    {
        if (tile.State == State.Preview)
        {
            this.transform.localPosition = new Vector3(tile.Column, 0, tile.Row);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
            {
                Game.Instance.Score.Take(tile.Piece);
                //DestroyImmediate(tile.Piece.gameObject);
            }

            return true;
        }

        return false;
    }

    public virtual List<Tile> PossibleMoves()
    {
        return null;
    }

    public List<Tile> Horizontally(int limit = 8)
    {
        var output = new List<Tile>();

        int x, y, l;

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            y++;
            l--;

            if (y > 8)
                break; 

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            y--;
            l--;

            if (y < 1)
                break;

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        return output;
    }

    public List<Tile> Vertically(int limit = 8)
    {
        var output = new List<Tile>();

        int x, y, l;

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            x++;
            l--;

            if (x > 8)
                break; 

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            x--;
            l--;

            if (x < 1)
                break; 

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        return output;
    }

    public List<Tile> Diagonally(int limit = 8)
    {
        var output = new List<Tile>();

        int x, y, l;

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            x--;
            y++;
            l--;

            if (x < 1 || y > 8)
                break;

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            x++;
            y++;
            l--;

            if (x > 8 || y > 8)
                break;

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            x--;
            y--;
            l--;

            if (x < 1 || y < 1)
                break;

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        x = CurrentTile.Row;
        y = CurrentTile.Column;
        l = limit;

        while (true)
        {
            x++;
            y--;
            l--;

            if (x > 8 || y < 1)
                break;

            var tile = Board.Tiles[x, y];

            if (Game.Instance.Turn == this.Team && tile.Piece != null && !CurrentTile.IsEnemy(tile))
                break;

            output.Add(tile);

            if (tile.Piece != null && CurrentTile.IsEnemy(tile))
                break;

            if (l <= 0)
                break;
        }

        return output;
    }
}