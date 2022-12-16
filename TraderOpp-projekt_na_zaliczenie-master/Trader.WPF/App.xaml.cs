using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Trader.FinancialModelingPrepAPI.Services;
using Trader.WPF.State.Accounts;
using Trader.WPF.State.Assets;
using Trader.WPF.State.Authenticators;
using Trader.WPF.State.Navigators;
using Trader.WPF.ViewModels;
using Trader.WPF.ViewModels.Factories;
using TraderOop.Domain;
using TraderOop.Domain.Models;
using TraderOop.Domain.Services;
using TraderOop.Domain.Services.AuthenticationServices;
using TraderOop.Domain.Services.TransactionServices;
using TraderOop.EntityFramework;
using TraderOop.EntityFramework.Services;

namespace Trader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            /*TraderDbContextFactory contextFactory = new TraderDbContextFactory();
            using (TraderDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.EnsureCreated();
            }*/

                IServiceProvider serviceProvider = CreateServiceProvider();

            Window mainWindow = serviceProvider.GetRequiredService<MainWindow>(); 
            mainWindow.Show();
            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<TraderDbContextFactory>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            services.AddSingleton<ICurrencyPriceService, CurrencyPriceService>();
            services.AddSingleton<IBuyService, BuyService>();
            services.AddSingleton<ISellService, SellService>();
            services.AddSingleton<ICurrencyService, CurrencyService>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<ITraderViewModelAbstractFactory, TradeViewModelAbstractFactory>();
            services.AddSingleton<BuyViewModel>();
            services.AddSingleton<SellViewModel>();
            services.AddSingleton<PortfolioViewModel>();
            //gdyby trzeba było jednej instancji HomeViewModel per aplikacje, limit api
            /*services.AddSingleton<HomeViewModel>(services => 
            
                new HomeViewModel(
                    CurrencyListingViewModel.LoadCurrencyViewModel(services.GetRequiredService<ICurrencyService>()))
            );*/
            services.AddSingleton<AssetSummaryViewModel>();
            services.AddSingleton<CreateViewModel<HomeViewModel>>(services =>
            {
                return () => new HomeViewModel(
                    services.GetRequiredService<AssetSummaryViewModel>(),
                    CurrencyListingViewModel.LoadCurrencyViewModel(services.GetRequiredService<ICurrencyService>()));
            });

            services.AddSingleton<CreateViewModel<BuyViewModel>>(services =>
            {
                return () => services.GetRequiredService<BuyViewModel>();
            });

            services.AddSingleton<CreateViewModel<SellViewModel>>(services =>
            {
                return () => services.GetRequiredService<SellViewModel>();
            });

            services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services =>
            {
                return () => services.GetRequiredService<PortfolioViewModel>();
            });

            services.AddSingleton<Renavigator<LoginViewModel>>();
            services.AddSingleton<CreateViewModel<RegisterViewModel>>(services =>
            {
                return () => new RegisterViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<Renavigator<LoginViewModel>>(),                   
                    services.GetRequiredService<Renavigator<LoginViewModel>>()
                    );
            });

            services.AddSingleton<Renavigator<HomeViewModel>>();
            services.AddSingleton<Renavigator<RegisterViewModel>>();
            services.AddSingleton<CreateViewModel<LoginViewModel>>(services =>
            {
                return () => new LoginViewModel(
                    services.GetRequiredService<IAuthenticator>(),
                    services.GetRequiredService<Renavigator<HomeViewModel>>(),
                    services.GetRequiredService<Renavigator<RegisterViewModel>>()
                    );
            });


            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator,Authenticator>();
            services.AddSingleton<IAccountStore, AccountStore>();
            services.AddSingleton<AssetStore>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<BuyViewModel>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
