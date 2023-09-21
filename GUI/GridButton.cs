using Godot;
using System;

public class GridButton : CheckButton
{
	public override void _Ready()
	{
		CheckButton node = GetNode<CheckButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer/CheckButton");
		node.Pressed = HankinsOptions.getHankinsOptions().showFrame;
	}

	private void _on_CheckButton_toggled(bool button_pressed)
	{
		GD.Print("Grid button was pressed!" + button_pressed);
		HankinsOptions.getHankinsOptions().showFrame = button_pressed;
		HankinsOptions.getHankinsOptions().SerializeNow();


	}
}
