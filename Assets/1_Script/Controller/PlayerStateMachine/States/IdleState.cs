using UnityEngine;

namespace MPGame.Controller.StateMachine
{
    public class IdleState : StateBase
    {

        public IdleState(PlayerController controller, PlayerStateMachine stateMachine) 
            : base(controller, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            vertInputRaw = horzInputRaw = 0f;        
        }

        public override void Exit()
        {
            base.Exit();
			controller.Rigidbody.AddForce(0f, 0f, 0f);
		}


        public override void HandleInput()
        {
            base.HandleInput();

            GetMovementInputRaw(out vertInputRaw, out horzInputRaw);
            GetMouseInput(out mouseX, out mouseY);
            GetInteractableInput();
            GetJumpInput(out isJumpPrssed);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Mathf.Abs(vertInputRaw) >= 0.9f || Mathf.Abs(horzInputRaw) >= 0.9f) 
            {
                stateMachine.ChangeState(controller.walkState);
            }

			controller.DetectIsFalling();

			controller.RotateWithMouse(mouseX, mouseY);
            controller.Jump(isJumpPrssed);

		}

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            controller.RaycastInteractableObject();
			controller.WalkWithArrow(0f, 0f, 0f);
		}
    }
}
