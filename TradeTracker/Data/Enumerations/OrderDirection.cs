// ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════╗ 
// ║ File:        Order.cs                                                                                        ║ 
// ║ Project:     TradeTracker                                                                                    ║ 
// ║ Author:      E. C. Fedele                                                                                    ║ 
// ║ Date:        February 11, 2023                                                                               ║ 
// ║ Description: Provides a small enumeration for use by Order objects and their children.                       ║ 
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

namespace TradeTracker.Data.Enumerations
{
    public enum OrderDirection
    {
        Buy,
        Sell,
        Dividend
    }
}