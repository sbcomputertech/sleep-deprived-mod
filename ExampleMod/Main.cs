using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SleepDeprivedMod;

[BepInPlugin(ModGuid, ModName, ModVersion)]
public class Main : BaseUnityPlugin
{
    private const string ModName = "Sleep Deprived";
    private const string ModAuthor  = "reddust9";
    private const string ModGuid = "com.reddust9.sd";
    private const string ModVersion = "1.0.0";
    internal void Awake()
    {
        var harmony = new Harmony(ModGuid);
        harmony.PatchAll();
        Logger.LogInfo($"{ModName} successfully loaded! Made by {ModAuthor}");
        SceneManager.sceneLoaded += OnSceneLoadEvent;
    }

    private void OnSceneLoadEvent(Scene arg0, LoadSceneMode arg1)
    {
        var fail = arg0.GetRootGameObjects()
            .Where(o => o.name.Contains("Background") || o.name.Contains("Lights"))
            .Select(o => o.SetActiveReturn(false))
            .Any();
        if(fail) Logger.LogWarning("Not all objects were set inactive!");
    }
}

public static class Extensions
{
    public static bool SetActiveReturn(this GameObject go, bool active)
    {
        go.SetActive(active);
        return active;
    }
}