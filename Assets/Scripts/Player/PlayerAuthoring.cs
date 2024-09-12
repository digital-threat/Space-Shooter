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
                value = authoring.speed
            };
            
            var cpProjectilePrefab = new ProjectilePrefab
            {
                entity = GetEntity(authoring.projectilePrefab, TransformUsageFlags.Dynamic)
            };
            
            AddComponent<PlayerTag>(entity);
            AddComponent<PlayerMoveInput>(entity);
            AddComponent(entity, cpMovementSpeed);
            AddComponent(entity, cpProjectilePrefab);
        }
    }
}

public struct PlayerTag : IComponentData { }

public struct FireProjectileTag : IComponentData { }

public struct PlayerMoveInput : IComponentData
{
    public float2 value;
}

public struct ProjectilePrefab : IComponentData
{
    public Entity entity;
}