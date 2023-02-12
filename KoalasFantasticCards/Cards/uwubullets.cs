using HarmonyLib;
using KFC.Cards;
using KFC.MonoBehaviors;
using ModsPlus;
using Photon.Pun;
using RarityLib.Utils;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using System.Reflection;
using UnboundLib;
using UnityEngine;

namespace KFC.Cards
{
    public class uwullets : CustomEffectCard<uwu_mono>
    {
        internal static CardInfo card = null;
        private static CardInfoStat stut = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "UwU",
            Description = "In need of a lumberjack are you?",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_UwU"),
            Rarity = RarityUtils.GetRarity("Common"),
            Theme = CardThemeColor.CardThemeColorType.MagicPink,
            Stats = new[]
            {
                new CardInfoStat
                {
                    amount = "<#FF00FF>UwU",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Bullets"
                },
                new CardInfoStat
                {
                    amount = "+25%",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "Damage"
                },
                stut
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.projectileColor = Color.clear;
            gun.damage = 1.25f;
            if(KFC.globalVolMute.Value == 0.69f)
            {
                stut = new CardInfoStat
                {
                    amount = "<#FFFF00><i>Who put\nFind",
                    positive = true,
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                    stat = "<#FFFF00><i>this here?\nthe clue"
                };
                if(KFC.goofyAh.Value == -0.25f)
                {
                    stut = new CardInfoStat
                    {
                        amount = "<#FF00FF>DM: Ancient_Koala#4486",
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "<#FF00FF> the number you have"
                    };
                }
            }
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            base.Added(player, gun, gunAmmo, data, health, gravity, block, characterStats);
            
            SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
            soundContainer.setting.volumeIntensityEnable = true;
            soundContainer.audioClip[0] = KFC.uwu;
            SoundEvent uwuSound = ScriptableObject.CreateInstance<SoundEvent>();
            uwuSound.soundContainerArray[0] = soundContainer;
            gun.soundGun.soundShotModifierBasic.single = uwuSound;
            gun.soundGun.soundShotModifierBasic.singleAutoLoop = uwuSound;
            gun.soundGun.soundShotModifierBasic.singleAutoTail = uwuSound;
            gun.soundGun.soundShotModifierBasic.shotgun = uwuSound;
            gun.soundGun.soundShotModifierBasic.shotgunAutoLoop = uwuSound;
            gun.soundGun.soundShotModifierBasic.shotgunAutoTail = uwuSound;
            if (!player.data.view.IsMine) return;
            KFC.instance.ExecuteAfterSeconds(1f, ()=>
            {
                if (KFC.globalVolMute.Value == 0.69f)
                {
                    UnityEngine.Debug.Log("d/dx 2x^3 + 2x^2 + 2x is very mysterious");
                }
            });
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class uwu_mono : CardEffect
    {
        public override void OnShoot(GameObject projectile)
        {
            base.OnShoot(projectile);
            var uwus = new[] { "KFC_UwU", "KFC_UwU2", "KFC_UwU3" };
            var owos = uwus[UnityEngine.Random.Range(0, uwus.Length)];
            var uwu = PhotonNetwork.Instantiate(owos, data.hand.transform.position, Quaternion.identity);
            uwu.gameObject.AddComponent<OwO>().parental = projectile;
        }
    }
    public class OwO : MonoBehaviour
    {
        public GameObject parental;
        private void Update()
        {
            if (parental == null) PhotonNetwork.Destroy(gameObject);
            var posatoin = parental.transform.position;
            posatoin.y += 0.75f;
            gameObject.transform.position = posatoin;
        }
    }
}