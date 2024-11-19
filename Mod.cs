using GDWeave;

namespace SmoreTweaks;

public class Mod : IMod {
    public Mod(IModInterface modInterface) {
        // Initializes my mod, game doesnt detect it otherwise
        modInterface.RegisterScriptMod(new SmoreTweaks(modInterface));
    }

    public void Dispose() {
        // Clean up resources if necessary
    }
}