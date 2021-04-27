using ShellBoost.Core;
using ShellBoost.Core.WindowsPropertySystem;
using System.Collections.Generic;
using System.Linq;

namespace ShellBoost_Project
{
    public class VirtualDrives : DisplayContainer
    {
        //custom property description
        public static readonly PropertyDescription IconUIProperty = PropertySystem.GetPropertyDescription("ShellBoost.Samples.LocalFolder.IconUI", true);
        public static readonly PropertyDescription IconProperty = PropertySystem.GetPropertyDescription("ShellBoost.Samples.LocalFolder.Icon", true);
       
        public VirtualDrives(RootFolderDisplay parent, string name)
        : base(parent, new StringKeyShellItemId(name))
        {
            ItemType = ".vhd";
            var iconsPath = @"C:\Windows\System32\imageres.dll";
            Thumbnail = new ShellThumbnail(iconsPath,27);

            AddColumn(IconUIProperty);
        }

        //context menu for drives
        protected override void MergeContextMenu(ShellFolder folder, IReadOnlyList<ShellItem> items, ShellMenu existingMenu, ShellMenu appendMenu)
        {
            if (items.Count == 1 && !items[0].IsFolder)
            {
                var modifyItem = new ShellMenuItem(appendMenu, "Custom Item Context");
                appendMenu.Items.Add(modifyItem);

                modifyItem.BitmapPath = @"f:\fileimage2.png";
                modifyItem.Items.Add(new ShellMenuItem(appendMenu, "Option 1"));
                modifyItem.Items.Add(new ShellMenuItem(appendMenu, "Option 2"));
                modifyItem.Items.Add(new ShellMenuSeparatorItem());
                modifyItem.Items.Add(new ShellMenuItem(appendMenu, "Option 3"));
            }
            else if (items.Count == 1 && items[0].IsFolder)
            {
                if (existingMenu.Items.FirstOrDefault(i => i.Text == "Custom Folder Context") == null)
                {
                    var newItem = new ShellMenuItem(appendMenu, "Custom Folder Context");
                    appendMenu.Items.Add(newItem);

                    newItem.BitmapPath = @"f:\folderimage2.png";
                    newItem.Items.Add(new ShellMenuItem(appendMenu, "Option 1"));
                    newItem.Items.Add(new ShellMenuItem(appendMenu, "Option 2"));
                    newItem.Items.Add(new ShellMenuSeparatorItem());
                    newItem.Items.Add(new ShellMenuItem(appendMenu, "Option 3"));
                }
            }
        }
    }
}
