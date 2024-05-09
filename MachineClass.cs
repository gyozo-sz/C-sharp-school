namespace MachineClass
{
    public class MachineOperation<AvailableMachineOperation>
        where AvailableMachineOperation : Enum
    {
        private readonly AvailableMachineOperation _operation;
        private readonly uint _addedWear;

        public MachineOperation(AvailableMachineOperation operation, uint addedWear)
        {
            _operation = operation;
            _addedWear = addedWear;
        }

        public AvailableMachineOperation Operation { get => _operation; }
        public uint AddedWear { get => _addedWear; }
    }

    abstract class Machine<AvailableMachineOperation>
        where AvailableMachineOperation : Enum
    {
        private bool _powered = false;

        public bool Powered { get => _powered }

        private uint _wear = 0;

        public uint Wear { get => _wear; }

        public void TurnOn()
        {
            _powered = true;
        }

        public void TurnOff()
        {
            _powered = false;
        }

        private void AddWear(uint wear)
        {
            _wear += wear;
        }

        protected void PerformOperation(MachineOperation<AvailableMachineOperation> operation)
        {
            Console.WriteLine($"Executing {Enum.GetName(typeof(AvailableMachineOperation), operation.Operation)}...");
            AddWear(operation.AddedWear);
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
