namespace BlazorWebAssemblyApp.Shared;
public class UiEntry
{
    public UiEntry()
    {
        Fields = new Dictionary<string, dynamic>();
    }

    public Guid Id { get; set; }

    public string Slug { get; set; }

    public string Version { get; set; }

    public Dictionary<string, object> Fields { get; set; }

    public List<string> UpdatedFields { get; private set; }

    public void UpdateField(string fieldName, dynamic value)
    {
        Fields[fieldName] = value;

        if (UpdatedFields == null)
        {
            UpdatedFields = new List<string>();
        }

        if (!UpdatedFields.Contains(fieldName))
        {
            UpdatedFields.Add(fieldName);
        }
    }
}
