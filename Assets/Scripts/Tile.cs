using UnityEngine;

namespace PathfindingDemo
{
    public class Tile
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public TileType Type { get; set; }
        public GameObject Visual { get; set; }
        public Vector3 WorldPosition { get; private set; }

        public Tile(int x, int y, TileType type, Vector3 worldPosition)
        {
            X = x;
            Y = y;
            Type = type;
            WorldPosition = worldPosition;
        }
        public bool IsTraversable()
        {
            return Type == TileType.Traversable;
        }
        public bool CanAttackThrough()
        {
            return Type == TileType.Traversable || Type == TileType.Cover;
        }
    }
}
