// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sexp.cs" company="Oliver M. Haynold">
// Copyright (c) 2011, Oliver M. Haynold
// All rights reserved.
// </copyright>
// <summary>
// Implements Sexpressions
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RserveCli
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A local representation for an S-Expression (a.k.a., Sexp, Rexp, R-expression).
    /// </summary>
    public class Sexp : IList<Sexp>, IDictionary<string, Sexp>, IList<object>, IDictionary<string, object>
    {
        #region Constants and Fields

        /// <summary>
        /// The Sexp attributes, if any
        /// </summary>
        private Dictionary<string, Sexp> attributes;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether [as bool].
        /// </summary>
        /// <value>
        /// <c>true</c> if [as bool]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool AsBool
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Syntactic sugar for explicit conversion to bool
        /// </summary>
        /// <param name="s">The Sexp</param>
        /// <returns>The converted value</returns>
        public static explicit operator bool(Sexp s)
        {
            return s.AsBool;
        }

        /// <summary>
        /// Gets as dictionary of objects.
        /// </summary>
        public IDictionary<string, object> AsDictionary
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets as double.
        /// </summary>
        /// <value>
        /// As double.
        /// </value>
        public virtual double AsDouble
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Syntactic sugar for explicit conversion to double
        /// </summary>
        /// <param name="s">The Sexp</param>
        /// <returns>The converted value</returns>
        public static explicit operator double(Sexp s)
        {
            return s.AsDouble;
        }

        /// <summary>
        /// Gets as int.
        /// </summary>
        /// <value>
        /// The value as an integer.
        /// </value>
        public virtual int AsInt
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Syntactic sugar for explicit conversion to int
        /// </summary>
        /// <param name="s">The Sexp</param>
        /// <returns>The converted value</returns>
        public static explicit operator int(Sexp s)
        {
            return s.AsInt;
        }

        /// <summary>
        /// Gets as list of objects.
        /// </summary>
        public IList<object> AsList
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets as sexp bool.
        /// </summary>
        /// <value>
        /// As sexp bool.
        /// </value>
        public virtual SexpBoolValue AsSexpBool
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Syntactic sugar for explicit conversion to SexpBoolValue
        /// </summary>
        /// <param name="s">The Sexp</param>
        /// <returns>The converted value</returns>
        public static explicit operator SexpBoolValue(Sexp s)
        {
            return s.AsSexpBool;
        }

        /// <summary>
        /// Gets as dictionary of Sexps.
        /// </summary>
        public IDictionary<string, Sexp> AsSexpDictionary
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets as string.
        /// </summary>
        /// <value>
        /// As string.
        /// </value>
        public virtual string AsString
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Syntactic sugar for explicit conversion to string
        /// </summary>
        /// <param name="s">The Sexp</param>
        /// <returns>The converted value</returns>
        public static explicit operator string(Sexp s)
        {
            return s.AsString;
        }

        /// <summary>
        /// Gets as Strings.
        /// </summary>
        public virtual string[] AsStrings
        {
            get
            {
                return this.Select<Sexp, string>(a => a.AsString).ToArray();
            }
        }

        /// <summary>
        /// Syntactic sugar for explicit conversion to string[]
        /// </summary>
        /// <param name="s">The Sexp</param>
        /// <returns>The converted value</returns>
        public static explicit operator string[](Sexp s)
        {
            return s.AsStrings;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public Dictionary<string, Sexp> Attributes
        {
            get
            {
                return this.attributes ?? (this.attributes = new Dictionary<string, Sexp>());
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public virtual int Count
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is NA.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is NA; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsNa
        {
            get
            {
                if (this.Count == 1)
                {
                    return this[0].IsNa;
                }

                throw new IndexOutOfRangeException("Only single values can be tested for NA.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is null.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is null; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsNull
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public virtual bool IsReadOnly
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public virtual ICollection<string> Keys
        {
            get
            {
                return this.Names.AsStrings;
            }
        }

        /// <summary>
        /// Gets the names.
        /// </summary>
        public virtual Sexp Names
        {
            get
            {
                return this.Attributes["names"];
            }
        }

        /// <summary>
        /// Gets the rank. E.g., a plain list has a rank of 1 and a matrix has a rank of 2.
        /// </summary>
        public virtual int Rank
        {
            get
            {
                return this.Attributes["dim"].Count;
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public virtual ICollection<Sexp> Values
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        ICollection<object> IDictionary<string, object>.Values
        {
            get
            {
                return (ICollection<object>)this.Values;
            }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="row">The zero-based row index.</param>
        /// <param name="col">The zero-based column index.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public virtual Sexp this[int row, int col]
        {
            get
            {
                if (this.Rank != 2)
                {
                    throw new ArithmeticException("Only Sexps of Rank 2 can be accessed as arrays.");
                }

                return this[(col * this.GetLength(0)) + row];
            }

            set
            {
                if (this.Rank != 2)
                {
                    throw new ArithmeticException("Only objects of rank 2 can be accessed as matrices.");
                }

                this[(col * this.GetLength(0)) + row] = value;
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public virtual Sexp this[int index]
        {
            get
            {
                throw new NotSupportedException();
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="key">The name of the value.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        public virtual Sexp this[string key]
        {
            get
            {
                var index = Array.IndexOf(this.Names.AsStrings, key);
                if (index < 0)
                {
                    throw new KeyNotFoundException("Could not find key '" + key + "' in names.");
                }

                return this[index];
            }

            set
            {
                var index = Array.IndexOf(this.Names.AsStrings, key);
                if (index < 0)
                {
                    this.Add(key, value);
                }
                else
                {
                    this[index] = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        object IList<object>.this[int index]
        {
            get
            {
                return this[index];
            }

            set
            {
                this[index] = Make(value);
            }
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="key">The name of the element.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        object IDictionary<string, object>.this[string key]
        {
            get
            {
                return this[key];
            }

            set
            {
                this[key] = Make(value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="x">
        /// The object to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(object x)
        {
            if (x is Sexp)
            {
                return (Sexp)x;
            }

            if (x is bool)
            {
                return Make((bool)x);
            }

            if (x is double)
            {
                return Make((double)x);
            }

            if (x is IEnumerable<double>)
            {
                return Make((IEnumerable<double>)x);
            }

            if (x is double[,])
            {
                return Make((double[,])x);
            }

            if (x is int)
            {
                return Make((int)x);
            }

            if (x is int[,])
            {
                return Make((int[,])x);
            }

            if (x is IEnumerable<int>)
            {
                return Make((IEnumerable<int>)x);
            }

            if (x is string)
            {
                return Make((string)x);
            }

            if (x is IEnumerable<string>)
            {
                return Make((IEnumerable<string>)x);
            }

            if (x is IDictionary<string, object>)
            {
                return Make((IDictionary<string, object>)x);
            }

            throw new ArgumentException(
                "I don't have an automatic conversion rule for type " + x.GetType().Name + " to Sexp.");
        }

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="x">
        /// The object to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(bool x)
        {
            return new SexpBool(x);
        }

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="x">
        /// The object to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(int x)
        {
            return new SexpInt(x);
        }

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="xs">
        /// The objects to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(IEnumerable<int> xs)
        {
            return new SexpArrayInt(xs);
        }

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="x">
        /// The object to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(double x)
        {
            return new SexpDouble(x);
        }

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="xs">
        /// The objects to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(IEnumerable<double> xs)
        {
            return new SexpArrayDouble(xs);
        }

        /// <summary>
        /// Makes a mathrix Sexp from a native matrix.
        /// </summary>
        /// <param name="xs">
        /// The native matrix.
        /// </param>
        /// <returns>
        /// The Sexp matrix.
        /// </returns>
        public static Sexp Make(double[,] xs)
        {
            var rows = xs.GetLength(0);
            var cols = xs.GetLength(1);
            var fortranXs = new double[rows * cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    fortranXs[(col * rows) + row] = xs[row, col];
                }
            }

            var res = new SexpArrayDouble(fortranXs);
            res.Attributes.Add("dim", Make(new[] { rows, cols }));
            return res;
        }

        /// <summary>
        /// Makes a mathrix Sexp from a native matrix.
        /// </summary>
        /// <param name="xs">
        /// The native matrix.
        /// </param>
        /// <returns>
        /// The Sexp matrix.
        /// </returns>
        public static Sexp Make(int[,] xs)
        {
            var rows = xs.GetLength(0);
            var cols = xs.GetLength(1);
            var fortranXs = new int[rows * cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    fortranXs[(col * rows) + row] = xs[row, col];
                }
            }

            var res = new SexpArrayInt(fortranXs);
            res.Attributes.Add("dim", Make(new[] { rows, cols }));
            return res;
        }

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="x">
        /// The object to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(string x)
        {
            return new SexpString(x);
        }

        /// <summary>
        /// Makes a Sexp from an object.
        /// </summary>
        /// <param name="xs">
        /// The objects to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(IEnumerable<string> xs)
        {
            return new SexpArrayString(xs);
        }

        /// <summary>
        /// Makes a Sexp from a Dictionary.
        /// </summary>
        /// <param name="xs">
        /// The objects to convert into an Sexp.
        /// </param>
        /// <returns>
        /// The Sexp made.
        /// </returns>
        public static Sexp Make(IDictionary<string, object> xs)
        {
            var res = new SexpList();
            foreach (var a in xs)
            {
                res.Add(a);
            }

            return res;
        }

        /// <summary>
        /// Makes a data frame.
        /// </summary>
        /// <param name="columns">
        /// The columns.
        /// </param>
        /// <param name="rowNames">
        /// The row names.
        /// </param>
        /// <returns>
        /// Sexp of data frame
        /// </returns>
        public static SexpList MakeDataFrame(
            IEnumerable<KeyValuePair<string, object>> columns = null, IEnumerable<string> rowNames = null)
        {
            var res = new SexpList();
            res.Attributes["class"] = new SexpString("data.frame");
            res.Attributes["names"] = new SexpArrayString();
            if (columns != null)
            {
                foreach (var col in columns)
                {
                    res.Attributes["names"].Add(new SexpString(col.Key));
                    res.Add(Make(col.Value));
                }

                if (rowNames != null)
                {
                    res.Attributes["row.names"] = new SexpArrayString(rowNames);
                }
            }

            return res;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="System.Object"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            throw new NotSupportedException("Don't have an equality override for type" + this.GetType());
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Gets the length of an array-like object with respect to a given dimension.
        /// </summary>
        /// <param name="dim">
        /// The zero-based index of the dimension.
        /// </param>
        /// <returns>
        /// Length of the object in the dimension requested.
        /// </returns>
        public virtual int GetLength(int dim)
        {
            return this.Attributes["dim"][dim].AsInt;
        }

        /// <summary>
        /// Converts the Sexp into the most appropriate native representation. Use with caution--this is more a rapid prototyping than
        /// a production feature.
        /// </summary>
        /// <returns>
        /// A CLI native representation of the Sexp
        /// </returns>
        public virtual object ToNative()
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Implemented Interfaces

        #region ICollection<KeyValuePair<string,object>>

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(new KeyValuePair<string, Sexp>(item.Key, Make(item.Value)));
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.Contains(new KeyValuePair<string, Sexp>(item.Key, Make(item.Value)));
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// Index of the array.
        /// </param>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.Remove(new KeyValuePair<string, Sexp>(item.Key, Make(item.Value)));
        }

        #endregion

        #region ICollection<KeyValuePair<string,Sexp>>

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public virtual void Add(KeyValuePair<string, Sexp> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public virtual bool Contains(KeyValuePair<string, Sexp> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// Index of the array.
        /// </param>
        public virtual void CopyTo(KeyValuePair<string, Sexp>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public virtual bool Remove(KeyValuePair<string, Sexp> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICollection<object>

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Add(object item)
        {
            this.Add(Make(item));
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(object item)
        {
            return this.Contains(Make(item));
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// Index of the array.
        /// </param>
        public void CopyTo(object[] array, int arrayIndex)
        {
            this.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public bool Remove(object item)
        {
            return this.Remove(Make(item));
        }

        #endregion

        #region ICollection<Sexp>

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        public virtual void Add(Sexp item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public virtual void Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public virtual bool Contains(Sexp item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// Index of the array.
        /// </param>
        public virtual void CopyTo(Sexp[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public virtual bool Remove(Sexp item)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IDictionary<string,object>

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">
        /// The object to use as the key of the element to add.
        /// </param>
        /// <param name="value">
        /// The object to use as the value of the element to add.
        /// </param>
        public void Add(string key, object value)
        {
            this.Add(key, Make(value));
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key whose value to get.
        /// </param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        public bool TryGetValue(string key, out object value)
        {
            return this.TryGetValue(key, out value);
        }

        #endregion

        #region IDictionary<string,Sexp>

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">
        /// The object to use as the key of the element to add.
        /// </param>
        /// <param name="value">
        /// The object to use as the value of the element to add.
        /// </param>
        public virtual void Add(string key, Sexp value)
        {
            if (this.Count == 0 && !this.Attributes.ContainsKey("names"))
            {
                this.Attributes["names"] = new SexpArrayString();
            }

            this.Add(value);
            this.Names.Add(new SexpString(key));
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </param>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the key; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        public virtual bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">
        /// The key of the element to remove.
        /// </param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key"/> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public virtual bool Remove(string key)
        {
            if (this.Count == 0)
            {
                return false;
            }

            var index = Array.IndexOf(this.Names.AsStrings, key);
            if (index < 0)
            {
                return false;
            }

            this.RemoveAt(index);
            this.Names.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key whose value to get.
        /// </param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        public virtual bool TryGetValue(string key, out Sexp value)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return
                (IEnumerator<KeyValuePair<string, object>>)
                ((IEnumerable<KeyValuePair<string, Sexp>>)this).GetEnumerator();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,Sexp>>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<KeyValuePair<string, Sexp>> IEnumerable<KeyValuePair<string, Sexp>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<object>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<object> IEnumerable<object>.GetEnumerator()
        {
            return (IEnumerator<object>)this.GetEnumerator();
        }

        #endregion

        #region IEnumerable<Sexp>

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public virtual IEnumerator<Sexp> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IList<object>

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(object item)
        {
            return this.IndexOf(Make(item));
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which <paramref name="item"/> should be inserted.
        /// </param>
        /// <param name="item">
        /// The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        public void Insert(int index, object item)
        {
            this.Insert(index, Make(item));
        }

        #endregion

        #region IList<Sexp>

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public virtual int IndexOf(Sexp item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index at which <paramref name="item"/> should be inserted.
        /// </param>
        /// <param name="item">
        /// The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </param>
        public virtual void Insert(int index, Sexp item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">
        /// The zero-based index of the item to remove.
        /// </param>
        public virtual void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        #endregion

        #endregion
    }
}