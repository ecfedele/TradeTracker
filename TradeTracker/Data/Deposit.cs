// ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗ 
// ║ File:        Deposit.cs                                                                                      ║ 
// ║ Project:     TradeTracker                                                                                    ║ 
// ║ Author:      E. C. Fedele                                                                                    ║ 
// ║ Date:        February 10, 2023                                                                               ║ 
// ║ Description: Implementation of a Deposit object, which descends from Transaction and is responsible for      ║
// ║              representing cash-inflow events.                                                                ║
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

namespace TradeTracker.Data
{
    public class Deposit : Transaction
    {
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
        /// Creates a new Deposit object with the specified parameters. This constructor does not account for fees.
        /// </summary>
        /// <param name="accountId">The account to credit the deposited amount to</param>
        /// <param name="sender">The external sending account used to deposit funds</param>
        /// <param name="depositTime">The time the amount was considered deposited</param>
        /// <param name="depositAmount">The amount deposited</param>
        public Deposit(string accountId, string sender, DateTime depositTime, decimal depositAmount)
        {
            this.AccountId = accountId;
            this.Symbol    = sender;
            this.Type      = "TRANSFER";
            this.Direction = "DEPOSIT";
            this.Quantity  = 1.0m;
            this.Time      = depositTime;
            this.Price     = depositAmount;
            this.Fees      = 0.0m;
            this.Subtotal  = this.Price - this.Fees;
        }

        /// <summary>
        /// Creates a new Deposit object with the specified parameters. This constructor accounts for fees.
        /// </summary>
        /// <param name="accountId">The account to credit the deposited amount to</param>
        /// <param name="sender">The external sending account used to deposit funds</param>
        /// <param name="depositTime">The time the amount was considered deposited</param>
        /// <param name="depositAmount">The amount deposited</param>
        /// <param name="fees">Any fees or commission charges associated with the deposit</param>
        public Deposit(string accountId, string sender, DateTime depositTime, decimal depositAmount, decimal fees)
        {
            this.AccountId = accountId;
            this.Symbol    = sender;
            this.Type      = "TRANSFER";
            this.Direction = "DEPOSIT";
            this.Quantity  = 1.0m;
            this.Time      = depositTime;
            this.Price     = depositAmount;
            this.Fees      = fees;
            this.Subtotal  = this.Price - this.Fees;
        }

        /// <summary>
        /// Inherited method from Transaction - provides the type cell colorization rule colors (in 6-char hex).
        /// Used by the UI logic to color the Type cell accordingly.
        /// </summary>
        /// <returns>A 2-tuple in the form (foreground, background)</returns>
        public override Tuple<string, string> GetTypeCellColorization()
        {
            return new Tuple<string, string>("#000000", "#FFFF00");
        }

        /// <summary>
        /// Inherited method from Transaction - provides the direction cell colorization rule colors (in 6-char 
        /// hex). Used by the UI logic to color the direction cell accordingly.
        /// </summary>
        /// <returns>A 2-tuple in the form (foreground, background)</returns>
        public override Tuple<string, string> GetDirectionCellColorization()
        {
            return new Tuple<string, string>("#000000", "#FFFF00");
        }

        #endregion
    }
}