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

namespace KFC
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willis.rounds.modsplus", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class KFC : BaseUnityPlugin
    {
        private const string ModId = "koala.koalas.fantastic.cards";
        private const string ModName = "Koalas Fantastic Cards";
        public const string Version = "1.0.6";
        public const string ModInitials = "KFC";
        public static string lmao;

        internal static KFC instance;

        //public static AudioClip uwu;

        internal static AssetBundle ArtAssets;

        public static ConfigEntry<float> globalVolMute;
        private void GlobalVolAction(float val)
        {
            globalVolMute.Value = val;
        }

        private void NewGUI(GameObject menu)
        {
            MenuHandler.CreateSlider("volume for KFC cards", menu, 50, 0f, 1f, globalVolMute.Value, GlobalVolAction, out UnityEngine.UI.Slider volumeSlider, false);
        }
        void Start()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            instance = this;
            var fs = new[] { "Fantastic", "Fabulous", "Fried", "Fancy", "Freaky", "Fat", "Foolish", "Funny", "False", "Fortuitus", "Fast", "Ferocious", "Fair", "Fashionable" };
            lmao = fs[UnityEngine.Random.Range(0,fs.Length)];
            Unbound.RegisterMenu("Koalas "+lmao+" Cards", () => { }, NewGUI, null, true);
            var globalVolMute = base.Config.Bind<float>("KFC", "KFC sounds volume", 100f, "KFC sounds volume");

            KFC.ArtAssets = AssetUtils.LoadAssetBundleFromResources("kfccards", typeof(KFC).Assembly);


            
            //uwu = ArtAssets.LoadAsset<AudioClip>("A_Pew");
            
            if (KFC.ArtAssets == null)
            {
                UnityEngine.Debug.Log("Chad Vanilla art asset bundle either doesn't exist or failed to load.");
            }
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_Excaliber", ArtAssets.LoadAsset<GameObject>("CrucibleSword"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_Boulder", ArtAssets.LoadAsset<GameObject>("Boulder"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_BlackHole", ArtAssets.LoadAsset<GameObject>("BlackHole"));

            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_UwU", ArtAssets.LoadAsset<GameObject>("Uwul"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_UwU2", ArtAssets.LoadAsset<GameObject>("Uwul2"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_UwU3", ArtAssets.LoadAsset<GameObject>("Uwul3"));

            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickR", ArtAssets.LoadAsset<GameObject>("legoBrickR"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickY", ArtAssets.LoadAsset<GameObject>("legoBrickY"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickG", ArtAssets.LoadAsset<GameObject>("legoBrickG"));
            PhotonNetwork.PrefabPool.RegisterPrefab("KFC_LegoBrickB", ArtAssets.LoadAsset<GameObject>("legoBrickB"));


            //CustomCard.BuildCard<splinter>((card) => { splinter.card = card; card.SetAbbreviation("Sp"); });
            CustomCard.BuildCard<indiajoenas>((card) => { indiajoenas.card = card; card.SetAbbreviation("Ij"); });
            CustomCard.BuildCard<legos>((card) => { legos.card = card; card.SetAbbreviation("Le"); });
            CustomCard.BuildCard<uwullets>((card) => { uwullets.card = card; card.SetAbbreviation("Uw"); });
            CustomCard.BuildCard<scp_500>((card) => { scp_500.card = card; card.SetAbbreviation("S5"); });
            //CustomCard.BuildCard<blackholegun>((card) => { blackholegun.card = card; card.SetAbbreviation("Bh"); });

            CustomCard.BuildCard<swordinstone>((card) => { swordinstone.card = card; card.SetAbbreviation("Ss"); });
            CustomCard.BuildCard<excaliber>((card) => { excaliber.card = card; card.SetAbbreviation("Ex"); });
            CustomCard.BuildCard<failure>((card) => { ModdingUtils.Utils.Cards.instance.AddHiddenCard(card); failure.card = card; card.SetAbbreviation("XX");});
        }
        public static bool Debug = false;
    }
}