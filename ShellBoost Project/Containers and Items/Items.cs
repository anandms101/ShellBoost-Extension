using ShellBoost.Core;
using ShellBoost.Core.Client;
using ShellBoost.Core.WindowsPropertySystem;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Props = ShellBoost.Core.WindowsPropertySystem;

namespace ShellBoost_Project
{
    public class Items : DisplayItem
    {
        public Items(ShellFolder parent, string name, FileInfo info)
        : base(parent, new StringKeyShellItemId(name))
        {
            DisplayName = Regex.Split(name, @"\\")[Regex.Split(name, @"\\").Length - 1];

            ItemType = "." + Regex.Split(DisplayName, @"\.")[Regex.Split(DisplayName, @"\.").Length - 1];

            //adding property value for all items
            var ms = new MemoryPropertyStore();
            ms.SetValue(Props.System.PropList.StatusIcons, "prop:" + Folders.IconProperty.CanonicalName);
            ms.SetValue(Props.System.PropList.StatusIconsDisplayFlag, (uint)2);

            if (info.Name.Contains("error"))
            {
                ms.SetValue(Folders.IconProperty, IconValue.Error);
            }
            else if (info.Name.Contains("warn"))
            {
                ms.SetValue(Folders.IconProperty, IconValue.Warning);
            }
            else
            {
                ms.SetValue(Folders.IconProperty, IconValue.Ok);
            }
            SetPropertyValue(Folders.IconUIProperty, ms);
        }

        //dynamic content
        public override ShellContent GetContent() => new MemoryShellContent(DisplayName + " - this is dynamic content created from .NET " + (Installer.IsNetCore ? "Core" : "Framework") + " at " + DateTime.Now);
    }
}
