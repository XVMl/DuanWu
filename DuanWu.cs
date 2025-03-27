using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using Microsoft.Xna.Framework;
using DuanWu.Content.System;
using Terraria.ID;
using System.Reflection;
using MonoMod.Utils;

namespace DuanWu
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	
    public class DuanWu : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            string type = reader.ReadString();
            if (NetTool.PacketHandlers.TryGetValue(type,out var handlerType))
            {
                NetTool handler = (NetTool)Activator.CreateInstance(handlerType);
                handler.RecievePacket(reader,whoAmI);
            }
            //switch (type)
            //{
            //    case "NetNPC":
            //        NetNPC.HandlePacket(reader, whoAmI);
            //        break;
            //    case "NetProjectlies":
            //        NetProjectlies.HandlePacket(reader, whoAmI);
            //        break;
            //    case "NetTime":
            //        NetTime.HandlePacket(reader, whoAmI);
            //        break;
            //    case "Netsponse":
            //        Netsponse.HandlePacket(reader, whoAmI);
            //        break;
            //    case "NetScoreboard":
            //        NetScoreboard.HandlePacket(reader, whoAmI);
            //        break;
            //    case "ServeSetQustion":
            //        ServeSetQustion.HandlePacket(reader, whoAmI);
            //        break;
            //    default:
            //        break;
            //}
        }
    }

}
