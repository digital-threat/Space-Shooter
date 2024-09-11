using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float speed;

    public GameObject projectilePrefab;
    
    class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            var cpMovementSpeed = new MovementSpeed
            {
                speed = authoring.speed
            };
            AddComponent(entity, cpMovementSpeed);

            
        }
    }
}

public struct PlayerMoveInput : IComponentData
{
    public float2 value;
}

public struct PlayerTag : IComponentData
{
    
}
