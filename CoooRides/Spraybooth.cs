using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoooRides
{
    public class Spraybooth
    {
        
        private static Spraybooth _instance;

      
        private static readonly object _instanceLock = new object();
        private readonly object _paintLock = new object();

        
        public Action<string> OnStatusChanged;

        
        private Spraybooth() { }

        
        public static Spraybooth Instance
        {
            get
            {
                
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new Spraybooth();
                    }
                    return _instance;
                }
            }
        }

       
        public void PaintVehicle(Automobile vehicle)
        {
           
            lock (_paintLock)
            {
                
                OnStatusChanged?.Invoke($"Spraying {vehicle.Color} {vehicle.ModelName}");

                int dryTime = vehicle is Car ? 5000 : 7000;
                Thread.Sleep(dryTime);

                OnStatusChanged?.Invoke("Idle");
            }
        }
    }
}
