using DlibDotNet;
using System;
using System.Collections.Generic;

namespace FaceRecognitionDotNet
{

    /// <summary>
    /// 描述人脸的左、上、右、下位置
    /// </summary>
    public sealed class Location : IEquatable<Location>
    {

        #region Constructors

        /// <summary>
        /// 初始化<see cref="Location"/> 结构，具有指定的左、上、右和下.
        /// </summary>
        /// <param name="left">左边距</param>
        /// <param name="top">顶边距</param>
        /// <param name="right">右边距</param>
        /// <param name="bottom">底边距</param>
        public Location(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        internal Location(Rectangle rect) :
            this(rect.Left, rect.Top, rect.Right, rect.Bottom)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取面矩形底部的y轴值。
        /// </summary>
        public int Bottom
        {
            get;
        }

        /// <summary>
        /// 获取面矩形左侧的x轴值。
        /// </summary>
        public int Left
        {
            get;
        }

        /// <summary>
        /// 获取面矩形右侧的x轴值。
        /// </summary>
        public int Right
        {
            get;
        }

        /// <summary>
        /// 获取面矩形顶部的y轴值。


        /// </summary>
        public int Top
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 比较两个 <see cref="Location"/> 是否相同。
        /// </summary>
        /// <param name="other">The face location to compare to this instance.</param>
        /// <returns><code>true</code> if both <see cref="Location"/> class contain the same <see cref="Left"/>, <see cref="Top"/>, <see cref="Right"/> and <see cref="Bottom"/> values; otherwise, <code>false</code>.</returns>
        public bool Equals(Location other)
        {
            return other != null &&
                   this.Bottom == other.Bottom &&
                   this.Left == other.Left &&
                   this.Right == other.Right &&
                   this.Top == other.Top;
        }

        #region Overrids

        /// <summary>
        /// Determines whether the specified <see cref="Object"/> is a <see cref="Location"/> and whether it contains the same face location as this <see cref="Location"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="Location"/> and contains the same <see cref="Left"/>, <see cref="Top"/>, <see cref="Right"/> and <see cref="Bottom"/> values as this <see cref="Location"/>; otherwise, <code>false</code>.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Location);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Location"/>.
        /// </summary>
        /// <returns>The hash code for this <see cref="Location"/> class.</returns>
        public override int GetHashCode()
        {
            var hashCode = -773114317;
            hashCode = hashCode * -1521134295 + this.Bottom.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Left.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Right.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Top.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two <see cref="Location"/> class for equality.
        /// </summary>
        /// <param name="location1">The first <see cref="Location"/> class to compare.</param>
        /// <param name="location2">The second <see cref="Location"/> class to compare.</param>
        /// <returns><code>true</code> if both the <see cref="Left"/>, <see cref="Top"/>, <see cref="Right"/> and <see cref="Bottom"/> face location of <paramref name="location1"/> and <paramref name="location2"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(Location location1, Location location2)
        {
            return EqualityComparer<Location>.Default.Equals(location1, location2);
        }

        /// <summary>
        /// Compares two <see cref="Location"/> class for inequality.
        /// </summary>
        /// <param name="location1">The first <see cref="Location"/> class to compare.</param>
        /// <param name="location2">The second <see cref="Location"/> class to compare.</param>
        /// <returns><code>true</code> if <paramref name="location1"/> and <paramref name="location2"/> have different <see cref="Left"/>, <see cref="Top"/>, <see cref="Right"/> or <see cref="Bottom"/> coordinates; <code>false</code> if <paramref name="location1"/> and <paramref name="location2"/> have the same <see cref="Left"/>, <see cref="Top"/>, <see cref="Right"/> and <see cref="Bottom"/> face location.</returns>
        public static bool operator !=(Location location1, Location location2)
        {
            return !(location1 == location2);
        }

        #endregion

        #endregion

    }

}
