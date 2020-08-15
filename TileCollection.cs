using System;
using System.Collections.Generic;

namespace AzulApp
{
    public class TileCollection : List<Color>
    {
        public TileCollection() : base() { }

        /**
         * Constructs a list containing the elements of the specified collection, in the order they are returned by the
         * collection's iterator.
         *
         * @param tc
         *            the collection whose elements are to be placed into this list
         * @throws NullPointerException
         *            if the specified collection is null
         */
        public TileCollection(TileCollection tc) : base(tc) { }

        public bool Empty
        {
            get
            {
                return Count == 0;
            }
        }

        /**
         * Removes the element at the specified position in this list.
         *
         * @param index
         *          the index of the element to be removed
         * @return the element that was removed from the list
         */
        private Color remove(int index)
        {
            Color result = this[index];
            RemoveAt(index);
            return result;
        }

        /**
         * A convenience method for mass-adding tiles of a specific Color.
         *
         * @param color
         *            the Color of tiles to add
         * @param count
         *            the number of tiles to add
         */
        public void addTiles(Color? color, int count)
        {
            if (count > 0 && color == null)
                throw new ArgumentException("Cannot add null tiles.");
            for (int i = 0; i < count; ++i)
            {
                Add((Color)color);
            }
        }

        /**
         * Selects the specified number of tiles (Color objects) randomly from this collection. If the number of tiles to
         * draw is larger than the size of this collection, then only as many tiles as are in this collection are drawn. The
         * selected tiles are removed from this collection.
         *
         * @param num
         *            the number of tiles to draw
         * @return a new TileCollection containing the drawn tiles
         * @throws IllegalArgumentException
         *             if the parameter is negative
         */
        public TileCollection drawTiles(int num)
        {
            if (num < 0)
                throw new ArgumentOutOfRangeException("num", "Cannot draw negative tiles.");
            TileCollection drawn = new TileCollection();
            if (num > Count)
                num = Count;
            for (int i = 0; i < num; ++i)
            {
                drawn.Add(remove((int)(Utils.rand() * Count)));
            }
            return drawn;
        }

        /**
         * Returns true if this collection contains Color objects that all have the same value.
         *
         * @return true if this collection contains Color objects that all have the same value.
         */
        public bool isAllOneColor()
        {
            if (Count == 0)
                return false;
            Color c = this[0];
            for (int i = 1; i < Count; ++i)
            {
                if (this[i] != c)
                    return false;
            }
            return true;
        }

        /**
         * Returns the color of tiles in this collection if it is all one color, null otherwise
         *
         * @return a <tt>Color</tt> object equal to those in this collection, or <tt>null</tt> if not all one color
         */
        public Color? getColor()
        {
            if (isAllOneColor())
                return this[0];
            return null;
        }

        /**
         * Returns the color of tiles in this collection if it is all one color, null otherwise; ignores WHITE tiles
         *
         * @return a <tt>Color</tt> object equal to those in this collection, or <tt>null</tt> if not all one color
         */
        public Color? getColorIgnoreWhite()
        {
            TileCollection temp = new TileCollection(this);
            temp.removeTilesOfColor(Color.WHITE);
            return temp.getColor();
        }

        /**
         * Removes the tiles of the specified Color from this collection, and returns them in a new collection.
         *
         * @param c
         *            the Color of tiles to remove
         * @return the removed tiles
         */
        public TileCollection removeTilesOfColor(Color c)
        {
            TileCollection tc = new TileCollection();
            for (int i = 0; i < Count; ++i)
            {
                if (this[i] == c)
                {
                    tc.Add(remove(i--)); // '--' here because we removed an element
                }
            }
            return tc;
        }

        public string ToLongString()
        {
            string result = "";
            foreach (Color c in this)
            {
                result += c + ", ";
            }
            return result;
        }
    }
}
