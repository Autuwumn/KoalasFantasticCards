using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using KFC.Cards;
using Sonigon;
using Sonigon.Internal;
using Photon.Pun;

namespace KFC.Cards
{
    public class uwullets : CustomEffectCard<uwu_mono>
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "UwU",
            Description = "UwU, :3, rawr~",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_UwU"),
            Rarity = RarityUtils.GetRarity("Common"),
            Theme = CardThemeColor.CardThemeColorType.MagicPink,
            OwnerOnly = true
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.projectileColor = Color.clear;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

        }
    }
}
namespace KFC.MonoBehaviors
{
    public class uwu_mono : CardEffect
    {
        private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0f, UpdateMode.Continuous);
        protected override void Start()
        {
            base.Start();
            /**
            soundParameterIntensity.intensity = 0.5f;
            SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
            soundContainer.setting.volumeIntensityEnable = true;
            soundContainer.audioClip[0] = Assets.PewClip;
            SoundEvent pewSound = ScriptableObject.CreateInstance<SoundEvent>();
            pewSound.soundContainerArray[0] = soundContainer;
            soundParameterIntensity.intensity *= CoolRoundsModLol.globalVolMute.Value;
            SoundManager.Instance.Play(pewSound, base.transform, new SoundParameterBase[]
            {
                soundParameterIntensity
            });
            **/
        }
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