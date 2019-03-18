using System;
using System.Collections;
using System.Collections.Generic;

namespace Extensions
{
    public static class Extensions
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (TSource item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }
        public static double Average(this IEnumerable<double> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            double sum = 0;
            long count = 0;
            foreach (double item in source)
            {
                checked
                {
                    sum += item;
                    ++count;
                }
            }
            if (count != 0)
                return sum / count;
            throw new InvalidOperationException();
        }
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            long count = 0;
            double sum = 0;
            foreach (TSource item in source)
            {
                checked
                {
                    count++;
                    sum += selector(item);
                }
            }
            if (count != 0)
                return sum / count;
            throw new InvalidOperationException();
        }
        #region Cast
        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is IEnumerable<TResult>)
            {
                return source as IEnumerable<TResult>;
            }

            return CastReturn<TResult>(source);
        }
        private static IEnumerable<TResult> CastReturn<TResult>(IEnumerable source)
        {
            foreach (TResult result in source)
                yield return result;
        }
        #endregion
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            foreach (TSource source in first)
                yield return source;
            foreach (TSource source in second)
                yield return source;
        }
        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            int count = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    checked
                    {
                        ++count;
                    }
                }
            }
            return count;
        }
        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            int count = 0;
            foreach (TSource item in source)
            {
                checked
                {
                    ++count;
                }
            }
            return count;
        }
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return source.DefaultIfEmpty(default(TSource));
        }
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            int count = 0;
            foreach (var item in source)
            {
                ++count;
                yield return item;
                if (count == 0)
                {
                    yield return defaultValue;
                }
            }
        }
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (source is IList<TSource> temp)
                return temp[index];
            foreach (TSource item in source)
            {
                if (index == 0)
                    return item;
                --index;
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (index < 0)
                return default(TSource);
            if (source is IList<TSource> temp)
            {
                if (index < temp.Count)
                    return temp[index];
            }
            else
            {
                foreach (TSource item in source)
                {
                    if (index == 0)
                        return item;
                    --index;
                }
            }
            return default(TSource);
        }
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return new TResult[0];
        }
        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            throw new InvalidOperationException("No Elements");
        }
        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source is IList<TSource> sourceList)
                return sourceList[0];
            else
            {
                foreach (TSource item in source)
                {
                    return item;
                }
            }
            throw new InvalidOperationException("No Elements");
        }
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            try
            {
                return First(source, predicate);
            }
            catch (InvalidOperationException)
            {
                return default(TSource);
            }
        }
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            try
            {
                return First(source);
            }
            catch (InvalidOperationException)
            {
                return default(TSource);
            }
        }
        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            TSource temp = default(TSource);
            bool flag = false;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    temp = item;
                    flag = true;
                }
            }
            if (flag)
                return temp;
            throw new InvalidOperationException("No Elements");
        }
        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source is IList<TSource> sourceList)
            {
                int count = sourceList.Count;
                if (count > 0)
                    return sourceList[count - 1];
            }
            else
            {
                TSource tempSource = default(TSource);
                long count = 0;
                foreach (TSource item in source)
                {
                    tempSource = item;
                    ++count;
                }
                if (count != 0)
                    return tempSource;
            }
            throw new InvalidOperationException("No Elements");
        }
        public static int Max(this IEnumerable<int> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            int max = Int32.MinValue;
            bool flag = false;
            foreach (int item in source)
            {
                if (flag)
                {
                    if (item > max)
                        max = item;
                }
                else
                {
                    max = item;
                    flag = true;
                }
            }
            if (flag)
                return max;
            throw new InvalidOperationException("No Elements");
        }
        public static int Min(this IEnumerable<int> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            int min = 0;
            bool flag = false;
            foreach (int item in source)
            {
                if (flag)
                {
                    if (item < min)
                        min = item;
                }
                else
                {
                    min = item;
                    flag = true;
                }
            }
            if (flag)
                return min;
            throw new InvalidOperationException("No Elements");
        }
        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (object item in source)
            {
                if (item is TResult)
                    yield return (TResult)item;
            }
        }
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0 || (long)(start + count - 1) > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(count));
            for (int i = 0; i < count; i++)
                yield return start + i;
        }
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            for (int i = 0; i < count; i++)
                yield return element;
        }
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));
            TSource temp = default(TSource);
            long count = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    temp = item;
                    checked
                    {
                        ++count;
                    }
                }
            }
            if (count == 0)
                throw new InvalidOperationException("No Elements");
            if (count == 1)
                return temp;
            throw new InvalidOperationException("More Than One Match");

        }
        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (source is IList<TSource> sourceList)
            {
                switch (sourceList.Count)
                {
                    case 0:
                        throw new InvalidOperationException("No Elements");
                    case 1:
                        return sourceList[0];
                }
            }
            else
            {
                int count = 0;
                TSource temp = default(TSource);
                foreach (TSource item in source)
                {
                    ++count;
                    temp = item;
                }
                if (count == 0)
                {
                    throw new InvalidOperationException("No Elements");
                }
                if (count == 1)
                    return temp;
            }
            throw new InvalidOperationException("More Than One Match");
        }
        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            foreach (TSource item in source)
            {
                --count;
                if (count <= 0)
                    yield return item;
            }
        }
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            first = first.Distinct<TSource>();

            foreach (TSource t in first)
            {
                if (!second.Contains(t))
                    yield return t;
            }



        }
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));

            first = first.Distinct<TSource>(comparer);

            foreach (TSource t in first)
            {
                if (!second.Contains(t, comparer))

                    yield return t;
            }
        }
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> first)
        {
            DynamicArray<TSource> array = new DynamicArray<TSource>();

            foreach (TSource t in first)
            {
                array.Add(t);
            }
            return array.DistinctIterator();

        }
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            DynamicArray<TSource> array = new DynamicArray<TSource>(comparer);

            foreach (TSource t in source)
            {
                array.Add(t);

            }
            return array.DistinctIterator();
        }
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            if (source != null)
            {
                foreach (TSource item in source)
                {
                    if (item.Equals(value))
                        return true;
                }
                return false;
            }
            else throw new ArgumentNullException();
        }
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            if (source != null)
            {
                foreach (TSource item in source)
                {
                    if (comparer.Equals(item, value))
                        return true;
                }
                return false;
            }
            else throw new ArgumentNullException();
        }
    }
    class DynamicArray<T>
    {
        T[] InnerArray;
        IEqualityComparer<T> comparer;
        int count;
        public T this[int index] { get => InnerArray[index]; set => InnerArray[index] = value; }
        public int Count => count;
        public DynamicArray(IEqualityComparer<T> p)
        {
            InnerArray = new T[4];
            comparer = p;
        }
        public DynamicArray() : this((IEqualityComparer<T>)null)
        {
        }
        public void Add(T item)
        {
            if (count >= InnerArray.Length)
            {
                T[] r1 = new T[2 * InnerArray.Length];
                InnerArray.CopyTo(r1, 0);
                InnerArray = r1;
                InnerArray[count++] = item;
            }
            else
            {
                InnerArray[count++] = item;
            }
        }
        public void Clear()
        {
            count = 0;
        }
        public bool Contains(T item)
        {
            bool List = false;
            for (int i = 0; i < Count; i++)
            {
                if (InnerArray[i].Equals(item))
                {
                    List = true;
                    break;
                }
            }
            return List;
        }
        public IEnumerable<T> DistinctIterator()
        {
            if (comparer == null)
                comparer = (IEqualityComparer<T>)EqualityComparer<T>.Default;
            for (int i = 0; i < count; i++)
            {
                int count = 0;
                for (int j = 0; j < i; j++)
                {
                    if (comparer.Equals(InnerArray[i], InnerArray[j]))
                    {
                        count++;
                    }
                }
                if (count < 1)
                    yield return InnerArray[i];
            }
        }
        public int IndexOf(T item)
        {
            if (Contains(item))
            {
                for (int i = 0; i < Count; i++)
                {
                    if (InnerArray[i].Equals(item))
                    {
                        return i;
                    }
                }
                return -1;
            }
            else
                return -1;
        }
        public void Insert(int index, T item)
        {
            if (index >= 0 && index < count)
            {
                if (count + 1 > InnerArray.Length)
                {
                    Add(item);
                    count--;
                }
                count++;
                for (int i = count; i > index; i--)
                {
                    InnerArray[i] = InnerArray[i - 1];
                }
                InnerArray[index] = item;
            }
        }
        public bool Remove(T item)
        {
            try
            {
                RemoveAt(IndexOf(item));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void RemoveAt(int index)
        {
            if (index < count && index >= 0)
            {
                for (int i = index; i < count - 1; i++)
                {
                    InnerArray[i] = InnerArray[i + 1];
                }
                count--;
            }
            else
            {
                throw new IndexOutOfRangeException("Error index");
            }
        }
        public void ShowOnConsole()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(InnerArray[i]);
            }
        }
    }
}
