using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct EnemyMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
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

    private void Execute(in EnemyTag tag, ref LocalTransform transform, in MovementSpeed speed, EnabledRefRW<Lifetime> lifetime)
    {
        transform.Position.x += -speed.value * deltaTime;
        if (transform.Position.x < -10)
        {
            lifetime.ValueRW = false;
        }
    }
}
