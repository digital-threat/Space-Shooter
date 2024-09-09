using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

public partial struct EnemySpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemySpawner>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var query = SystemAPI.QueryBuilder().WithAll<MoveLeft>().Build();

        if (query.IsEmpty)
        {
            var spawner = SystemAPI.GetSingleton<EnemySpawner>();
            var instances = state.EntityManager.Instantiate(spawner.prefab, spawner.enemiesPerWave, Allocator.Temp);
        }
    }
}
