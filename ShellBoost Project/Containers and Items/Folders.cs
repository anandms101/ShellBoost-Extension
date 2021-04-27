using ShellBoost.Core;
using ShellBoost.Core.WindowsPropertySystem;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShellBoost_Project
{
    public class Folders : DisplayContainer
    {
        //custom property description
        public static readonly PropertyDescription IconUIProperty = PropertySystem.GetPropertyDescription("ShellBoost.Samples.LocalFolder.IconUI", true);
        public static readonly PropertyDescription IconProperty = PropertySystem.GetPropertyDescription("ShellBoost.Samples.LocalFolder.Icon", true);
        
        public Folders(ShellFolder parent, string name)
           : base(parent, new StringKeyShellItemId(name))
        {
            ItemType = "directory";
            DisplayName = Regex.Split(name, @"\\")[Regex.Split(name, @"\\").Length - 1];

            AddColumn(IconUIProperty);

            CanCopy = true;
            CanPaste = true;
            CanMove = true;
            CanDelete = true;
            CanRename = true;
        }

        //context menu for folders
        protected override void MergeContextMenu(ShellFolder folder, IReadOnlyList<ShellItem> items, ShellMenu existingMenu, ShellMenu appendMenu)
        {
            if (items.Count == 1)
            {
                var modifyItem = new ShellMenuItem(appendMenu, "Custom Item Context");
                appendMenu.Items.Add(modifyItem);

                modifyItem.BitmapPath = @"f:\fileimage2.png";
                modifyItem.Items.Add(new ShellMenuItem(appendMenu, "Option 1"));
                modifyItem.Items.Add(new ShellMenuItem(appendMenu, "Option 2"));
                modifyItem.Items.Add(new ShellMenuSeparatorItem());
                modifyItem.Items.Add(new ShellMenuItem(appendMenu, "Option 3"));
            }
        }
    }
}
