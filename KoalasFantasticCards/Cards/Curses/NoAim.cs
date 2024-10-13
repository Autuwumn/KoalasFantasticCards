using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnboundLib.GameModes;
using WillsWackyManagers.Utils;
using System.Runtime.InteropServices;
using InControl;
using InControl.NativeProfile;


namespace KFC.Cards
{
    public class NoAim : CustomEffectCard<AimlessMono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Cant Aim",
            Description = "What are you aiming at",
            ModName = KFC.CurseInt,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_NoAim"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            OwnerOnly = true
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory };
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class AimlessMono : CardEffect
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        public override void OnShoot(GameObject projectile)
        {
            MousePoint mp;
            GetCursorPos(out mp);
            var cursPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, player.transform.position.z);
            var offset = new Vector3(mp.X - cursPos.x, mp.Y + cursPos.y - Screen.height);
            SetCursorPos((int)offset.x + (int)(Screen.width * Random.Range(0.05f, 0.95f)), (int)offset.y + (int)(Screen.height * Random.Range(0.05f, 0.95f)));
        }
    }
}