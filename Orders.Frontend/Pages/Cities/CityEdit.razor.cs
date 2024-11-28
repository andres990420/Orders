using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Frontend.Shared;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Cities
{
    public partial class CityEdit
    {
        private City? city;

        private FormWithName<City>? cityForm;

        [Inject]
        private IRepository repository { get; set; } = null!;

        [Inject]
        private SweetAlertService sweetAlertService { get; set; } = null!;

        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Parameter]
        public int CityId { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            var responseHttp = await repository.GetAsync<City>($"/api/cities/{CityId}");
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
            city = responseHttp.Response;
        }

        private async Task EditAsync()
        {
            var responseHttp = await repository.PutAsync("/api/cities", city);
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
            cityForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo($"/states/details/{city!.StateId}");
        }
    }
}
