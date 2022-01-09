using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikroFramework.IOC
{
    public interface IIOCContainer {
        /// <summary>
        /// RegisterInstance a object (not singleton) of type T to the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>();

        /// <summary>
        /// Register an existing singleton of type T to the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegisterInstance<T>(object instance);


        /// <summary>
        /// Register an existing singleton to the container
        /// </summary>
        /// <param name="instance"></param>
        void RegisterInstance(object instance);

        /// <summary>
        /// Register an singleton of type T to the container, this singleton is a concrete class of its base class
        /// When getting this singleton, you can use GetInstance<TBase> to get the concrete instance with the type of base class
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TConcrete"></typeparam>
        void RegisterInstance<TBase, TConcrete>() where TConcrete : TBase;

        /// <summary>
        /// Get a new instance of a registered object from the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>() where T:class;


        /// <summary>
        /// Get a singleton from the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetInstance<T>() where T : class;


        /// <summary>
        /// Inject all dependency properties of an object. Those properties should be marked attributes [IOCInject],
        /// and the instances of those properties be registered into the IOC Container
        /// </summary>
        /// <param name="obj"></param>
        void Inject(object obj);

        void Clear();
    }
}
