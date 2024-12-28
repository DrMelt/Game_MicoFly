using Godot;
using System;

public partial class LevelAreaCollisionShape : CollisionShape3D
{
	[Export]
	PackedScene levelAreaMeshScene = null;

	LevelAreaMesh levelAreaMeshRef = null;
	public override void _Ready()
	{
		levelAreaMeshRef = levelAreaMeshScene.Instantiate<LevelAreaMesh>();
		AddChild(levelAreaMeshRef);

		var mesh = levelAreaMeshRef.Mesh as BoxMesh;
		BoxShape3D shape = Shape as BoxShape3D;

		mesh.Size = shape.Size;
	}

	public override void _Process(double delta)
	{
	}
}
