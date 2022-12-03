using System;
using System.IO;
using System.Runtime.InteropServices;
using CTFAK.CCN.Chunks.Banks;
using CTFAK.Memory;
using CTFAK.MFA;
using CTFAK.Utils;
using Microsoft.Xna.Framework.Graphics;
using TestApp.Editor.Utils;

namespace TestApp.Editor;

public class FGame
{
    public MFAData MfaFile;

    public static FGame FromMFAData(MFAData mfa)
    {
        var newGame = new FGame();
        newGame.MfaFile = mfa;
        return newGame;
    }
    public static FGame FromMFAFile(string filePath)
    {
        var fileReader = new FileStream(filePath, FileMode.Open);
        var newMFA = new MFAData();
        newMFA.Read(new ByteReader(fileReader));
        var newGame = FromMFAData(newMFA);
        fileReader.Close();
        fileReader.Dispose();
        return newGame;
    }
    
    public Image GetImageByHandle(int handle)
    {
        if (MfaFile.Images.Items.ContainsKey(handle))
            return MfaFile.Images.Items[handle];
        return null;
    }

    public Texture2D LoadImage(GraphicsDevice device,Image fusionImg)
    {
        var newTex = new Texture2D(device,fusionImg.Width,fusionImg.Height);
        var newData = ImageLoader.TranslateImage(fusionImg.Width, fusionImg.Height, fusionImg.imageData, fusionImg.Transparent,
            fusionImg.Flags["Alpha"]);
        newTex.SetData(newData);
        return newTex;
    }
}