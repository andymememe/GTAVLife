using System;
using System.IO;
using System.Windows.Forms;
using GTA;
using LemonUI;
using LemonUI.Menus;
using GTAVLife;
using GTAVLife.GameData;
using GTAVLife.Controller;
using GTAVLife.Helper;

public class GTAVLifeScript : Script
{
    // UI Related
    private ObjectPool objectPool;

    // Game Related
    private Router router;
    private Logger logger;
    private Save save;
    private SaveManager saveManager;

    public GTAVLifeScript()
    {
        string[] loggerPath = new string[] { BaseDirectory, "GTAVLifeScript.log" };
        logger = Logger.GetInstance(Path.Combine(loggerPath), LogLevel.Debug);

        string[] savePath = new string[] { BaseDirectory, "GTAVLifeScript.json" };
        save = Save.GetInstance(Path.Combine(savePath));
        saveManager = SaveManager.GetInstance(save);

        router = Router.Instance;
        objectPool = new ObjectPool();

        foreach (IController controller in router.Controllers)
        {
            if (controller.View != null)
            {
                objectPool.Add(controller.View.Menu);
                foreach (NativeMenu submenu in controller.View.Submenus)
                {
                    objectPool.Add(submenu);
                }
            }
        }

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    public void OnTick(object sender, EventArgs e)
    {
        if (isFreeAndPlayable())
        {
            if (Life.Instance.IsActivate)
            {
                Gate.ControlGate();
                ScriptTerminator.DisableEverything();
            }
        }
        else
        {
            objectPool.HideAll();
        }

        saveManager.Process();
        router.Process();
        objectPool.Process();
    }

    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F10)
        {
            if (isFreeAndPlayable())
            {
                objectPool.HideAll();
                router.Route("main");
            }
            else
            {
                showUnablePlayText();
            }
        }

        // Debug
        if (e.KeyCode == Keys.F11)
        {
            objectPool.HideAll();
            router.Route("debug");
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
