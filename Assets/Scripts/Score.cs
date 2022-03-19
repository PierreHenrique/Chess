using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject BlackPrefab;
    public GameObject WhitePrefab;

    private static readonly List<GameObject> Black = new List<GameObject>();
    private static readonly List<GameObject> White = new List<GameObject>();

    private void Start()
    {

    }

    public void Take(Piece piece)
    {
        if (piece.Team == Team.Black)
        {
            piece.gameObject.transform.parent = BlackPrefab.transform;
            Black.Add(piece.gameObject);
            piece.gameObject.transform.localPosition = new Vector3(Black.Count > 8 ? Black.Count - 8 : Black.Count, 0, Black.Count > 8 ? 0 : 1);
        }
        else
        {
            piece.gameObject.transform.parent = WhitePrefab.transform;
            White.Add(piece.gameObject);
            piece.gameObject.transform.localPosition = new Vector3(White.Count > 8 ? White.Count - 8 : White.Count, 0, White.Count > 8 ? 1 : 0);
        }
    }
}
