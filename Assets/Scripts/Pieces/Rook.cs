using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Rook : Piece
{
    private void Awake()
    {
        this.GetComponent<Renderer>().material.color = Color.gray;
    }

    private void Start()
    {
        this.name = "Rook";
    }

    public override List<Tile> PossibleMoves()
    {
        var h = this.Horizontally();
        var v = this.Vertically();

        return h.Concat(v).ToList();
    }
}
