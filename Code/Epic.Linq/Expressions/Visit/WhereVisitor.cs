//  
//  WhereVisitor.cs
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
using System.Linq;

namespace Epic.Linq.Expressions.Visit
{
    // TODO : optimize and fix! this is just a draft
    public sealed class WhereVisitor<TEntity> : VisitorsComposition.VisitorBase, ICompositeVisitor<MethodCallExpression>
    {
        public WhereVisitor (VisitorsComposition chain)
            : base(chain)
        {
        }
        
        internal protected override ICompositeVisitor<TExpression> AsVisitor<TExpression> (TExpression target)
        {
            ICompositeVisitor<TExpression> visitor = base.AsVisitor (target);
            if(null != visitor)
            {
                MethodCallExpression expression = target as MethodCallExpression;
                if(!expression.Method.DeclaringType.Equals(typeof(Queryable)) || !expression.Method.Name.Equals("Where")) 
                {
                    return null;
                }
            }
            return visitor;
        }

        #region ICompositeVisitor[MethodCallExpression] implementation
        public Expression Visit (MethodCallExpression target, IVisitState state)
        {
            ICompositeVisitor<Expression> sourceVisitor = GetVisitor<Expression>(target.Arguments[0]);
            RelationExpression relation = sourceVisitor.Visit(target.Arguments[0], state) as RelationExpression;
            
            ICompositeVisitor<Expression> predVisitor = GetVisitor<Expression>(target.Arguments[1]);
            UnaryExpression quote = target.Arguments[1] as UnaryExpression;
            Expression predicateExp = predVisitor.Visit(quote.Operand, state.Add<RelationExpression>(relation));
            
            return new SelectionExpression(relation, predicateExp as PredicateExpression);
        }
        #endregion

    }
}

