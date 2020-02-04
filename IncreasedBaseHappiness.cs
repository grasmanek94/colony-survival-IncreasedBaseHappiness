using System.IO;
using Pipliz;
using static ModLoader;
using static ServerManager;

namespace grasmanek94.IncreasedBaseHappiness
{
    [ModManager]
    public static class IncreasedBaseHappiness
    {
        static int baseHappiness;

        [ModCallback(EModCallbackType.OnAssemblyLoaded, "OnAssemblyLoaded")]
        static void OnLoad(string assemblyPath)
        {        
            baseHappiness = 1000;
            string config = Path.Combine(Path.GetDirectoryName(assemblyPath), "baseHappiness.txt");
            if (File.Exists(config))
            {
                string data = File.ReadAllText(config);
                if(!int.TryParse(data, out baseHappiness))
                {
                    Log.WriteError("IncreasedBaseHappiness: Failed to parse baseHappiness.txt");
                } else {
                    baseHappiness = Math.Clamp(baseHappiness, 1, int.MaxValue / 2); 
                }             
            }

            Log.Write("IncreasedBaseHappiness: baseHappiness = {0}", baseHappiness);
        }

        [ModCallback(EModCallbackType.AfterWorldLoad, "AfterWorldLoad")]
        static void AfterWorldLoad()
        {
            ServerSettings.Colony.BaseHappiness = baseHappiness;
        }
    }
}
