namespace Maincotech.Blazor
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class TagSelect
    {
        private readonly IList<TagSelectOption> _options = new List<TagSelectOption>();
        private bool _checkedAll;
        private bool _expand = false;

        [Parameter] public bool Expandable { get; set; }

        [Parameter] public bool HideCheckAll { get; set; }

        [Parameter] public string SelectAllText { get; set; } = "全部";

        [Parameter] public string CollapseText { get; set; } = "收起";

        [Parameter] public string ExpandText { get; set; } = "展开";

        [Parameter] public IList<string> Value { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public Action<List<string>> OnValueChanged { get; set; }

        private List<string> _value = new List<string>();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            SetClassMap();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (Value?.Count > 0 == true)
            {
                foreach (var option in _options)
                {
                    if (Value.Contains(option.Value))
                    {
                        option.Check(true);
                    }
                }
            }
        }

        protected void SetClassMap()
        {
            ClassMapper
                .Clear()
                .Add("tagSelect")
                .If("hasExpandTag", () => Expandable)
                .If("expanded", () => _expand);
        }

        private void HandleExpand()
        {
            _expand = !_expand;
        }

        private void HandleCheckedChange(bool isChecked)
        {
            _checkedAll = isChecked;
            foreach (var option in _options)
            {
                option.Check(_checkedAll);
            }
            _value.Clear();
            if (isChecked)
            {
                _value.AddRange(_options.Select(x => x.Value));
            }
            OnValueChanged?.Invoke(_value);
        }

        public void AddOption(TagSelectOption option)
        {
            _options.Add(option);
        }

        public void SelectItem(string value)
        {
            _value.Add(value);
            Value?.Add(value);
            OnValueChanged?.Invoke(_value);
        }

        public void UnSelectItem(string value)
        {
            _value.Add(value);
            Value?.Remove(value);
            OnValueChanged?.Invoke(_value);
        }
    }
}