using DuanWu.Content.UI;
using Luminance.Common.StateMachines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace DuanWu.Content.System
{
    public class DuanWuUI : ModSystem
    {

        public static Type[] _UIstate = [];

        public static Dictionary<BaseUIState, UserInterface> keyValuePairs = new();

        public List<UserInterface> _UserInterface = new();


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
                BaseUIState _state =(BaseUIState)Activator.CreateInstance(type,null);
                UserInterface _userInterface = new();
                _userInterface.SetState(_state);
                _UserInterface.Add(_userInterface);
                keyValuePairs[_state] = _userInterface;
            }  

        }

        public override void UpdateUI(GameTime gameTime)
        {
            foreach (UserInterface type in _UserInterface)
            {
                UserInterface userInterface = type;
                userInterface?.Update(gameTime);
            }

        }


        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            foreach (var item in keyValuePairs)
            {
                if (!item.Key.IsLoaded())
                {
                    continue;
                }
                int Index1 = layers.FindIndex(layer => layer.Name.Equals(item.Key.Layers_FindIndex));
                layers.Insert(Index1, new LegacyGameInterfaceLayer(
                   "DuanWu:" + item.Key.ToString(),
                   delegate
                   {
                       item.Value.Draw(Main.spriteBatch, new GameTime());
                       return true;
                   },
                   InterfaceScaleType.UI)
               );
            }

        }
    }

    /// <summary>
    /// 通过反射自动查找继承它的子类，然后在ModSystem中自动注册
    /// </summary>
    public abstract class BaseUIState : UIState
    {
        /// <summary>
        /// 字典中子类注册名
        /// </summary>
        public virtual string TypeName { get; }
        /// <summary>
        /// 是否加载此UI
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLoaded() => true; 
        /// <summary>
        /// 此UI绘制图层的位置
        /// </summary>
        public abstract string Layers_FindIndex { get; }

        public static Asset<Texture2D> BaseTexture(string path) => ModContent.Request<Texture2D>("DuanWu/Content/UI/"+path); 
    }

}
