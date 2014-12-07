using System.Collections.Generic;

namespace SoftCareManager.Contracts.Authorization
{
    // ReSharper disable once InconsistentNaming
    public interface IUIVisibilityBinder
    {
        void Initialize(IEnumerable<RoleRightRelation> roleRightRelations, IUser currentUser);

        void SetVisibility(object element, Right right);

        void UpdateCurrentUser(IUser currentUser);
    }
}