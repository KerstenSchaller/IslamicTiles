using Godot;
using System;

public class ColorButton : CheckButton
{
	private void _on_CheckButton_toggled(bool button_pressed)
	{
		GD.Print("Color button was pressed!" + button_pressed);
		HankinsOptions.showPoly = button_pressed;
	}
}


