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

namespace DuanWu
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class DuanWu : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            string type = reader.ReadString();
            if (type== "NetNPC")
            {
                NetNPC.HandlePacket(reader, whoAmI);
            }
            else if (type== "NetProjectlies")
            {
                NetProjectlies.HandlePacket(reader, whoAmI);
            }
           

        }
    }
}
