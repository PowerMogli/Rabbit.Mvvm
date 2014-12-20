namespace SoftCareManager.Common.UI.Groups.Base
{
    public class GroupShellRelation
    {
        private readonly string groupName;
        private readonly int shellId;

        public GroupShellRelation(string groupName, int shellId)
        {
            this.groupName = groupName;
            this.shellId = shellId;
        }

        internal string GroupName { get { return groupName; } }

        internal int ShellId { get { return shellId; } }
    }
}