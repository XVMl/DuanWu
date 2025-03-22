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

namespace DuanWu
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class DuanWu : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            string type = reader.ReadString();
            Main.NewText(type);
            switch (type)
            {
                case "NetNPC":
                    NetNPC.HandlePacket(reader, whoAmI);
                    break;
                case "NetProjectlies":
                    NetProjectlies.HandlePacket(reader, whoAmI);
                    break;
                case "NetTime":
                    NetTime.HandlePacket(reader, whoAmI);
                    break;
                case "Netsponse":
                    Netsponse.HandlePacket(reader, whoAmI);
                    break;
                case "NetScoreboard":
                    NetScoreboard.HandlePacket(reader, whoAmI);
                    break;
                case "ServeSetQustion":
                    ServeSetQustion.HandlePacket(reader, whoAmI);
                    break;
                default:
                    break;
            }
            //if (type == "NetNPC")
            //{
            //    NetNPC.HandlePacket(reader, whoAmI);
            //}
            //else if (type == "NetProjectlies")
            //{
            //    NetProjectlies.HandlePacket(reader, whoAmI);
            //}
            //else if (type == "NetTime")
            //{
            //    NetTime.HandlePacket(reader, whoAmI);
            //}
            //else if (type == "Netsponse")
            //{

            //    Netsponse.HandlePacket(reader, whoAmI);
            //}
            //else if (type == "NetScoreboard")
            //{

            //    NetScoreboard.HandlePacket(reader, whoAmI);
            //}
            //else if (type == "ServeSetQustion")
            //{

            //    NetScoreboard.HandlePacket(reader, whoAmI);
            //}


        }
    }
}
