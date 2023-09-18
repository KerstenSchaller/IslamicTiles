using Godot;
using System;

public class GridButton : CheckButton
{
	private void _on_CheckButton_toggled(bool button_pressed)
	{
		GD.Print("Grid button was pressed!" + button_pressed);
		HankinsOptions.showFrame = button_pressed;


	}
}
