using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData {  get; private set; }

    [field: SerializeField] public float PlayerChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;

    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitiontime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float DealingStartTransitiontime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float DealingEndTransitiontime { get; private set; }
}
