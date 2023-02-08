using System.Xml;
using VodLibrary;

namespace ValidateOscalDocuments;

public enum ModelType: int
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
    private const string VERSION = "v1.0.4";
    private readonly Dictionary<ModelType, string> schemaNames = new ()
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
    public List<ValidationError> ValidationErrors { get; }

    public ValidateOscalDocuments(ModelType modelType=ModelType.SSP)
    {
        this.modelType = modelType;

        XmlReaderSettings settings = new();
        settings.Schemas.Add(CODE_DOES_NOT_COMPLICE);

        ValidationErrors = new();
    }

    public bool IsValid() => true;
}
