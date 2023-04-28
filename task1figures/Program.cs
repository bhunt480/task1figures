using System;
using System.Collections;

public class Program
{
    public static void Main(string[] args)
    {
        Point p1 = new Point(0, 1);//new point
        //move point
        p1.Move(0, 1);
        p1.Print();
        
        //clockwise 90 degree rotation
        p1.Rotate(-Math.PI/2, 0 ,0);
        p1.Print();
        
        //anticlockwise 45 degree rotation
        p1.Rotate(Math.PI/4, 0 ,0);
        p1.Print();
        
        //own point rotation (pointless because point cannot rotate
        p1.Rotate(Math.PI, p1.mx, p1.my);
        p1.Print();
        
        //Line with gradient 1, y intercept 0
        Line l1 = new Line(0, 0, 1, 1);
        l1.Move(1, 0);//shift line to the right
        l1.Print();
        
        l1.Move(-1, 0);//shift line to the left(origin)
        l1.Print();
        
        l1.Rotate(Math.PI, 0, 0);// 180 rotation on origin (end point shift)
        l1.Print();

        //180 degree line rotation on its midpoint
        Point limp = l1.GetMidpoint();
        l1.Rotate(Math.PI, limp.mx, limp.my);
        l1.Print();
        
        //Circle
        Circle c1 = new Circle(1, 1, 1);//Circle with radius point on (1, 1) with radius length 1
        c1.Print();
        c1.Move(-2, -2);//move circle left downward
        c1.Print();
        
        c1.Rotate(Math.PI, 0, 0);
        c1.Print();
        
        c1.Rotate(Math.PI, c1.mc.mx, c1.mc.my); //circle rotation on it radius point (pointless, does not change anything)
        c1.Print();
        
        //Aggregation
        Aggregate a1 = new Aggregate();
        a1.addShape(new Point(1, 2));//Add point in Aggregation
        a1.addShape(new Line(1, 1, 2, 2));//Add line in Aggregation
        a1.addShape(new Circle(1, 1, 1));//Add Circle in Aggregation
        a1.Print();
        
        a1.Move(1, 1);//move all shapes to the right upward
        a1.Print();
        a1.Move(-1, -1);//move all shapes to the left downward
        a1.Print();
        
        a1.Rotate(Math.PI, 0, 0);//rotate all shapes
        a1.Print();
    }
}

public abstract class Shape
{
    public abstract void Move(double tx, double ty);//relocation function
    public abstract void Rotate(double rt, double rx, double ry);//rotation of figures
    public abstract void Print();//display the state of figures
}

//Point Class
public class Point : Shape
{
    public double mx, my;

    public Point(double x, double y)
    {
        this.mx = x;
        this.my = y;
    }

    //x and y axis movement of point
    public override void Move(double tx, double ty)
    {
        this.mx += tx;
        this.my += ty;
    }

    public override void Rotate(double rt, double rx, double ry)
    {
        double dx = this.mx - rx;
        double dy = this.my - ry;
        double cos = Math.Cos(rt);
        double sin = Math.Sin(rt);

        this.mx = Math.Round((dx * cos) - (dy * sin) + rx, 5);
        this.my = Math.Round((dx * sin) + (dy * cos) + ry, 5);
    }

    public override void Print()
    {
        Console.WriteLine("Point is located at ({0}, {1})", this.mx, this.my);
    }
}

//Line Class
public class Line : Shape
{   
    //represent 2 points
    public Point ms, me;

    public Line(double sx, double sy, double ex, double ey)
    {
        this.ms = new Point(sx, sy);
        this.me = new Point(ex, ey);
    }

    public Point GetMidpoint()
    {
        return new Point((ms.mx + me.mx) / 2, (ms.my + me.my) / 2);
    }

    //movement of 2 points/a line
    public override void Move(double tx, double ty)
    {
        this.ms.Move(tx, ty);
        this.me.Move(tx, ty);
    }
    
    //rotation of line
    public override void Rotate(double rt, double rx, double ry)
    {
        this.ms.Rotate(rt, rx, ry);
        this.me.Rotate(rt, rx, ry);
    }
    
    public override void Print()
    {
        Console.WriteLine("Line that represent two points: ");
        Console.Write("Start");
        this.ms.Print();
        Console.Write("End");
        this.me.Print();
    }
}

//Circle class
public class Circle : Shape
{
    public Point mc;//center point
    public double mr;// radius

    public Circle(double cx, double cy, double cr)
    {
        this.mc = new Point(cx, cy);
        this.mr = cr;
    }

    //movement of circle
    public override void Move(double tx, double ty)
    {
        this.mc.Move(tx, ty);
    }

    //rotation of circle
    public override void Rotate(double rt, double rx, double ry)
    {
        this.mc.Rotate(rt, rx, ry);
    }

    public override void Print()
    {
        Console.Write("circle or radius {0} with center ", this.mr);
        this.mc.Print();
    }
}

//Aggregation
public class Aggregate : Shape
{
    //Array of Shapes
    public ArrayList ma;

    public Aggregate()
    {
        this.ma = new ArrayList();
    }

    //Add Shapes
    public void addShape(Shape ns)
    {
        this.ma.Add(ns);
    }
    
    //Move all Shapes
    public override void Move(double tx, double ty)
    {
        foreach (Shape s in this.ma)
        {
            s.Move(tx, ty);
        }
    }
    
    //Rotate all Shapes
    public override void Rotate(double rt, double rx, double ry)
    {
        foreach (Shape s in this.ma)
        {
            s.Rotate(rt, rx, ry);
        }
    }

    //Display all state of Shapes
    public override void Print()
    {
        foreach (Shape s in this.ma)
        {
            s.Print();
        }
    }
}