using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelItUp.Core.ViewModels
{
    public class BuildViewModel : MvxViewModel
    {
        public BuildViewModel() { }

        public INC<String> Name = new NC<String>();

        public int id { get; set; }
    }
}
