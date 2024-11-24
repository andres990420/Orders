﻿using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] private IRepository repository { get; set; } = null!;

        [Inject]
        private SweetAlertService sweetAlertService { get; set; } = null!;

        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        public List<Country>? Countries { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();

        }

        private async Task LoadAsync()
        {
            var responseHttp = await repository.GetAsync<List<Country>>("api/countries");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Countries = responseHttp.Response;
        }
        private async Task DeleteAsync(Country country)
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmacion",
                Text = $"Esta seguro de quere borrar el pais : {country.Name}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm) 
            {
                return;
            }

            var responseHttp = await repository.DeleteAsync<Country>($"api/countries/{country.Id}");
            if (responseHttp.Error) 
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound) 
                {
                    navigationManager.NavigateTo("/countries");
                }
                else 
                { 
                    var mesajeError = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", mesajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions 
            { 
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
        }

    }

}