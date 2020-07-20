using System.Linq;

namespace Recodme.ShokuDex.WebApi.SupportMethods
{
    public class SupportMethods
    {
        public static bool Equals<VMT, T>(VMT objVM, T obj)
        {
            var properties = objVM.GetType().GetProperties();
            foreach (var item in properties)
            {
                var current = obj.GetType().GetProperty(item.Name).GetValue(obj);
                var updated = objVM.GetType().GetProperty(item.Name).GetValue(objVM);
                if (!current.Equals(updated))
                    return false;
            }
            return true;
        }

        public static T Update<VMT, T>(VMT objVM, T obj)
        {
            var properties = objVM.GetType().GetProperties().Where(x => x.Name != "Id");
            foreach (var item in properties)
            {
                var current = obj.GetType().GetProperty(item.Name).GetValue(obj);
                var updated = objVM.GetType().GetProperty(item.Name).GetValue(objVM);
                if (!current.Equals(updated))
                {
                    var property = obj.GetType().GetProperty(item.Name);
                    property.SetValue(obj,updated);
                }
            }
            return obj;
        }
    }
}
