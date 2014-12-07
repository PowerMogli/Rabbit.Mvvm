using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using SoftCareManager.Contracts.Authorization;

namespace SoftCareManager.Common.UI.Authorization
{
    // ReSharper disable once InconsistentNaming
    public class UIVisibilityBinder : IUIVisibilityBinder
    {
        public static readonly DependencyProperty AssociatedRightProperty = DependencyProperty.RegisterAttached("AssociatedRight",
                                                                                                                typeof(string),
                                                                                                                typeof(UIVisibilityBinder),
                                                                                                                new PropertyMetadata(default(string), OnRightNamePropertyChanged));

        public static readonly DependencyProperty UiVisibilityBinderProperty = DependencyProperty.RegisterAttached("UiVisibilityBinder",
                                                                                                                   typeof(IUIVisibilityBinder),
                                                                                                                   typeof(UIVisibilityBinder));

        private readonly IDictionary<Right, RoleRightRelation> _roleRightRelations;
        private IUser _currentUser;

        public UIVisibilityBinder()
        {
            _roleRightRelations = new Dictionary<Right, RoleRightRelation>();
        }

        public static string GetAssociatedRight(DependencyObject element)
        {
            return (string)element.GetValue(AssociatedRightProperty);
        }

        public static IUIVisibilityBinder GetUiVisibilityBinder(DependencyObject element)
        {
            return (IUIVisibilityBinder)element.GetValue(UiVisibilityBinderProperty);
        }

        public static void SetAssociatedRight(DependencyObject element, string value)
        {
            element.SetValue(AssociatedRightProperty, value);
        }

        public static void SetUiVisibilityBinder(DependencyObject element, IUIVisibilityBinder value)
        {
            element.SetValue(UiVisibilityBinderProperty, value);
        }

        public void Initialize(IEnumerable<RoleRightRelation> roleRightRelations, IUser currentUser)
        {
            InitializeDictionary(roleRightRelations);

            _currentUser = currentUser;
        }

        public void SetVisibility(object element, Right right)
        {
            FrameworkElement frameworkElement = element as FrameworkElement;
            RoleRightRelation relation;

            if (frameworkElement == null
                || _roleRightRelations.TryGetValue(right, out relation) == false)
            {
                return;
            }

            relation.SetVisibility(_currentUser);
            frameworkElement.Loaded += OnUiElementLoaded(frameworkElement, relation);
        }

        public void UpdateCurrentUser(IUser currentUser)
        {
            _currentUser = currentUser;

            foreach (var relation in _roleRightRelations)
            {
                relation.Value.SetVisibility(_currentUser);
            }
        }

        private static MultiBinding CreateMultiBinding(UIElement uiElement, RoleRightRelation relation)
        {
            Binding currentVisibilityBinding = BindingOperations.GetBinding(uiElement, UIElement.VisibilityProperty);
            if (currentVisibilityBinding == null)
            {
                return null;
            }

            MultiBinding multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(currentVisibilityBinding);
            multiBinding.Bindings.Add(new Binding("IsVisible")
            {
                Source = relation,
            });

            multiBinding.NotifyOnSourceUpdated = true;
            multiBinding.Converter = new RightVisibilityConverter();

            return multiBinding;
        }

        private static void OnRightNamePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            string rightName = e.NewValue as string;
            if (string.IsNullOrWhiteSpace(rightName))
            {
                return;
            }

            IUIVisibilityBinder uiVisibilityBinder = GetUiVisibilityBinder(Application.Current.MainWindow);
            Right right = (Right)Enum.Parse(typeof(Right), rightName);
            uiVisibilityBinder.SetVisibility(dependencyObject, right);
        }

        private static RoutedEventHandler OnUiElementLoaded(FrameworkElement frameworkElement, RoleRightRelation relation)
        {
            RoutedEventHandler onLoadedEventHandler = null;
            onLoadedEventHandler = (s, e) =>
            {
                frameworkElement.Loaded -= onLoadedEventHandler;
                SetVisibilityBinding(frameworkElement, relation);
            };

            return onLoadedEventHandler;
        }

        private static void SetVisibilityBinding(UIElement uiElement, RoleRightRelation relation)
        {
            MultiBinding multiBinding = CreateMultiBinding(uiElement, relation);

            BindingOperations.ClearBinding(uiElement, UIElement.VisibilityProperty);
            BindingOperations.SetBinding(uiElement, UIElement.VisibilityProperty, multiBinding as BindingBase ?? new Binding("IsVisible")
            {
                Source = relation,
                Converter = new BooleanToVisibilityConverter()
            });
        }

        private void InitializeDictionary(IEnumerable<RoleRightRelation> roleRightRelations)
        {
            foreach (var relation in roleRightRelations)
            {
                _roleRightRelations.Add(relation.Right, relation);
            }
        }
    }
}