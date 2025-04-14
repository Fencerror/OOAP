using System.Collections.Generic;

namespace lab3.Models
{
    public class FormBlock
    {
        public string Title { get; set; }
        public List<FormField> Fields { get; set; }

        public FormBlock(string title)
        {
            Title = title;
            Fields = new List<FormField>();
        }

        public void AddField(FormField field)
        {
            Fields.Add(field);
        }
    }
}
