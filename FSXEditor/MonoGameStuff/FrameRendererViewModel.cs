using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.WpfCore.MonoGameControls;
using TestApp.Editor.Rendering;

namespace TestApp.MonoGameStuff;

public class FrameRendererViewModel : MonoGameViewModel
{

    private SpriteBatch _spriteBatch;
    
    public Camera2D Camera = new Camera2D();
    public List<Renderable> Renderables = new List<Renderable>();
    public Vector2 FrameSize;
    public Color FrameBackground = Color.White;
    public Texture2D frameBackgroundTexture;

    public override void LoadContent()
    {

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        frameBackgroundTexture = new Texture2D(GraphicsDevice,1,1);
        frameBackgroundTexture.SetData(new[]{Color.White});
    }

    public override void Update(GameTime gameTime)
    {
        
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied,
            null, null, null, null, Camera.GetTransform());
        
        _spriteBatch.Draw(frameBackgroundTexture,new Rectangle(0,0,(int)FrameSize.X,(int)FrameSize.Y),null,FrameBackground);
        
        foreach (var renderable in Renderables)
        {
            _spriteBatch.Draw(renderable.Image, new Vector2(renderable.XPos,renderable.YPos), null, Color.White, 0, new Vector2(renderable.XSpot,renderable.YSpot), new Vector2(1,1), SpriteEffects.None, 0f);

        }
        
        _spriteBatch.End();
    }
}
