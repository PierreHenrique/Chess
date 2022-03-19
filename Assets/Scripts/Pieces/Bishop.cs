using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Bishop : Piece
{
    private void Awake()
    {
        this.GetComponent<Renderer>().material.color = Color.cyan;
    }

    private void Start()
    {
        this.name = "Bishop";
    }

    public override List<Tile> PossibleMoves()
    {
        var d = this.Diagonally();

        return d;
    }
}