using ShellBoost.Core;

namespace ShellBoost_Project
{
    public abstract class DisplayItem : ShellItem
    {
        public DisplayItem(ShellFolder parent, StringKeyShellItemId name)
        : base(parent, name)
        {
            CanCopy = true;
            CanPaste = true;
            CanMove = true;
            CanRename = true;
            CanDelete = true;
        }


    }
}
