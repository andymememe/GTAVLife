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
    private SaveHelper save;
    private SaveManager saveManager;

    public GTAVLifeScript()
    {
        string[] loggerPath = new string[] { BaseDirectory, "GTAVLifeScript.log" };
        logger = Logger.GetInstance(Path.Combine(loggerPath), LogLevel.Debug);

        string[] savePath = new string[] { BaseDirectory, "GTAVLifeScript.json" };
        save = SaveHelper.GetInstance(Path.Combine(savePath));
        saveManager = SaveManager.GetInstance(save);
        saveManager.Load();

        string[] charsPath = new string[] { BaseDirectory, "chars.json" };
        MPFreemodeModels.Instance.LoadModels(Path.Combine(charsPath));

        router = Router.Instance;
        objectPool = new ObjectPool();

        foreach (IController controller in router.Controllers)
        {
            if (controller.View != null)
            {
                logger.Debug(controller.View.Menu.Title.Text + "\t" + controller.View.Menu.Subtitle + "\t" + controller.GetType().ToString());
                objectPool.Add(controller.View.Menu);
                if (controller.View.Submenus != null)
                {
                    foreach (NativeMenu submenu in controller.View.Submenus)
                    {
                        logger.Debug("\t" + submenu.Title.Text + "\t" + submenu.Subtitle);
                        objectPool.Add(submenu);
                    }
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
            if (GTAVLife.GameData.Environment.Instance.IsActivated)
            {
                Gate.ControlGate();
                ScriptTerminator.DisableEverything();
            }
            
            if (GTAVLife.GameData.Environment.Instance.IsJustInitialized)
            {
                Life.Instance.ForceDirty();
                objectPool.HideAll();
                router.Route("main");
                GTAVLife.GameData.Environment.Instance.IsJustInitialized = false;
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
                if (Life.Instance.Name == null)
                {
                    router.Route("char");
                }
                else
                {
                    router.Route("main");
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
            objectPool.HideAll();
            router.Route("debug");
        }
    }

    private bool isFreeAndPlayable()
    {
        return (
            !GameStatus.IsMissionActive &&
            !GameStatus.IsRandomEventActive &&
            !GameStatus.IsCutsceneActive &&
            !GameStatus.IsLoading &&
            PlayerInfo.Player.CanControlCharacter
        );
    }

    private void showUnablePlayText()
    {
        if (GameStatus.IsMissionActive)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~during a ~g~Mission~s~", beep: false);
        }
        else if (GameStatus.IsRandomEventActive)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~when closing to a ~g~Random Event~s~", beep: false);
        }
        else if (GameStatus.IsCutsceneActive)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~during a ~g~Cutscene~s~", beep: false);
        }
        else if (GameStatus.IsLoading)
        {
            GTA.UI.Screen.ShowHelpText("You cannot ~r~Change Life ~s~during ~g~Loading~s~", beep: false);
        }
        else
        {
            GTA.UI.Screen.ShowHelpText("~g~Player~s~ is ~r~busy~s~", beep: false);
        }
    }
}
