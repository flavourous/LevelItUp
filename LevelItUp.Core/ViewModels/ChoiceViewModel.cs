using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class ChoiceGroup : List<Choice>
    {
        public String Group { get; set; }
    }
    public class Choice
    {
        public String Name { get; set; }
    }
    public class ChoiceViewModel : MvxViewModel
    {
        public IList<ChoiceGroup> Choices { get; set; }
        public IMvxAsyncCommand<Choice> Choose { get; set; }
    }
}
