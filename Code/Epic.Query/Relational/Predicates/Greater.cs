//  
//  Greater.cs
//  
//  Author:
//       Marco Veglio <m.veglio@gmail.com>
// 
//  Copyright (c) 2010-2013 Giacomo Tesio
// 
//  This file is part of Epic.NET.
// 
//  Epic.NET is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Epic.NET is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
// 
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//  
using System;

namespace Epic.Query.Relational.Predicates
{
    /// <summary>
    /// Implementation of the Greater predicate. It is true if the first operand is greater than the second one.
    /// </summary>
    [Serializable]
    public sealed class Greater: ScalarPredicateBase, IEquatable<Greater>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epic.Query.Relational.Predicates.Greater"/> class.
        /// </summary>
        /// <param name='leftOperand'>
        /// Left operand.
        /// </param>
        /// <param name='rightOperand'>
        /// Right operand.
        /// </param>
        public Greater (Scalar leftOperand, Scalar rightOperand): base(leftOperand, rightOperand)
        {
        }

        /// <summary>
        /// Determines whether the specified <see cref="ScalarPredicateBase"/> is equal to the
        /// current <see cref="Epic.Query.Relational.Predicates.Greater"/>.
        /// </summary>
        /// <param name='other'>
        /// The <see cref="ScalarPredicateBase"/> to compare with the current <see cref="Epic.Query.Relational.Predicates.Greater"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="ScalarPredicateBase"/> is equal to the current
        /// <see cref="Epic.Query.Relational.Predicates.Greater"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals (ScalarPredicateBase other)
        {
            return Equals(other as Greater);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Greater"/> is equal to the current <see cref="Greater"/>.
        /// </summary>
        /// <param name='other'>
        /// The <see cref="Greater"/> to compare with the current <see cref="Greater"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Greater"/> is equal to the current
        /// <see cref="Greater"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Greater other)
        {
            if (other == null) return false;
            return this.Left.Equals (other.Left) && this.Right.Equals (other.Right);
        }

        /// <summary>
        /// Accept the specified visitor and context.
        /// </summary>
        /// <param name='visitor'>
        /// Visitor.
        /// </param>
        /// <param name='context'>
        /// Context.
        /// </param>
        /// <typeparam name='TResult'>
        /// The type of the result of the visit.
        /// </typeparam>
        /// <returns>Result of the visit.</returns>
        public override TResult Accept<TResult> (IVisitor<TResult> visitor, IVisitContext context)
        {
            return AcceptMe(this, visitor, context);
        }
    }
}

