using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoooRides
{
    public interface ICommand
    {
        void Execute();
    }

    public class BuildOrderCommand : ICommand
    {
        private AssemblyLines _receiver;
        private string _color;

        public BuildOrderCommand(AssemblyLines receiver, string color)
        {
            _receiver = receiver;
            _color = color;
        }

        public void Execute()
        {
            _receiver.ProduceVehicle(_color);
        }
    }

    public class CorporateHQ
    {
        private Queue<ICommand> _carOrderQueue = new Queue<ICommand>();
        private Queue<ICommand> _minibusOrderQueue = new Queue<ICommand>();

        private bool _isCarLineRunning = false;
        private bool _isMinibusLineRunning = false;

        public Action<int> OnCarQueueCountChanged;
        public Action<int> OnMinibusQueueCountChanged;

        public void PlaceOrder(ICommand command, string type)
        {
            if (type == "Car")
            {
                _carOrderQueue.Enqueue(command);
                OnCarQueueCountChanged?.Invoke(_carOrderQueue.Count);

               
                if (!_isCarLineRunning)
                {
                    ProcessCarQueue();
                }
            }
            else if (type == "Minibus")
            {
                _minibusOrderQueue.Enqueue(command);
                OnMinibusQueueCountChanged?.Invoke(_minibusOrderQueue.Count);

                if (!_isMinibusLineRunning)
                {
                    ProcessMinibusQueue();
                }
            }
        }

        private void ProcessCarQueue()
        {
            _isCarLineRunning = true;

            Task.Run(() =>
            {
                while (_carOrderQueue.Count > 0)
                {
                    ICommand cmd = _carOrderQueue.Peek(); // Look at the next order
                    cmd.Execute();                        // Wait for it to build and paint

                    _carOrderQueue.Dequeue();             
                    OnCarQueueCountChanged?.Invoke(_carOrderQueue.Count); 
                }
                _isCarLineRunning = false;
            });
        }

        private void ProcessMinibusQueue()
        {
            _isMinibusLineRunning = true;

            Task.Run(() =>
            {
                while (_minibusOrderQueue.Count > 0)
                {
                    ICommand cmd = _minibusOrderQueue.Peek();
                    cmd.Execute();

                    _minibusOrderQueue.Dequeue();
                    OnMinibusQueueCountChanged?.Invoke(_minibusOrderQueue.Count);
                }
                _isMinibusLineRunning = false;
            });
        }
    }
}
