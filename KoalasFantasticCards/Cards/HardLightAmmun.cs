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
using SimulationChamber;

namespace KFC.Cards
{
    public class HardLightAmmun : CustomEffectCard<BulletSword>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Hard Light Ammunition",
            Description = "Hit people with your bullets… without shooting! May cause loss of friendships. ",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_HardLight"),
            Rarity = RarityUtils.GetRarity("Epic"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
            gun.reloadTime = 10;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.AddComponent<SimulatedGun>();
            SimulatedGun swordMaker = player.GetComponent<SimulatedGun>();
            swordMaker.CopyGunStatsExceptActions(gun);
            swordMaker.CopyAttackAction(gun);
            swordMaker.CopyShootProjectileAction(gun);
            swordMaker.destroyBulletAfter = 0.5f;
        }
        protected override void Removed(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            Destroy(player.gameObject.GetComponent<SimulatedGun>());
        }
    }
}

namespace KFC.MonoBehaviors
{
    public class BulletSword : CardEffect
    {
        private bool going = true;
        private SimulatedGun swordMaker;
        public override IEnumerator OnJumpCoroutine()
        {
            if (!swordMaker)
            {
                swordMaker = player.GetComponent<SimulatedGun>();
            }
            yield return MakeBulletSword();
        }
        public override IEnumerator OnRoundStart(IGameModeHandler gameModeHandler)
        {
            if (!swordMaker)
            {
                swordMaker = player.GetComponent<SimulatedGun>();
            }
            going = true;
            yield return MakeBulletSword();
        }
        public override IEnumerator OnRoundEnd(IGameModeHandler gameModeHandler)
        {
            going = false;
            yield return null;
        }

        IEnumerable MakeBulletSword()
        {
            while(going)
            {
                yield return new WaitForSecondsRealtime(0.2f);
                swordMaker.SimulatedAttack(player.playerID, gun.shootPosition.position, data.aimDirection, 0f, gun.damage);
            }
            yield return null;
        }
    }
}