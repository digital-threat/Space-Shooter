using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup)), UpdateBefore(typeof(TransformSystemGroup))]
public partial struct FireProjectileSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
        state.RequireForUpdate<FireProjectileTag>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var player = SystemAPI.GetSingletonEntity<PlayerTag>();
        var projectilePrefab = state.EntityManager.GetComponentData<ProjectilePrefab>(player);
        var transform = state.EntityManager.GetComponentData<LocalTransform>(player);
        
        var newProjectile = state.EntityManager.Instantiate(projectilePrefab.entity);
        var projectileTransform = LocalTransform.FromPositionRotationScale(transform.Position, quaternion.identity, 0.15f);
        state.EntityManager.SetComponentData(newProjectile, projectileTransform);
        state.EntityManager.RemoveComponent<LinkedEntityGroup>(newProjectile);

        state.EntityManager.RemoveComponent<FireProjectileTag>(player);
    }
}
