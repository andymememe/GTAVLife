using GTA.Native;

namespace GTAVLife.Helper
{
    public class ScriptTerminator
    {
        public static void DisableRestrictedZone()
        {
            // Army Base
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_armybase");
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_armybase");
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "restrictedareas");
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_doors");

            // LSIA
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_lossantosintl");

            // Prision
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prison");
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prisonvanbreak");

            // Maryweather
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "restrictedareas");
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_doors");
        }

        public static void DisableVendingMachine()
        {
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "ob_vend1");
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "ob_vend2");
        }
    }
}