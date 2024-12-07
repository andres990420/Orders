using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Frontend.Shared;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public partial class CityCreate
    {
        private City city = new();

        private FormWithName<City>? cityForm;

        [Parameter]
        public int StateId { get; set; }

        [Inject]
        private IRepository Repository { get; set; } = null!;

        [Inject]
        private SweetAlertService SweetAlertService { get; set; } = null!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            city.StateId = StateId;
            var responseHttp = await Repository.PostAsync("api/cities", city);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }

            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 300
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con exito");
        }

        private void Return()
        {
            cityForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/states/details/{StateId}");
        }
    }
}
