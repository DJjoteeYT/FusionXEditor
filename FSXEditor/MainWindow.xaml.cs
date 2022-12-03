/************************************************************************
   AvalonDock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://opensource.org/licenses/MS-PL
 ************************************************************************/

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AvalonDock.Layout;
using System.Diagnostics;
using System.IO;
using AvalonDock.Layout.Serialization;
using AvalonDock;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using CTFAK;
using CTFAK.MFA;
using CTFAK.MFA.MFAObjectLoaders;
using CTFAK.Utils;
using Microsoft.Xna.Framework.Graphics;
using TestApp.Editor;
using TestApp.Editor.Rendering;
using TestApp.MonoGameStuff;
using MessageBox = System.Windows.MessageBox;

namespace TestApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static FEditor Editor;
		public FrameRendererViewModel FrameRenderer;
		public FGame SelectedGame;
		public MainWindow()
		{
			InitializeComponent();
			Core.Init();
			Settings.Build = 294;
			Settings.gameType = Settings.GameType.NORMAL;
			Core.parameters = "";
			Settings.isMFA = true;
			FrameRenderer = DataContext as FrameRendererViewModel;
			Editor = new FEditor();
			//var newMFA = new MFAData();
			//var mfaReader = new FileStream()
			//newMFA.Read();
		}

		

		


		private void DockManager_DocumentClosing(object sender, DocumentClosingEventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to close the document?", "AvalonDock Sample", MessageBoxButton.YesNo) == MessageBoxResult.No)
				e.Cancel = true;
		}


		private void LoadMFA_OnClick(object sender, RoutedEventArgs e)
		{
			var openFileDiaglog = new OpenFileDialog();
			openFileDiaglog.Filter = "CTF Application|*.mfa";
			var dlgRes = openFileDiaglog.ShowDialog();
			if (openFileDiaglog.FileName != null && File.Exists(openFileDiaglog.FileName))
			{
				SelectedGame = Editor.Load(openFileDiaglog.FileName);
				
			}
			RefreshApps();

		}

		void RefreshApps()
		{
			foreach (var game in Editor.LoadedGames)
			{
				var gameTreeItem = new TreeViewItem();
				gameTreeItem.Header = game.MfaFile.Name;
				gameTreeItem.Tag = game;
				gameTreeItem.MouseDoubleClick += OnGameDoubleClick;
				foreach (var frm in game.MfaFile.Frames)
				{
					var frameTreeItem = new TreeViewItem();
					frameTreeItem.Tag = frm;
					frameTreeItem.Header = frm.Name;
					frameTreeItem.MouseDoubleClick += OnFrameDoubleClick;
					gameTreeItem.Items.Add(frameTreeItem);
				}

				WorkspaceTree.Items.Add(gameTreeItem);
			}
		}

		private void OnFrameDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var item = ((TreeViewItem)sender).Tag;
			if (item is MFAFrame frm)
			{
				ShowFrame(frm);
			}

		}

		private void OnGameDoubleClick(object sender, MouseButtonEventArgs e)
		{
			


		}

		void ShowFrame(MFAFrame frm)
		{
			FrameRenderer.Renderables.Clear();
			foreach (var objInst in frm.Instances)
			{
				var newRenderable = new Renderable();
				newRenderable.XPos = objInst.X;
				newRenderable.YPos = objInst.Y;
				MFAObjectInfo objInfo = null;
				foreach (var objectInfo in frm.Items)
				{
					if (objInst.ItemHandle == objectInfo.Handle)
					{
						objInfo = objectInfo;
						break;
					}
				}

				int imghandle = 0;
				if (objInfo.Loader is MFAQuickBackdrop qb)
				{
					imghandle = qb.Image;
				}
				else if (objInfo.Loader is MFABackdrop bg)
				{
					imghandle = bg.Handle;
				}
				else if (objInfo.Loader is MFAActive active)
				{
					imghandle = active.Items[0].Directions[0].Frames[0];
				}

				var imgData = SelectedGame.GetImageByHandle(imghandle);

				if (imgData != null)
				{
					newRenderable.XSpot = imgData.HotspotX;
					newRenderable.YSpot = imgData.HotspotY;
					newRenderable.Image =
						SelectedGame.LoadImage(FrameRenderer.GraphicsDeviceService.GraphicsDevice, imgData);
				}
				else newRenderable.Image = new Texture2D(FrameRenderer.GraphicsDeviceService.GraphicsDevice,32,32);
				
				FrameRenderer.Renderables.Add(newRenderable);
			}
		}
		private void SaveMFA_OnClick(object sender, RoutedEventArgs e)
		{
		}
	}
}
