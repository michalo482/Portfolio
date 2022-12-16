namespace Trader.WPF.ViewModels
{
    public interface ISearchSymbolViewModel
    {
        decimal CurrencyPrice { set; }
        string ErrorMessage { set; }
        string SearchResultSymbol { set; }
        string Symbol { get; }
    }
}