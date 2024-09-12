using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float speed;
    
    class Baker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
            var cpMovementSpeed = new MovementSpeed
            {
                value = authoring.speed
            };
            AddComponent(entity, cpMovementSpeed);

            var cpEnemy = new EnemyTag();
            AddComponent(entity, cpEnemy);
            
            var cpLifetime = new Lifetime();
            AddComponent(entity, cpLifetime);
        }
    }
}

public struct Lifetime : IComponentData, IEnableableComponent
{
}

public struct EnemyTag : IComponentData
{
}
