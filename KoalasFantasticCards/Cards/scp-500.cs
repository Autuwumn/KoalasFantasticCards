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
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using WillsWackyManagers.Utils;
using System.Collections;
using UnboundLib.GameModes;

namespace KFC.Cards
{
    public class scp_500 : CustomEffectCard<sp500Manager>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "SCP-500",
            Description = "A true clensing",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_SCP_500"),
            Rarity = RarityUtils.GetRarity("Mythical"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            statModifiers.health = 1.1f;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<int> ints = new List<int>();
            foreach (var c in player.data.currentCards)
            {
                foreach (var cu in CurseManager.instance.Curses)
                {
                    if (c.cardName == cu.cardName)
                    {
                        ints.Add(player.data.currentCards.IndexOf(c));
                    }
                }
            }
            if (ints.Count > 0)
            {
                KFC.instance.ExecuteAfterFrames(10, () =>
                {
                    ModdingUtils.Utils.Cards.instance.RemoveCardsFromPlayer(player, ints.ToArray());
                });
            }
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                gun.recoil = 0;
                gun.spread = 0;
                gun.destroyBulletAfter = 0;
                foreach (var c in player.data.currentCards)
                {
                    var g = c.gameObject.GetComponent<Gun>();
                    var h = c.gameObject.GetComponent<CharacterStatModifiers>();
                    if (g.reloadTimeAdd > 0) gunAmmo.reloadTimeAdd -= g.reloadTimeAdd;
                    if (g.ammo < 0) gunAmmo.maxAmmo -= g.ammo;
                    if (g.attackSpeed > 1) gun.attackSpeed /= g.attackSpeed;
                    if (g.reloadTime > 1) gunAmmo.reloadTime /= g.reloadTime;
                    if (h.health < 1) characterStats.health /= h.health;
                    if (g.damage < 1) gun.damage /= g.damage;
                    if (g.projectileSpeed < 1) gun.projectileSpeed /= g.projectileSpeed;
                    if (h.movementSpeed < 1) characterStats.movementSpeed /= h.movementSpeed;
                    if (g.dmgMOnBounce < 1) gun.dmgMOnBounce /= g.dmgMOnBounce;
                    if (g.speedMOnBounce < 1) gun.speedMOnBounce /= g.speedMOnBounce;
                }
            });
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class sp500Manager : CardEffect
    {
        private int indexing = -1;
        public override void OnTakeDamage(Vector2 damage, bool selfDamage)
        {
            if(selfDamage)
            {
                var dmgMag = damage.magnitude;
                if(dmgMag < 10)
                {
                    player.data.health += dmgMag;
                } else
                {
                    player.data.health += 10;
                }
            }
            base.OnTakeDamage(damage, selfDamage);
        }
        private void ReverseBads(CardInfo c)
        {
            var g = c.gameObject.GetComponent<Gun>();
            var h = c.gameObject.GetComponent<CharacterStatModifiers>();
            if (g.reloadTimeAdd > 0) gunAmmo.reloadTimeAdd -= g.reloadTimeAdd;
            if (g.ammo < 0) gunAmmo.maxAmmo -= g.ammo;
            if (g.attackSpeed > 1) gun.attackSpeed /= g.attackSpeed;
            if (g.reloadTime > 1) gunAmmo.reloadTime /= g.reloadTime;
            if (h.health < 1) characterStats.health /= h.health;
            if (g.damage < 1) gun.damage /= g.damage;
            if (g.projectileSpeed < 1) gun.projectileSpeed /= g.projectileSpeed;
            if (h.movementSpeed < 1) characterStats.movementSpeed /= h.movementSpeed;
            if (g.dmgMOnBounce < 1) gun.dmgMOnBounce /= g.dmgMOnBounce;
            if (g.speedMOnBounce < 1) gun.speedMOnBounce /= g.speedMOnBounce;
        }
        public override IEnumerator OnPointStart(IGameModeHandler gameModeHandler)
        {
            if(indexing == -1) indexing = player.data.currentCards.IndexOf(scp_500.card) + 1;
            if (indexing > player.data.currentCards.Count)
            {
                var cardstoCheck = player.data.currentCards;
                cardstoCheck.RemoveRange(0, indexing - 1);
                foreach (var c in cardstoCheck)
                {
                    ReverseBads(c);
                }
                indexing = player.data.currentCards.Count;
            }
            yield break;
        }
        public override void OnJump()
        {
            if(indexing == -1) indexing = player.data.currentCards.IndexOf(scp_500.card) + 1;
            if (indexing > player.data.currentCards.Count)
            {
                var cardstoCheck = player.data.currentCards;
                cardstoCheck.RemoveRange(0, indexing - 1);
                foreach (var c in cardstoCheck)
                {
                    ReverseBads(c);
                }
                indexing = player.data.currentCards.Count;
            }
            base.OnJump();
        }
    }
}