using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody;
using ReactiveUI.Fody.Helpers;



namespace RUIDDMyExamples
{
    public class SampleViewModel : ReactiveObject
    {
        public SampleViewModel( SampleModel model)
        {
            Model = model;





        }


        [Reactive]
        public SampleModel Model { get; set; }
    }
}
