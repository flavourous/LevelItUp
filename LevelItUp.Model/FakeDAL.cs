using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LevelItUp.Model
{
    public class FakeDAL
    {
        public static FakeDAL OurFakeDal()
        {
            var dal = new FakeDAL(x => ((BaseModel)x).id, (x,v) => ((BaseModel)x).id = v);
            dal.AddFkr((BuildLevelParameter x) => x.Parameter);
            dal.AddFkr((BuildParameter x) => x.Type);
            dal.AddFkr((BuildParameterRequiement x) => x.Depend);
            dal.AddFkr((BuildParameterRequiement x) => x.On);
            dal.AddFkr((BuildParameterTypeLevelPoints x) => x.Type);
            return dal;
        }

        int cid = 1;
        readonly Dictionary<Type, Dictionary<int, Object>> repo = new Dictionary<Type, Dictionary<int, object>>();
        readonly Dictionary<Type, List<Delegate>> fkr = new Dictionary<Type, List<Delegate>>();
        readonly Func<object, int> getID;
        readonly Action<object, int> setID;
        public FakeDAL(Func<object,int> getID, Action<object, int> setID)
        {
            this.getID = getID;
            this.setID = setID;
        }
        public void AddFkr<T, D>(Func<T, D> selector)
        {
            Ensure<T>();
            fkr[typeof(T)].Add(selector);
        }
        Dictionary<int, Object> Ensure<T>()
        {
            return Ensure(typeof(T));
        }
        Dictionary<int, Object> Ensure(Type tt)
        {
            if (!repo.ContainsKey(tt))
            {
                repo[tt] = new Dictionary<int, object>();
                fkr[tt] = new List<Delegate>();
            }
            return repo[tt];
        }
        public void Delete<T>(T item)
        {
            Ensure<T>().Remove(getID(item));
        }
        public IEnumerable<T> Get<T>()
        {
            return Ensure<T>().Values.Cast<T>();
        }
        public void Save<T>(T item)
        {
            Save(typeof(T), item);
        }
        void Save(Type t, object item)
        {
            // get the table and itemid
            var tabl = Ensure(t);
            int id = getID(item);

            // new item
            if(id == 0)
            {
                id = cid++;
                setID(item, id);
            }
            tabl[id] = item;

            // save fkrs
            foreach(var fd in fkr[t])
            {
                var f = fd.DynamicInvoke(item);
                if (f != null) Save(f.GetType(), f);
            }
        }
    }
}
