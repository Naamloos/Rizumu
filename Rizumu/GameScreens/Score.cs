/*
 * Screen for displaying scores.
 * Not a lot to improve here.
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;

namespace Rizumu.GameScreens
{
    class Score
    {
        static string rank = "";
        public static void draw(SpriteBatch spriteBatch)
        {
            GameResources.scorebackmouse = Mouse.GetState();
            rank = RankGen.generate((GameResources.fscore + GameResources.gscore + GameResources.hscore + GameResources.jscore), GameResources.totalnotes);
            MouseState mstate = Mouse.GetState();
            Background bg = new Background(spriteBatch, GameResources.songbg);
            bg.draw();
            if (rank == "ss")
            {
                Sprite rank = new Sprite(spriteBatch, 30, 30, GameResources.SSRank, GameResources.basecolor);
                rank.draw();
            }
            if (rank == "s")
            {
                Sprite rank = new Sprite(spriteBatch, 30, 30, GameResources.SRank, GameResources.basecolor);
                rank.draw();
            }
            if (rank == "a")
            {
                Sprite rank = new Sprite(spriteBatch, 30, 30, GameResources.ARank, GameResources.basecolor);
                rank.draw();
            }
            if (rank == "b")
            {
                Sprite rank = new Sprite(spriteBatch, 30, 30, GameResources.BRank, GameResources.basecolor);
                rank.draw();
            }
            if (rank == "c")
            {
                Sprite rank = new Sprite(spriteBatch, 30, 30, GameResources.CRank, GameResources.basecolor);
                rank.draw();
            }
            if (rank == "d")
            {
                Sprite rank = new Sprite(spriteBatch, 30, 30, GameResources.DRank, GameResources.basecolor);
                rank.draw();
            }

            int screenheight = Game1.graphics.PreferredBackBufferHeight;
            int screenwidth = Game1.graphics.PreferredBackBufferWidth;
            int drawloc = screenwidth - GameResources.ScoreBack.Width;
            int drawtimes = (screenheight / 10) + 1;
            int i = 0;
            while (i < drawtimes)
            {
                Sprite scoreback = new Sprite(spriteBatch, drawloc, i * 10, GameResources.ScoreBack, GameResources.basecolor);
                scoreback.draw();
                i++;
            }

            Sprite scoreleft = new Sprite(spriteBatch, drawloc + 15, (GameResources.ScoreMiss.Height * 2) * 0, GameResources.ScoreLeft, GameResources.basecolor);
            scoreleft.scale = 1.6f;
            scoreleft.draw();

            Sprite scoreright = new Sprite(spriteBatch, drawloc + 15, (GameResources.ScoreMiss.Height * 2) * 1, GameResources.ScoreRight, GameResources.basecolor);
            scoreright.scale = 1.6f;
            scoreright.draw();

            Sprite scoreup = new Sprite(spriteBatch, drawloc + 15, (GameResources.ScoreMiss.Height * 2) * 2, GameResources.ScoreUp, GameResources.basecolor);
            scoreup.scale = 1.6f;
            scoreup.draw();

            Sprite scoredown = new Sprite(spriteBatch, drawloc + 15, (GameResources.ScoreMiss.Height * 2) * 3, GameResources.ScoreDown, GameResources.basecolor);
            scoredown.scale = 1.6f;
            scoredown.draw();

            Sprite scoremiss = new Sprite(spriteBatch, drawloc + 15, (GameResources.ScoreMiss.Height * 2) * 4, GameResources.ScoreMiss, GameResources.basecolor);
            scoremiss.scale = 1.6f;
            scoremiss.draw();

            Sprite scorecombo = new Sprite(spriteBatch, drawloc + 15, (GameResources.ScoreMiss.Height * 2) * 5, GameResources.ScoreCombo, GameResources.basecolor);
            scorecombo.scale = 1.6f;
            scorecombo.draw();

            int totalmiss = GameResources.fmiss + GameResources.gmiss + GameResources.hmiss + GameResources.jmiss;
            Text.draw(GameResources.font, "Left: " + GameResources.fscore.ToString(), drawloc + 150, ((GameResources.ScoreMiss.Height * 2) * 0) + 20, spriteBatch);
            Text.draw(GameResources.font, "Up: " + GameResources.gscore.ToString(), drawloc + 150, ((GameResources.ScoreMiss.Height * 2) * 2) + 20, spriteBatch);
            Text.draw(GameResources.font, "Right: " + GameResources.hscore.ToString(), drawloc + 150, ((GameResources.ScoreMiss.Height * 2) * 1) + 20, spriteBatch);
            Text.draw(GameResources.font, "Down: " + GameResources.jscore.ToString(), drawloc + 150, ((GameResources.ScoreMiss.Height * 2) * 3) + 20, spriteBatch);
            Text.draw(GameResources.font, "Miss: " + totalmiss.ToString(), drawloc + 150, ((GameResources.ScoreMiss.Height * 2) * 4) + 20, spriteBatch);
            Text.draw(GameResources.font, "Combo: " + GameResources.combo.ToString(), drawloc + 150, ((GameResources.ScoreMiss.Height * 2) * 5) + 20, spriteBatch);

            Sprite backbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 100, GameResources.Button, GameResources.basecolor);
            if (backbtn.hitbox.Intersects(Game1.cursorbox))
            {
                if (mstate.LeftButton == ButtonState.Pressed)
                {
                    GameResources.GameScreen = 1;
                    MapScreen.loaded = false;
                    MapScreen.timer = 0;
                }
                backbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 100, GameResources.ButtonSelected, GameResources.basecolor);
            }
            else
            {
                backbtn = new Sprite(spriteBatch, 0, Game1.graphics.PreferredBackBufferHeight - 100, GameResources.Button, GameResources.basecolor);
            }
            backbtn.draw();
            Text.draw(GameResources.font, "Back", 10, Game1.graphics.PreferredBackBufferHeight - 70, spriteBatch);
        }
    }
}
