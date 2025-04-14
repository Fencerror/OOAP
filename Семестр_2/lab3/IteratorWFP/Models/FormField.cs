namespace lab3.Models
{
    public enum FieldType
    {
        Text,
        Dropdown
    }

    public class FormField
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Description { get; set; } 
        public FieldType Type { get; set; } 
        public string[] Options { get; set; } 

        public FormField(string label, string description = "", FieldType type = FieldType.Text, string[]? options = null)
        {
            Label = label;
            Value = string.Empty;
            Description = description;
            Type = type;
            Options = options ?? new string[0];
        }
    }
}
