using VodLibrary;
using System.Xml;

string uri = "./examples/incorrect-ssp.xml";

ValidateOscalDocuments v = new();
v.Validate(uri);
if (!v.IsValid)
{
	Console.WriteLine($"{uri} is an invalid document");
	Console.WriteLine(@"The errors are: ");
	foreach (var item in v.ValidationErrors)
	{
		Console.WriteLine($"{item.Severity} | {item.Error}");
	}
}
else
{
	Console.WriteLine($"{uri} is a valid document");
}
