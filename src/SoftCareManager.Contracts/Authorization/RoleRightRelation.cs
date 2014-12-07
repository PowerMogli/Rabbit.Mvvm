using System.Collections.Generic;
using System.Linq;

using SoftCareManager.Contracts.General;

namespace SoftCareManager.Contracts.Authorization
{
    public class RoleRightRelation : ObservableObject
    {
        private bool _isVisible;

        public RoleRightRelation(Role roles, Right right)
        {
            Roles = roles;
            Right = right;
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        public Right Right { get; private set; }

        public Role Roles { get; private set; }

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
            IEnumerable<Role> flags = Roles.GetFlags();
            return flags.Any(flag => userRoles.HasFlag(flag));
        }
    }
}