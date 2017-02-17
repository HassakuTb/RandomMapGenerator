using UnityEngine;
using System.Collections.Generic;

namespace Citrus.MapGenerator.Model {
    public class NaturalMapGenerator : Generator{

        public class Builder {
            public int SizeX { get; private set; }
            public int SizeY { get; private set; }
            public float Liquidness { get; private set; }
            public int SeedValue { get; private set; }
            public int ControlPointMergineValue { get; private set; }

            public Builder(){
                SizeX = 150;
                SizeY = 120;
                Liquidness = 20.0f;
                ControlPointMergineValue = 5;
            }
            
            /// <summary>
            /// set map size.
            /// default : 150, 120
            /// </summary>
            /// <param name="x">require > 10</param>
            /// <param name="y">require > 10</param>
            /// <returns></returns>
            public Builder MapSize(int x, int y) {
                SizeX = x;
                SizeY = y;
                return this;
            }

            /// <summary>
            /// set liquidness of noise wave.
            /// default : 20.0f
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public Builder PerlinNoiseLiquidness(float liquidness) {
                Liquidness = liquidness;
                return this;
            }

            /// <summary>
            /// set random seed.
            /// default : 0
            /// </summary>
            /// <param name="seed"></param>
            /// <returns></returns>
            public Builder Seed(int seed) {
                SeedValue = seed;
                return this;
            }

            /// <summary>
            /// mergine for control point filter.
            /// defaule : 5
            /// </summary>
            /// <param name="controlPointMergine"></param>
            /// <returns></returns>
            public Builder ControlPointMergine(int controlPointMergine) {
                ControlPointMergineValue = controlPointMergine;
                return this;
            }

            /// <summary>
            /// build generator.
            /// </summary>
            /// <returns></returns>
            public NaturalMapGenerator Build(){
                return new NaturalMapGenerator(this);
            }
        }

        private struct Location{
            public int X{ get; set; }
            public int Y { get; set; }

            /// <summary>
            /// Is location in bound of map?
            /// </summary>
            /// <param name="l"></param>
            /// <returns></returns>
            private bool IsInBound(int mapX, int mapY) {
                if (X < 0) return false;
                if (Y < 0) return false;
                if (X >= mapX) return false;
                if (Y >= mapY) return false;
                return true;
            }
        }

        private int SizeX { get; set; }
        private int SizeY { get; set; }
        private float Liquidness { get; set; }
        private int Seed { get; set; }
        private int ControlPointMergine { get; set; }

        private float[,] Depths { get; set; }
        private List<Location> ControlPoints { get; set; }

        

        private NaturalMapGenerator(Builder builder){
            SizeX = builder.SizeX;
            SizeY = builder.SizeY;
            Liquidness = builder.Liquidness;
            Seed = builder.SeedValue;
            ControlPointMergine = builder.ControlPointMergineValue;
        }

        private void GenerateDepthMap() {
            Depths = new float[SizeX, SizeY];
            float origX = Random.value * 1000.0f;
            float origY = Random.value * 1000.0f;

            for (int y = 0; y < SizeY; ++y) {
                for (int x = 0; x < SizeX; ++x) {
                    float sampleX = origX + (float)x / Liquidness;
                    float sampleY = origY + (float)y / Liquidness;
                    Depths[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
                }
            }
        }

        private bool IsLocalMinimum(Location l) {
            if (Depths[l.X - 1, l.Y - 1] > Depths[l.X, l.Y]) return false;
            if (Depths[l.X + 0, l.Y - 1] > Depths[l.X, l.Y]) return false;
            if (Depths[l.X + 1, l.Y - 1] > Depths[l.X, l.Y]) return false;
            if (Depths[l.X - 1, l.Y + 0] > Depths[l.X, l.Y]) return false;
            if (Depths[l.X + 1, l.Y + 0] > Depths[l.X, l.Y]) return false;
            if (Depths[l.X - 1, l.Y + 1] > Depths[l.X, l.Y]) return false;
            if (Depths[l.X + 0, l.Y + 1] > Depths[l.X, l.Y]) return false;
            if (Depths[l.X + 1, l.Y + 1] > Depths[l.X, l.Y]) return false;
            return true;
        }

        private void ExtractContolPoints() {
            ControlPoints = new List<Location>();

            for (int x = ControlPointMergine; x < SizeX - ControlPointMergine; ++x) {
                for (int y = ControlPointMergine; y < SizeY - ControlPointMergine; ++y) {
                    Location loc = new Location() { X = x, Y = y };
                    if (IsLocalMinimum(loc)) ControlPoints.Add(loc); 
                }
            }
        }

        private void ExtractPath() {

        }

        public Map Generate() {
        }

    }
}
