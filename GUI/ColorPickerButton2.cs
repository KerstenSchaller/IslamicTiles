using Godot;
using System;

public class ColorPickerButton2 : ColorPickerButton
{

	public override void _Ready()
	{
		this.Color = Colors.Green;
		_on_ColorPickerButton2_color_changed(this.Color);
	}
	private void _on_ColorPickerButton2_color_changed(Color color)
	{
        HankinsOptions.colors[3] = color;
	}
}



