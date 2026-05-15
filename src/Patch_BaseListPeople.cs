using HarmonyLib;

namespace NpcShopReroll
{
    [HarmonyPatch(typeof(BaseListPeople), "OnRefreshMenu")]
    static class Patch_BaseListPeople_OnRefreshMenu
    {
        static bool _isRerolling;

        static void Postfix(BaseListPeople __instance)
        {
            if (_isRerolling) return;
            if (__instance is not ListPeopleBuySlave buySlave) return;

            var menuLeft = buySlave.window?.menuLeft;
            if (menuLeft == null) return;

            menuLeft.AddButton2Line(
                Lang.isJP ? "再入荷" : "Restock",
                () => Lang.isJP ? "影響力 -1" : "-1 Influence",
                _ => OnReroll(buySlave),
                null,
                "2line"
            );
        }

        static void OnReroll(ListPeopleBuySlave buySlave)
        {
            var zone = EClass._zone;
            if (zone == null) return;

            if (zone.influence < 1)
            {
                Msg.Say(Lang.isJP ? "影響力が足りない。" : "Not enough Influence.");
                return;
            }

            zone.ModInfluence(-1);

            var data = buySlave.data;
            data.dateRefresh = 0;
            data.TryRefresh(buySlave.owner);

            _isRerolling = true;
            try
            {
                buySlave.List();
            }
            finally
            {
                _isRerolling = false;
            }
        }
    }
}
