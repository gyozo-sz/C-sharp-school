using System;

namespace MyApp
{
    enum MessengerState
    {
        StateOne,
        StateTwo, 
        StateThree, 
        StateFour
    }

    class Messenger
    {
        static MessengerState _state;
        string _message;
        int _messageCounter;

        public MessengerState State { get { return _state; } }

        static Messenger()
        {
            _state = MessengerState.StateOne;
        }

        public Messenger(string message, int messageCounter)
        {
            this._message = message;
            this._messageCounter = messageCounter;
        }

        public void ChangeState(MessengerState newState)
        {
            _state = newState;
        }

        internal void Deconstruct(out string message, out int messageCounter) { 
            message = _message;
            messageCounter = _messageCounter;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Messenger msg = new Messenger("some message", 30);
            Messenger msg2 = new Messenger("some other message", 0);
            (string message, int  msgCounter) = msg;

            Console.WriteLine(message);
            Console.WriteLine(msgCounter);

            Console.WriteLine("\nMessenger states:");
            Console.WriteLine($"Messenger 1: {msg.State}");
            Console.WriteLine($"Messenger 2: {msg2.State}");

            Console.WriteLine("\nChanging messenger states after changing the state using Messenger 1:");
            msg.ChangeState(MessengerState.StateThree);
            Console.WriteLine($"Messenger 1: {msg.State}");
            Console.WriteLine($"Messenger 2: {msg2.State}");


        }
    }
}