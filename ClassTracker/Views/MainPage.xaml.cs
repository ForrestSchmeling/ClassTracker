using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ClassTracker.Models;
using ClassTracker.Views;

namespace ClassTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            PublicClass.initializeTermsCollection();
            InitializeComponent();
        }

        private async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermFormPage());
        }

        private async void Term_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var term = (Term)layout.BindingContext;
            await Navigation.PushAsync(new TermPage(term));
        }
    }
}

