using ShellBoost.Core;
using ShellBoost.Core.Utilities;
using ShellBoost.Core.WindowsShell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ShellBoost_Project
{
    public abstract class DisplayContainer : ShellFolder
    {
        public DisplayContainer(ShellFolder parent, StringKeyShellItemId name)
            : base(parent, name)
        {
            IsDropTarget = true;
        }

        public DisplayContainer(ShellItemIdList idList)
        : base(idList)
        {
        }
        public override IEnumerable<ShellItem> EnumItems(SHCONTF options)
        {
            APICalls apiObj = new APICalls();

            apiObj.RunAsyncGetFileIdByFilePath(FullDisplayName).Wait();

            apiObj.RunAsyncGetFirstChildById(apiObj.currentFileId).Wait();

            for (int i = 0; i < apiObj.maxItemsToDisplay; i++)
            {
                if (string.Equals(apiObj.childItemsDetails[i].FileType, "file", StringComparison.OrdinalIgnoreCase))
                {
                    yield return new Items(this, apiObj.childItemsDetails[i].Path, new FileInfo(apiObj.childItemsDetails[i].Path));

                }
                else
                {
                    yield return new Folders(this, apiObj.childItemsDetails[i].Path);
                }
            }
        }

        //context menu 
        protected override void MergeContextMenu(ShellFolder folder, IReadOnlyList<ShellItem> items, ShellMenu existingMenu, ShellMenu appendMenu)
        {
            if (items.Count > 0)
            {
                appendMenu.Items.Add(new ShellMenuSendToItem());
            }
        }

        //information bar
        protected override InformationBar GetInformationBar()
        {
            var bar = new InformationBar();
            bar.Guid = Guid.NewGuid();
            bar.Message = "This is a Sample Information bar (Click to get the Menu)";
            return bar;
        }
        protected override void CreateInformationBarMenu(InformationBar bar, ShellMenu appendMenu)
        {
            var item = new ShellMenuItem(appendMenu, "Which folder am I currently in?");
            appendMenu.Items.Add(item);
        }
        protected override async void HandleInformationBarMenu(InformationBar bar, IntPtr hwndOwner, int id)
        {
            if (id == 1)
            {
                await WindowsUtilities.DoModelessAsync(() =>
                {
                    MessageBox.Show(new Win32Window(hwndOwner), "Folder Path is: " + FullDisplayName, "Current Folder");
                });
                return;
            }
        }

        //drag and drop support
        protected async override void OnDragDropTarget(DragDropTargetEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            if (e.Type == DragDropTargetEventType.DragDrop)
            {
                var list = string.Join(Environment.NewLine, e.DataObject.ItemsIdLists.Select(id => id.GetName(SIGDN.SIGDN_NORMALDISPLAY)));
                Console.WriteLine(list);
                await WindowsUtilities.DoModelessAsync(() =>
                {
                    MessageBox.Show(null, "Drag and Drop for virtual items is working", "Drag And Drop Functionality");
                });
            }
        }

        //operation support(rename, delete etc)
        protected override void OnOperate(ShellOperationEventArgs e)
        {
            switch (e.Operation)
            {
                case ShellOperation.SetNameOf:
                case ShellOperation.RenameItem:
                    OnRename(e);
                    RefreshShellViews();
                    break;

                case ShellOperation.RemoveItem:
                    OnRemove(e);
                    RefreshShellViews();
                    break;
            }
        }
        private void OnRename(ShellOperationEventArgs e)
        {
            APICalls apiObj = new APICalls();
            apiObj.RunAsyncGetFileIdByFilePath(e.Item.FullDisplayName).Wait();
            string[] arr = Regex.Split(e.Item.FullDisplayName, @"\\");
            string newpath = "";
            for (int i = 0; i < arr.Length - 1; i++)
                newpath += arr[i] + "\\";
            newpath += e.NewName;
            apiObj.RunAsyncRenameByFileId(apiObj.currentFileId, newpath).Wait();
        }
        private void OnRemove(ShellOperationEventArgs e)
        {
            APICalls apiObj = new APICalls();
            apiObj.RunAsyncGetFileIdByFilePath(e.Item.FullDisplayName).Wait();
            apiObj.RunAsyncDeleteByFileId(apiObj.currentUpdateFileId).Wait();
        }
    }
}
