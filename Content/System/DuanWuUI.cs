using DuanWu.Content.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DuanWu.Content.System
{
    public class DuanWuUI:ModSystem
    {
        private UserInterface _lisaoQAInterface;
        internal LisaoChoice lisaoQA;

        private UserInterface _lisaoQustion;
        internal LisaoQuestion lisaoQuestion;

        private UserInterface _drawHitBox;
        internal DrawHitBox drawHitBox;
        public override void Load()
        {
            _lisaoQAInterface=new UserInterface();
            lisaoQA = new LisaoChoice();
            _lisaoQAInterface.SetState(lisaoQA);
            _lisaoQustion=new UserInterface();
            lisaoQuestion = new LisaoQuestion();
            _lisaoQustion.SetState(lisaoQuestion);
            _drawHitBox = new UserInterface();
            drawHitBox=new DrawHitBox();
            _drawHitBox.SetState(drawHitBox);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            UserInterface lisaointerface = _lisaoQAInterface;
            lisaointerface?.Update(gameTime);
            UserInterface lisaoquestion = _lisaoQustion;
            lisaoquestion?.Update(gameTime);



        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 2"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "DuanWu:LisaoQA",
                   delegate
                   {
                       _lisaoQAInterface.Draw(Main.spriteBatch, new GameTime());
                       return true;
                   },
                   InterfaceScaleType.UI)
               );

                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "DuanWu:LisaoQuestion",
                   delegate
                   {
                       _lisaoQustion.Draw(Main.spriteBatch, new GameTime());
                       return true;
                   },
                   InterfaceScaleType.UI)
               );

                
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
