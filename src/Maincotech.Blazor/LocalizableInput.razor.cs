namespace Maincotech.Blazor
{
    using AntDesign;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;
    using OneOf;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    public partial class LocalizableInput
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public class Model
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string SupportLanguages { get; set; }

            [Required]
            public string Terms { get; set; }
        }

        private Model _model = new Model();

        private string _name;

        [Parameter]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a callback that updates the bound value.
        /// </summary>
        [Parameter]
        public EventCallback<string> NameChanged { get; set; }

        private void OnNameChanged(string value)
        {
            if (_name.NotEqualsIgnoreCase(value))
            {
                _name = value;
                NameChanged.InvokeAsync(_name);
            }
        }

        [Parameter]
        public List<string> SupportLanguages { get; set; }

        [Parameter]
        public Func<string, string> OnGetTerms { get; set; }

        [Parameter]
        public Action<string, string> OnAddTerms { get; set; }

        [Inject] AntDesign.JsInterop.InteropService InteropService { get; set; }

        [Inject] private MessageService MessageService { get; set; }
        public string CurrentLanguage { get; set; }

        private string _terms;

        public string Terms
        {
            get { return _terms; }
            set
            {
                if (_terms.NotEqualsIgnoreCase(value))
                {
                    _terms = value;
                    StateHasChanged();
                }
            }
        }

        private bool _visible = false;
        private bool _loading = false;

        public bool IsLoading
        {
            get { return _loading; }
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    StateHasChanged();
                }
            }
        }

        private List<LabeledValue> _languages;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            _languages = new List<LabeledValue>();
            foreach (var lan in SupportLanguages)
            {
                _languages.Add(new LabeledValue(lan, lan));
            }
        }

        private void ShowModal()
        {
            _visible = true;
        }

        private void OnChange(OneOf<string, IEnumerable<string>, LabeledValue, IEnumerable<LabeledValue>> value, OneOf<SelectOption, IEnumerable<SelectOption>> option)
        {
            try
            {
                IsLoading = true;
                Terms = OnGetTerms(CurrentLanguage);
            }
            finally
            {
                IsLoading = false;
            }
            // Console.WriteLine($"selected: ${value}");
        }

        private async Task HandleOk(MouseEventArgs e)
        {
            try
            {
                IsLoading = true;
                OnAddTerms(CurrentLanguage, Terms);
                _visible = false;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void HandleCancel(MouseEventArgs e)
        {
            _visible = false;
        }

        private async ValueTask Copy()
        {
            await InteropService.Copy(Name);
            await MessageService.Success("The value has copied.");
        }
    }
}