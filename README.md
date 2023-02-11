# TradeTracker

TradeTracker is a basic Windows application designed to assist financial traders in maintaining detailed logs of 
trading activities, assets transacted, and account performance. Written using the Windows Presentation Foundation 
(WPF) framework with Syncfusion Community components, TradeTracker is intended to provide a basic, no-frills 
approach to routine financial instrument transaction tracking.

## Rationale

TradeTracker is an evolution of the myriad of (often quite complicated) Excel workbooks that I maintain during the 
course of my trading endeavors. While Excel works great for basic tracking of large amounts of homogeneous data, it 
is difficult to make it work in a "fully-online" fashion; there are also other things I had always wanted such as 
real-time equity charts, complicated statistical and performance analyses, and other features which would be 
excruciating to implement either via Excel formula raw or VBA.

At the same time, I also wanted to be able to take my programming out of the basic command-line level of evolution 
and finally learn to design some high-performance applications using modern UI tools. For this purpose, I chose the 
WPF framework as it is both developmentally mature and widely-used.

## Installation

While work is currently in progress and there is no installer built for TradeTracker, the only way of installing or 
running the application is to clone the repository wholesale and build the Visual Studio solution wholesale. As 
such, the requirements for doing so are:

- Visual Studio 2022
- .NET Framework 4.8+
- Access to Syncfusion controls (via their Community license) and the following references/assemblies:
    - `Syncfusion.Data.WPF`
    - `Syncfusion.SfChart.WPF`
    - `Syncfusion.SfSkinManager.WPF`
    - `Syncfusion.Shared.Wpf`
    - `Syncfusion.Themes.Office2010Black`

## Limitations

As this is primarily intended to be an educational tool and mainly for my use, there are a number of limitations, 
unaddressed points, or issues inherent in its design that may or may not be addressed in future revisions. 
Currently, known issues with the data model are:

- No support for uninvested (cash) dividends. The only support for dividends via the current data model is a specific type of [`Order`](https://github.com/ecfedele/TradeTracker/blob/main/TradeTracker/Data/Order.cs) using the [`OrderDirection.Dividend`](https://github.com/ecfedele/TradeTracker/blob/main/TradeTracker/Data/Enumerations/OrderDirection.cs) option. This prevents the reinvested dividend from (directly) affecting the cash balance ledger, but is a metaphor which cannot be extended to uninvested dividends. In a future extension, cash dividends *may* be implemented by subclassing [`Deposit`](https://github.com/ecfedele/TradeTracker/blob/main/TradeTracker/Data/Deposit.cs).
- Deposits and withdrawals curiously possess the quantity 1.0, due to a non-nullable `decimal` quantity field. In an earlier revision of TradeTracker, this was addressed by making the field nullable (i.e. `decimal?`) which complicated some of the descendant logic unnecessarily. It was decided that this nullability would be removed and the [`Deposit`](https://github.com/ecfedele/TradeTracker/blob/main/TradeTracker/Data/Deposit.cs) and [`Withdrawal`](https://github.com/ecfedele/TradeTracker/blob/main/TradeTracker/Data/Withdrawal.cs) objects would report a unit quantity of 1.
- Market data support for equities (stocks) and foreign exchange instruments only. The "portfolio status" feature of TradeTracker will almost certainly use a stripped-down version of the freely-available [Twelve Data](https://twelvedata.com/) financial data API plans to obtain price information about currently-open positions. This data-gathering is simplified for equities and forex instruments because these are perpetual assets with no expiry. Options and futures, however, are assets with definite expiration dates. In particular, the sorts of APIs that are capable of providing pricing information for options chains and future front-month contracts are the sorts of APIs that one probably needs a Wall Street firm to pay for. Also, implementing these data APIs in a method that makes sense might constrain the core logic in ways that defeats the original purpose of this app ("basic" and "educational").