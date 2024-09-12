using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Projectile
{
    public partial struct ProjectileMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new ProjectileMoveJob
            {
                deltaTime = SystemAPI.Time.DeltaTime
            };

            job.Schedule();
        }
    }
}

[BurstCompile]
partial struct ProjectileMoveJob : IJobEntity
{
    public float deltaTime;

    private void Execute(in ProjectileTag tag, ref LocalTransform transform, in MovementSpeed speed, EnabledRefRW<Lifetime> lifetime)
    {
        transform.Position.x += speed.value * deltaTime;
        if (transform.Position.x > 10)
        {
            lifetime.ValueRW = false;
        }
    }
}
