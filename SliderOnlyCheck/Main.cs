using MapsetVerifierFramework;
using System.Globalization;

namespace SliderOnlyCheck
{
    public class Main
    {
        public static void Run() {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CheckerRegistry.RegisterCheck(new CheckSliderOnly());
        }
    }
}
