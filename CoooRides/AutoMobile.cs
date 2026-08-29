using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoooRides
{
    public abstract class Automobile
    {
        public string ModelName { get; set; }
        public string Color { get; set; }
    }

    public class Car : Automobile
    {
        public Car() { ModelName = "LUX1000"; }
    }

    public class Minibus : Automobile
    {
        public Minibus() { ModelName = "MV500"; }
    }

    // --- 2. The Abstract Part Interfaces ---
    // (We use interfaces here so the assembly line doesn't care if it's a Car or Minibus part)
    public interface IChassis { }
    public interface IShell { }
    public interface IWheel { }
    public interface ITrim { }

    // --- 3. The Abstract Factory Interface ---
    public interface IAutoPartsFactory
    {
        IChassis CreateChassis();
        IShell CreateShell();
        IWheel CreateWheel();
        ITrim CreateTrim();
    }

    // --- 4. The Concrete Factories ---
    // Notice the Thread.Sleep() delays exactly match your assignment specs!

    public class CarPartsFactory : IAutoPartsFactory
    {
        public IChassis CreateChassis() { Thread.Sleep(2000); return null; } // 2 seconds
        public IShell CreateShell() { Thread.Sleep(2000); return null; }     // 2 seconds
        public IWheel CreateWheel() { Thread.Sleep(500); return null; }      // Half a second
        public ITrim CreateTrim() { Thread.Sleep(1000); return null; }       // 1 second
    }

    public class MinibusPartsFactory : IAutoPartsFactory
    {
        public IChassis CreateChassis() { Thread.Sleep(2000); return null; } // 2 seconds
        public IShell CreateShell() { Thread.Sleep(3000); return null; }     // 3 seconds
        public IWheel CreateWheel() { Thread.Sleep(500); return null; }      // Half a second
        public ITrim CreateTrim() { Thread.Sleep(2000); return null; }       // 2 seconds
    }
}
