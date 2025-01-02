using Godot;
using System;

public partial class Camera3d : Camera3D
{
	[ExportGroup("Refs")]
	[Export]
	CharacterBody3D body3D = null;

	[ExportGroup("Configs")]
	[Export]
	float sensitivity_Mouse = 1.0f;
	[Export]
	float sensitivity_Joy = 1.0f;
	[Export]
	float cameraXMin = -(float)Math.PI * 60 / 180f;
	[Export]
	float cameraXMax = (float)Math.PI * 60 / 180f;


	[Export]
	float rotationZSensitivity = 0.2f * Mathf.Pi;
	[Export]
	float rotationZRestoreSpeed = 0.2f;


	float rotationZ = 0.0f * Mathf.Pi;


	public override void _Process(double delta)
	{
		RotateView(delta);

		Position = body3D.Position + body3D.Basis.Column1 * 0.85f;

	}

	Vector2 mouseMoved = new Vector2();
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouse)
		{
			if (InputConfig.InstanceRef.IsMouseCaptured)
			{
				mouseMoved += eventMouse.Relative * GetWindow().Size.Y / 648.0f;
			}
		}
	}

	void RotateView(double delta)
	{
		rotationZ *= Mathf.Pow(rotationZRestoreSpeed, (float)delta);

		if (!body3D.IsOnFloor())
		{
			float rotationZActive = Input.GetActionStrength("Move_Left") - Input.GetActionStrength("Move_Right");

			rotationZ += (rotationZActive * rotationZSensitivity * (float)delta + Mathf.Pi) % (2 * Mathf.Pi) - Mathf.Pi;
		}

		mouseMoved.X += (Input.GetActionStrength("View_Move_R") - Input.GetActionStrength("View_Move_L")) * sensitivity_Joy;
		mouseMoved.Y += (Input.GetActionStrength("View_Move_D") - Input.GetActionStrength("View_Move_U")) * sensitivity_Joy;

		float rotationOfX = -mouseMoved.Y * sensitivity_Mouse * (float)delta;
		float rotationOfY = -mouseMoved.X * sensitivity_Mouse * (float)delta;

		RotateCamera(new Vector3(rotationOfX, rotationOfY, rotationZ));

		mouseMoved = Vector2.Zero;
	}

	void RotateCamera(Vector3 rotateRadianVec)
	{
		float newXRadian = Rotation.X + rotateRadianVec.X;
		float clampedX = Math.Clamp(newXRadian, cameraXMin, cameraXMax);

		float newYRadian = Rotation.Y + rotateRadianVec.Y;

		Rotation = new Vector3(clampedX, newYRadian, rotateRadianVec.Z);
	}

}
