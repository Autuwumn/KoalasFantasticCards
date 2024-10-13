using System.Reflection;
using UnityEngine;
using RarityLib.Utils;
using ModsPlus;
using UnboundLib;
using KFC.MonoBehaviors;
using UnboundLib.Networking;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace KFC.Cards
{
    public class swordinstone : SimpleCard
    {
        internal static CardInfo card = null;
        public override CardDetails Details => new CardDetails
        {
            Title = "Sword in the stone",
            Description = "Atempt to pull the sword out",
            ModName = KFC.ModInitials,
            Art = KFC.ArtAssets.LoadAsset<GameObject>("C_SwordInStone"),
            Rarity = RarityUtils.GetRarity("Rare"),
            Theme = CardThemeColor.CardThemeColorType.DestructiveRed
        };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation") };
        }
        protected override void Added(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (!player.data.view.IsMine) return;
            int rng = UnityEngine.Random.Range(0, 2);
            foreach (var card in player.data.currentCards)
            {
                if (card == failure.card) rng = 0;
                if (card == excaliber.card) rng = 0;
            }
            KFC.instance.ExecuteAfterFrames(20, () =>
            {
                NetworkingManager.RPC(typeof(swordinstone), nameof(SSRNG), player.playerID, rng);
            });
        }
        [UnboundRPC]
        public static void SSRNG(int pid, int num)
        {
            var player = PlayerManager.instance.GetPlayerWithID(pid);
            if(num == 1)
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, excaliber.card, false, "++", 0, 0);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, excaliber.card);
            } else
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, failure.card, false, "--", 0, 0);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, failure.card);
            }
        }
    }
}