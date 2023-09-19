using Godot;
using System;

using System.Collections.Generic; //for list

public interface IShape
{
    double getSideLength();

    int getNumberOfVertices();

}

public class Shape : Node2D
{
        public List<Vector2> vertices;
    	protected Polygon polygon = new Polygon();
	    protected int scale;

    public void init(int numberOfVertices,int _scale, Vector2[] startLine = null)
	{
		scale = _scale;
		if(startLine == null)startLine = new Vector2[]{new Vector2(0,0),new Vector2(scale, 0)};


		this.AddChild(polygon);

		vertices = PolyHelper.CreatePolygonVertices(startLine, numberOfVertices, scale);
		polygon.init(vertices.ToArray());
	}

    public Vector2[] getInvertedVertice(int index)
    {
        //return new Vector2[]{ vertices[index], vertices[index+1]};
        return new Vector2[]{ vertices[index+1], vertices[index]};
    }
}

public class Tesselator : Node2D
{

    int width;
    int height;

    IPattern pattern;

    Vector2 originShift;
    int scale = 1;

    List<ILine> polygonSides = new List<ILine>();
    List<ILine> hankinslines = new List<ILine>();

    List<Vector2[]> polys = new List<Vector2[]>();
    List<int> polyColorIndex = new List<int>();
    public void addPolys(List<Vector2[]> _polys, int colorIndex)
    {
        polys = _polys;
        foreach (var p in _polys)
        {
            polyColorIndex.Add(colorIndex);
        }
    }

    public void addPoly(Vector2[] _poly, int colorIndex)
    {
        polys.Add(_poly);
        polyColorIndex.Add(colorIndex);

    }

    public void clearPolys()
    {
        polys.Clear();
        polyColorIndex.Clear();
    }

    public void addPolygonSide(PolygonSide polySide)
    {
        polygonSides.Add(polySide);
    }

    public void clearPolygonSides()
    {
        polygonSides.Clear();
    }

    public void addHankinsline(HankinLine polySide)
    {
        hankinslines.Add(polySide);
    }

    public void clearHankinLines()
    {
        hankinslines.Clear();
    }

    void getScreenSize()
    {
        var size = GetViewport().Size;
        width = (int)((size.x / 100) + 5);
        height = (int)((size.y / 100) + 8);

        if (HankinsOptions.printSingleTileOnly)
        {
            width = 1;
            height = 1;
            originShift = new Vector2(300, 300);
            scale = 2;
        }
        else
        {
            originShift = new Vector2(-100, -100);
            scale = 1;
        }
    }

    public void init()
    {
        getScreenSize();
        switch (HankinsOptions.shape)
        {
            case HankinsOptions.Shapes.Square:
                pattern = new SquarePattern();
                AddChild((SquarePattern)pattern);
                ((SquarePattern)pattern).init(100);
                break;
            case HankinsOptions.Shapes.Hexagon:
                pattern = new HexagonPattern();
                AddChild((HexagonPattern)pattern);
                break;
            case HankinsOptions.Shapes.MultiTile:
                pattern = new MultiTilePattern();
                AddChild((MultiTilePattern)pattern);
                break;
           case HankinsOptions.Shapes.Triangle:
                pattern = new TrianglePattern();
                AddChild((TrianglePattern)pattern);
                ((TrianglePattern)pattern).init(100);
                break;
            default:
                GD.Print("No valid pattern");
            break;
        }
    }

    public override void _Ready()
    {
        
        this.AddToGroup("Tesselator");
    }

    public Tesselator()
    {
        this.AddToGroup("Tesselator");
    }

    void deleteChildrenRecursively(Node node)
    {
        foreach(Node n in node.GetChildren())
        {
            deleteChildrenRecursively(n);
        }
        node.QueueFree();
    }

    void reset()
    {
        clearPolys();
        clearHankinLines();
        clearPolygonSides();
        foreach(Node n in this.GetChildren())
        {
            deleteChildrenRecursively(n);
        }
        pattern = null;
        init();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(HankinsOptions.resetRequest == true)
        {
            HankinsOptions.resetRequest = false;
            reset();
        }



        var size = GetViewport().Size;
        width = 50;
        height = 15;
        Update();
    }

    public override void _Draw()
    {
        if (polys.Count == 0) return;
        getScreenSize();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Draw all polygonlines multiple times over the plane 
                if (HankinsOptions.showPoly)
                {
                    // Draw all hankinslines multiple times over the plane 
                    Vector2[] tPoly;
                    for (int i = 0; i < polys.Count; i++)
                    {
                        var offset = originShift + new Vector2(x * (float)pattern.getXDist() + (y % 2) * (float)pattern.getXOffset(), y * (float)pattern.getYDist() + (x % 2) * (float)pattern.getYOffset());
                        tPoly = new Vector2[polys[i].Length];
                        for (int j = 0; j < polys[i].Length; j++)
                        {
                            tPoly[j] = scale * polys[i][j] + offset;
                        }
                        DrawPolygon(tPoly, new Color[] { HankinsOptions.colors[polyColorIndex[i] + 1] });
                    }

                }

                if (true)
                {
                    // Draw all hankinslines multiple times over the plane 
                    foreach (var p in hankinslines)
                    {
                        var offset = originShift + new Vector2(x * (float)pattern.getXDist() + (y % 2) * (float)pattern.getXOffset(), y * (float)pattern.getYDist() + (x % 2) * (float)pattern.getYOffset());

                        var start = (scale * p.Start + offset);
                        var end = (scale * p.End + offset);
                        DrawLine(start, end, HankinsOptions.colors[0], 1);
                    }
                }

                if (HankinsOptions.showFrame)
                {
                    foreach (var p in polygonSides)
                    {
                        var offset = originShift + new Vector2(x * (float)pattern.getXDist() + (y % 2) * (float)pattern.getXOffset(), y * (float)pattern.getYDist() + (x % 2) * (float)pattern.getYOffset());

                        var start = (scale * p.Start + offset);
                        var end = (scale * p.End + offset);
                        DrawLine(start, end, Colors.White);
                
                        
                    }

                }


            }
        }
        //clearPolys();
        //clearHankinLines();
        //clearPolygonSides();


    }
}
