using System;
using System.Linq;

namespace Recodme.ShokuDex.SupportMethods
{
    public class SupportMethods
    {
        public static bool Equals<VMT, T>(VMT objVM, T obj)
        {
            var allProperties = obj.GetType().GetProperties();
            var properties = allProperties.Where(x => (x.Name != "CreatedAt") && (x.Name != "UpdatedAt") && (x.Name != "IsDeleted"));
            foreach (var item in properties)
            {
                var current = obj.GetType().GetProperty(item.Name).GetValue(obj);
                var updated = objVM.GetType().GetProperty(item.Name).GetValue(objVM);
                if (current.ToString() != updated.ToString())
                    return false;
            }
            return true;
        }

        public T Update<VMT, T>(VMT objVM, T obj)
        {
            var allProperties = obj.GetType().GetProperties();
            var properties = allProperties.Where(x => (x.Name != "CreatedAt") && (x.Name != "UpdatedAt") && (x.Name != "IsDeleted"));
            foreach (var item in properties)
            {
                var current = obj.GetType().GetProperty(item.Name).GetValue(obj);
                var updated = objVM.GetType().GetProperty(item.Name).GetValue(objVM);
                if (current.ToString() != updated.ToString())
                    current = updated;
            }
            return obj;
        }
}
