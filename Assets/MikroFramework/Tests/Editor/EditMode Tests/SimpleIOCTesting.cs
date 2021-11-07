using System.Collections;
using System.Collections.Generic;
using MikroFramework.IOC;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SimpleIOCTesting{
    [Test]
    public void SimpleIOCRegisterGetTest() {
        var iocContainer = new IOCContainer();

        iocContainer.Register<IOCContainer>();
        var obj = iocContainer.Get<IOCContainer>();

        Assert.IsNotNull(obj);
        Assert.AreNotEqual(iocContainer,obj);
    }

    [Test]
    public void SimpleIOCGetRegisteredType() {
        var simpleIoc = new IOCContainer();

        var obj = simpleIoc.Get<IOCContainer>();

        Assert.IsNull(obj);
    }

    [Test]
    public void SimpleIOCRegisterTwice() {
        var simpleIoc = new IOCContainer();

        simpleIoc.Register<IOCContainer>();
        simpleIoc.Register<IOCContainer>();

        Assert.IsTrue(true);
    }

    [Test]
    public void SimpleIOCRegisterInstance() {
        var simpleIOC = new IOCContainer();

        simpleIOC.RegisterInstance(new IOCContainer());


        var instanceA = simpleIOC.GetInstance<IOCContainer>();
        var instanceB = simpleIOC.GetInstance<IOCContainer>();

        Assert.AreEqual(instanceA,instanceB);
    }

    [Test]
    public void SimpleIOCRegisterDependency() {
        var simpleIOC = new IOCContainer();

        simpleIOC.RegisterInstance<IIOCContainer, IOCContainer>();
        var ioc = simpleIOC.GetInstance<IIOCContainer>();

        Assert.AreEqual(ioc.GetType(),typeof(IOCContainer));
    }

    [Test]
    public void SimpleIOCRegisterInstanceDependency() {
        var simpleIOC = new IOCContainer();

        simpleIOC.RegisterInstance<IIOCContainer>(simpleIOC);

        var iocA = simpleIOC.GetInstance<IIOCContainer>();
        var iocB = simpleIOC.GetInstance<IIOCContainer>();

        Assert.AreEqual(iocA,simpleIOC);
        Assert.AreEqual(iocB,iocA);
    }


    class SomeDependencyB { }

    class SomeDependencyA { }

    class SomeCtrl {
        [IOCInject]
        public SomeDependencyA A { get; set; }

        [IOCInject]
        public SomeDependencyB B { get; set; }
    }

    [Test]
    public void SimpleIOCInject() {
        var simpleIOC = new IOCContainer();

        simpleIOC.RegisterInstance(new SomeDependencyA());
        simpleIOC.Register<SomeDependencyB>();

        SomeCtrl someCtrl = new SomeCtrl();

        simpleIOC.Inject(someCtrl);

        Assert.IsNotNull(someCtrl.A);
        Assert.IsNotNull(someCtrl.B);

        Assert.AreEqual(someCtrl.A.GetType(),typeof(SomeDependencyA));
        Assert.AreEqual(someCtrl.B.GetType(),typeof(SomeDependencyB));

        Assert.AreNotEqual(someCtrl.B,simpleIOC.Get<SomeDependencyB>());
        Assert.AreEqual(someCtrl.A,simpleIOC.GetInstance<SomeDependencyA>());
    }

    [Test]
    public void SimpleIOCClear() {
        var simpleIOC = new IOCContainer();

        simpleIOC.RegisterInstance(new SomeDependencyA());
        simpleIOC.RegisterInstance<IIOCContainer>(simpleIOC);
        simpleIOC.Register<SomeDependencyB>();

        simpleIOC.Clear();

        SomeDependencyA someDependencyA = simpleIOC.GetInstance<SomeDependencyA>();
        SomeDependencyB someDependencyB = simpleIOC.Get<SomeDependencyB>();
        var ioc = simpleIOC.GetInstance<IIOCContainer>();

        Assert.IsNull(someDependencyA);
        Assert.IsNull(someDependencyB);
        Assert.IsNull(ioc);

    }
}
