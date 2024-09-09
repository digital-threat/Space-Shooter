using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public partial struct MoveLeftSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        throw new System.NotImplementedException();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        throw new System.NotImplementedException();
    }
}

[BurstCompile]
partial struct MoveLeftJob : IJobEntity
{
    public float deltaTime;

    private void Execute(ref LocalTransform transform)
    {
        
    }
}
