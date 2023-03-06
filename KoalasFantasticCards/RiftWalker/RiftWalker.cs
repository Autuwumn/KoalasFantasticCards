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
    public class RiftWalker : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RiftClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Rift Walker",
            Description = "Begin your path hunting through the void",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_RiftWalker"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "+30%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                new CardInfoStat
                {
                    amount = "+30%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Health"
                },

            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = KFC.ArtAssets.LoadAsset<Sprite>("locked");
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.4f, 0.4f, 1);
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Mask>().enabled = false;
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            cardInfo.allowMultiple = false;
            gun.damage = 1.3f;
            statModifiers.health = 1.3f;
            gun.projectileColor = new Color(0.8f,0,0.8f,1f);
        }
    }
}
