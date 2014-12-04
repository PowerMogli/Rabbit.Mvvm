using SoftCareManager.Contracts.Authorization;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SoftCareManager.Common.UI.Authorization
{
    public class UIVisibilityBinder
    {
        private IDictionary<Right, RoleRightRelation> _roleRightRelations;
        private IUser _currentUser;

        public UIVisibilityBinder(IEnumerable<RoleRightRelation> roleRightRelations, IUser currentUser)
        {
            _roleRightRelations = new Dictionary<Right, RoleRightRelation>();

            InitializeDictionary(roleRightRelations);

            _currentUser = currentUser;
        }

        private void InitializeDictionary(IEnumerable<RoleRightRelation> roleRightRelations)
        {
            foreach (var relation in roleRightRelations)
            {
                _roleRightRelations.Add(relation.Right, relation);
            }
        }

        public void UpdateCurrentUser(IUser currentUser)
        {
            _currentUser = currentUser;

            foreach (var relation in _roleRightRelations)
            {
                relation.Value.SetVisibility(_currentUser);
            }
        }

        public void SetVisibility(UIElement uiElement, Right right)
        {
            RoleRightRelation relation;
            if (_roleRightRelations.TryGetValue(right, out relation) == false)
            {
                return;
            }

            relation.SetVisibility(_currentUser);
            BindingOperations.SetBinding(uiElement, UIElement.VisibilityProperty, new Binding("IsVisible")
            {
                Source = relation,
                Converter = new BooleanToVisibilityConverter()
            });
        }
    }
}