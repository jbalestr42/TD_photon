using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class PlayerController : Bolt.EntityBehaviour<IPlayerState> {

    const float MOUSE_SENSITIVITY = 2f;

    bool _forward;
    bool _backward;
    bool _left;
    bool _right;
    bool _jump;

    PlayerMotor _motor;

    void Start() {
        if (entity.HasControl()) {
            CameraManager.Instance.SetCameraMovement(new FollowCamera(gameObject.transform, new Vector3(0f, 5, -10)));
        }
    }

    public override void Attached() {
        _motor = GetComponent<PlayerMotor>();
        state.SetTransforms(state.Transform, transform);
    }

    void Update() {
        PollKeys(true);
    }

    void PollKeys(bool mouse) {
        _forward = Input.GetKey(KeyCode.W);
        _backward = Input.GetKey(KeyCode.S);
        _left = Input.GetKey(KeyCode.A);
        _right = Input.GetKey(KeyCode.D);
        _jump = Input.GetKeyDown(KeyCode.Space);
    }

    public override void SimulateController() {
        PollKeys(false);

        IPlayerCommandInput input = PlayerCommand.Create();

        input.Forward = _forward;
        input.Backward = _backward;
        input.Left = _left;
        input.Right = _right;
        input.Jump = _jump;

        entity.QueueInput(input);
    }

    public override void ExecuteCommand(Command command, bool resetState) {
        PlayerCommand cmd = (PlayerCommand)command;

        if (resetState) {
            // we got a correction from the server, reset (this only runs on the client)
            _motor.SetState(cmd.Result.Position, cmd.Result.Velocity, cmd.Result.IsGrounded, cmd.Result.JumpFrames);
        } else {
            // apply movement (this runs on both server and client)
            PlayerMotor.State motorState = _motor.Move(cmd.Input.Forward, cmd.Input.Backward, cmd.Input.Left, cmd.Input.Right, cmd.Input.Jump);

            // copy the motor state to the commands result (this gets sent back to the client)
            cmd.Result.Position = motorState.position;
            cmd.Result.Velocity = motorState.velocity;
            cmd.Result.IsGrounded = motorState.isGrounded;
            cmd.Result.JumpFrames = motorState.jumpFrames;
        }
    }
}
