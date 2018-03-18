using LevelItUp.Core.ViewModels;
using LevelItUp.Model;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections;

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

            RegisterNavigationServiceAppStart<MainPageViewModel>();
        }
    }
}
