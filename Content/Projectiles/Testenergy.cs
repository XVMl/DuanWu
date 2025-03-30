using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Luminance.Assets;
using Luminance.Core.Graphics;
using Luminance.Common.Utilities;
using Microsoft.CodeAnalysis;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.UI;
using Microsoft.Xna.Framework.Graphics;
using Luminance;
namespace DuanWu.Content.Projectiles
{
    public class Testenergy:ModProjectile,IPixelatedPrimitiveRenderer
    {
        /// <summary>
        /// How long this energy has existed for, in frames.
        /// </summary>
        public ref float Time => ref Projectile.ai[0];

        /// <summary>
        /// The current angle of this energy.
        /// </summary>
        public ref float Angle => ref Projectile.ai[1];

        /// <summary>
        /// The radius of this energy.
        /// </summary>
        public ref float Radius => ref Projectile.ai[2];

        /// <summary>
        /// How long this energy should exist for, in frames.
        /// </summary>
        public static int Lifetime => Utilities.SecondsToFrames(3f);

        /// <summary>
        /// The palette that this energy cycles through when rendering.
        /// </summary>
        public static readonly Palette EnergyPalette = new Palette().
            AddColor(new Color(0, 0, 0)).
            AddColor(new Color(71, 35, 137)).
            AddColor(new Color(120, 60, 231)).
            AddColor(new Color(46, 156, 211)).
            AddColor(new Color(0, 0, 0)).
            AddColor(new Color(245, 245, 193)).
            AddColor(new Color(0, 0, 0));

        public override string Texture => MiscTexturesRegistry.InvisiblePixelPath;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 100;
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = 2000;
        }

        public override void SetDefaults()
        {
            Projectile.width = Main.rand?.Next(15, 50) ?? 100;
            Projectile.height = Projectile.width;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.hostile = true;
            Projectile.hide = true;
            Projectile.timeLeft = Lifetime;
            Projectile.Opacity = 0f;

            CooldownSlot = ImmunityCooldownID.Bosses;
        }

        public override void AI()
        {
            Vector2 flyDestination = Main.LocalPlayer.Center;
            if (Time == 1f)
            {
                Angle = flyDestination.AngleTo(Projectile.Center);
                Radius = flyDestination.Distance(Projectile.Center);
                Projectile.netUpdate = true;
            }

            float erring = Utilities.AperiodicSin(Projectile.Center.X * 0.0093f + Projectile.Center.Y * 0.0041f + Time / 30f) * 0.07f +
                           Utilities.AperiodicSin(Projectile.Center.X * 0.0045f + Projectile.Center.Y * 0.0088f + Time / 25f) * 0.07f;

            Radius *= 0.91f;
            Angle += erring * Utilities.InverseLerp(0f, 16f, Time);

            if (Radius >= 50f)
            {
                Radius -= 6f;
            }
            else if (Time >= 2f)
            {
                Projectile.timeLeft = 3;
                Projectile.MaxUpdates = 4;
                Projectile.Center = flyDestination;

                if (Projectile.width > 9)
                    Projectile.width = Utils.Clamp(Projectile.width - 1, 0, 1000);

                if (Projectile.oldPos[^1].WithinRange(Projectile.oldPos[0], 5f))
                    Projectile.Kill();
            }

            if (Time >= 2f)
                Projectile.Center = flyDestination + Angle.ToRotationVector2() * new Vector2(1f, 0.56f).SafeNormalize(Vector2.Zero) * Radius;

            Projectile.Opacity = Utilities.InverseLerp(10f, 30f, Time);

            Time++;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public float EnergyWidthFunction(float completionRatio)
        {
            float baseWidth = Projectile.width;
            return baseWidth * Utilities.InverseLerp(0f, 0.4f, completionRatio) * Projectile.scale;
        }

        public Color EnergyColorFunction(float completionRatio)
        {
            float lifetimeRatio = Time / Lifetime + completionRatio * 0.4f;
            float hue = Projectile.identity / 17f;
            hue += Utilities.InverseLerp(0.4f, 0.5f, lifetimeRatio) * 0.35f;
            hue += Utilities.InverseLerp(0.81f, 0.9f, lifetimeRatio) * 0.25f;

            return EnergyPalette.SampleColor(hue.Modulo(1f)) * Utilities.InverseLerpBump(0f, 0.4f, 0.6f, 0.9f, completionRatio) * Projectile.Opacity;
        }

        public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
        {
            ManagedShader trailShader = ShaderManager.GetShader("DuanWu.TestEnergyShader");
            trailShader.SetTexture(ModContent.Request<Texture2D>("DuanWu/Content/Images/PerlinNoise"), 1, SamplerState.LinearWrap);
            trailShader.Apply();

            PrimitiveSettings settings = new PrimitiveSettings(EnergyWidthFunction, EnergyColorFunction, _ => Projectile.Size * 0.5f, Pixelate: true, Shader: trailShader);
            PrimitiveRenderer.RenderTrail(Projectile.oldPos, settings, 60);
        }
    }
    public class Palette
    {
        private readonly List<Vector4> colors = [];

        public Palette(params Color[] colors)
        {
            for (int i = 0; i < colors.Length; i++)
                AddColor(colors[i]);
        }

        public Palette(Vector3[] colors)
        {
            for (int i = 0; i < colors.Length; i++)
                AddColor(colors[i]);
        }

        /// <summary>
        /// Adds a new <see cref="Vector3"/> color representation to the palette.
        /// </summary>
        public Palette AddColor(Vector3 color)
        {
            colors.Add(new Vector4(color, 1f));
            return this;
        }

        /// <summary>
        /// Adds a new <see cref="Vector4"/> color representation to the palette.
        /// </summary>
        public Palette AddColor(Vector4 color)
        {
            colors.Add(color);
            return this;
        }

        /// <summary>
        /// Adds a new <see cref="Color"/> color representation to the palette.
        /// </summary>
        public Palette AddColor(Color color) => AddColor(color.ToVector4());

        /// <summary>
        /// Samples across this palette based on a given 0-1 interpolant.
        /// </summary>
        public Vector4 SampleVector(float interpolant)
        {
            if (colors.Count <= 1)
                return colors[0];

            // Apply interpolant safety checks.
            if (float.IsNaN(interpolant) || float.IsInfinity(interpolant))
                interpolant = 0f;
            interpolant = Utils.Clamp(interpolant, 0f, 0.999f);

            int gradientStartingIndex = (int)(interpolant * colors.Count);
            float currentColorInterpolant = interpolant * colors.Count % 1f;
            Vector4 gradientSubdivisionA = colors[gradientStartingIndex];
            Vector4 gradientSubdivisionB = colors[Utils.Clamp(gradientStartingIndex + 1, 0, colors.Count - 1)];
            return Vector4.Lerp(gradientSubdivisionA, gradientSubdivisionB, currentColorInterpolant);
        }

        /// <summary>
        /// Samples across this palette based on a given 0-1 interpolant, returning the result as a <see cref="Color"/>.
        /// </summary>
        public Color SampleColor(float interpolant) => new Color(SampleVector(interpolant));
    }

}