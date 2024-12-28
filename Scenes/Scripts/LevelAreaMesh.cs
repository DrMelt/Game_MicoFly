using Godot;
using System;

public partial class LevelAreaMesh : MeshInstance3D
{
	Material material;
	Color originalColor;
	[Export]
	Color rampColor = new Color(0.833f, 0.19f, 0.494f, 0.173f);

	public override void _Ready()
	{

		material = GetSurfaceOverrideMaterial(0);
		originalColor = (Color)material.Get("shader_parameter/base_color");
	}

	public override void _Process(double delta)
	{
	}


	public void LerpColor(float mix)
	{
		Color mixedColor = mix * rampColor + (1 - mix) * originalColor;
		material.Set("shader_parameter/base_color", mixedColor);
	}
}
