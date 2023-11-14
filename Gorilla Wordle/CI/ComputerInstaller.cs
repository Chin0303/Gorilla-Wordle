using ComputerInterface.Interfaces;
using Zenject;

namespace Gorilla_Wordle.CI
{
    internal class ComputerInstaller : Installer
    {
        public override void InstallBindings() => Container.Bind<IComputerModEntry>().To<WordleEntry>().AsSingle();
    }
}
