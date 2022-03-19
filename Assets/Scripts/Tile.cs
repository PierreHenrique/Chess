using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private static readonly List<Tile> CachedPreviews = new List<Tile>();

    public int Row { get; internal set; }
    public int Column { get; internal set; }
    public Piece Piece;
    public State State;

    public Material BackupMaterial;

    public Tile Initialize(int row, int column)
    {
        this.Row = row;
        this.Column = column;
        this.State = State.None;
        this.BackupMaterial = this.GetComponent<Renderer>().material;

        return this;
    }

    private void Update()
    {
        if (PlayerInstance.SelectedTile != this && this.GetComponent<Renderer>().material == Resources.Load<Material>("Material/Selected"))
        {
            this.ChangeState(State.None);
        }
    }

    public void Reset()
    {
        this.ChangeState(State.None);

        foreach (var tile in CachedPreviews.ToList())
        {
            tile.ChangeState(State.None);

            CachedPreviews.Remove(tile);
        }
    }

    public void ChangeState(State state)
    {
        var material = BackupMaterial;

        switch (state)
        {
            case State.Selected:
                material = Resources.Load<Material>("Material/Selected");
                break;

            case State.Preview:
                material = Resources.Load<Material>("Material/Preview");
                CachedPreviews.Add(this);
                break;
            case State.Debug:
                material = Resources.Load<Material>("Material/Debug");
                CachedPreviews.Add(this);
                break;
        }

        this.GetComponent<Renderer>().material = material;
        this.State = state;
    }

    public bool IsEnemy(Tile tile = null)
    {
        if (tile != null && tile.Piece != null)
        {
            if (this.Piece != null && this.Piece.Team != tile.Piece.Team)
                return true;

            return false;
        }

        if (this.Piece != null && this.Piece.Team != PlayerInstance.Player.Team)
            return true;

        return false;
    }

    public bool IsBetween(Tile tileTarget, Tile middleTarget, int orientation = 0)
    {
        switch (orientation)
        {
            case 0:
                if (this.Row > middleTarget.Row && tileTarget.Row < middleTarget.Row)
                    return true;

                if (this.Row < middleTarget.Row && tileTarget.Row > middleTarget.Row)
                    return true;
                break;
            case 1:
                if (this.Column > middleTarget.Column && tileTarget.Column < middleTarget.Column)
                    return true;

                if (this.Column < middleTarget.Column && tileTarget.Column > middleTarget.Column)
                    return true;

                break;
            case 2:
                if (this.Row != middleTarget.Row && this.Row != tileTarget.Row && middleTarget.Row != tileTarget.Row)
                {
                    if (this.Column != middleTarget.Column && this.Column != tileTarget.Column && middleTarget.Column != tileTarget.Column)
                    {
                        if (this.Row < middleTarget.Row && tileTarget.Row > middleTarget.Row)
                            return true;

                        if (this.Row > middleTarget.Row && tileTarget.Row < middleTarget.Row)
                            return true;
                    }
                }

                break;
        }



        return false;
    }

    public bool IsVertically(Tile tile)
    {
        if (this.Column != tile.Column)
            return false;

        return true;
    }

    public bool IsHorizontally(Tile tile)
    {
        if (this.Row != tile.Row)
            return false;

        return true;
    }

    public bool IsDiagonally(Tile tile)
    {
        if (this.Column != tile.Column && this.Row == tile.Row)
            return false;

        if (this.Column == tile.Column && this.Row != tile.Row)
            return false;

        return true;
    }

    public bool IsStarting()
    {
        return Row == 7 || Row == 2;
    }

    public bool IsBackRank()
    {
        return true;
    }
}