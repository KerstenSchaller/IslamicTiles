using Godot;
using System;

public class SinglePolyButton : CheckButton
{
	private void _on_CheckButton_toggled(bool button_pressed)
	{
		GD.Print("Single Poly button was pressed!" + button_pressed);
		HankinsOptions.printSingleTileOnly = button_pressed;
	}
}

