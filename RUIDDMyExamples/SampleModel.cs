using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace RUIDDMyExamples
{
    public class SampleModel : ReactiveObject
    {


        [Reactive]
        public bool NeedsReview { get; set; }

        [Reactive]
        public int Id { get; set; }
    }
}
