using System;
using System.Linq;

class EventHandlerExample
{
    public event EventHandler<RaisingArgs> OnRaising = delegate (object e, RaisingArgs args) { Console.WriteLine(args.name); };
    delegate void Del(object e, RaisingArgs args);
    public void Raise(string a)
    {
        OnRaising += PrintReverse;
        OnRaising += PrintReverseAndVerse;
        OnRaising += delegate (object e, RaisingArgs args) { Console.WriteLine("kennedy bobao"); };
        OnRaising(this, new RaisingArgs(a));
    }

    private void PrintReverse(object e, RaisingArgs args)
    {
        Console.WriteLine(new string(args.name.Reverse().ToArray()));
    }
    private void PrintReverseAndVerse(object e, RaisingArgs args)
    {
        Console.WriteLine(new string(args.name.Reverse().ToArray()) + $"| {args.name} ");
    }
}

class RaisingArgs : EventArgs
{
    public string name { get; set; }
    public RaisingArgs(string name)
    {
        this.name = name;
    }

}