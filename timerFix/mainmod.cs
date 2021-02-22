using Harmony;
using BWModLoader;
using UnityEngine;
using System.IO;

namespace timerFix
{
    [Mod]
    public class mainmod : MonoBehaviour
    {

        public static mainmod Instance;
        static Vector3 defaultPos;
        static Vector3 offset = new Vector3(0, 0, 0);
        static string configFile = "timerFix.cfg";
        static bool changeRuntime = false;
        static bool hasStarted = false;
        static string updateKey;

        void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(this);
            }
        }

        void Update()
        {
            if (Input.GetKeyUp(updateKey) && changeRuntime)
            {
                Logger.log("updating...");
                loadFromConfig();
                if (hasStarted)
                {
                    Logger.log("Not null");
                    applyOffset();
                }
            }
        }

        void loadFromConfig()
        {
            fileExists();
            string[] array = File.ReadAllLines(configFile);
            char splitCharacter = '=';
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Contains("="))
                {
                    string[] splitArr = array[i].Split(splitCharacter);
                    if (splitArr.Length >= 2)
                    {
                        switch (splitArr[0])
                        {
                            case "Change_at_runtime":
                                bool.TryParse(splitArr[1], out changeRuntime);
                                Logger.log("Set to: " + changeRuntime);
                                break;
                            case "Update_key":
                                updateKey = splitArr[1];
                                Logger.log("Set to: -" + splitArr[1] + "-");
                                break;
                            case "x":
                                float.TryParse(splitArr[1], out offset.x);
                                break;
                            case "y":
                                float.TryParse(splitArr[1], out offset.y);
                                break;
                            case "z":
                                float.TryParse(splitArr[1], out offset.z);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            Logger.log("Loaded offset: " + offset.ToString());
        }

        void fileExists()
        {
            if (!File.Exists(configFile))
            {
                StreamWriter streamWriter = new StreamWriter(configFile);
                streamWriter.WriteLine("Update_key=[");
                streamWriter.WriteLine("Change_at_runtime=true");
                streamWriter.WriteLine("x=0");
                streamWriter.WriteLine("y=0");
                streamWriter.WriteLine("z=0");
                streamWriter.Close();
            }
        }

        void Start()
        {
            //Setup harmony patching
            HarmonyInstance harmony = HarmonyInstance.Create("com.github.archie");
            harmony.PatchAll();

            loadFromConfig();
        }

        static void applyOffset()
        {
            UI.Instance.matchHUD.íðæåñóççíìè.transform.localPosition = defaultPos;
            Logger.log("Set to Default: " + defaultPos.ToString());
            UI.Instance.matchHUD.íðæåñóççíìè.transform.localPosition += offset;
            Logger.log("Set offset: " + offset.ToString());
            Logger.log("Set to: " + UI.Instance.matchHUD.íðæåñóççíìè.transform.localPosition.ToString());
        }

        [HarmonyPatch(typeof(MatchHUD), "Start")]
        static class getHudPatch
        {
            static void Postfix(MatchHUD __instance)
            {
                hasStarted = true;
                defaultPos = UI.Instance.matchHUD.íðæåñóççíìè.transform.localPosition;
                Logger.log("Set Default: " + defaultPos.ToString());
            }
        }

        [HarmonyPatch(typeof(MatchHUD), "OnEnable")]
        static class applyOffsetPatch
        {
            static void Postfix()
            {
                applyOffset();
            }
        }
    }
}
