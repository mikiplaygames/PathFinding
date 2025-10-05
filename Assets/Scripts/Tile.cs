using UnityEngine;

namespace PathfindingDemo
{
    public class Tile
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public TileType Type { get; set; }
        public TileVisual Visual { get; set; }
        public Vector3 WorldPosition => Visual.transform.position;
        public Tile(int x, int y, TileType type, TileVisual visual)
        {
            X = x;
            Y = y;
            Type = type;
            Visual = visual;
        }
        public bool IsWalkable()
        {
            return Type == TileType.Walkable;
        }
        public bool CanAttackThrough()
        {
            return Type == TileType.Walkable || Type == TileType.Cover;
        }
    }
}
