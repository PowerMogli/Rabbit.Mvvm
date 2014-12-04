using SoftCareManager.Contracts.Model;
using System.Collections.ObjectModel;

namespace SoftCareManager.ViewModel
{
    public static class ModelExtension
    {
        public static ObservableCollection<object> Cast<TModel>(this ObservableCollection<TModel> source)
            where TModel : IModel
        {
            var items = new ObservableCollection<object>();
            if (source == null)
            {
                return items;
            }

            foreach(var model in source)
            {
                items.Add(model);
            }

            return items;
        }
    }
}