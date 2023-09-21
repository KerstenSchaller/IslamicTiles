using Godot;
using System;

public class ColorPickerButton2 : ColorPickerButton
{
	public override void _Ready()
	{
		ColorPickerButton node = GetNode<ColorPickerButton>("/root/Node2D/GUI/VBoxContainer/HBoxContainer7/ColorPickerButton2");
		node.Color = HankinsOptions.getHankinsOptions().colors[1];
	}

	private void _on_ColorPickerButton2_color_changed(Color color)
	{
        HankinsOptions.getHankinsOptions().colors[1] = color;
		HankinsOptions.getHankinsOptions().SerializeNow();
	}
}



