// ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗ 
// ║ File:        Withdrawal.cs                                                                                   ║ 
// ║ Project:     TradeTracker                                                                                    ║ 
// ║ Author:      E. C. Fedele                                                                                    ║ 
// ║ Date:        February 11, 2023                                                                               ║ 
// ║ Description: Implementation of a Withdrawal object, which descends from Transaction and is responsible for   ║
// ║              representing cash-outflow events. Otherwise identical to Deposit except for amount polarity.    ║
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
    public class Withdrawal : Transaction
    {
        #region Public accessors

        public int      SequenceNumber { get { return base.SequenceNumber; } set { base.SequenceNumber = value; } }
        public string   AccountId      { get { return base.AccountId;      } set { base.AccountId = value;      } }
        public string   Symbol         { get { return base.Symbol;         } set { base.Symbol = value;         } }
        public string   Type           { get { return base.Type;           } set { base.Type = value;           } }
        public decimal  Quantity       { get { return base.Quantity;       } set { base.Quantity = value;       } }
        public decimal  Price          { get { return base.Price;          } set { base.Price = value;          } } 
        public decimal  Fees           { get { return base.Fees;           } set { base.Fees = value;           } } 
        public decimal  Subtotal       { get { return base.Subtotal;       } set { base.Subtotal = value;       } } 
        public decimal  RunningBalance { get { return base.RunningBalance; } set { base.RunningBalance = value; } }
        public DateTime Time           { get { return base.Time;           } set { base.Time = value;           } }

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new Withdrawal object with the specified parameters. This constructor does not account for 
        /// fees.
        /// </summary>
        /// <param name="accountId">The account to debit the withdrawn amount from</param>
        /// <param name="sender">The external sending account to withdraw funds to</param>
        /// <param name="withdrawalTime">The time the amount was considered withdrawn</param>
        /// <param name="withdrawalAmount">The amount withdrawn</param>
        public Withdrawal(string accountId, string sender, DateTime withdrawalTime, decimal withdrawalAmount)
        {
            this.AccountId = accountId;
            this.Symbol    = sender;
            this.Type      = "WITHDRAWAL";
            this.Quantity  = 1.0m;
            this.Time      = withdrawalTime;
            this.Price     = -1.0m * withdrawalAmount;
            this.Fees      = 0.0m;
            this.Subtotal  = this.Price - this.Fees;
        }

        /// <summary>
        /// Creates a new Withdrawal object with the specified parameters. This constructor accounts for fees.
        /// </summary>
        /// <param name="accountId">The account to debit the withdrawn amount from</param>
        /// <param name="sender">The external sending account to withdraw funds to</param>
        /// <param name="withdrawalTime">The time the amount was considered withdrawn</param>
        /// <param name="withdrawalAmount">The amount withdrawn</param>
        /// <param name="fees">Any fees or commission charges associated with the withdrawal</param>
        public Withdrawal(string accountId, string sender, DateTime withdrawalTime, decimal withdrawalAmount, 
                         decimal fees)
        {
            this.AccountId = accountId;
            this.Symbol    = sender;
            this.Type      = "WITHDRAWAL";
            this.Quantity  = 1.0m;
            this.Time      = withdrawalTime;
            this.Price     = -1.0m * withdrawalAmount;
            this.Fees      = fees;
            this.Subtotal  = this.Price - this.Fees;
        }

        #endregion
    }
}