using SoftCareManager.Contracts.General;
using System.Linq;

namespace SoftCareManager.Contracts.Authorization
{
    public class RoleRightRelation : ObservableObject
    {
        public Role Roles { get; private set; }
        public Right Right { get; private set; }

        private bool _isVisible;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        public RoleRightRelation(Role roles, Right right)
        {
            this.Roles |= roles;
            this.Right = right;
        }

        public void SetVisibility(IUser user)
        {
            IsVisible = IsUserInAnyRoles(user.Roles)
                && HasUserRights(user.Rights);
        }

        private bool HasUserRights(Right userRights)
        {
            return userRights.HasFlag(Right);
        }

        private bool IsUserInAnyRoles(Role userRoles)
        {
            var flags = Roles.GetFlags();
            return flags.Any(flag => userRoles.HasFlag(flag));
        }
    }
}