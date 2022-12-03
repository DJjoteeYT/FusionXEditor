using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.WpfCore.MonoGameControls;
using TestApp.Editor.Rendering;
using Cursor = System.Windows.Forms.Cursor;
using Cursors = System.Windows.Forms.Cursors;
using Point = System.Windows.Point;

namespace TestApp.MonoGameStuff;

public class FrameRendererViewModel : MonoGameViewModel
{

    private SpriteBatch _spriteBatch;
    
    public Camera2D Camera = new Camera2D();
    public List<Renderable> Renderables = new List<Renderable>();
    public Vector2 FrameSize;
    public Color FrameBackground = Color.White;
    public Texture2D frameBackgroundTexture;
    public Point mouse;
    public MainWindow parent;

    public override void LoadContent()
    {

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        frameBackgroundTexture = new Texture2D(GraphicsDevice,1,1);
        frameBackgroundTexture.SetData(new[]{Color.White});
    }

    public override void Update(GameTime gameTime)
    {
        bool foundHover = false;
        foreach (var renderable in Renderables)
        {
            if (renderable.GetBounds(Camera.GetTransform()).Contains(new Microsoft.Xna.Framework.Point((int)mouse.X, (int)mouse.Y)))
            {
                foundHover = true;
                break;
            }
        }
        if(foundHover)
            Cursor.Current=Cursors.Hand;
        else Cursor.Current=Cursors.Default;
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        var cameraTransform = Camera.GetTransform();
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
            null, null, null, null, cameraTransform);
        
        _spriteBatch.Draw(frameBackgroundTexture,new Rectangle(0,0,(int)FrameSize.X,(int)FrameSize.Y),null,FrameBackground);

        foreach (var renderable in Renderables)
        {
            _spriteBatch.Draw(renderable.Image, new Vector2(renderable.XPos, renderable.YPos), null, Color.White, 0,
                new Vector2(renderable.XSpot, renderable.YSpot), new Vector2(1, 1), SpriteEffects.None, 0f);
            Console.WriteLine(renderable.GetBounds(Camera.GetTransform()));
            //_spriteBatch.Draw(frameBackgroundTexture, , null, Color.Black);






        }


        _spriteBatch.End();
        
    }
    static Microsoft.Xna.Framework.Point Transform(Microsoft.Xna.Framework.Point point, Matrix matrix)
    {
        var vector = point.ToVector2();
        var transformedVector = Vector2.Transform(vector, matrix);
        return transformedVector.ToPoint();
    }
}
