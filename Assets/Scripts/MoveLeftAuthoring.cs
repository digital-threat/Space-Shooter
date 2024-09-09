using Unity.Entities;
using UnityEngine;

public class MoveLeftAuthoring : MonoBehaviour
{
    class Baker : Baker<MoveLeftAuthoring>
    {
        public override void Bake(MoveLeftAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var component = new MoveLeft();
            
            AddComponent(entity, component);
        }
    }
}

public struct MoveLeft : IComponentData
{
    
}
