using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using System.Linq;
using System.Collections.Generic;
using System;
using ClassesManagerReborn.Util;

namespace KFC.Cards
{
    public class RiftGun: SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RiftClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Hands of the Rift",
            Description = "Your hands reach into the rift, and pull out power",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_RiftGun"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "+60%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "-30%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Attack Speed"
                },
                new CardInfoStat
                {
                    amount = "+9",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Ammo"
                },
                new CardInfoStat
                {
                    amount = "<#FF00FF>Unstoppable",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bullets"
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            try
            {
                this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = KFC.ArtAssets.LoadAsset<Sprite>("locked");
                this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.4f, 0.4f, 1);
                this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Mask>().enabled = false;
                this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            } catch { }
            cardInfo.allowMultiple = false;
            gun.damage = 1.6f;
            gun.attackSpeed = 0.7f;
            gun.ignoreWalls = true;
            gun.unblockable = true;
            gun.ammo = 9;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            KFC.instance.ExecuteAfterFrames(20, () => {
                if (KFC.hasCard(RiftGun.card, player) && KFC.hasCard(RiftBody.card, player) && KFC.hasCard(RiftMind.card, player) && !KFC.hasCard(RiftSoul.card, player))
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, RiftSoul.card, false, "Rs", 0, 0);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, RiftSoul.card);
                }
            });
        }
    }
}
