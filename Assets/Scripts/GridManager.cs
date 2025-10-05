using UnityEngine;
using System.Collections.Generic;

namespace PathfindingDemo
{
    public class GridManager : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private int gridWidth = 10;
        [SerializeField] private int gridHeight = 10;
        [SerializeField] private float tileSize = 1f;
        [SerializeField] private GameObject tilePrefab;

        private Tile[,] grid;
        private Dictionary<Vector2Int, Tile> tileDict = new();

        private void Awake()
        {
            GenerateGrid();
        }
        public void GenerateGrid()
        {
            // clear existing grid
            if (grid != null)
            {
                foreach (var tile in grid)
                {
                    if (tile != null && tile.Visual != null)
                        Destroy(tile.Visual);
                }
            }
            tileDict.Clear();

            grid = new Tile[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Tile tile = SpawnTile(x, y);
                    grid[x, y] = tile;
                    tileDict[new Vector2Int(x, y)] = tile;
                }
            }
        }
        private Tile SpawnTile(int x, int y)
        {
            Vector3 worldPos = new(x * tileSize, 0, y * tileSize);
            TileVisual visual = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform).GetComponent<TileVisual>();
            Tile tile = new(x, y, TileType.Walkable, visual);
            visual.Initialize(tile, this);
            return tile;
        }
        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                return grid[x, y];
            return null;
        }
        public bool TryGetTile(int x, int y, out Tile tile)
        {
            if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
            {
                tile = grid[x, y];
                return true;
            }
            tile = null;
            return false;
        }
        public Tile GetTileAtWorldPosition(Vector3 worldPos)
        {
            int x = Mathf.RoundToInt(worldPos.x / tileSize);
            int y = Mathf.RoundToInt(worldPos.z / tileSize);
            return GetTile(x, y);
        }
        public List<Tile> GetNeighbors(Tile tile, bool attackMode = false)
        {
            List<Tile> neighbors = new();
            
            // directions: N, S, E, W
            int[] dx = { 0, 0, 1, -1 };
            int[] dy = { 1, -1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int newX = tile.X + dx[i];
                int newY = tile.Y + dy[i];
                
                if (TryGetTile(newX, newY, out Tile neighbor) && (attackMode ? neighbor.CanAttackThrough() : neighbor.IsWalkable()))
                    neighbors.Add(neighbor);
            }

            return neighbors;
        }
        private void OnValidate()
        {
            gridWidth = Mathf.Max(3, gridWidth);
            gridHeight = Mathf.Max(3, gridHeight);
            tileSize = Mathf.Max(0.1f, tileSize);
        }
    }
}
