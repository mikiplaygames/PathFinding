using UnityEngine;
using UnityEngine.UI;
namespace PathfindingDemo
{
    public class TileSelector : MonoBehaviour
    {
        public static TileType SelectedType = TileType.Walkable;

        [SerializeField] Button walkableButton;
        [SerializeField] Button obstacleButton;
        [SerializeField] Button coverButton;
        Button lastUsed;
        void Awake()
        {
            lastUsed = walkableButton;
            walkableButton.interactable = false;
            walkableButton.onClick.AddListener(SetWalkable);
            obstacleButton.onClick.AddListener(SetObstacle);
            coverButton.onClick.AddListener(SetCover);
        }
        public void SetWalkable()
        {
            walkableButton.interactable = false;
            lastUsed.interactable = true;
            SelectedType = TileType.Walkable;
            lastUsed = walkableButton;
        }
        public void SetObstacle()
        {
            obstacleButton.interactable = false;
            lastUsed.interactable = true;
            SelectedType = TileType.Obstacle;
            lastUsed = obstacleButton;
        }
        public void SetCover()
        {
            coverButton.interactable = false;
            lastUsed.interactable = true;
            SelectedType = TileType.Cover;
            lastUsed = coverButton;
        }
    }
}