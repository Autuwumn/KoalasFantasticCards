using BepInEx;
using HarmonyLib;
using KFC.Cards;
using UnboundLib.Cards;
using Photon.Pun;
using Jotunn.Utils;
using UnityEngine;
using ModsPlus;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using BepInEx.Configuration;
using UnboundLib.Utils.UI;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using Sonigon.Internal;
using Sonigon;
using RarityLib.Utils;
using System.Net.NetworkInformation;
using Photon.Compression;
using UnboundLib.GameModes;
using System.Collections;
using System;
using WillsWackyManagers.Utils;
using UnboundLib.Utils;
using CardThemeLib;
using KFC.MonoBehaviors;
using Photon.Realtime;
using System.Reflection;
using System.Data;

namespace KFC
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willis.rounds.modsplus", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class KFC : BaseUnityPlugin
    {
        private const string ModId = "koala.koalas.fantastic.cards";
        private const string ModName = "Koalas Fantastic Cards";
        public const string Version = "3.6.0";
        public const string ModInitials = "KFC";
        public const string ModIntDed = "KFC";
        public const string CurseInt = "KFC Curses";

        public static KFC instance;

        public static AudioClip uwu;
        public static AudioClip honk;
        public static AudioClip owo;
        public static int pogess = 0;
        
        internal static AssetBundle ArtAssets;

        public static ConfigEntry<bool> cardSounds;

        public ObjectsToSpawn wallbounce;

        private static float myst = 477.2f;
        public static object[] mysteryValue = new object[] { myst, 1 + myst / 500f, (myst / 5f) + "%", (myst / 15f) + "%" };
        private void HearCardsAction(bool val)
        {
            cardSounds.Value = val;
        }
        private void NewGUI(GameObject menu)
        {
            var fs = new[] { "Fake", "Fantastic", "Fabulous", "Fried", "Fancy", "Freaky", "Fat", "Foolish", "Funny", "False", "Fortuitus", "Fast", "Ferocious", "Fair", "Fashionable", "Finger-lickin", "Female", "Fucking Annoying", "Forbidden"};
            MenuHandler.CreateToggle(cardSounds.Value, "Custom Card Sounds", menu, HearCardsAction, 50);
            
        }
        public static bool hasCard(CardInfo card, Player player)
        {
            foreach(var cord in player.data.currentCards)
            {
                if (cord.name == card.name) return true;
            }
            return false;
        }
        void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;


            var fieldInfo = typeof(UnboundLib.Utils.CardManager).GetField("defaultCards", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            var vanillaCards = (CardInfo[])fieldInfo.GetValue(null);
            var mayhemCard = vanillaCards.Where((c) => c.cardName.ToLower() == "mayhem").ToArray()[0];
            var proj = mayhemCard.gameObject.GetComponent<Gun>().objectsToSpawn[0];
            wallbounce = proj;
            var fs = new[] { "Fake", "Fantastic", "Fabulous", "Fried", "Fancy", "Freaky", "Fat", "Foolish", "Funny", "False", "Fortuitus", "Fast", "Ferocious", "Fair", "Fashionable", "Finger-lickin", "Female", "Fucking Annoying", "Forbidden" };
            var lmao = fs[UnityEngine.Random.Range(0, fs.Length)];
            Unbound.RegisterMenu("Koala's " + lmao + " Cards", () => { }, this.NewGUI, null, true);
            cardSounds = base.Config.Bind<bool>("KFC", "HearStuff", true);

            KFC.ArtAssets = AssetUtils.LoadAssetBundleFromResources("kfccards", typeof(KFC).Assembly);

            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality", new CardThemeColor() { bgColor = new Color( 0f, 0f, 0f), targetColor = new Color(0.9f, 0f, 0.9f) });


            uwu = ArtAssets.LoadAsset<AudioClip>("uwu");
            owo = ArtAssets.LoadAsset<AudioClip>("owo");
            honk = ArtAssets.LoadAsset<AudioClip>("honk");

            if (KFC.ArtAssets == null)
            {
                UnityEngine.Debug.Log("Chad Vanilla art asset bundle either doesn't exist or failed to load.");
            }
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_Excaliber", ArtAssets.LoadAsset<GameObject>("CrucibleSword"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_Boulder", ArtAssets.LoadAsset<GameObject>("Boulder"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_BlackHole", ArtAssets.LoadAsset<GameObject>("BlackHole"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_Turret", ArtAssets.LoadAsset<GameObject>("Turret"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_Buff", ArtAssets.LoadAsset<GameObject>("Buff"));

            //PhotonNetwork.PrefabPool.RegisterPrefab("KFC_UwU", ArtAssets.LoadAsset<GameObject>("Uwul"));
            //PhotonNetwork.PrefabPool.RegisterPrefab("KFC_UwU2", ArtAssets.LoadAsset<GameObject>("Uwul2"));
            //PhotonNetwork.PrefabPool.RegisterPrefab("KFC_UwU3", ArtAssets.LoadAsset<GameObject>("Uwul3"));

            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickR", ArtAssets.LoadAsset<GameObject>("legoBrickR"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickY", ArtAssets.LoadAsset<GameObject>("legoBrickY"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickG", ArtAssets.LoadAsset<GameObject>("legoBrickG"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickB", ArtAssets.LoadAsset<GameObject>("legoBrickB"));


            //CustomCard.BuildCard<AncientKoala>((card) => { AncientKoala.card = card; card.SetAbbreviation("Ak"); });
            //CustomCard.BuildCard<KoalaGlory>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); KoalaGlory.card = card; });
            //CustomCard.BuildCard<KoalaThief>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); KoalaThief.card = card; });
            //CustomCard.BuildCard<KoalaMight>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); KoalaMight.card = card; });
            //CustomCard.BuildCard<KoalaStats>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); KoalaStats.card = card; });
            //CustomCard.BuildCard<MasterThief>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); MasterThief.card = card; });

            //CustomCard.BuildCard<KoalaWrath>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); KoalaWrath.card = card; });


            CustomCard.BuildCard<splinter>((card) => { splinter.card = card; card.SetAbbreviation("Sp"); });
            CustomCard.BuildCard<indiajoenas>((card) => { indiajoenas.card = card; card.SetAbbreviation("Ij"); });
            CustomCard.BuildCard<legos>((card) => { legos.card = card; card.SetAbbreviation("Le"); });
            CustomCard.BuildCard<uwullets>((card) => { uwullets.card = card; card.SetAbbreviation("Uw"); });
            CustomCard.BuildCard<scp_500>((card) => { scp_500.card = card; card.SetAbbreviation("S5"); });
            CustomCard.BuildCard<doomSlayer>((card) => { doomSlayer.card = card; card.SetAbbreviation("DS"); });
            CustomCard.BuildCard<blackholegun>((card) => { blackholegun.card = card; card.SetAbbreviation("Bh"); });
            CustomCard.BuildCard<Armor>((card) => { Armor.card = card; card.SetAbbreviation("Ar"); });
            CustomCard.BuildCard<Extendo>((card) => { Extendo.card = card; card.SetAbbreviation("Ex"); });
            //CustomCard.BuildCard<UncappedAmmo>((card) => { UncappedAmmo.card = card; card.SetAbbreviation("Ua"); });
            CustomCard.BuildCard<KingOfSpeed>((card) => { KingOfSpeed.card = card; });
            //CustomCard.BuildCard<HardLightAmmun>((card) => { HardLightAmmun.card = card; });
            //CustomCard.BuildCard<Sacrafice>((card) => { Sacrafice.card = card; });


            CustomCard.BuildCard<swordinstone>((card) => { swordinstone.card = card; card.SetAbbreviation("Ss"); });
            CustomCard.BuildCard<excaliber>((card) => { excaliber.card = card; card.SetAbbreviation("Ex"); });
            CustomCard.BuildCard<failure>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); failure.card = card; card.SetAbbreviation("XX"); });

            CustomCard.BuildCard<RiftWalker>((card) => { RiftWalker.card = card; card.SetAbbreviation("Rw"); });
            CustomCard.BuildCard<RiftGun>((card) => { RiftGun.card = card; card.SetAbbreviation("Rh"); });
            CustomCard.BuildCard<RiftBody>((card) => { RiftBody.card = card; card.SetAbbreviation("Rb"); });
            CustomCard.BuildCard<RiftMind>((card) => { RiftMind.card = card; card.SetAbbreviation("Rm"); });
            CustomCard.BuildCard<RiftSoul>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); RiftSoul.card = card; card.SetAbbreviation("Rs"); });
            CustomCard.BuildCard<Rifted>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); Rifted.card = card; });
            
            CustomCard.BuildCard<ViruzCard>((card) => { ViruzCard.card = card; });
            CustomCard.BuildCard<Ard>((card) => { Ard.card = card; card.SetAbbreviation("A+"); });
            CustomCard.BuildCard<Bard>((card) => { Bard.card = card; card.SetAbbreviation("B+"); });
            CustomCard.BuildCard<Ccard>((card) => { Ccard.card = card; card.SetAbbreviation("C+"); });

            CustomCard.BuildCard<VoloMori>((card) => { VoloMori.card = card; });
            CustomCard.BuildCard<ImCursed>((card) => { ImCursed.card = card; });
            CustomCard.BuildCard<F3nxCorgi>((card) => { F3nxCorgi.card = card; });
            CustomCard.BuildCard<Alyssa>((card) => { Alyssa.card = card; });
            CustomCard.BuildCard<Geballion>((card) => { Geballion.card = card; });
            CustomCard.BuildCard<HaruShijun>((card) => { HaruShijun.card = card; });
            CustomCard.BuildCard<Pexiltd>((card) => { Pexiltd.card = card; });
            //CustomCard.BuildCard<Merlin>((card) => { Merlin.card = card; });

            //CustomCard.BuildCard<RockBottom>((card) => { RockBottom.card = card; card.SetAbbreviation("Rb"); });

            CustomCard.BuildCard<LaggyBullets>((card) => { LaggyBullets.card = card; CurseManager.instance.RegisterCurse(card); });
            CustomCard.BuildCard<Shadow>((card) => { Shadow.card = card; CurseManager.instance.RegisterCurse(card); });
            CustomCard.BuildCard<rerollNew>((card) => { rerollNew.card = card; CurseManager.instance.RegisterCurse(card); });
            CustomCard.BuildCard<NoAim>((card) => { NoAim.card = card; CurseManager.instance.RegisterCurse(card); });

            GameModeManager.AddHook(GameModeHooks.HookGameStart, GameStart);
        }
        public IEnumerator GameStart(IGameModeHandler gameModeHandler)
        {
            foreach(var p in PlayerManager.instance.players)
            {
                p.gameObject.AddComponent<KoalasBonusStats>();
            }
            //scp_500.card.rarity = RarityUtils.GetRarity("Divine");
            yield break;
        }
        public static bool Debug = false;
    }
}

namespace KFC.MonoBehaviors
{
    public class KoalasBonusStats : MonoBehaviour
    {
        public Vector2 shootPos;
    }
}