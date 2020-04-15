using AlbionFastOverlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbionFastOverlay
{
    internal static class OverrideHelper
    {
        public static int HashCodes(params int[] hashCodes)
        {
            if (hashCodes == null) throw new ArgumentNullException(nameof(hashCodes));
            if (hashCodes.Length == 0) throw new ArgumentOutOfRangeException(nameof(hashCodes));

            unchecked
            {
                int hash = 17;

                foreach (int code in hashCodes)
                {
                    hash = (hash * 23) + code;
                }

                return hash;
            }
        }

        public static string ToString(params string[] strings)
        {
            if (strings == null) throw new ArgumentNullException(nameof(strings));
            if (strings.Length == 0 || strings.Length % 2 != 0) throw new ArgumentOutOfRangeException(nameof(strings));

            StringBuilder sb = new StringBuilder(16);

            sb.Append("{ ");

            for (int i = 0; i < strings.Length - 1; i += 2)
            {
                string name = strings[i];
                string value = strings[i + 1];

                if (name == null)
                {
                    if (value == null)
                    {
                        sb.Append("null");
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }
                else if (value == null)
                {
                    sb.Append(name).Append(": null");
                }
                else
                {
                    sb.Append(name).Append(": ").Append(value);
                }

                sb.Append(", ");
            }

            sb.Length -= 2;

            sb.Append(" }");

            return sb.ToString();
        }
    }
}
    public struct Rect
    {
        /// <summary>
        /// The x-coordinate of the upper-left corner of the Rectangle.
        /// </summary>
        public int Left;

        /// <summary>
        /// The y-coordinate of the upper-left corner of the Rectangle.
        /// </summary>
        public int Top;

        /// <summary>
        /// The x-coordinate of the bottom-right corner of the Rectangle.
        /// </summary>
        public int Right;

        /// <summary>
        /// The y-coordinate of the bottom-right corner of the Rectangle.
        /// </summary>
        public int Bottom;

        /// <summary>
        /// Gets the width of this Rectangle.
        /// </summary>
        public int Width => Right - Left;

        /// <summary>
        /// Gets the height of this Rectangle.
        /// </summary>
        public int Height => Bottom - Top;

        /// <summary>
        /// Initializes a new Rectangle using the given coordinates.
        /// </summary>
        /// <param name="left">The x-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="top">The y-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="right">The x-coordinate of the bottom-right corner of the Rectangle.</param>
        /// <param name="bottom">The y-coordinate of the bottom-right corner of the Rectangle.</param>
        public Rect(float left, float top, float right, float bottom)
        {
            Left = (int)left;
            Top = (int)top;
            Right = (int)right;
            Bottom = (int)bottom;
        }

        /// <summary>
        /// Initializes a new Rectangle using the given coordinates.
        /// </summary>
        /// <param name="left">The x-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="top">The y-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="right">The x-coordinate of the bottom-right corner of the Rectangle.</param>
        /// <param name="bottom">The y-coordinate of the bottom-right corner of the Rectangle.</param>
        public Rect(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Creates a new Rectangle structure using the given dimension.
        /// </summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>The Rectangle this method creates.</returns>
        public static Rect Create(float x, float y, float width, float height)
        {
            return new Rect(x, y, x + width, y + height);
        }

        /// <summary>
        /// Creates a new Rectangle structure using the given dimension.
        /// </summary>
        /// <param name="x">The x-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="y">The y-coordinate of the upper-left corner of the Rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle</param>
        /// <returns>The Rectangle this method creates.</returns>
        public static Rect Create(int x, int y, int width, int height)
        {
            return new Rect(x, y, x + width, y + height);
        }

        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="T:System.Object" /> represent the same type and value.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true" /> if <paramref name="obj" /> is a Rectangle and equal to this instance; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Rect value)
            {
                return value.Left == Left
                    && value.Top == Top
                    && value.Right == Right
                    && value.Bottom == Bottom;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return OverrideHelper.HashCodes(
                Left.GetHashCode(),
                Top.GetHashCode(),
                Right.GetHashCode(),
                Bottom.GetHashCode());
        }

        /// <summary>
        /// Converts this Rectangle structure to a human-readable string.
        /// </summary>
        /// <returns>A string representation of this Rectangle.</returns>
        public override string ToString()
        {
            return OverrideHelper.ToString(
                "Left", Left.ToString(),
                "Top", Top.ToString(),
                "Right", Right.ToString(),
                "Bottom", Bottom.ToString());
        }

        /// <summary>
        /// Converts a Rectangle structure to a SharpDX RawRectangleF.
        /// </summary>
        /// <param name="rectangle">A Rectangle structure.</param>
       

        /// <summary>
        /// Determines whether two specified instances are equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> represent the same value; otherwise, <see langword="false" />.</returns>
        public static bool operator ==(Rect left, Rect right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified instances are not equal.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> do not represent the same value; otherwise, <see langword="false" />.</returns>
        public static bool operator !=(Rect left, Rect right)
        {
            return !left.Equals(right);
        }
    }
    
