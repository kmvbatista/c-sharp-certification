using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

public class Program
{
  private Tipos x
  {
    get => Tipos.cama;
    set
    {
      x = value;
    }
  }

  public static void Main()
  {
   object x = new {};
   var z = x as Artist;
  }

  private static  void FileHandling() {
    File.Open("", FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
  }
  private static void DataContractSerializerHandler() {
    DataContractSerializer x = new DataContractSerializer(typeof(Artist));
    using(FileStream stream = new FileStream("seri.json", FileMode.Create)) {
      x.WriteObject(stream, new Artist("Jebbedt"));
    }
  }

  private static void Linq()
  {
    var musicTracks = new[] { new { ArtistId = 1 }, new { ArtistId = 2 }, new { ArtistId = 3 }, };
    var artists = new[] { new { ID = 1, Name = "João" }, new { ID = 2, Name = "Marcos" }, new { ID = 3, Name = "Antônio" }, };

    var result = musicTracks.Join(artists,
      track => track.ArtistId,
      artist => artist.ID,
      (artist, track) => new { artist.ArtistId, track.Name }).ToList();

    var artistSummary = (from track in musicTracks
                         join artist in artists on track.ArtistId equals artist.ID
                         group track by artist.Name
                    into artistTrackSummary
                         select new
                         {
                           ID = artistTrackSummary.Key,
                         }).ToList();
    Console.WriteLine("");
  }

  private static void ReadXmlDOM()
  {
    string XMLDocument = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
    "<MusicTrack xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
    "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
    "<Artist>Rob Miles</Artist>" +
    "<Title>My Way</Title>" +
    "<Length>150</Length>" +
    "</MusicTrack>";

    XmlDocument doc = new XmlDocument();
    doc.LoadXml(XMLDocument);

    System.Xml.XmlElement rootElement = doc.DocumentElement;
    // make sure it is the right element
    if (rootElement.Name != "MusicTrack")
    {
      Console.WriteLine("Not a music track");
    }
    else
    {
      string artist = rootElement["Artist"].FirstChild.Value;
      string title = rootElement["Title"].FirstChild.Value;
      Console.WriteLine("Artist:{0} Title:{1}", artist, title);
    }
  }
  private static void ReadXml()
  {
    string XMLDocument = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" +
    "<MusicTrack xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
    "xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
    "<Artist>Rob Miles</Artist>" +
    "<Title>My Way</Title>" +
    "<Length>150</Length>" +
    "</MusicTrack>";

    using (StringReader stringReader = new StringReader(XMLDocument))
    {
      XmlTextReader reader = new XmlTextReader(stringReader);

      while (reader.Read())
      {
        string description = string.Format("Type:{0} Name:{1} Value:{2}",
            reader.NodeType.ToString(),
            reader.Name,
            reader.Value);
        Console.WriteLine(description);
      }
    }
  }

  private static void Reflection()
  {
    int i = 48;
    MethodInfo compareToMethod = i.GetType().GetMethod("CompareTo", new Type[] { typeof(int) });
    int result = (int)compareToMethod.Invoke(i, new object[] { 41 });
  }

  private static void ImplicitCasting()
  {
    var money = new Money(42);
    string moneyInt = money;
  }

  private static void anonymousObject()
  {
    var list = new string[] { "dadfas", "dafsd" };
    var y = new
    {
      ahdsfa = "ddfsas",
      bbb = 3
    };
  }

  static void showDrives()
  {
    DriveInfo[] drives = DriveInfo.GetDrives();
    foreach (DriveInfo drive in drives)
    {
      Console.Write("Name:{0} ", drive.Name);
      if (drive.IsReady)
      {
        Console.Write("  Type:{0}", drive.DriveType);
        Console.Write("  Format:{0}", drive.DriveFormat);
        Console.Write("  Free space:{0}", drive.TotalFreeSpace);
      }
      else
      {
        Console.Write("  Drive not ready");
      }
      Console.WriteLine();
    }
  }

  private static void EsqueciONome()
  {
    var client = new HttpClient();
    IDisposable d = client;

    var aaa = new Implementor();
    var adaptedContext = ((IObjectContextAdapter)aaa).ObjectContext;

    var x = new MoveableOject();
    ((IRight)x).Move();

  }

  private static void RegexExample()
  {
    string pattern = "(Ms\\.? | Mrs\\.? | Miss | Ms\\.? )";
    string[] names = { "Mrs. Henry Hunt", "Mr. Sara Samuels",
    "Abraham Adams", "Ms. Nicole Norris" };
    foreach (string name in names)
      Console.WriteLine(Regex.Replace(name, pattern, String.Empty));
  }

  private static void fileAtributesExample()
  {
    string filePath = "TextFile.txt";

    File.WriteAllText(path: filePath, contents: "This text goes in the file");
    FileInfo info = new FileInfo(filePath);
    Console.WriteLine("Name: {0}", info.Name);
    Console.WriteLine("Full Path: {0}", info.FullName);
    Console.WriteLine("Last Access: {0}", info.LastAccessTime);
    Console.WriteLine("Length: {0}", info.Length);
    Console.WriteLine("Attributes: {0}", info.Attributes);
    Console.WriteLine("Make the file read only");
    info.Attributes |= FileAttributes.ReadOnly;
    Tipos x = Tipos.cama | Tipos.isto;
    x &= Tipos.macc;
    x &= Tipos.maca;
  }

  private static void TraceSwitchExample()
  {
    TraceSwitch control = new TraceSwitch("Control", "Control the trace output");
    control.Level = TraceLevel.Info;

    if (control.TraceWarning)
    {
      Console.WriteLine("An error has occurred");
    }
    Trace.WriteLineIf(control.TraceWarning, "A warning message");
  }

  static WeakReference data;
  public static void Run()
  {
    object result = GetData();
    GC.Collect();
    result = GetData();
    Console.Write("");
  }


  const int DELAY = 1000;
  const int MIN = 1;
  const int MAX = 10;
  static async Task Main(string[] args)
  {
    await foreach (int number in GetAsyncEnumerable())
    {
      Console.WriteLine(number);
    }
    Console.ReadLine();
  }
  static async IAsyncEnumerable<int> GetAsyncEnumerable()
  {
    for (int i = MIN; i < MAX; i++)
    {
      yield return i;
      await Task.Delay(DELAY);
    }
  }
  private static object GetData()
  {
    if (data == null)
    {
      data = new WeakReference(LoadLargeList());
    }
    if (data.Target == null)
    {
      data.Target = LoadLargeList();
    }
    return data.Target;
  }
  private static List<Product> LoadLargeList()
  {
    return new List<Product> { new Product(), new Product(), new Product(), new Product() };
  }

  public class Product
  {
    ~Product()
    {

    }
    public decimal Price { get; set; }
  }
  public static class MyExtensions
  {
  }
  public class Calculator
  {
  }

  interface ILeft
  {
    void Move();
  }
  interface IRight
  {
    void Move();
  }
  class MoveableOject : ILeft, IRight
  {
    void ILeft.Move() { }
    void IRight.Move() { }
  }

  class Money
  {
    public Money(decimal amount)
    {
      Amount = amount;
    }
    public decimal Amount { get; set; }
    // public static implicit operator decimal(Money money)
    // {
    //   return money.Amount;
    // }
    public static implicit operator string(Money money)
    {
      return money.Amount.ToString();
    }
  }
  class Rectangle
  {
    public Rectangle(int width, int height)
    {
      Width = width;
      Height = height;
    }
    public virtual int Height { get; set; }
    public virtual int Width { get; set; }
    public int Area
    {
      get
      {
        return Height * Width;
      }
    }
  }

  class Square : Rectangle
  {
    public Square(int size) : base(size, size)
    {

    }
    public override int Width
    {
      get
      {
        return base.Width;
      }
      set
      {
        base.Width = value;
        base.Height = value;
      }
    }
    public override int Height
    {
      get
      {
        return base.Height;
      }
      set
      {
        base.Height = value;
        base.Width = value;
      }
    }
  }
  public static void ParseCultureInfo()
  {
    CultureInfo english = new CultureInfo("En");
    CultureInfo dutch = new CultureInfo("Nl");
    string value = "€19,95";
    bool x = value == "dfafsd" | true;
    var style = NumberStyles.Currency | NumberStyles.AllowCurrencySymbol;
    decimal d = decimal.Parse(value, style, dutch);
    Console.WriteLine(d.ToString(english)); // Displays 19.95
  }
  public static void StringComparison()
  {

    String[] cultureNames = { "en-US", "se-SE" };
    String[] strings1 = { "case",  "encyclopædia",
                            "encyclopædia", "Archæology" };
    String[] strings2 = { "Case", "encyclopaedia",
                            "encyclopedia" , "ARCHÆOLOGY" };
    StringComparison[] comparisons = (StringComparison[])Enum.GetValues(typeof(StringComparison));

    HashSet<string> x = new HashSet<string>();
    x.Add("dfdfas");

    foreach (var cultureName in cultureNames)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
      Console.WriteLine("Current Culture: {0}", CultureInfo.CurrentCulture.Name);
      for (int ctr = 0; ctr <= strings1.GetUpperBound(0); ctr++)
      {
        foreach (var comparison in comparisons)
          Console.WriteLine("   {0} = {1} ({2}): {3}", strings1[ctr],
                            strings2[ctr], comparison,
                            String.Equals(strings1[ctr], strings2[ctr], comparison));

        Console.WriteLine();
      }
      Console.WriteLine();
    }
  }

  public static void SerializeArtist()
  {
    var art = new Artist("Kennedy");
    BinaryFormatter formatter = new BinaryFormatter();
    using (FileStream stream = new FileStream("Artist.bin", FileMode.Create))
    {
      formatter.Serialize(stream, art);
    }
  }

  public static void DeserializeArtist()
  {
    BinaryFormatter formatter = new BinaryFormatter();
    using (FileStream stream = new FileStream("Artist.bin", FileMode.Open))
    {
      Artist artist = (Artist)formatter.Deserialize(stream);
      artist.Name = "";
    }
  }

  public static void matrix()
  {
    string[,] compass = new string[3, 3]
    {
      { "NW","N","NE" },
      {"W", "C", "E" },
      { "SW", "S", "SE" }
    };

    Console.WriteLine(compass[0, 0]);  // prints NW
    Console.WriteLine(compass[2, 2]);

    int[][] jaggedArray = new int[][]
    {
      new int[] {1,2,3,4 },
      new int[] {5,6,7},
      new int[] {11,12}
    };
  }
}

public class Test : IFormattable
{
  public string ToString(string format, IFormatProvider formatProvider)
  {
    throw new NotImplementedException();
  }
}

[Flags]
public enum Tipos
{
  maca = 1,
  cama = 2,
  macc = 4,
  isto = 16,
}
