using Godot;
using System;

public partial class MeshInstance3dHorizontalLine : MeshInstance3D
{
	[Export]
	Camera3D camera3D = null;
	[Export]
	float distanceFromCamera = 0.2f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Vector3 dir = -camera3D.GlobalTransform.Basis.Column2;
		dir.Y = 0;
		dir = dir.Normalized();

		GlobalPosition = camera3D.GlobalPosition + dir * distanceFromCamera;
	}
}
