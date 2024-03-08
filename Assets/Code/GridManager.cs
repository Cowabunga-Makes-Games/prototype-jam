using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

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
