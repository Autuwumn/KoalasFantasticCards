using CardChoiceSpawnUniqueCardPatch.CustomCategories;
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
    public class uwullets : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "UwU",
            Description = "In need of a lumberjack are you?",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_UwU"),
            Rarity = CardInfo.Rarity.Uncommon,
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
                }
            }
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity") };
            cardInfo.allowMultiple = false;
            gun.damage = 1.25f;
            gun.projectileColor = Color.magenta;
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            base.Added(player, gun, gunAmmo, data, health, gravity, block, characterStats);
            SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
            soundContainer.setting.volumeIntensityEnable = true;
            soundContainer.audioClip[0] = KFC.uwu;
            SoundEvent uwuSound = ScriptableObject.CreateInstance<SoundEvent>();
            uwuSound.soundContainerArray[0] = soundContainer;
            SoundContainer sc2 = ScriptableObject.CreateInstance<SoundContainer>();
            sc2.setting.volumeIntensityEnable = true;
            sc2.audioClip[0] = KFC.owo;
            SoundEvent owoSound = ScriptableObject.CreateInstance<SoundEvent>();
            owoSound.soundContainerArray[0] = sc2;
            gun.soundGun.soundShotModifierBasic.single = uwuSound;
            gun.soundGun.soundShotModifierBasic.singleAutoLoop = uwuSound;
            gun.soundGun.soundShotModifierBasic.singleAutoTail = uwuSound;
            gun.soundGun.soundShotModifierBasic.shotgun = owoSound;
            gun.soundGun.soundShotModifierBasic.shotgunAutoLoop = owoSound;
            gun.soundGun.soundShotModifierBasic.shotgunAutoTail = owoSound;
            UnityEngine.Debug.Log("d/dx 2x^3 + 2x^2 + 2x is very mysterious");
        }
    }
}
/**namespace KFC.MonoBehaviors
{
    public class uwu_mono : CardEffect
    {
        private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0f, UpdateMode.Continuous);
        private GameObject[] arms;
        public void Start()
        {
            //arms[0] = PhotonNetwork.Instantiate("KFC_Buff", Vector3.zero, Quaternion.identity);
            //arms[0].gameObject.AddComponent<Arm>().parental = player.transform.GetChild(3).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject;
            //arms[1] = PhotonNetwork.Instantiate("KFC_Buff", Vector3.zero, Quaternion.identity);
            //arms[1].gameObject.AddComponent<Arm>().parental = player.transform.GetChild(3).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).gameObject;
        }
        public override void OnShoot(GameObject projectile)
        {
            base.OnShoot(projectile);
            var uwus = new[] { "KFC_UwU", "KFC_UwU2", "KFC_UwU3" };
            var owos = uwus[UnityEngine.Random.Range(0, uwus.Length)];
            var uwu = PhotonNetwork.Instantiate(owos, data.hand.transform.position, Quaternion.identity);
            uwu.gameObject.AddComponent<OwO>().parental = projectile;

            /**
            soundParameterIntensity.intensity = 0.5f;
            SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
            soundContainer.setting.volumeIntensityEnable = true;
            soundContainer.audioClip[0] = KFC.uwu;
            SoundEvent uwuSound = ScriptableObject.CreateInstance<SoundEvent>();
            uwuSound.soundContainerArray[0] = soundContainer;
            soundParameterIntensity.intensity *= KFC.globalVolMute.Value*5f;
            SoundManager.Instance.Play(uwuSound, base.transform, new SoundParameterBase[]
            {
                soundParameterIntensity
            });
            
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
    public class Arm : MonoBehaviour
    {
        public GameObject parental;
        private void Update()
        {
            var posatoin = parental.transform.position;
            gameObject.transform.position = posatoin;
        }
    }
}**/