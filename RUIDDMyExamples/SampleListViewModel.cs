using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody;
using ReactiveUI.Fody.Helpers;



namespace RUIDDMyExamples
{

    public class SampleListViewModel : ReactiveObject
    {


        public ModelFactory<SampleModel> SampleModelFactory = new();
        public ViewModelFactory<SampleModel, SampleViewModel> SampleViewModelFactory = new((vm, m) => vm.Model = m);

        [Reactive]
        public Paging Paging { get; set; } = new();

        [Reactive]
        public string Search { get; set; } = "";

        [Reactive]
        public bool NeedsReviewFilter { get; set; }


        public ObservableCollection<SampleModel> models = new();

        [Reactive]
        public SampleViewModel Selected { get; set; }


        public ReadOnlyObservableCollection<SampleViewModel> modelVMs;


        public SampleListViewModel()
        {



            foreach (var id in Enumerable.Range(0, 1000))
            {
                var m = SampleModelFactory.CreateNewModel();
                m.Id = id;
                m.NeedsReview = id > 800 && id < 900;
                models.Add(m);
            }





            var wav_searchfilters = this.WhenAnyValue(x => x.Search, x => x.NeedsReviewFilter);





            wav_searchfilters.Subscribe();


            var last_filtered_models = models.ToObservableChangeSet().AutoRefreshOnObservable(_ => wav_searchfilters).AutoRefresh(x => x.NeedsReview)
                .Filter(x => !NeedsReviewFilter && !x.NeedsReview && x.Id.ToString().Contains(Search) || (x.NeedsReview && NeedsReviewFilter) && x.Id.ToString().Contains(Search))
                .Sort(SortExpressionComparer<SampleModel>.Descending(x => models.IndexOf(x)));

            last_filtered_models.Subscribe();





            var last_filtered_vms = last_filtered_models
                .PageEx(Paging).Select(x => x).Transform(x => SampleViewModelFactory.GetOrCreateViewModel(x));






            last_filtered_vms.Bind(out modelVMs).Do(x =>
            {
                SetSelectedIfSingle();

            }).Subscribe();



            Paging.PageUpdate.Do(x => SetSelectedIfSingle()).Subscribe();






        }


        private void SetSelectedIfSingle()
        {
            if (modelVMs?.Count() >= 1)
                Selected = modelVMs.First();

        }

    }
}
