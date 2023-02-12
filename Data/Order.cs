// ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗ 
// ║ File:        Order.cs                                                                                        ║ 
// ║ Project:     TradeTracker                                                                                    ║ 
// ║ Author:      E. C. Fedele                                                                                    ║ 
// ║ Date:        February 11, 2023                                                                               ║ 
// ║ Description: Implementation of an Order object, which is used to indicate trade actions.                     ║
// ╠══════════════════════════════════════════════════════════════════════════════════════════════════════════════╣ 
// ║ Copyright (C) 2023 E. C. Fedele                                                                              ║ 
// ║                                                                                                              ║ 
// ║ Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in          ║ 
// ║ compliance with the License. You may obtain a copy of the License at                                         ║ 
// ║                                                                                                              ║ 
// ║       http://www.apache.org/licenses/LICENSE-2.0                                                             ║ 
// ║                                                                                                              ║ 
// ║ Unless required by applicable law or agreed to in writing, software distributed under the License is         ║ 
// ║ distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or             ║ 
// ║ implied. See the License for the specific language governing permissions and limitations under the           ║ 
// ║ License.                                                                                                     ║ 
// ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using TradeTracker.Data.Enumerations;

namespace TradeTracker.Data
{
    public class Order : Transaction
    {
        private OrderDirection tradeDirection;

        #region Public accessors

        public int      SequenceNumber { get { return base.SequenceNumber; } set { base.SequenceNumber = value; } }
        public string   AccountId      { get { return base.AccountId;      } set { base.AccountId = value;      } }
        public string   Symbol         { get { return base.Symbol;         } set { base.Symbol = value;         } }
        public string   Type           { get { return base.Type;           } set { base.Type = value;           } }
        public string   Direction      { get { return base.Direction;      } set { base.Direction = value;      } }
        public decimal  Quantity       { get { return base.Quantity;       } set { base.Quantity = value;       } }
        public decimal  Price          { get { return base.Price;          } set { base.Price = value;          } } 
        public decimal  Fees           { get { return base.Fees;           } set { base.Fees = value;           } } 
        public decimal  Subtotal       { get { return base.Subtotal;       } set { base.Subtotal = value;       } } 
        public decimal  RunningBalance { get { return base.RunningBalance; } set { base.RunningBalance = value; } }
        public DateTime Time           { get { return base.Time;           } set { base.Time = value;           } }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new Order object with the specified parameters. This constructor does not account for fees.
        /// </summary>
        /// <param name="accountId">The account which performed the order operation</param>
        /// <param name="symbol">The asset or instrument symbol which was transacted</param>
        /// <param name="dir">The trade direction - see OrderDirection enumeration</param>
        /// <param name="quantity">The quantity of asset transacted</param>
        /// <param name="time">The date and time of the transaction</param>
        /// <param name="price">The price the asset was purchased or sold at</param>
        public Order(string accountId, string symbol, OrderDirection dir, decimal quantity, DateTime time, 
                     decimal price)
        {
            this.AccountId = accountId;
            this.Symbol    = symbol;
            this.Quantity  = quantity;
            this.Price     = price;
            this.Fees      = 0.0m;
            this.Time      = time;
            this.Direction = "ORDER";
            switch (dir) {
                case OrderDirection.Buy: 
                    this.Type     = "BUY";
                    this.Subtotal = (-1.0 * (this.Quantity * this.Price)) - this.Fees;
                    break;
                case OrderDirection.Sell:
                    this.Type     = "SELL";
                    this.Subtotal = (this.Quantity * this.Price) - this.Fees;
                    break;
                case OrderDirection.Dividend:
                    this.Type     = "DIVIDEND";
                    // By nature, reinvested dividends do not affect the cash ledger
                    break;
            }
        }

        /// <summary>
        /// Creates a new Order object with the specified parameters. This constructor accounts for fees.
        /// </summary>
        /// <param name="accountId">The account which performed the order operation</param>
        /// <param name="symbol">The asset or instrument symbol which was transacted</param>
        /// <param name="dir">The trade direction - see OrderDirection enumeration</param>
        /// <param name="quantity">The quantity of asset transacted</param>
        /// <param name="time">The date and time of the transaction</param>
        /// <param name="price">The price the asset was purchased or sold at</param>
        /// <param name="fees">Any fees or commissions associated with this trade</param>
        public Order(string accountId, string symbol, OrderDirection dir, decimal quantity, DateTime time, 
                     decimal price, decimal fees)
        {
            this.AccountId = accountId;
            this.Symbol    = symbol;
            this.Quantity  = quantity;
            this.Price     = price;
            this.Fees      = fees;
            this.Time      = time;
            this.Direction = "ORDER";
            switch (dir) {
                case OrderDirection.Buy: 
                    this.Type     = "BUY";
                    this.Subtotal = (-1.0 * (this.Quantity * this.Price)) - this.Fees;
                    break;
                case OrderDirection.Sell:
                    this.Type     = "SELL";
                    this.Subtotal = (this.Quantity * this.Price) - this.Fees;
                    break;
                case OrderDirection.Dividend:
                    this.Type     = "DIVIDEND";
                    // By nature, reinvested dividends do not affect the cash ledger
                    break;
            }
        }

        /// <summary>
        /// Quick convenience method which returns the cost basis of the transaction. Note that, by virtue of the 
        /// sign conventions observed here, the cost basis is always positive.
        /// </summary>
        /// <returns>The cost basis of the transaction</returns>
        public decimal GetCostBasis()
        {
            return this.Quantity * this.Price;
        }

        /// <summary>
        /// Convenience method which returns the trade direction enumeration object. This is a little kludgy; it is
        /// designed to preempt any issue with ObservableCollections and SfDataGrid which may be caused by the 
        /// Order descendant object possessing a different number of public properties than its ultimate parent,
        /// the Transaction class. This method has been crafted to allow an Order object to expose its direction in
        /// a programmatically-friendly manner without risking unusual behavior in the UI logic.
        /// </summary>
        /// <returns>The OrderDirection enumeration corresponding to the trade direction</returns>
        public OrderDirection GetDirection()
        {
            return this.tradeDirection;
        }

        /// <summary>
        /// Inherited method from Transaction - provides the type cell colorization rule colors (in 6-char hex).
        /// Used by the UI logic to color the Type cell accordingly.
        /// </summary>
        /// <returns>A 2-tuple in the form (foreground, background)</returns>
        public override Tuple<string, string> GetTypeCellColorization()
        {
            return new Tuple<string, string>("#000000", "#FFFFFF");
        }

        /// <summary>
        /// Inherited method from Transaction - provides the direction cell colorization rule colors (in 6-char 
        /// hex). Used by the UI logic to color the direction cell accordingly.
        /// </summary>
        /// <returns>A 2-tuple in the form (foreground, background)</returns>
        public override Tuple<string, string> GetDirectionCellColorization()
        {
            switch (this.tradeDirection) {
                case OrderDirection.Buy:      return new Tuple<string, string>("#000000", "#52FF52");
                case OrderDirection.Sell:     return new Tuple<string, string>("#000000", "#FF5353");
                case OrderDirection.Dividend: return new Tuple<string, string>("#000000", "#00FFFF");
            }
        }

        #endregion
    }
}