using ShellBoost.Core;

namespace ShellBoost_Project
{
    public class DisplayItemServer : ShellFolderServer
    {
        private RootFolderDisplay _root;
        protected override ShellFolder GetFolderAsRoot(ShellItemIdList idList)
        {
            if (_root == null)
            {
                _root = new RootFolderDisplay(idList);
            }
            return _root;
        }
    }
}
