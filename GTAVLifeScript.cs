using System;
using System.IO;
using System.Windows.Forms;
using GTA;
using LemonUI;
using LemonUI.Menus;
using GTAVLife;
using GTAVLife.Controller;
using GTAVLife.Helper;

public class GTAVLifeScript : Script
{
    // UI Related
    private ObjectPool objectPool;

    // Game Related
    private Router router;
    private Logger logger;

    public GTAVLifeScript()
    {
        string[] loggerPath = new string[] { BaseDirectory, "GTAVLifeScript.log" };
        logger = Logger.GetInstance(Path.Combine(loggerPath), LogLevel.Debug);
        router = Router.Instance;
        objectPool = new ObjectPool();

        foreach (IController controller in router.Controllers)
        {
            objectPool.Add(controller.View.Menu);
            foreach (NativeMenu submenu in controller.View.Submenus)
            {
                objectPool.Add(submenu);
            }
        }

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    public void OnTick(object sender, EventArgs e)
    {
        if (isFreeAndPlayable())
        {
            Gate.ControlGate();
            RestrictedZone.DisableRestrictedZone();
            RelationshipHelper.MakeFriendly(RelationshipGroupHash.ARMY);
            RelationshipHelper.MakeFriendly(RelationshipGroupHash.COP);
            RelationshipHelper.MakeFriendly(RelationshipGroupHash.MEDIC);
            RelationshipHelper.MakeFriendly(RelationshipGroupHash.FIREMAN);
        }
        else
        {
            objectPool.HideAll();
        }

        router.Process();
        objectPool.Process();
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F10)
        {
            if (isFreeAndPlayable())
            {
                if (!objectPool.AreAnyVisible)
                {
                    router.Route("main");
                }
                else
                {
                    router.Route("hide");
                }
            }
            else
            {
                showUnablePlayText();
            }
        }

        // Debug
        if (e.KeyCode == Keys.F11)
        {
            if (!objectPool.AreAnyVisible)
            {
                router.Route("debug");
            }
            else
            {
                router.Route("hide");
            }
        }
    }

    private bool isFreeAndPlayable()
    {
        return (
            !Game.IsMissionActive &&
            !Game.IsRandomEventActive &&
            !Game.IsCutsceneActive &&
            !Game.IsLoading &&
            Game.Player.CanControlCharacter
        );
    }

    private void showUnablePlayText()
    {
        if (Game.IsMissionActive)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~during a ~g~Mission~s~", beep: false);
        }
        else if (Game.IsRandomEventActive)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~when closing to a ~g~Random Event~s~", beep: false);
        }
        else if (Game.IsCutsceneActive)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~during a ~g~Cutscene~s~", beep: false);
        }
        else if (Game.IsLoading)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~during ~g~Loading~s~", beep: false);
        }
        else
        {
            GTA.UI.Screen.ShowHelpText("~g~Player~s~ is ~r~busy~s~", beep: false);
        }
    }
}
