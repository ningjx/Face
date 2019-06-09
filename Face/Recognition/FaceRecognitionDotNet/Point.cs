using System;

namespace FaceRecognitionDotNet
{

    /// <summary>
    ///表示整数x和y坐标的有序对，它定义二维平面上的一个点。
    /// </summary>
    public struct Point : IEquatable<Point>
    {

        #region Constructors

        /// <summary>
        /// 实例初始化 <see cref="Point"/> 使用指定的坐标构造
        /// </summary>
        /// <param name="x">点的水平位置。</param>
        /// <param name="y">点的垂直位置。.</param>
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        internal Point(DlibDotNet.Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 得到这个的x坐标<see cref="Point"/>.
        /// </summary>
        public int X
        {
            get;
        }

        /// <summary>
        /// 得到这个的y坐标 <see cref="Point"/>.
        /// </summary>
        public int Y
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares two <see cref="Point"/> structures for equality.
        /// </summary>
        /// <param name="other">The point to compare to this instance.</param>
        /// <returns><code>true</code> if both <see cref="Point"/> structures contain the same <see cref="X"/> and <see cref="Y"/> values; otherwise, <code>false</code>.</returns>
        public bool Equals(Point other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        #region Overrids

        /// <summary>
        /// Determines whether the specified <see cref="Object"/> is a <see cref="Point"/> and whether it contains the same coordinates as this <see cref="Point"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="Point"/> and contains the same <see cref="X"/> and <see cref="Y"/> values as this <see cref="Point"/>; otherwise, <code>false</code>.</returns>
        public override bool Equals(object obj)
        {
            return obj is Point && Equals((Point)obj);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Point"/>.
        /// </summary>
        /// <returns>The hash code for this <see cref="Point"/> structure.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two <see cref="Point"/> structures for equality.
        /// </summary>
        /// <param name="point1">The first <see cref="Point"/> structure to compare.</param>
        /// <param name="point2">The second <see cref="Point"/> structure to compare.</param>
        /// <returns><code>true</code> if both the <see cref="X"/> and <see cref="Y"/> coordinates of <paramref name="point1"/> and <paramref name="point2"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        /// <summary>
        /// Compares two <see cref="Point"/> structures for inequality.
        /// </summary>
        /// <param name="point1">The first <see cref="Point"/> structure to compare.</param>
        /// <param name="point2">The second <see cref="Point"/> structure to compare.</param>
        /// <returns><code>true</code> if <paramref name="point1"/> and <paramref name="point2"/> have different <see cref="X"/> or <see cref="Y"/> coordinates; <code>false</code> if <paramref name="point1"/> and <paramref name="point2"/> have the same <see cref="X"/> and <see cref="Y"/> coordinates.</returns>
        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }

        #endregion

        #endregion

    }

}
