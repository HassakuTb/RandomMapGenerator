using UnityEngine;

namespace Citrus.MapGenerator.Model {

    /// <summary>
    /// a part of Map.
    /// </summary>
    public class Cell{

        /// <summary>
        /// this cell is wall? .
        /// </summary>
        public bool IsWall { get; private set; }

        Cell(bool isWall) {
            IsWall = isWall;
        }
    }

}