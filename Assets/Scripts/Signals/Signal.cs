namespace Assets.Scripts.Signals
{
    public class Signal<T> : ISignal
    {
        public T Arg1
        {
            get;
        }

        public Signal(T arg1)
        {
            Arg1 = arg1;
        }
    }

    public class Signal<T, T1> : ISignal
    {
        public T Arg1
        {
            get;
        }

        public T1 Arg2
        {
            get;
        }

        public Signal(T arg1, T1 arg2)
        {
            Arg1 = arg1;
            Arg2 = arg2;
        }
    }

    public class Signal<T, T1, T2> : ISignal
    {
        public T Arg1
        {
            get;
        }

        public T1 Arg2
        {
            get;
        }

        public T2 Arg3
        {
            get;
        }

        public Signal(T arg1, T1 arg2, T2 arg3)
        {
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
        }
    }

    public class Signal : ISignal
    {

    }
}