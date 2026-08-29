using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoooRides
{
    public abstract class AssemblyLines
    {
        // 1. The connection to the Abstract Factory Pattern
        protected internal IAutoPartsFactory _partsFactory;

        // 2. Delegate to send live status updates back to the MainForm GUI
        public Action<string> OnStatusChanged;

        // 3. The Factory Method itself (defers instantiation to subclasses)
        protected abstract Automobile BuildAutomobile();

        // Helper method for specific assembly delays
        protected abstract int GetFinalAssemblyTime();

        // 4. The main workflow triggered by the Command Pattern
        public void ProduceVehicle(string color)
        {
            // Step A: Use the Factory Method to get the base vehicle type
            Automobile vehicle = BuildAutomobile();
            vehicle.Color = color;
            string fullName = $"{color} {vehicle.ModelName}";

            // Step B: Use the Abstract Factory to get the parts (delays happen inside these calls)
            OnStatusChanged?.Invoke($"Creating {fullName} Chassis");
            _partsFactory.CreateChassis();

            OnStatusChanged?.Invoke($"Creating {fullName} Shell");
            _partsFactory.CreateShell();

            string[] wheels = { "Left Front Wheel", "Right Front Wheel", "Left Back Wheel", "Right Back Wheel" };
            foreach (string wheel in wheels)
            {
                OnStatusChanged?.Invoke($"Creating {fullName} {wheel}");
                _partsFactory.CreateWheel();
            }

            OnStatusChanged?.Invoke($"Creating {fullName} Trim");
            _partsFactory.CreateTrim();

            // Step C: Final Assembly
            OnStatusChanged?.Invoke($"Performing final assembly of {fullName}");
            Thread.Sleep(GetFinalAssemblyTime());

            // Step D: Send to the Singleton Spraybooth
            OnStatusChanged?.Invoke($"Sent {fullName} for painting");
            Spraybooth.Instance.PaintVehicle(vehicle);

            // Done! Clear the label.
            OnStatusChanged?.Invoke("Idle");
        }
    }

    // --- The ConcreteFactories ---

    public class CarAssemblyLine : AssemblyLines
    {
        public CarAssemblyLine()
        {
            // Assign the specific concrete factory for cars
            _partsFactory = new CarPartsFactory();
        }

        protected override Automobile BuildAutomobile()
        {
            return new Car();
        }

        protected override int GetFinalAssemblyTime()
        {
            return 2000; // 2 seconds for Car Assembly after parts are received
        }
    }

    public class MinibusAssemblyLine : AssemblyLines
    {
        public MinibusAssemblyLine()
        {
            // Assign the specific concrete factory for minibuses
            _partsFactory = new MinibusPartsFactory();
        }

        protected override Automobile BuildAutomobile()
        {
            return new Minibus();
        }

        protected override int GetFinalAssemblyTime()
        {
            return 3000; // 3 seconds for Minibus Assembly after parts are received
        }
    }
}
