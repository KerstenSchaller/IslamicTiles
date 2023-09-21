using Godot;
using System;

public class SinglePolyButton : CheckButton
{
	public override void _Ready()
	{
		CheckButton node = GetNode<CheckButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer3/CheckButton2");
		node.Pressed = HankinsOptions.getHankinsOptions().printSingleTileOnly;
	}

	private void _on_CheckButton_toggled(bool button_pressed)
	{
		GD.Print("Single Poly button was pressed!" + button_pressed);
		HankinsOptions.getHankinsOptions().printSingleTileOnly = button_pressed;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}

