using LevelItUp.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using MvvmCross.ViewModels;
using MvvmCross.IoC;

namespace LevelItUp.Core
{
    public static class CoreExt
    {
        public static IList ToNGList<T>(this IEnumerable<T> l)
        {
            return l.ToList() as IList;
        }
    }
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<MainPageViewModel>();
        }
    }
}
