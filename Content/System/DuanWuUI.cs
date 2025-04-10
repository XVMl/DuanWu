using DuanWu.Content.UI;
using Luminance.Common.StateMachines;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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


        private UserInterface _testui;
        internal TestUI testUI;

        public Type[] _UIstate = [];

        public List<UserInterface> _UserInterface = new();
        //private UserInterface _playvideo;
        //internal PlayVideo playvideo;

        public override void Load()
        {
            if (Main.dedServ)
            {
                return;
            }
            _UIstate = Mod.Code.GetTypes()
                .Where(x=>x.BaseType==typeof(BaseUIState))
                .ToArray ();

            foreach (var type in _UIstate)
            {
                var _state =(BaseUIState)Activator.CreateInstance(type);
                UserInterface _userInterface = new();
                _userInterface.SetState(_state);
                _UserInterface.Add(_userInterface);
            }  
            _lisaoQustion = new UserInterface();
            lisaoQuestion = new LisaoQuestion();
            _lisaoQustion.SetState(lisaoQuestion);
            _drawHitBox = new UserInterface();
            drawHitBox = new DrawHitBox();
            _drawHitBox.SetState(drawHitBox);
            _lisao = new UserInterface();
            lisao = new Lisao();
            _lisao.SetState(lisao);
            _scoreboard = new UserInterface();
            scoreboard = new Scoreboard();
            _scoreboard.SetState(scoreboard);
            _testui = new UserInterface();
            testUI = new TestUI();
            _testui.SetState(testUI);

            //_playvideo = new UserInterface();
            //playvideo= new PlayVideo();
            //_playvideo.SetState(playvideo);

        }

        

        public override void UpdateUI(GameTime gameTime)
        {
            foreach (UserInterface type in _UserInterface)
            {
                UserInterface userInterface = type;
                userInterface?.Update(gameTime);
            }
            

            UserInterface lisaoquestion = _lisaoQustion;
            lisaoquestion?.Update(gameTime);
            UserInterface lisao = _lisao;
            lisao?.Update(gameTime);
            UserInterface score = _scoreboard;
            score?.Update(gameTime);
            UserInterface testui = _testui;
            testui?.Update(gameTime);

            //UserInterface playvideo=_playvideo;
            //playvideo?.Update(gameTime);
        }


        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            foreach (var item in _UIstate)
            {
                int Index1 = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 2"));
                layers.Insert(Index1, new LegacyGameInterfaceLayer(
                   "DuanWu:LisaoQuestion",
                   delegate
                   {
                       _lisaoQustion.Draw(Main.spriteBatch, new GameTime());
                       return true;
                   },
                   InterfaceScaleType.UI)
               );
            }

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

                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   "DuanWu:TestUI",
                   delegate
                   {
                       _testui.Draw(Main.spriteBatch, new GameTime());
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
    public abstract class BaseUIState : UIState
    {
        public virtual string TypeName { get; }

        public static readonly Dictionary<string, Type> AutoUIState = new();

        
    }
    internal class GameState : BaseUIState
    {
        public override string TypeName => "_Game";
    }
}
