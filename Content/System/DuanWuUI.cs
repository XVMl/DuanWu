using DuanWu.Content.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace DuanWu.Content.System
{
    public class DuanWuUI : ModSystem
    {
        private UserInterface _lisaoQustion;
        internal LisaoQuestion lisaoQuestion;

        private UserInterface _drawHitBox;
        internal DrawHitBox drawHitBox;

        private UserInterface _lisao;
        internal Lisao lisao;

        private UserInterface _scoreboard;
        internal Scoreboard scoreboard;

        //private UserInterface _playvideo;
        //internal PlayVideo playvideo;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                _lisaoQustion = new UserInterface();
                lisaoQuestion = new LisaoQuestion();
                _lisaoQustion.SetState(lisaoQuestion);
                _drawHitBox = new UserInterface();
                drawHitBox = new DrawHitBox();
                _drawHitBox.SetState(drawHitBox);
                _lisao=new UserInterface();
                lisao = new Lisao();
                _lisao.SetState(lisao);
                _scoreboard = new UserInterface();
                scoreboard = new Scoreboard();
                _scoreboard.SetState(scoreboard);
                //_playvideo = new UserInterface();
                //playvideo= new PlayVideo();
                //_playvideo.SetState(playvideo);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
           
            UserInterface lisaoquestion = _lisaoQustion;
            lisaoquestion?.Update(gameTime);
            UserInterface lisao = _lisao;
            lisao?.Update(gameTime);
            UserInterface score=_scoreboard;
            score?.Update(gameTime);
            //UserInterface playvideo=_playvideo;
            //playvideo?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 2"));
            if (MouseTextIndex != -1)
            {

                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "DuanWu:LisaoQuestion",
                   delegate
                   {
                       _lisaoQustion.Draw(Main.spriteBatch, new GameTime());
                       return true;
                   },
                   InterfaceScaleType.UI)
               );

                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "DuanWu:Lisao",
                   delegate
                   {
                       _lisao.Draw(Main.spriteBatch, new GameTime());
                       return true;
                   },
                   InterfaceScaleType.UI)
               );

               // layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
               //    "DuanWu:PlayVideo",
               //    delegate
               //    {
               //        _playvideo.Draw(Main.spriteBatch, new GameTime());
               //        return true;
               //    },
               //    InterfaceScaleType.UI)
               //);

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                       "DuanWu:Scoreboard",
                       delegate
                       {
                           _scoreboard.Draw(Main.spriteBatch, new GameTime());
                           return true;
                       },
                       InterfaceScaleType.UI)
                   );
                }
            }

            int MouseTextIndex1 = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 3"));
            if (MouseTextIndex1 != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "DuanWu:DrawHitBox",
                   delegate
                   {
                       _drawHitBox.Draw(Main.spriteBatch, new GameTime());
                       return true;
                   },
                   InterfaceScaleType.UI)
               );
            }

        }

    }
}
