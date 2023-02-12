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

namespace KFC.Cards
{
    public class scp_500 : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "SCP-500",
            Description = "A true clensing",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_SCP_500"),
            Rarity = RarityUtils.GetRarity("Divine"),
            Theme = CardThemeColor.CardThemeColorType.TechWhite
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            statModifiers.health = 1.1f;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (!player.data.view.IsMine) return;
            gun.recoil = 0;
            gun.spread = 0;
            gun.destroyBulletAfter = 0;
            List<float> statChange = new List<float>();
            foreach(var c in player.data.currentCards)
            {
                var g = c.gameObject.GetComponent<Gun>();
                var h = c.gameObject.GetComponent<CharacterStatModifiers>();
                if (g.reloadTimeAdd > 0) gunAmmo.reloadTimeAdd -= g.reloadTimeAdd;
                if (g.ammo < 0) gun.ammo -= g.ammo;
                if(g.attackSpeed > 1) gun.attackSpeed /= g.attackSpeed;
                if (g.reloadTime > 1) gunAmmo.reloadTime /= g.reloadTime;
                if (h.health < 1) characterStats.health /= h.health;
                if (g.damage < 1) gun.damage /= g.damage;
                if (g.projectileSpeed < 1) gun.projectileSpeed /= g.projectileSpeed;
                if (h.movementSpeed < 1) characterStats.movementSpeed /= h.movementSpeed;
            }
        }
    }
}
