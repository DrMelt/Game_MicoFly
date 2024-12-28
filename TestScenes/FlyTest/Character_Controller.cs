using Godot;
using System;

public partial class Character_Controller : CharacterBody3D
{
	[ExportGroup("Refs")]
	[Export]
	Camera3D camera3D = null;

	[ExportGroup("Configs")]
	[Export]
	float thrustAcc = 13.0f;
	[Export]
	float speed_Floor = 5.0f;
	[Export]
	float accSpeed_Floor = 5.0f;
	[Export]
	float speed_Air = 3.0f;
	// [Export]
	// float accSpeed_Air = 3.0f;
	[Export]
	float damping_Air = 0.85f;
	[Export]
	float damping_Floor = 0.1f;


	[Export]
	float JumpVelocity = 4.5f;


	Vector3 moveDir = Vector3.Forward;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 newVelocity = Velocity;
		// Add the damping.
		if (IsOnFloor())
		{
			newVelocity *= Mathf.Pow(damping_Floor, (float)delta);
		}
		else
		{
			newVelocity *= Mathf.Pow(damping_Air, (float)delta);
		}


		// Add the gravity.
		if (!IsOnFloor())
		{
			newVelocity += GetGravity() * (float)delta;
		}
		// Add the thrust.
		float fly_up_action = Input.GetActionStrength("Move_Fly_Up");
		newVelocity += Basis.Column1 * (float)delta * thrustAcc * fly_up_action;


		Basis cameraBasis = camera3D.Basis;
		cameraBasis.Column0 = -cameraBasis.Column0;
		cameraBasis.Column2 = -cameraBasis.Column2;
		Basis = Basis.Slerp(cameraBasis, (float)delta * 5.0f).Orthonormalized();


		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("Move_Left", "Move_Right", "Move_Forward", "Move_Back");
		Vector3 direction = -(Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();


		if (IsOnFloor())
		{
			if (direction != Vector3.Zero)
			{
				newVelocity.X += direction.X * accSpeed_Floor * (float)delta;
				newVelocity.Z += direction.Z * accSpeed_Floor * (float)delta;
			}
		}

		Velocity = newVelocity;
		MoveAndSlide();
	}
}
