using ShellBoost.Core;
using ShellBoost.Core.WindowsPropertySystem;

namespace ShellBoost_Project
{
    public class CustomPropertyHandler
    {
        private string schemaLocation;

        public CustomPropertyHandler(string fullFileName)
        {
            schemaLocation = new ShellFolderConfiguration().ExtractAssemblyResource(typeof(Program).Namespace + ".Resources." + fullFileName);
        }
        public void schemaPropertyRegister()
        {
            PropertySystem.RegisterPropertySchema(schemaLocation);
        }
        public void schemaPropertyUnRegister()
        {
            PropertySystem.UnregisterPropertySchema(schemaLocation);
        }
    }
}
