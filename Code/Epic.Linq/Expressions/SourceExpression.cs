//  
//  SourceExpression.cs
//  
//  Author:
//       Giacomo Tesio <giacomo@tesio.it>
// 
//  Copyright (c) 2010-2011 Giacomo Tesio
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
using Epic.Linq.Expressions.Visit;

namespace Epic.Linq.Expressions
{
    public sealed class SourceExpression : DomainExpression
    {
        public SourceExpression (Type type, string name)
            : base(type, name)
        {
        }
        
        #region implemented abstract members of Epic.Linq.Expressions.DomainExpression
        public override bool Equals (DomainExpression other)
        {
            if(object.ReferenceEquals(this, other))
                return true;
            SourceExpression otherQuery = other as SourceExpression;
            if(null == otherQuery)
                return false;
            return Name.Equals(otherQuery.Name) && Type.Equals(otherQuery.Type);
        }
        #endregion
        
        public override System.Linq.Expressions.Expression Accept (ICompositeVisitor visitor, IVisitState state)
        {
            ICompositeVisitor<SourceExpression> queryVisitor = visitor.GetVisitor<SourceExpression>(this);
            return queryVisitor.Visit(this, state);
        }
    }
}
