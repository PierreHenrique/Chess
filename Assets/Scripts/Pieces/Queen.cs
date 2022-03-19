using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Queen : Piece
{
    private void Awake()
    {
        this.GetComponent<Renderer>().material.color = Color.magenta;
    }

    private void Start()
    {
        this.name = "Queen";
    }

    public override List<Tile> PossibleMoves()
    {
        var h = this.Horizontally();
        var v = this.Vertically();
        var d = this.Diagonally();

        return h.Concat(v).Concat(d).ToList();
    }
}
