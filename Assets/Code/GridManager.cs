using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
   [SerializeField] private Vector2Int gridSize;
   [SerializeField] private int unityGridSize;
   public int UnityGridSize
   {
      get { return unityGridSize; }
   }

   private Dictionary<Vector2Int, PosNode> _grid = new Dictionary<Vector2Int, PosNode>();
   Dictionary<Vector2Int, PosNode> Grid
   {
      get { return _grid; }
   }
   
   /**
    * return a random PosNode in the grid
    * that contains no occupants.
    */
   public PosNode findUnoccupiedTile()
   {
      int[] checkedIndices = new int[] { };
      PosNode foundTile = null;
      
      while (checkedIndices.Length < _grid.Count)
      {
         int randomNum = Random.Range(0, _grid.Count);
         checkedIndices.Append(randomNum);

         PosNode tile = _grid.ElementAt(randomNum).Value;
         if (!checkedIndices.Contains(randomNum) && tile.isOpen())
         {
            foundTile = tile;
            break;
         }
      }

      return foundTile;

   }
   
   private void Awake()
   {
      for (int x = 0; x < gridSize.x; x++)
      {
         for (int y = 0; y < gridSize.y; y++)
         {
            Vector2Int pos = new Vector2Int(x, y);
            _grid.Add(pos, new PosNode(pos));
         }
      }
   }
}
