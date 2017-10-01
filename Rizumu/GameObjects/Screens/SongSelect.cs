﻿using Rizumu.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rizumu.GuiObjects;
using Rizumu.Objects;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Rizumu.Enums;

namespace Rizumu.GameObjects.Screens
{
    class SongSelect : IScreen
    {
        public Background Background;
        public List<MapData> MapDatas;
        public Button BackButton;
        public Button PlayButton;
        public Text MapInfo;
        public Text ModCollection;
        public ModSelector MSelector;
        public int ScreenWidth;

        public Screen Name => Screen.Select;

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle cursor, bool clicked, GraphicsDevice g)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
                scrollac = new Random().Next((MapDatas.Count * -4),
                    (MapDatas.Count * 4));
            Background.Draw(spriteBatch);
            foreach (MapData m in MapDatas)
            {
                if (m.Y > (m.MapDataHolder.Texture.Height * 2 + 50) - 1 && m.Y < (m.MapDataHolder.Texture.Height * 2 + 50) + m.MapDataHolder.Texture.Height)
                {
                    m.Selected = true;
                    var map = GameData.MapManager.Maps.Find(x => x.MD5 == m.MapMD5);
                    GameData.MapManager.Current = map;
                    MapInfo.Content = map.Description;
                    GameData.MusicManager.Change(GameData.MapManager.Current);
                }
                else
                    m.Selected = false;
                m.Draw(spriteBatch);
            }
            BackButton.Draw(spriteBatch, cursor, clicked);
            PlayButton.Draw(spriteBatch, cursor, clicked);
            MapInfo.Draw(spriteBatch);
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                MSelector.Draw(spriteBatch);
            }
            else
            {
                ModCollection.Content = GameData.Instance.Mods.GetCollectionString();
                ModCollection.X = (ScreenWidth - ModCollection.Width) - 5;
                ModCollection.Draw(spriteBatch);
            }
        }

        public void Preload(SpriteBatch spriteBatch, GraphicsDeviceManager Graphics)
        {
            Background = new Background(GameData.Instance.CurrentSkin.MenuBackground, Color.White, GameData.globalwidth, GameData.globalheight);
            int BarWidth = GameData.Instance.CurrentSkin.SongBar.Width;
            int BarHeight = GameData.Instance.CurrentSkin.SongBar.Height;
            int index = new Random().Next(0, GameData.MapManager.Maps.Count - 1);
            int Y = -((BarHeight + 25) * (index - 3));
            MapDatas = new List<MapData>();
            foreach (Map m in GameData.MapManager.Maps)
            {
                MapDatas.Add(new MapData((GameData.globalwidth / 2) - (BarWidth / 2), Y, m.Name, m.Creator, false, m.MD5));
                Y += BarHeight + 25;
            }

            GameData.MusicManager.Change(MapDatas[index].MapMD5);

            BackButton = new Button(25, GameData.globalheight - GameData.Instance.CurrentSkin.Button.Height - 25,
                GameData.Instance.CurrentSkin.Button, GameData.Instance.CurrentSkin.ButtonHover, "Back");
            BackButton.OnClick += (sender, e) =>
            {
                GameData.Instance.CurrentScreen = Screen.Main;
            };

            PlayButton = new Button(25,
                GameData.globalheight - GameData.Instance.CurrentSkin.Button.Height * 2 - 35, GameData.Instance.CurrentSkin.Button,
                GameData.Instance.CurrentSkin.ButtonHover, "Play");
            PlayButton.OnClick += (sender, e) =>
            {
                GameData.Instance.CurrentScreen = Screen.Ingame;
            };
            MapInfo = new Text(GameData.Instance.CurrentSkin.FontSmall, "mapinfo", 25, 25, Color.White);
            ModCollection = new Text(GameData.Instance.CurrentSkin.FontSmall, "", GameData.globalwidth, 5, Color.Azure);
            MSelector = new ModSelector(GameData.globalwidth, GameData.globalheight);
            ScreenWidth = GameData.globalwidth;
        }

        int osv;
        int scrollac = 0;
        public void Update(GameTime gameTime, Rectangle cursor, bool clicked)
        {
            #region ScrollShit
            if (osv > Mouse.GetState().ScrollWheelValue && MapDatas.Last().Y > MapDatas.Last().MapDataHolder.Texture.Height * 2 + 70)
            {
                scrollac -= 15;
            }
            else if (osv < Mouse.GetState().ScrollWheelValue && MapDatas.First().Y < MapDatas.First().MapDataHolder.Texture.Height * 2 + 70)
            {
                scrollac += 15;
            }
            osv = Mouse.GetState().ScrollWheelValue;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.LeftShift) && MapDatas.First().Y < MapDatas.First().MapDataHolder.Texture.Height * 2 + 70)
            {
                scrollac = 2;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.LeftShift) && MapDatas.Last().Y > MapDatas.Last().MapDataHolder.Texture.Height * 2 + 70)
            {
                scrollac = -2;
            }

            foreach (MapData m in MapDatas)
            {
                m.Y += scrollac;
            }

            if (scrollac < 0 && !(MapDatas.Last().Y < MapDatas.Last().MapDataHolder.Texture.Height * 2 + 70))
            {
                scrollac++;
            }
            else if (scrollac > 0 && !(MapDatas.First().Y > MapDatas.First().MapDataHolder.Texture.Height * 2 + 70))
            {
                scrollac--;
            }
            else
            {
                scrollac = 0;
            }

            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (var t in touchCollection)
            {
                float x = t.Position.X;
                float y = t.Position.Y;

                if (y > GameData.globalheight / 2)
                {
                    scrollac -= 5;
                }
                else
                {
                    scrollac += 5;
                }
            }

            if (scrollac == 0)
            {
                while (!MapDatas.Any(x => x.Selected))
                {
                    if (MapDatas.Last().Y > MapDatas.Last().MapDataHolder.Texture.Height * 2 + 70)
                    {
                        foreach (MapData m in MapDatas)
                        {
                            m.Y--;
                            if (m.Y > (m.MapDataHolder.Texture.Height * 2 + 50) - 1 && m.Y < (m.MapDataHolder.Texture.Height * 2 + 50) + m.MapDataHolder.Texture.Height)
                                m.Selected = true;
                            else
                                m.Selected = false;
                        }
                    }
                    else
                    {
                        foreach (MapData m in MapDatas)
                        {
                            m.Y++;
                            if (m.Y > (m.MapDataHolder.Texture.Height * 2 + 50) - 1 && m.Y < (m.MapDataHolder.Texture.Height * 2 + 50) + m.MapDataHolder.Texture.Height)
                                m.Selected = true;
                            else
                                m.Selected = false;
                        }
                    }
                }
                if (GameData.MapManager.Current?.Background != null)
                    Background.Texture = GameData.MapManager.Current.Background;
            }
            #endregion
        }
    }
}
