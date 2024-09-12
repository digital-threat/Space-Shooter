using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new PlayerMoveJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };

        job.Schedule();
    }
}

[BurstCompile]
partial struct PlayerMoveJob : IJobEntity
{
    public float deltaTime;

    private void Execute(ref LocalTransform transform, in MovementSpeed speed, in PlayerMoveInput input)
    {
        transform.Position.xy += input.value * speed.value * deltaTime;
        transform.Position.xy = math.clamp(transform.Position.xy, new float2(-8.75f, -4.75f), new float2(8.75f, 4.75f));
    }
}
