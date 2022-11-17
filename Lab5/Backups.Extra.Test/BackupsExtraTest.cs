using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Backups.Extra.Test;

public class Product
{
    private decimal _cost;

    public Product(decimal cost, string name)
    {
        _cost = cost;
        Name = name;
    }

    public string Name { get; }
}

public class BackupsExtraTest
{
    public static void Mmain()
    {
        var product = new Product(50, "bread");
        string output = JsonConvert.SerializeObject(product, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        var deserializedProduct = JsonConvert.DeserializeObject(output);
        Console.WriteLine(output);
    }
}