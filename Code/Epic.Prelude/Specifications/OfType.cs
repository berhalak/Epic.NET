//
//  OfType.cs
//
//  Author:
//       Giacomo Tesio <giacomo@tesio.it>
//
//  Copyright (c) 2010-2012 Giacomo Tesio
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

namespace Epic.Specifications
{
    [Serializable]
    public sealed class OfType<TCandidate, TInitial> : SpecificationBase<OfType<TCandidate, TInitial>, TCandidate>,
                                                       IEquatable<OfType<TCandidate, TInitial>>
        where TCandidate : class
        where TInitial : class
    {
        static OfType ()
        {
            if (!typeof(TCandidate).IsAssignableFrom (typeof(TInitial)) && !typeof(TInitial).IsAssignableFrom (typeof(TCandidate))) {
                string message = string.Format ("Cannot cast neither from {0} to {1} nor from {1} to {0}.", typeof(TInitial), typeof(TCandidate));
                throw new InvalidCastException (message);
            }
        }
        
        private readonly ISpecification<TInitial> _innerSpecification;

        public OfType ()
            : this(Any<TInitial>.Specification)
        {
        }

        public ISpecification<TInitial> InnerSpecification
        {
            get
            {
                return _innerSpecification;
            }
        }
        
        public OfType (ISpecification<TInitial> innerSpecification)
        {
            if (null == innerSpecification)
                throw new ArgumentNullException ("innerSpecification");
            _innerSpecification = innerSpecification;
        }

        #region implemented abstract members of Epic.Specifications.SpecificationBase
        protected override bool EqualsA (OfType<TCandidate, TInitial> otherSpecification)
        {
            return _innerSpecification.Equals (otherSpecification._innerSpecification);
        }

        protected override bool IsSatisfiedByA (TCandidate candidate)
        {
            return _innerSpecification.IsSatisfiedBy (candidate as TInitial);
        }
        
        protected override ISpecification<TOther> OfAnotherType<TOther> ()
        {
            return _innerSpecification.OfType<TOther> ();
        }

        public override Type CandidateType {
            get {
                return _innerSpecification.CandidateType;
            }
        }
        #endregion
    }
}
