using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineClass
{
    abstract class Machine
    {
        private bool _powered = false;

        public bool Powered
        {
            get { return _powered; }
        }

        private uint _wear = 0;

        public uint Wear { get { return _wear; } }

        public void TurnOn()
        {
            _powered = true;
        }

        public void TurnOff()
        {
            _powered = false;
        }

        public void AddWear(uint wear)
        {
            _wear += wear;
        }

        public bool Clean()
        {
            if (!_powered)
            {
                Console.WriteLine("Machine has to be turned on for cleaning!");
                return false;
            }

            bool cleanSuccess = SelfClean();
            if (cleanSuccess)
            {
                _wear = 0;
            }

            return cleanSuccess;
        }

        abstract public bool SelfClean();
    }
}
