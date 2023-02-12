// ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗ 
// ║ File:        Transaction.cs                                                                                  ║ 
// ║ Project:     TradeTracker                                                                                    ║ 
// ║ Author:      E. C. Fedele                                                                                    ║ 
// ║ Date:        February 10, 2023                                                                               ║ 
// ║ Description: Abstract implementation prototype for a Transaction object.                                     ║ 
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
    public abstract class Transaction
    {
        #region Public accessors

        public int      SequenceNumber;
        public string   AccountId;
        public string   Symbol;
        public string   Type;
        public string   Direction;
        public decimal  Quantity;
        public decimal  Price;
        public decimal  Fees;
        public decimal  Subtotal;
        public decimal  RunningBalance;
        public DateTime Time;

        #endregion

        #region Inheritable methods

        public abstract Tuple<string, string> GetTypeCellColorization();
        public abstract Tuple<string, string> GetDirectionCellColorization();

        #endregion
    }
}