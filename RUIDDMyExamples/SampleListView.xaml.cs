using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody;
using ReactiveUI.Fody.Helpers;



namespace RUIDDMyExamples
{
    /// <summary>
    /// Interaction logic for SampleListView.xaml
    /// </summary>
    public partial class SampleListView
    {
        public SampleListView()
        {
            InitializeComponent();
            ViewModel = new SampleListViewModel();
            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Paging.CurrentPage, v => v.tbxPageNr.Text).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Paging.PerPage, v => v.tbxPerPage.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Paging.MaxPage, v => v.lblMaxPage.Content).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.NeedsReviewFilter, v => v.cbxReview.IsChecked).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.modelVMs, v => v.icSamples.ItemsSource).DisposeWith(d);
                this.Bind(ViewModel, vm => vm.Search, v => v.tbxSearch.Text).DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.Paging.NextPageCommand, v => v.btnNext).DisposeWith(d); ;
                this.BindCommand(ViewModel, vm => vm.Paging.PrevPageCommand, v => v.btnPrev).DisposeWith(d); ;
              

            });


        }
    }
}
