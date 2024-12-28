using Godot;
using System;

public partial class ButtonToScene : Button
{
	[Export]
	Node worldRoot = null;
	[Export]
	PackedScene targetScene = null;


	public override void _Pressed()
	{
		Node scene = targetScene.Instantiate();
		worldRoot.AddChild(scene);
	}
}
