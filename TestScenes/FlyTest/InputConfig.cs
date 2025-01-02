using Godot;
using System;

public partial class InputConfig : Node
{
	static InputConfig instance = null;
	public static InputConfig InstanceRef => instance;
	InputConfig()
	{
		instance = this;
	}
	bool isMouseCaptured = true;
	public bool IsMouseCaptured => isMouseCaptured;
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Mouse_Detach"))
		{
			if (isMouseCaptured)
			{
				Input.MouseMode = Input.MouseModeEnum.Visible;
				isMouseCaptured = false;
			}
			else
			{
				Input.MouseMode = Input.MouseModeEnum.Captured;
				isMouseCaptured = true;
			}
		}

	}
}
