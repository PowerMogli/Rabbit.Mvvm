//-------------------------------------------------------------------------------
// <copyright file="ScanResult.cs" company="Appccelerate">
//   Copyright (c) 2008-2014
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Appccelerate.EventBroker.Internals.Inspection
{
    using System.Collections.Generic;

    public class ScanResult
    {
        public ScanResult(
            IEnumerable<PropertyPublicationScanResult> publications, 
            IEnumerable<PropertySubscriptionScanResult> subscription)
        {
            this.Publications = publications;
            this.Subscription = subscription;
        }

        public IEnumerable<PropertyPublicationScanResult> Publications { get; private set; }

        public IEnumerable<PropertySubscriptionScanResult> Subscription { get; private set; }
    }
}