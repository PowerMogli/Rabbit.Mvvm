//-------------------------------------------------------------------------------
// <copyright file="InvalidSubscriptionSignatureException.cs" company="Appccelerate">
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

namespace Appccelerate.EventBroker.Internals.Exceptions
{
    using System.Reflection;

    using Appccelerate.EventBroker.Exceptions;

    /// <summary>
    /// An <see cref="EventBrokerException"/> thrown when an invalid subscription signature is found while registering a subscriber.
    /// </summary>
    public class InvalidSubscriptionSignatureException : EventBrokerException
    {
        /// <summary>
        /// The method info of the handler with invalid signature.
        /// </summary>
        private readonly MethodInfo methodInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSubscriptionSignatureException"/> class.
        /// </summary>
        /// <param name="methodInfo">The method info.</param>
        public InvalidSubscriptionSignatureException(MethodInfo methodInfo)
            : base("Subscriber handler has invalid signature: '{0}'", methodInfo.DeclaringType.FullName, methodInfo.Name)
        {
            this.methodInfo = methodInfo;
        }

        /// <summary>
        /// Gets the method info of the handler with invalid signature.
        /// </summary>
        /// <value>Method info of invalid subscription handler.</value>
        public MethodInfo MethodInfo
        {
            get { return this.methodInfo; }
        }
    }
}
