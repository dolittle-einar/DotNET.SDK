﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using doLittle.Runtime.Applications;
using doLittle.Runtime.Transactions;

namespace doLittle.Commands
{
    /// <summary>
    /// Represents a request for executing a command
    /// </summary>
    public class CommandRequest
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommandRequest"/>
        /// </summary>
        /// <param name="correlationId"><see cref="TransactionCorrelationId"/> for the transaction</param>
        /// <param name="type"><see cref="IApplicationResourceIdentifier">Identifier</see> of the command</param>
        /// <param name="content">Content of the command</param>
        public CommandRequest(TransactionCorrelationId correlationId, IApplicationResourceIdentifier type, IDictionary<string, object> content)
        {
            CorrelationId = correlationId;
            Type = type;
            Content = content;
        }

        /// <summary>
        /// Gets the <see cref="TransactionCorrelationId"/> representing the transaction
        /// </summary>
        /// <returns>The <see cref="TransactionCorrelationId"/></returns>
        public TransactionCorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResourceIdentifier"/> representing the type of the Command
        /// </summary>
        /// <returns>
        /// <see cref="IApplicationResourceIdentifier"/> representing the type of the Command
        /// </returns>
        public IApplicationResourceIdentifier   Type { get; }

        /// <summary>
        /// Gets the content of the command
        /// </summary>
        /// <returns>
        /// <see cref="IDictionary{TKey, TValue}">Content</see> of the command
        /// </returns>
        public IDictionary<string, object> Content { get; }
    }
}