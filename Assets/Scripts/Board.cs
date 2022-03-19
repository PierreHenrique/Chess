using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject PiecePrefab;
    public GameObject TilePrefab;

    public static readonly Tile[,] Tiles = new Tile[9,9];
    public static readonly List<Piece> Pieces = new List<Piece>();

    void Awake()
    {
        var black = Resources.Load<Material>("Material/Black");
        var white = Resources.Load<Material>("Material/White");

        foreach (var t in GameObject.FindGameObjectsWithTag("Tile"))
        {
            var reverse = Convert.ToInt32(t.transform.parent.name) % 2 == 0;
            var index = Convert.ToInt32(t.name);

            if (index % 2 == 0)
            {
                t.GetComponent<Renderer>().material = reverse ? black : white;
            }
            else
            {
                t.GetComponent<Renderer>().material = reverse ? white : black;
            }
        }
    }

    private void Start()
    {
        foreach (var t in GameObject.FindGameObjectsWithTag("Tile"))
        {
            var row = Convert.ToInt32(t.transform.parent.name);
            var index = Convert.ToInt32(t.name);

            Tiles[row, index] = t.AddComponent<Tile>().Initialize(row, index);
        }

        SpawnPieces(Team.Black);
        SpawnPieces(Team.White);

        //for (int x = 0; x < Size.x; x++)
        //{
        //    for (int z = 0; z < Size.y; z++)
        //    {
        //        var gObject  = Object.Instantiate(TilePrefab, this.transform);
        //        gObject.name = $"Tile [{x + 1}/{z + 1}]";

        //        gObject.transform.position = new Vector3(x, 0, z);
        //    }
        //}
    }

    private void SpawnPieces(Team team)
    {
        if (team == Team.White)
        {
            for (int i = 1; i <= 8; i++)
            {
                var gObject = Instantiate(PiecePrefab, GameObject.Find("Pieces").transform);
                gObject.AddComponent<Pawn>();
                gObject.GetComponent<Piece>().Team = team;
                gObject.transform.localPosition = new Vector3(i, 0, team == Team.Black ? 2 : 7);

                Pieces.Add(gObject.GetComponent<Piece>());
            }
        }

        for (int i = 1; i <= 8; i++)
        {
            var gObject = Instantiate(PiecePrefab, GameObject.Find("Pieces").transform);

            switch (i)
            {
                case 1:
                    gObject.AddComponent<Rook>();
                    break;
                case 2:
                    gObject.AddComponent<Knight>();
                    break;
                case 3:
                    gObject.AddComponent<Bishop>();
                    break;
                case 4:
                    gObject.AddComponent<King>();
                    //if (team != Team.Black)
                    //    gObject.AddComponent<King>();
                    //else
                    //    gObject.AddComponent<Queen>();
                    break;
                case 5:
                    gObject.AddComponent<Queen>();
                    //if (team == Team.Black)
                    //    gObject.AddComponent<King>();
                    //else
                    //    gObject.AddComponent<Queen>();
                    break;
                case 6:
                    gObject.AddComponent<Bishop>();
                    break;
                case 7:
                    gObject.AddComponent<Knight>();
                    break;
                case 8:
                    gObject.AddComponent<Rook>();
                    break;
            }

            gObject.GetComponent<Piece>().Team = team;
            if (i == 4 || i == 5)
            {
                gObject.transform.localPosition = new Vector3(i, 0, team == Team.Black ? 3 : 6);
            }
            else
            {
                gObject.transform.localPosition = new Vector3(i, 0, team == Team.Black ? 1 : 8);
            }

            Pieces.Add(gObject.GetComponent<Piece>());
        }
    }
}
