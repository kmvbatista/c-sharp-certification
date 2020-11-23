using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Security.Cryptography;

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

    private static void thisMethod()
    {
        var departments = new List<string>() { "finance", "media", "marketing" };
        bool exists = departments.Any(x => x.Equals("finanace"));
        Console.WriteLine(exists);
        var departmentsParallel = departments.AsParallel<string>();

    }

    private static async Task AsParallelExample()
    {
        var departments = new List<string>() { "finance", "media", "marketing" };
        departments.AsParallel().ForAll(x => x += "kennedy");
    }

    private static void ConcurrentDictionaryExample()
    {
        var dic = new ConcurrentDictionary<string, int>();
        string key = "kennedy";
        int initialValue = 1;
        dic.TryAdd(key, initialValue);
        int value;
        dic.TryGetValue(key, out value);
        dic.TryUpdate(key, value + 6, initialValue);

        ///////////////////////

        dic.AddOrUpdate("carlos", initialValue, (string key, int value) => value + 6);
        //new Dictionary<string, int>().AddOrUpdate() -------- not exists

    }

    public static void Main()
    {
        CreateXElementWithLinq();
    }

    private static void CreateXElementWithLinq()
    {
        var contactList = new List<dynamic> { new { Name = "Kennedy", Number = "996207788" }, new { Name = "joão", Number = "996207702" } };

        XElement contadcts = new XElement("contacts",
          from c in contactList
          select new XElement("contact",
            new XAttribute("number", c.Number),
            new XElement("name", c.Name)
            )
          );
        Console.WriteLine("");
        using (var writer = new StreamWriter("contacts" + ".xml"))
        {
            writer.Write(contadcts); ;
        }
    }

    private static void Conversion()
    {
        float fNumber = 99.87F;
        object obj = fNumber;
        int intNumber = 0;
        intNumber = (int)(float)obj;
    }

    [Conditional("DEBUG")]
    private static void Print()
    {
        Console.WriteLine("KENNE");

    }

    private static void ExpandoObjectExample(object x)
    {
        dynamic message = new ExpandoObject();
        message.From = "kennegy";
    }
    /// ESTUDAR WEB CLIENT

    private static void ActivatorCreateInstance()
    {
        Type type1 = typeof(DateTime);
        object obj = Activator.CreateInstance(type1);
    }

    private static void EventLogExample()
    {
        //EventLog applicationLog = new EventLog("", ".", "testEventLogEvent");
        //applicationLog.EntryWritten += (sender, e) =>
        //{
        //  Console.WriteLine(e.Entry.Message);
        //};
        //applicationLog.EnableRaisingEvents = true;
        //applicationLog.WriteEntry(“Test message”, EventLogEntryType.Information);
    }

    private static void TaskContinuationOptions()
    {
        //by default, continuewith continues wether there was completion or exception
        Task.Run(() => { throw new Exception(""); }).ContinueWith(
            ContinueWhenFaulted, System.Threading.Tasks.TaskContinuationOptions.OnlyOnFaulted);

        "".GetType().GetProperties().First(x => x.Name == "").GetValue("");
    }

    private static void ContinueWhenFaulted(Task task)
    {
        Console.WriteLine("continuou");
    }

    private static void FileWebRequestExample()
    {
        FileWebRequest request = FileWebRequest.Create("https://www.google.com") as FileWebRequest;
        using (FileWebResponse response = request.GetResponse() as FileWebResponse)
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        using (var writer = new StreamWriter("dfafda" + ".dat"))
        {
            writer.Write(reader.ReadToEnd()); ;
        }
    }

    private static void ConsoleSetOutAndStreams()
    {
        using (StreamWriter writer = new StreamWriter(@"C:\Users\Public\console.txt"))
        {
            Console.SetOut(writer);
            using (FileStream stream = new FileStream(@"C:\Users\Public\file.txt", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (true)
                    {
                        Console.WriteLine(reader.ReadLine());
                    }
                }
            }
        }
    }

    private static void RethrowingExceptions()
    {
        try
        {
            //do some work
        }
        catch (System.ApplicationException)
        {
            throw;
            //rethrow with the current stack trace
        }
        catch (System.Exception ex)
        {
            throw ex;
            //rethrow clearing up stack trace; BAD PRACTICE
        }
    }

    private static void WebRequestHandler()
    {
        WebRequest web = WebRequest.Create("https://www.microsoft.com");
        var response = web.GetResponse();
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            string content = reader.ReadToEnd();
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Public\msc.html"))
            {
                writer.Write(content);
            }
        }
    }
    private static void WebClientUploadString(string url, int intA, int intB)
    {
        var client = new WebClient();
        /* var data = string.Format("a={0}&b={1}", intA, intB); */
        var data2 = new NameValueCollection() { { "a", intA.ToString() }, { "b", intB.ToString() } };
        client.UploadValuesTaskAsync(new Uri(url), data2);
    }

    private static void getPropValueByReflection()
    {
        Person person = new Person();
        person.Name = "Kennedy";
        PropertyInfo propInfo = person.GetType().GetProperties().First(x => x.Name == "Name");
        var propValue = propInfo.GetValue(person).ToString();
        Console.Write(propValue);
    }

    private static void ValidateValidatable()
    {
        var user = new User();
        var validations = user.Validate();
        Console.WriteLine("");
    }

    private static void HashWithSHA1()
    {
        byte[] block1 = Encoding.ASCII.GetBytes("This");
        byte[] block2 = Encoding.ASCII.GetBytes("is");
        byte[] block3 = Encoding.ASCII.GetBytes("Sparta");

        SHA1 sha = new SHA1Managed();
        sha.TransformBlock(inputBuffer: block1, inputOffset: 0, inputCount: block1.Length, outputBuffer: block1, outputOffset: 0);
        sha.TransformBlock(inputBuffer: block2, inputOffset: 0, inputCount: block2.Length, outputBuffer: block2, outputOffset: 0);
        sha.TransformFinalBlock(block3, 0, block3.Length);
        sha.TransformFinalBlock(inputBuffer: block3, inputOffset: 0, inputCount: block3.Length);
        byte[] result = sha.Hash;
    }

    private static void EasyHashWithSHA1()
    {
        SHA1 sha = new SHA1Managed();
        UnicodeEncoding ue = new UnicodeEncoding();
        byte[] buffer = ue.GetBytes("kennedy");
        byte[] hashValue = sha.ComputeHash(buffer);
    }

    private static void CriptographyWithAes()
    {
        string textToEncrypt = "ultrasecretPassword";
        byte[] encrypted;
        string decrypted;
        using (Aes aesAlg = Aes.Create())
        {
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream memStream = new MemoryStream())
            {
                using (CryptoStream criptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(criptoStream))
                    {
                        swEncrypt.Write(textToEncrypt);
                    }
                    encrypted = memStream.ToArray();

                }
            }
            using (MemoryStream memStream = new MemoryStream(encrypted))
            {
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (CryptoStream decryptStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(decryptStream))
                    {
                        decrypted = srDecrypt.ReadToEnd();
                        Debug.Assert(decrypted == textToEncrypt);
                    }
                }
            }
        }

    }

    private static void PerformanceCounterExample()
    {
        var counterCreationDataCollection = new CounterCreationDataCollection();
        CounterCreationData counterCreationData1 = new CounterCreationData();
        counterCreationData1.CounterName = "data trans/sec";
        counterCreationData1.CounterType = PerformanceCounterType.NumberOfItems64;
        counterCreationDataCollection.Add(counterCreationData1);
        PerformanceCounterCategory category = PerformanceCounterCategory.Create(Assembly.GetExecutingAssembly().FullName, "Program category for IOT data",
            PerformanceCounterCategoryType.SingleInstance, counterCreationDataCollection);
        var finalCounter = new PerformanceCounter(categoryName: category.CategoryName, counterName: counterCreationData1.CounterName);
    }

    private static void checkedOverflow()
    {
        int i = 2147483647;
        i = checked(i + 4000000);
        checked
        {
            int j = 2147483647;
            j = j + 400000;
        }
    }

    private static void FileHandling()
    {
        File.Open("", FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
    }
    private static void DataContractSerializerHandler()
    {
        DataContractSerializer x = new DataContractSerializer(typeof(Artist));

        using (FileStream stream = new FileStream("seri.xml", FileMode.Create))
        {
            x.WriteObject(stream, new Artist("Jebbedt"));
        }
    }

    private static void Linq()
    {
        var musicTracks = new[] { new { ArtistId = 1 }, new { ArtistId = 2 }, new { ArtistId = 3 }, };
        var artists = new[] { new { ID = 1, Name = "João" }, new { ID = 2, Name = "Marcos" }, new { ID = 3, Name = "Antônio" },
                                new { ID = 4, Name = "João" }, new { ID = 5, Name = "João" } };
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
        var intoExample = from artist in artists group artist by artist.Name into tt select new { key = tt.Key, t = tt };


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
        ((IRight)x).Move(); //conversão implícita
        ILeft y = new MoveableOject();//conversão implícita
        y.Move();

    }

    private static void RegexReplaceExample()
    {
        string pattern = "(Ms\\.? | Mrs\\.? | Miss | Ms\\.? )";
        string[] names = { "Mrs. Henry Hunt", "Mr. Sara Samuels",
    "Abraham Adams", "Ms. Nicole Norris" };
        foreach (string name in names)
            Console.WriteLine(Regex.Replace(name, pattern, String.Empty));
    }

    private static void RegexMatchesExample(string price)
    {
        Regex reg = new Regex(@"^\d+(\.\d\s)?$");
        //Regex reg = new Regex(@"^\d+(\.\d\s)?$", RegexOptions.Compiled);
        MatchCollection matches = reg.Matches(price);
        var matched = (List<string>)matches.GetEnumerator();
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                Console.WriteLine(match.Value);
            }
        }
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

    [Obsolete("não use essa porra")]
    private static void DeprecatedMethod()
    {
        Console.Write("usou");
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



    public static string TestMethod(int callDuration, out int threadId)
    {
        Console.WriteLine("Test method begins.");
        Thread.Sleep(callDuration);
        threadId = Thread.CurrentThread.ManagedThreadId;
        return String.Format("My call time was {0}.", callDuration.ToString());
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
        void Do();
    }
    interface IRight
    {
        void Move();
    }
    class MoveableOject : ILeft, IRight
    {
        void ILeft.Move() { }
        void IRight.Move() { }
        void ILeft.Do() { }
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

    private static void PrintFormattedDateAndMoney()
    {
        Console.WriteLine(string.Format("{0:t} {0:dd} {1:N2}", DateTime.Now, 20.9));
        Console.WriteLine(string.Format("{0:hh:mm} {0:MM/dd/yyyy} {1:N2}", DateTime.Now, 20.988888));
        //t is time, d is date and N2 is centesimal point
        Console.WriteLine(string.Format("{0:D4}", 20));
        Console.WriteLine(string.Format("{0:0000#}", 30));
        Console.WriteLine(40.ToString("000"));
        Console.WriteLine(40.ToString("##0"));
    }

    private static bool? DictContains()
    {
        var dict = new Dictionary<string, int>() {
        {"Accounting", 1},
        {"Marketing", 2},
        {"Operations", 3}
      };
        bool? shouldBeNull = dict.ContainsKey("1");
        /* var shouldBeTrue = dict.Contains(new KeyValuePair<string, int>("Accounting", 1));
        var shouldBeNull = dict.Contains(new KeyValuePair<string, int>("Finance", 0));
        var shouldBeFalse = dict.Contains(new KeyValuePair<string, int>("Accounting", 2)); */
        return shouldBeNull;
    }
}

public delegate string AsyncMethodCaller(int callDuration, out int threadId);

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
