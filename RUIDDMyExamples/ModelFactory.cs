namespace RUIDDMyExamples
{
    public class ModelFactory<TModel> where TModel : new()
    {
        private readonly List<TModel> _models = new List<TModel>();

        public TModel CreateNewModel()
        {
            var newModel = new TModel();
            _models.Add(newModel);
            return newModel;
        }
    }
}
