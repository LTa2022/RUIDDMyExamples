namespace RUIDDMyExamples
{
    public class ViewModelFactory<TModel, TViewModel>
    {
        private Action<TViewModel, TModel> _setModel;
        public ViewModelFactory(Action<TViewModel, TModel> SetModel)
        {
            _setModel = SetModel;
        }
        private readonly Dictionary<TModel, TViewModel> _viewModelMap = new Dictionary<TModel, TViewModel>();

        public TViewModel GetOrCreateViewModel(TModel model)
        {
            if (_viewModelMap.ContainsKey(model))
            {
                return _viewModelMap[model];
            }
            else
            {
                TViewModel newViewModel = (TViewModel)Activator.CreateInstance(typeof(TViewModel), model);
                //_setModel(newViewModel, model);
                _viewModelMap.Add(model, newViewModel);
                return newViewModel;
            }
        }
    }
}
