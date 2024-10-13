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
using System.Collections;
using UnboundLib.GameModes;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.Extensions;
using UnboundLib.Networking;
using ExitGames.Client.Photon.StructWrapping;

namespace KFC.Cards
{
    public class KoalaThief : CustomEffectCard<Thief>
    {
        internal static CardInfo card = null;
        internal List<string> blacklistedCategories = new List<string>
        {
            "CardManipulation",
            "Grants Curses",
            "GivesNulls"
        };
        public override CardDetails Details => new CardDetails
        {
            Title = "Stealth of Koala",
            Description = "Steal a card from the rich every other pick phase",
            ModName = KFC.ModIntDed,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_KoalaThief"),
            Rarity = RarityUtils.GetRarity("Legendary"),
            Theme = CardThemeLib.CardThemeLib.instance.CreateOrGetType("Koality")
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation") };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                var others = PlayerManager.instance.players.Where((p) => p.playerID != player.playerID).ToList();
                Dictionary<Player, List<CardInfo>> stuff = new Dictionary<Player, List<CardInfo>>();
                foreach (var o in others)
                {
                    var cords = o.data.currentCards.Where((c) => c.categories.Select(c => c.name.ToLowerInvariant()).Intersect(blacklistedCategories.Select(s => s.ToLowerInvariant())).Count() <= 0 && ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player,c)).ToList();
                    string cardStuff = "";
                    foreach(var c in cords)
                    {
                        cardStuff += c.cardName + ", ";
                    }
                    if (cords.Count() > 0)
                    {
                        stuff.Add(o, cords);
                    }
                }
                if (stuff.Count == 0)
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, MasterThief.card, false, "", 0, 0);
                }
                else
                {
                    foreach (var kvp in stuff)
                    {
                        string cardNames = "";
                        foreach (var c in kvp.Value)
                        {
                            cardNames += c.cardName + ", ";
                        }
                        cardNames.Remove(0, cardNames.Length - 2);
                        print("ID: " + kvp.Key.playerID + " | Cards: " + cardNames);
                    }
                    var dicIndex = UnityEngine.Random.Range(0, stuff.Count);
                    var victim = stuff.ElementAt(dicIndex);
                    var randCard = victim.Value[UnityEngine.Random.Range(0, victim.Value.Count)];
                    var index = victim.Key.data.currentCards.IndexOf(randCard);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randCard, false, "", 0, 0);
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(victim.Key, index);
                }
            });
        }
        public override bool GetEnabled()
        {
            return false;
        }
    }
}
namespace KFC.MonoBehaviors
{
    public class Thief : CardEffect
    {
        private int stealDelay = 0;
        private int coolDown = 2;
        internal List<string> blacklistedCategories = new List<string>
        {
            "CardManipulation",
            "Grants Curses",
            "GivesNulls"
        };
        public override IEnumerator OnPickPhaseEnd(IGameModeHandler gameModeHandler)
        {
            stealDelay++;
            if (stealDelay >= coolDown)
            {
                stealDelay = 0;
                var others = PlayerManager.instance.players.Where((p) => p.playerID != player.playerID).ToList();
                Dictionary<Player, List<CardInfo>> stuff = new Dictionary<Player, List<CardInfo>>();
                foreach (var o in others)
                {
                    var cords = o.data.currentCards.Where((c) => c.categories.Select(c => c.name.ToLowerInvariant()).Intersect(blacklistedCategories.Select(s => s.ToLowerInvariant())).Count() <= 0 && ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, c)).ToList();
                    string cardStuff = "";
                    foreach (var c in cords)
                    {
                        cardStuff += c.cardName + ", ";
                    }
                    if (cords.Count() > 0)
                    {
                        stuff.Add(o, cords);
                    }
                }
                if (stuff.Count == 0)
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, MasterThief.card, false, "", 0, 0);
                }
                else
                {
                    foreach (var kvp in stuff)
                    {
                        string cardNames = "";
                        foreach (var c in kvp.Value)
                        {
                            cardNames += c.cardName + ", ";
                        }
                        cardNames.Remove(0, cardNames.Length - 2);
                        print("ID: " + kvp.Key.playerID + " | Cards: " + cardNames);
                    }
                    var dicIndex = UnityEngine.Random.Range(0, stuff.Count);
                    var victim = stuff.ElementAt(dicIndex);
                    var randCard = victim.Value[UnityEngine.Random.Range(0, victim.Value.Count)];
                    var index = victim.Key.data.currentCards.IndexOf(randCard);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, randCard, false, "", 0, 0);
                    ModdingUtils.Utils.Cards.instance.RemoveCardFromPlayer(victim.Key, index);
                }
            }
            yield return null;
        }
    }
}