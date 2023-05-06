public interface IPattern
{
    double getXDist();
    double getYDist();
    double getXOffset();
    double getYOffset();
}

public class Pattern : IPattern
{
    double xDist;
    double yDist;
    double xOffset;
    double yOffset;
    public void setXDist(double dist){xDist = dist;}
    public void setYDist(double dist){yDist = dist;}
    public void setXOffset(double offset){xOffset = offset;}
    public void setYOffset(double offset){yOffset = offset;}

    public double getXDist(){return xDist;}
    public double getYDist(){return yDist;}
    public double getXOffset(){return xOffset;}
    public double getYOffset(){return yOffset;}
}