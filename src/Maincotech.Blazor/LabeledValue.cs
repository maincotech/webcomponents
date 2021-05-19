namespace Maincotech.Blazor
{
    public class LabeledValue
    {
        public LabeledValue(string key, object label, bool disabled = false)
        {
            Key = key;
            Label = label;
            Disabled = disabled;
        }

        public string Key { get; set; }
        public object Label { get; set; }
        public bool Disabled { get; set; }
    }
}