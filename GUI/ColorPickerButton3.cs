using Godot;
using System;

public class ColorPickerButton3 : ColorPickerButton
{
	public override void _Ready()
	{
		this.Color = Colors.Blue;
		_on_ColorPickerButton3_color_changed(this.Color);
	}

	private void _on_ColorPickerButton3_color_changed(Color color)
	{
        HankinsOptions.colors[1] = color;
	}
}



