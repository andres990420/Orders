using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Frontend.Shared;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateEdit
    {
        private State? state;

        private FormWithName<State>? stateForm;

        [Inject]
        private IRepository repository { get; set; } = null!;

        [Inject]
        private SweetAlertService sweetAlertService { get; set; } = null!;

        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Parameter]
        public int StateId { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            var responseHttp = await repository.GetAsync<State>($"/api/states/{StateId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            state = responseHttp.Response;
        }

        private async Task EditAsync()
        {
            var responseHttp = await repository.PutAsync("/api/states", state);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message);
                return;
            }

            Return();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 300
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con exito");
        }
        private void Return()
        {
            stateForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo($"/countries/details/{state!.CountryId}");
        }
    }
}
