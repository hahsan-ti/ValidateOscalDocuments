using System.Xml;
using System.Xml.Schema;
using System.ComponentModel;

namespace VodLibrary
{
    public enum ModelType : int
    {
        CATALOG = 0,
        PROFILE,
        COMPONENT,
        SSP,
        ASSESS_PLAN,
        ASSESS_RESULT,
        POAM,
        COMPLETE,
        UNKNOWN,
        ASSESS_ACTIVITY,
        OTHER
    };

    public class ValidateOscalDocuments
    {
        private const string VERSION = @"v1.0.4";
        public const string NAMESPACE = @"http://csrc.nist.gov/ns/oscal/1.0";

        private readonly Dictionary<ModelType, string> schemaNames = new()
        {
            { ModelType.CATALOG, "catalog" },
            { ModelType.PROFILE, "profile" },
            { ModelType.COMPONENT, "component" },
            { ModelType.SSP, "ssp" },
            { ModelType.ASSESS_PLAN, "assessment-plan" },
            { ModelType.ASSESS_RESULT, "assessment-result" },
            { ModelType.POAM, "poam" },
            { ModelType.COMPLETE, "complete" },
        };

        private readonly ModelType modelType = ModelType.UNKNOWN;
        private readonly XmlReaderSettings Settings = new();
        public List<ValidationError> ValidationErrors { get; }

        public ValidateOscalDocuments(string document, ModelType modelType = ModelType.SSP)
            : this(modelType) => Validate(document);

        public ValidateOscalDocuments(ModelType modelType = ModelType.SSP)
        {
            if (modelType >= ModelType.UNKNOWN)
            {
                throw new InvalidEnumArgumentException("OSCAL Model Type Unknown or Not Implemented");
            }

            this.modelType = modelType;
            ValidationErrors = new();

            Settings.Schemas.Add(NAMESPACE, $"./schema/{VERSION}/oscal_{schemaNames[this.modelType]}_schema.xsd");
            Settings.ValidationType = ValidationType.Schema;
            Settings.ValidationEventHandler
                += (object? sender, ValidationEventArgs e) =>
                    ValidationErrors.Add(new()
                    {
                        Severity = e.Severity,
                        Error = e.Message
                    });
        }

        public void Validate(string document)
        {
            if (string.IsNullOrEmpty(document))
            {
                throw new ArgumentNullException(nameof(document));
            }
            var x = Path.GetExtension(document);
            if (Path.GetExtension(document) != ".xml")
            {
                ValidationErrors.Add(new() { 
                    Severity = XmlSeverityType.Error,
                    Error = $"{document} must have the XML extension"
                });
                return;
            }

            using XmlReader reader = XmlReader.Create(document, Settings);
            while (reader.Read()) { }
        }

        public bool IsValid => ValidationErrors.Count == 0;
    }
}
