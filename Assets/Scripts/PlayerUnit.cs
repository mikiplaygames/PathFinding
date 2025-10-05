using UnityEngine;

namespace PathfindingDemo
{
    public class PlayerUnit : Unit
    {
        [Header("Player Stats")]
        [SerializeField] private int moveRange = 5;public int MoveRange => moveRange;
        [SerializeField] private int attackRange = 3;public int AttackRange => attackRange;
        public void SetMoveRange(int range)
        {
            moveRange = Mathf.Max(1, range);
        }
        public void SetAttackRange(int range)
        {
            attackRange = Mathf.Max(1, range);
        }
    }
}
