using Microsoft.Extensions.Configuration;

namespace MauiSettings;

public partial class MainPage 
{
    public MainPage(IConfiguration configuration)
    {
        
        InitializeComponent();

        lbEnvironment.Text = $"Environment: {configuration.GetValue<string>("Info:Environment")}";
        lbGreetings.Text = $"Greetings: {configuration.GetValue<string>("Info:Greettings")}";
    }
}