using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroFramework.Event
{
    public interface IOnEvent<T> {
        void OnEvent(T e);
    }

    public static class OnGlobalEventExtension {
        public static IUnRegister RegisterEvent<T>(this IOnEvent<T> self) where T : struct {
            return TypeEventSystem.RegisterGlobalEvent<T>(self.OnEvent);
        }

        public static void UnRegisterEvent<T>(this IOnEvent<T> self) where T : struct {
            TypeEventSystem.UnRegisterGlobalEvent<T>(self.OnEvent);
        }
    }
}
