using Unity.Entities;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float speed;
    
    class Baker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            var cpMovementSpeed = new MovementSpeed
            {
                value = authoring.speed
            };

            AddComponent<ProjectileTag>(entity);
            AddComponent<Lifetime>(entity);
            AddComponent(entity, cpMovementSpeed);
        }
    }
}

public struct ProjectileTag : IComponentData { }
