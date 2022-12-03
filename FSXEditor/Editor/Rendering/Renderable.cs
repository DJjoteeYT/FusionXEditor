using System;
using System.Windows;
using CTFAK.MFA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Point = System.Windows.Point;

namespace TestApp.Editor.Rendering;

public class Renderable
{
    public int XPos;
    public int YPos;
    public float XSpot;
    public float YSpot;
    public Texture2D Image;
    public object Payload;
    
    static Microsoft.Xna.Framework.Point Transform(Microsoft.Xna.Framework.Point point, Matrix matrix)
    {
        var vector = point.ToVector2();
        var transformedVector = Vector2.Transform(vector, matrix);
        return transformedVector.ToPoint();
    }
    public Rectangle GetBounds(Matrix cameraTransform)
    {
       

        
        var begin = Transform(new Microsoft.Xna.Framework.Point((int)(XPos - XSpot),
            (int)(YPos - YSpot)), cameraTransform);
        var end = Transform(new Microsoft.Xna.Framework.Point((int)(XPos - XSpot+Image.Width),
            (int)(YPos - YSpot+Image.Height)), cameraTransform);
        var size = end - begin;
        return new Rectangle(begin, size);
    }
}
