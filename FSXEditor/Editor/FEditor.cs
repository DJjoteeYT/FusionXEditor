using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TestApp.Editor;

public class FEditor
{
    public List<FGame> LoadedGames = new List<FGame>();

    public FGame Load(string path)
    {
        var game = FGame.FromMFAFile(path);
        LoadedGames.Add(game);
        return game;
    }

    public void Unload(FGame game)
    {
        LoadedGames.Remove(game);
    }

    
}