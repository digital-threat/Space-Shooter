using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(TransformSystemGroup))]
public partial struct EnemyCollisionSystem : ISystem
{
    private float dsq;
    
    public void OnCreate(ref SystemState state)
    {
        dsq = 0.4f * 0.4f;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // There's no 2D physics for entities and I didn't want to spend hours on this, hence this naive double loop.
        // I'm hoping the compiler will at least auto-vectorize it.
        // Looked at the assembly in Burst inspector but I can't tell what's happening in there because it's all code gen.

        foreach (var (enemyTransform, enemyLifetime) in SystemAPI.Query<RefRO<LocalTransform>, EnabledRefRW<Lifetime>>().WithAll<EnemyTag>())
        {
            foreach (var (projectileTransform, projectileLifetime) in SystemAPI.Query<RefRO<LocalTransform>, EnabledRefRW<Lifetime>>().WithAll<ProjectileTag>())
            {
                if (math.distancesq(enemyTransform.ValueRO.Position, projectileTransform.ValueRO.Position) <= dsq)
                {
                    enemyLifetime.ValueRW = false;
                    projectileLifetime.ValueRW = false;
                }

                // This is bugged when you spam projectiles.
                // bool comp = math.distancesq(enemyTransform.ValueRO.Position, projectileTransform.ValueRO.Position) <= dsq;
                // enemyLifetime.ValueRW = !comp;
                // projectileLifetime.ValueRW = !comp;
            }
        }
    }
}
