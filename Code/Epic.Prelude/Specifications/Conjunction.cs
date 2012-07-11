//
//  And.cs
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
using System.Collections.Generic;

namespace Epic.Specifications
{
    [Serializable]
    public sealed class Conjunction<TCandidate> : SpecificationBase<Conjunction<TCandidate>, TCandidate>,
                                                  IEquatable<Conjunction<TCandidate>>,
                                                  IEnumerable<ISpecification<TCandidate>>
        where TCandidate : class
    {
        private readonly ISpecification<TCandidate>[] _specifications;
        public Conjunction(ISpecification<TCandidate> first, ISpecification<TCandidate> second)
        {
            if (null == first)
                throw new ArgumentNullException("first");
            if (null == second)
                throw new ArgumentNullException("second");
            if (first.Equals(second))
                throw new ArgumentException("Cannot create a Conjuction of two equals specification.", "second");

            List<ISpecification<TCandidate>> specifications = new List<ISpecification<TCandidate>>();

            Conjunction<TCandidate> firstAnd = first as Conjunction<TCandidate>;
            Conjunction<TCandidate> secondAnd = second as Conjunction<TCandidate>;

            if (null == firstAnd)
                specifications.Add(first);
            else
                specifications.AddRange(firstAnd._specifications);

            if (null == secondAnd)
            {
                // No need to check that the second is not included in first, since AndAlso already check this.
                specifications.Add(second);
            }
            else if(null == firstAnd)
            {
                for(int i = 0; i < secondAnd._specifications.Length; ++i)
                {
                    ISpecification<TCandidate> toAdd = secondAnd._specifications[i];
                    if(!first.Equals(toAdd))
                    {
                        specifications.Add(toAdd);
                    }
                }
            }
            else
            {
                for(int i = 0; i < secondAnd._specifications.Length; ++i)
                {
                    bool alreadyPresent = false;
                    ISpecification<TCandidate> toAdd = secondAnd._specifications[i];
                    for(int j = 0; j < firstAnd._specifications.Length && !alreadyPresent; ++j)
                    {
                        if(toAdd.Equals(firstAnd._specifications[j]))
                        {
                            alreadyPresent = true;
                        }
                    }
                    if (!alreadyPresent)
                    {
                        specifications.Add(toAdd);
                    }
                }
            }

            _specifications = specifications.ToArray();
        }

        /// <summary>
        /// Number of specifications in the conjuction.
        /// </summary>
        public int NumberOfSpecifications
        {
            get
            {
                return _specifications.Length;
            }
        }

        #region implemented abstract members of Epic.Specifications.SpecificationBase

        protected override ISpecification<TCandidate> AndAlso (ISpecification<TCandidate> other)
        {
            Conjunction<TCandidate> otherAnd = other as Conjunction<TCandidate>;
            if(null == otherAnd)
            {
                for(int i = 0; i < _specifications.Length; ++i)
                {
                    if(other.Equals(_specifications[i]))
                        return this;
                }
            }
            return base.AndAlso (other);
        }

        protected override bool EqualsA (Conjunction<TCandidate> otherSpecification)
        {
            if(_specifications.Length != otherSpecification._specifications.Length)
                return false;
            for(int i = 0; i < _specifications.Length; ++i)
                if(!_specifications[i].Equals(otherSpecification._specifications[i]))
                    return false;
            return true;
        }

        protected override bool IsSatisfiedByA (TCandidate candidate)
        {
            for(int i = 0; i < _specifications.Length; ++i)
                if(!_specifications[i].IsSatisfiedBy(candidate))
                    return false;
            return true;
        }

        #endregion

        #region IEnumerable implementation
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
        {
            return _specifications.GetEnumerator();
        }
        #endregion

        #region IEnumerable implementation
        IEnumerator<ISpecification<TCandidate>> IEnumerable<ISpecification<TCandidate>>.GetEnumerator ()
        {
            return (_specifications as IEnumerable<ISpecification<TCandidate>>).GetEnumerator();
        }
        #endregion
    }
}

