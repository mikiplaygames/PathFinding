using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PathfindingDemo
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected Tile currentTile;public Tile CurrentTile => currentTile;
        protected GridManager gridManager;

        public virtual void Initialize(Tile startTile, GridManager manager)
        {
            gridManager = manager;
            SetTile(startTile);
        }
        public virtual void SetTile(Tile tile)
        {
            currentTile = tile;
            if (tile != null)
                transform.position = tile.WorldPosition + Vector3.up * 0.5f; // offset Y to sit on top of tile
        }
        public virtual IEnumerator MoveAlongPath(List<Tile> path, float moveSpeed = 3f)
        {
            if (path == null || path.Count <= 1) yield break;

            // skip the first tile (current position)
            for (int i = 1; i < path.Count; i++)
            {
                Tile targetTile = path[i];
                Vector3 startPos = transform.position;
                Vector3 targetPos = targetTile.WorldPosition + Vector3.up * 0.5f;
                
                float distance = Vector3.Distance(startPos, targetPos);
                float duration = distance / moveSpeed;
                float elapsed = 0f;

                // set rotation towards target
                Vector3 direction = (targetPos - startPos).normalized;
                if (direction != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(direction);

                while (elapsed < duration)
                {
                    transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }

                transform.position = targetPos;
                currentTile = targetTile;
            }
        }
    }
}
