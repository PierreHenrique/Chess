using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; set; }
    public Score Score;
    public Board Board;

    public Team Turn;

    private void Awake()
    {
        Instance = GameObject.Find("Game").GetComponent<Game>();
    }

    private void Start()
    {
        Turn = Team.Black;
    }

    private void LateUpdate()
    {
        var defeatKing = Board.Pieces.Where(x => x is King).FirstOrDefault(y => y.PossibleMoves().Count == 0);

        if (defeatKing != null)
        {
            Debug.LogWarning($"Team: {defeatKing.Team} LOST");
        } 
    }

    public void FinishTurn()
    {
        this.Turn = this.Turn == Team.Black ? Team.White : Team.Black;
    }
}