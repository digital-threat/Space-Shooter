using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct EnemySpawnerSystem : ISystem
{
    private uint counter;
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EnemySpawner>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var query = SystemAPI.QueryBuilder().WithAll<Lifetime>().Build();

        if (query.IsEmpty)
        {
            var spawner = SystemAPI.GetSingleton<EnemySpawner>();
            var instances = state.EntityManager.Instantiate(spawner.prefab, spawner.enemiesPerWave, Allocator.Temp);

            foreach (var entity in instances)
            {
                state.EntityManager.RemoveComponent<LinkedEntityGroup>(entity);
            }
            var random = Random.CreateFromIndex(counter++);
            
            var job = new EnemySpawnJob()
            {
                random = random
            };

            job.Schedule();
        }
    }
}

[BurstCompile]
partial struct EnemySpawnJob : IJobEntity
{
    public Random random;
    
    private void Execute(in EnemyTag enemy, ref LocalTransform transform)
    {
        transform.Position = new float3(random.NextFloat(9, 25), random.NextFloat(-4.5f, 4.5f), 0);
    }
}
