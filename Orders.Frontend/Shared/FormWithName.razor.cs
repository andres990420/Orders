using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Orders.Shared.Interfaces;

namespace Orders.Frontend.Shared
{
    public partial class FormWithName<TModel> where TModel : IEntityWithName
    {
        private EditContext editContext = null!;

        [Parameter, EditorRequired]
        public TModel Model { get; set; } = default!;

        [Parameter, EditorRequired]
        public string Label { get; set; } = null!;

        [Parameter, EditorRequired]
        public EventCallback OnValidSubmit { get; set; }

        [Parameter, EditorRequired]
        public EventCallback ReturnAction { get; set; }

        [Inject]
        public SweetAlertService SweetAlertService { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; }

        protected override void OnInitialized()
        {
            editContext = new(Model);
        }

        private async Task OnBeforeInteralNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();
            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmacion",
                Text = "Deseas abandonar la pagina actual y perder los cambios?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirma = !string.IsNullOrEmpty(result.Value);
            if (confirma)
            {
                return;
            }

            context.PreventNavigation();
        }
    }
}