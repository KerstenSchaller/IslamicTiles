using Godot;
using System;

public class ColorButton : CheckButton
{
	public override void _Ready()
	{
		CheckButton node = GetNode<CheckButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer2/CheckButton3");
		node.Pressed = HankinsOptions.getHankinsOptions().showPoly;

	}

	private void _on_CheckButton_toggled(bool button_pressed)
	{
		GD.Print("Color button was pressed!" + button_pressed);
		HankinsOptions.getHankinsOptions().showPoly = button_pressed;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}


