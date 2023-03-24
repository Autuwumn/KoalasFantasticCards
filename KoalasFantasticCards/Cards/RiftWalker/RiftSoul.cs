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
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_RiftSoul"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeColor.CardThemeColorType.EvilPurple
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
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            KFC.instance.ExecuteAfterFrames(10, () =>
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, Rifted.card, false, "__", 0, 0);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, Rifted.card);
                gun.damage *= 0.99f;
                gun.dmgMOnBounce *= 0.99f;
                gun.speedMOnBounce *= 0.99f;
                gun.spread += 0.01f;
                gun.projectileSpeed *= 0.99f;
                characterStats.movementSpeed *= 0.99f;
                player.data.maxHealth *= 0.99f;
                player.data.health = player.data.maxHealth;
                gun.reloadTime *= 1.01f;
                gun.attackSpeed *= 1.01f;
                gun.reloadTimeAdd += 0.05f;
            });
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
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            KFC.instance.ExecuteAfterFrames(10, () =>
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, Rifted.card, false, "__", 0, 0);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, Rifted.card);
                gun.damage *= 0.99f;
                gun.dmgMOnBounce *= 0.99f;
                gun.speedMOnBounce *= 0.99f;
                gun.spread += 0.01f;
                gun.projectileSpeed *= 0.99f;
                characterStats.movementSpeed *= 0.99f;
                player.data.maxHealth *= 0.99f;
                player.data.health = player.data.maxHealth;
                gun.reloadTime *= 1.01f;
                gun.attackSpeed *= 1.01f;
                gun.reloadTimeAdd += 0.05f;
            }); 
            yield break;
        }
    }
}
