using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[BurstCompile, UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class PlayerInputSystem : SystemBase
{
    private InputActions inputActions;
    private Entity player;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<PlayerTag>();
        state.RequireForUpdate<PlayerMoveInput>();
    }
    
    
    protected override void OnStartRunning()
    {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Game.Shoot.performed += OnShoot;
        player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (!SystemAPI.Exists(player)) return;

        EntityManager.AddComponent<FireProjectileTag>(player);
    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        Vector2 moveInput = inputActions.Game.Move.ReadValue<Vector2>();

        SystemAPI.SetSingleton(new PlayerMoveInput { value = moveInput });
    }

    protected override void OnStopRunning()
    {
        inputActions.Disable();
        player = Entity.Null;
    }
}
