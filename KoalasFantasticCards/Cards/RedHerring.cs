using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

namespace KFC.Cards
{
    class RedHerring : SimpleCard
    {
        internal static CardInfo card = null;
        private static String desc = null;
        private static String color = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Red Herring",
            Description = desc,
            ModName = KFC.ModInitials,
            Art = null,
            Rarity = CardInfo.Rarity.Uncommon,
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#" + RandomChar() + RandomChar() + RandomChar() + RandomChar() + RandomChar() + RandomChar()+">"+"? Random",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Stats"
                }
            }
        };
        private float GetRandomBalancedFloat()
        {
            float num = 0;
            float pos = UnityEngine.Random.Range(0f, 1f);
            float mult = UnityEngine.Random.Range(0.2f, 1f);
            if(pos > 0.5f)
            {
                num = 1f / mult; 
            } else
            {
                num = mult;
            }
            return num;
        }
        private string RandomChar()
        {
            var chars = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            return chars[UnityEngine.Random.Range(0,chars.Length)];
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            color = "<#" + RandomChar() + RandomChar() + RandomChar() + RandomChar() + RandomChar() + RandomChar()+">";
            cardInfo.allowMultiple = false;
            gun.damage = GetRandomBalancedFloat();
            gun.attackSpeed = GetRandomBalancedFloat();
            gun.reloadTime = GetRandomBalancedFloat();
            gun.projectileSpeed = GetRandomBalancedFloat();
            gun.ammo = UnityEngine.Random.Range(-5, 5);
            gun.numberOfProjectiles = UnityEngine.Random.Range(-5, 5);
            statModifiers.health = GetRandomBalancedFloat();
            statModifiers.movementSpeed = GetRandomBalancedFloat();
            gun.spread = 0.1f;
            switch (KFC.sliderChange.Value)
            {
                case 1: desc = "vwky qod byvvon"; break;
                case 2: desc = "uvjx pnc axuunm"; break;
                case 3: desc = "tuiw omb zwttml"; break;
                case 4: desc = "sthv nla yvsslk"; break;
                case 5: desc = "rsgu mkz xurrkj"; break;
                case 6: desc = "qrft ljy wtqqji"; break;
                case 7: desc = "pqes kix vsppih"; break;
                case 8: desc = "opdr jhw uroohg"; break;
                case 9: desc = "nocq igv tqnngf"; break;
                case 10: desc = "mnbp hfu spmmfe"; break;
                case 11: desc = "lmao get rolled"; break;
                case 12: desc = "klzn fds qnkkdc"; break;
                case 13: desc = "jkym ecr pmjjcb"; break;
                case 14: desc = "ijxl dbq oliiba"; break;
                case 15: desc = "hiwk cap nkhhaz"; break;
                case 16: desc = "ghvj bzo mjggzy"; break;
                case 17: desc = "fgui ayn liffyx"; break;
                case 18: desc = "efth zxm kheexw"; break;
                case 19: desc = "desg ywl jgddwv"; break;
                case 20: desc = "cdrf xvk ifccvu"; break;
                case 21: desc = "bcqe wuj hebbut"; break;
                case 22: desc = "abpd vti gdaats"; break;
                case 23: desc = "zaoc ush fczzsr"; break;
                case 24: desc = "yznb trg ebyyrq"; break;
                case 25: desc = "xyma sqf daxxqp"; break;
                case 26: desc = "wxlz rpe czwwpo"; break;
            }
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            this.Details.Stats[0].amount = "<#" + RandomChar() + RandomChar() + RandomChar() + RandomChar() + RandomChar() + RandomChar() + ">" + "? Random";
            KFC.instance.ExecuteAfterFrames(10, () =>
            {
                if(gun.numberOfProjectiles < 1)
                {
                    gun.numberOfProjectiles = 1;
                }
            });
        }
    }
}