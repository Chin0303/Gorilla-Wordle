using BepInEx;
using Gorilla_Wordle.CI;
using Bepinject;

namespace Gorilla_Wordle
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class Main : BaseUnityPlugin
    {
        internal const string
            GUID = "chin.gorillawordle",
            NAME = "Gorilla Wordle",
            VERSION = "1.0.0";

        private void Awake()
        {
            Zenjector.Install<ComputerInstaller>().OnProject();

        }
    }
}
