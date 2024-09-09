using Unity.Entities;
using UnityEngine;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    public GameObject prefab;
    public int enemiesPerWave;

    class Baker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var component = new EnemySpawner
            {
                prefab = GetEntity(authoring.prefab, TransformUsageFlags.None),
                enemiesPerWave = authoring.enemiesPerWave
            };
            
            AddComponent(entity, component);
        }
    }
}

struct EnemySpawner : IComponentData
{
    public Entity prefab;
    public int enemiesPerWave;
}
