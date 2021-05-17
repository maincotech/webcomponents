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

        public string Key { get; }
        public object Label { get; }
        public bool Disabled { get; }
    }
}