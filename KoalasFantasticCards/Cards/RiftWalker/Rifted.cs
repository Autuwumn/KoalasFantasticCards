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
using System.IO;

namespace KFC.Cards
{
    public class Rifted : SimpleCard
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RiftClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Void Fragment",
            Description = "Certifed Ruh Roh Raggy Moment",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_Rifted"),
            Rarity = RarityUtils.GetRarity("Trinket"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FF00FF>-1%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Stats"
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
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}
