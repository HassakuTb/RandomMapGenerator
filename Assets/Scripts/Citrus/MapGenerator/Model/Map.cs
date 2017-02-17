using UnityEngine;

namespace Citrus.MapGenerator.Model {

    public class Map{

        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        public Cell[,] cells { get; private set; }

        /// <summary>
        /// construct map.
        /// </summary>
        /// <param name="sizeX">require : sizeX > 0</param>
        /// <param name="sizeY">require : sizeX > 0</param>
        Map(int sizeX, int sizeY) {
            SizeX = sizeX;
            SizeY = sizeY;

            cells = new Cell[sizeX, sizeY];
        }
    }
}
