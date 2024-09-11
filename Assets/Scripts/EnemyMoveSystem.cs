using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct EnemyMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var entityQuery = SystemAPI.QueryBuilder().WithOptions(EntityQueryOptions.IncludePrefab).WithDisabled<Lifetime>().Build();
        
        //EntityQuery query = GetEntityQuery(new EntityQueryDesc[] {entityQuery});
        
        state.EntityManager.DestroyEntity(entityQuery);
        
        var job = new EnemyMoveJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };

        job.Schedule();
    }
}

[BurstCompile]
partial struct EnemyMoveJob : IJobEntity
{
    public float deltaTime;

    private void Execute(ref LocalTransform transform, in MovementSpeed movementSpeed, EnabledRefRW<Lifetime> lifetime)
    {
        transform = transform.Translate(new float3(-movementSpeed.speed * deltaTime, 0, 0));
        if (transform.Position.x < -5)
        {
            lifetime.ValueRW = false;
        }
    }
}
