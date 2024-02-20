using System.Reactive.Linq;
using DynamicData;



namespace RUIDDMyExamples
{
    public static class PageExtensions
    {


        public static IObservable<IChangeSet<T>> PageEx<T>(this IObservable<IChangeSet<T>> changeset, Paging pager)
        {


            changeset.ToCollection().Do(x =>
                 {
                     pager.UpdateCount(x.Count());

                 }).Subscribe();





            return changeset.Page(pager.GetPageInfo());


        }

    }
}
