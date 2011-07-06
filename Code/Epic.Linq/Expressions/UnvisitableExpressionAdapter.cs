//  
//  UnvisitableExpressionAdapter.cs
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
using System.Linq.Expressions;
using Epic.Linq.Expressions.Visit;
using System.Collections.Generic;

namespace Epic.Linq.Expressions
{
    internal sealed class UnvisitableExpressionAdapter : VisitableExpression
    {
        private readonly Expression _expression;
        
        private static Type GetUnvisitableExpressionType(Expression unvisitableExpression)
        {
            if(null == unvisitableExpression)
                throw new ArgumentNullException("unvisitableExpression");
            if(unvisitableExpression.NodeType.Equals(VisitableExpression.VisitableExpressionNodeType))
                throw new ArgumentException("The expression provided is visitable.");        
            return unvisitableExpression.Type;
        }
        
        public UnvisitableExpressionAdapter (Expression unvisitableExpression)
            : base(GetUnvisitableExpressionType(unvisitableExpression))
        {
            _expression = unvisitableExpression;
        }
        
        #region implemented abstract members of Epic.Linq.Expressions.VisitableExpression
        public override void Accept (ICompositeVisitor visitor)
        {
            throw new NotImplementedException ();
        }
        #endregion
        /*
        internal class UnvisitableExpressionVisitor
        {
            private Stack<KeyValuePair<ICompositeVisitor, int>> _visitors;
            public UnvisitableExpressionVisitor(ICompositeVisitor visitor)
            {
                if(null == visitor)
                    throw new ArgumentNullException("visitor");
                _visitors = new Stack<KeyValuePair<ICompositeVisitor, int>>();
                _visitors.Push(new KeyValuePair<ICompositeVisitor, int>(visitor, 0));
            }
            
            private ICompositeVisitor<TExpression> TakeVisitor<TExpression>()
            {
                ICompositeVisitor top = _visitors.Peek();
                ICompositeVisitor<TExpression> visitor = top.GetVisitor<TExpression>();
                if(null != visitor && !object.ReferenceEquals(top, visitor))
                    _visitors.Push(visitor);
                return visitor;
            }
        }*/
    }
}

