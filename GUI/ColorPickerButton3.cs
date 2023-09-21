using Godot;
using System;

public class ColorPickerButton3 : ColorPickerButton
{
	public override void _Ready()
	{
		ColorPickerButton node = GetNode<ColorPickerButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer7/ColorPickerButton3");
		node.Color = HankinsOptions.getHankinsOptions().colors[2];
	}

	private void _on_ColorPickerButton3_color_changed(Color color)
	{
        HankinsOptions.getHankinsOptions().colors[2] = color;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}



