using ShellBoost.Core;
using ShellBoost.Core.WindowsShell;
using System.Collections.Generic;

namespace ShellBoost_Project
{
    public class RootFolderDisplay : DisplayContainer
    {
        public RootFolderDisplay(ShellItemIdList idList)
        : base(idList)
        {
        }
        public override IEnumerable<ShellItem> EnumItems(SHCONTF options)
        { 
            APICalls apiObj = new APICalls();

            apiObj.RunAsyncGetAllDrives().Wait();

            for (int i = 0; i < apiObj.maxItemsToDisplay; i++)
            {
                yield return new VirtualDrives(this, apiObj.childItemsDetails[i].Path);
            }
        }

        protected override void MergeContextMenu(ShellFolder folder, IReadOnlyList<ShellItem> items, ShellMenu existingMenu, ShellMenu appendMenu)
        {
            if (items.Count == 1)
            {
                var modifyItem = new ShellMenuItem(appendMenu, "Custom Drive Context");
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
