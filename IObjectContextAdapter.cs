internal interface IObjectContextAdapter
{
  string ObjectContext { get; set; }
}

internal class Implementor : IObjectContextAdapter
{
  public string ObjectContext { get; set; }

}