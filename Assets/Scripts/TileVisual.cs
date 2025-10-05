using UnityEngine;
using UnityEngine.EventSystems;

namespace PathfindingDemo
{
    [RequireComponent(typeof(Renderer))]
    public class TileVisual : MonoBehaviour , IPointerClickHandler
    {
        [SerializeField] Material walkableMaterial;
        [SerializeField] Material obstacleMaterial;
        [SerializeField] Material coverMaterial;

        private Tile tile;
        private GridManager gridManager;
        private Renderer tileRenderer;

        public void Initialize(Tile assignedTile, GridManager manager)
        {
            name = $"Tile_{assignedTile.X}_{assignedTile.Y}";
            tile = assignedTile;
            
            gridManager = manager;
            tileRenderer = GetComponent<Renderer>();
            SetRightMaterial();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            tile.Type = TileSelector.SelectedType;
            SetRightMaterial();
        }
        private void SetRightMaterial()
        {
            if (tileRenderer == null) return;

            switch (tile.Type)
            {
                case TileType.Walkable:
                    tileRenderer.material = walkableMaterial;
                    break;
                case TileType.Obstacle:
                    tileRenderer.material = obstacleMaterial;
                    break;
                case TileType.Cover:
                    tileRenderer.material = coverMaterial;
                    break;
            }
        }
    }
}
