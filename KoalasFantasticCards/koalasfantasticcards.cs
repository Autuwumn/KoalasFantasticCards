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
        public const string Version = "3.0.0";
        public const string ModInitials = "KFC";
        public const string ModIntDed = "KFC DediCat";

        internal static KFC instance;

        public static AudioClip uwu;
        public static AudioClip honk;
        public static AudioClip owo;
        public static int pogess = 0;

        internal static AssetBundle ArtAssets;

        public static ConfigEntry<float> globalVolMute;
        public static ConfigEntry<string> goofyAh;
        public static ConfigEntry<float> sliderChange;

        private void GlobalVolAction(float val)
        {
            globalVolMute.Value = val;
        }
        private void GoofyAction(string val)
        {
            goofyAh.Value = val;
        }
        private void SliderChangeAction(float val)
        {
            sliderChange.Value = val;
        }

        private void NewGUI(GameObject menu)
        {
            var fs = new[] { "Fantastic", "Fabulous", "Fried", "Fancy", "Freaky", "Fat", "Foolish", "Funny", "False", "Fortuitus", "Fast", "Ferocious", "Fair", "Fashionable", "Finger-lickin", "Female", "Fucking Annoying" };
            MenuHandler.CreateSlider("Funny Number", menu, 50, -10f, 10f, globalVolMute.Value, GlobalVolAction, out UnityEngine.UI.Slider volumeSlider, false);
            //MenuHandler.CreateInputField("I like salads", 50, menu, GoofyAction);
            MenuHandler.CreateSlider("Salad", menu, 50, 1, 26, sliderChange.Value, SliderChangeAction, out UnityEngine.UI.Slider sluder, true);
            
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
            var fs = new[] { "Fantastic", "Fabulous", "Fried", "Fancy", "Freaky", "Fat", "Foolish", "Funny", "False", "Fortuitus", "Fast", "Ferocious", "Fair", "Fashionable", "Finger-lickin", "Female", "Fucking Annoying" };
            var lmao = fs[UnityEngine.Random.Range(0, fs.Length)];
            Unbound.RegisterMenu("Koala's " + lmao + " Cards", () => { }, this.NewGUI, null, true);
            globalVolMute = base.Config.Bind<float>("KFC", "card volume", 100f, "hmmm");
            //goofyAh = base.Config.Bind<string>("KFC", "ceaser", "I like salads", "hmmm");
            sliderChange = base.Config.Bind<float>("KFC", "Ceaser", 1f, "hmmm");

            KFC.ArtAssets = AssetUtils.LoadAssetBundleFromResources("kfccards", typeof(KFC).Assembly);



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


            //CustomCard.BuildCard<splinter>((card) => { splinter.card = card; card.SetAbbreviation("Sp"); });
            CustomCard.BuildCard<indiajoenas>((card) => { indiajoenas.card = card; card.SetAbbreviation("Ij"); });
            //CustomCard.BuildCard<turret>((card) => { turret.card = card; card.SetAbbreviation("Tu"); });
            CustomCard.BuildCard<legos>((card) => { legos.card = card; card.SetAbbreviation("Le"); });
            CustomCard.BuildCard<uwullets>((card) => { uwullets.card = card; card.SetAbbreviation("Uw"); });
            CustomCard.BuildCard<scp_500>((card) => { scp_500.card = card; card.SetAbbreviation("S5"); });
            CustomCard.BuildCard<doomSlayer>((card) => { doomSlayer.card = card; card.SetAbbreviation("DS"); });
            CustomCard.BuildCard<blackholegun>((card) => { blackholegun.card = card; card.SetAbbreviation("Bh"); });
            CustomCard.BuildCard<Armor>((card) => { Armor.card = card; card.SetAbbreviation("Ar"); });
            CustomCard.BuildCard<Extendo>((card) => { Extendo.card = card; card.SetAbbreviation("Ex"); });
            CustomCard.BuildCard<RedHerring>((card) => { RedHerring.card = card; card.SetAbbreviation("Rh"); });

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
            CustomCard.BuildCard<Ard>((card) => { Ard.card = card; });
            CustomCard.BuildCard<Bard>((card) => { Bard.card = card; });
            CustomCard.BuildCard<Ccard>((card) => { Ccard.card = card; });


            CustomCard.BuildCard<VoloMori>((card) => { VoloMori.card = card; });
            CustomCard.BuildCard<ImCursed>((card) => { ImCursed.card = card; });
            CustomCard.BuildCard<F3nxCorgi>((card) => { F3nxCorgi.card = card; });
            CustomCard.BuildCard<Alyssa>((card) => { Alyssa.card = card; });
            CustomCard.BuildCard<Geballion>((card) => { Geballion.card = card; });
            CustomCard.BuildCard<HaruShijun>((card) => { HaruShijun.card = card; });
        }
        public string randValue(bool posGood)
        {
            string s = null;
            float num = UnityEngine.Random.Range(-5f, 5f)*100;
            if (num > 0 && posGood) s = "<#00ff00>+" + (int)num + "%";
            if (num < 0 && !posGood) s = "<#00ff00>" + (int)num + "%";
            if (num < 0 && posGood) s = "<#ff0000>" + (int)num + "%";
            if (num > 0 && !posGood) s = "<#ff0000>+" + (int)num + "%";
            if(num == 0)
            {
                s = "<#ffff00>+0%";
            }
            return s;
        }
        public string randInt()
        {
            string s = null;
            int num = UnityEngine.Random.Range(-5, 5);
            if (num > 0)
            {
                s = "<#00ff00>+" + num;
            }
            if (num < 0)
            {
                s = "<#ff0000>" + num;
            }
            if (num == 0)
            {
                s = "<#ffff00>+" + num;
            }
            return s;
        }
        public void Update()
        {
            if (RedHerring.card)
            {
                RedHerring.card.cardDestription = "Discalimer: These are randomized on pickup";
                RedHerring.card.cardStats = new[]
                {
                    new CardInfoStat()
                    {
                        amount = randValue(true),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Damage"
                    },
                    new CardInfoStat()
                    {
                        amount = randValue(false),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Attack Speed"
                    },
                    new CardInfoStat()
                    {
                        amount = randValue(false),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Reload Time"
                    },
                    new CardInfoStat()
                    {
                        amount = randValue(true),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Projectile Speed"
                    },
                    new CardInfoStat()
                    {
                        amount = randValue(true),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Health"
                    },
                    new CardInfoStat()
                    {
                        amount = randValue(true),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Movement Speed"
                    },
                    new CardInfoStat()
                    {
                        amount = randInt(),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Ammo"
                    },
                    new CardInfoStat()
                    {
                        amount = randInt(),
                        positive = true,
                        simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                        stat = "Bullets"
                    }
                };
            }
        }
        public IEnumerator GameStart(IGameModeHandler gameModeHandler)
        {
            //scp_500.card.rarity = RarityUtils.GetRarity("Divine");
            yield break;
        }
        public static bool Debug = false;
    }
}