using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.RiftShenanigans;
using KFC.Cards;
using System.Linq;
using System.Collections.Generic;
using System;
using ClassesManagerReborn.Util;
using System.Collections;
using UnboundLib.GameModes;
using UnboundLib.Extensions;

namespace KFC.Cards
{
    public class RiftSoul : CustomEffectCard<RiftSoulMono>
    {
        internal static CardInfo card = null;
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = RiftClass.name;
        }
        public override CardDetails Details => new CardDetails
        {
            Title = "Soul of the Rift",
            Description = "Power at a cost",
            ModName = "RIFT",
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_RiftSoul"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = KFC.ArtAssets.LoadAsset<Sprite>("locked");
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.4f, 0.4f, 1);
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Mask>().enabled = false;
            this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            cardInfo.allowMultiple = false;
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}
namespace KFC.RiftShenanigans
{
    public class RiftSoulMono : CardEffect
    {
        private bool isRift(CardInfo card)
        {
            if (card.name == RiftWalker.card.name || card.name == RiftGun.card.name || card.name == RiftBody.card.name || card.name == RiftMind.card.name || card.name == RiftSoul.card.name || card.name == Rifted.card.name) return true;
            return false;
        }
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, Rifted.card, false, "__", 0, 0);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, Rifted.card);
            yield break;
        }
    }
}
