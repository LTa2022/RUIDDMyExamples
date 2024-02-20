using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;



namespace RUIDDMyExamples
{
    /// <summary>
    /// Use this with PageEx in an Observable chain instead of Page. 
    /// PageUpdate is public, and can be used to react on changes.
    /// </summary>
    public class Paging : ReactiveObject
    {

        [Reactive]
        PageRequest pagereq { get; set; }
        private BehaviorSubject<IPageRequest> pageSizeRequest;

        public BehaviorSubject<IPageRequest> GetPageInfo() => pageSizeRequest;


        /// <summary>
        /// PerPage or CurrentPage changed.
        /// </summary>
        public IObservable<(int, int)> PageUpdate;


        [Reactive]
        public int CurrentPage { get; private set; } = 1;

        [Reactive]
        public int PerPage { get; private set; } = 10;

        [Reactive]
        public int MaxPage { get; private set; } = 10;

        private int lastcount = 0;

        public ReactiveCommand<Unit, Unit> NextPageCommand;
        public ReactiveCommand<Unit, Unit> PrevPageCommand;


        public Paging()
        {
            pagereq = new PageRequest(0, 2);
            pageSizeRequest = new BehaviorSubject<IPageRequest>(pagereq);



            PageUpdate = this.WhenAnyValue(x => x.PerPage, x => x.CurrentPage).Do(x => Update());

            PageUpdate.Subscribe();

            NextPageCommand = ReactiveCommand.Create(() => this.Next());
            PrevPageCommand = ReactiveCommand.Create(() => this.Previous());

        }


        public void UpdateCount(int count)
        {
            lastcount = count;
            Update();

        }

        private void Previous()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
            }
        }

        private void Next()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
            }
        }

        private void Update()
        {

            MaxPage = (int)Math.Ceiling((double)lastcount / (double)PerPage);

            pageSizeRequest.OnNext(new PageRequest(CurrentPage, PerPage));

            if (CurrentPage > MaxPage)
                CurrentPage = 1;
        }


    }
}
