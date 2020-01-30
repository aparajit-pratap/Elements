﻿using System;
using System.Collections.Generic;
using System.Linq;
using Elements.Geometry;
using Elements.MathUtils;

namespace Elements.Spatial
{
    /// <summary>
    /// Represents a 2-dimensional grid which can be subdivided 
    /// </summary>
    public class Grid2d
    {

        private void InitializeUV(Domain1d uDomain, Domain1d vDomain)
        {
            U = new Grid1d(uDomain);
            U.TopLevelGridChange += TopLevelGridChange;
            V = new Grid1d(vDomain);
            V.TopLevelGridChange += TopLevelGridChange;

        }

        private void TopLevelGridChange(Grid1d sender, EventArgs e)
        {
            Cells = new List<List<Grid2d>>();
            var uCells = U.IsSingleCell ? new List<Grid1d> { U } : U.Cells;
            var vCells = V.IsSingleCell ? new List<Grid1d> { V } : V.Cells;
            foreach (var uCell in uCells)
            {
                var column = new List<Grid2d>();
                foreach (var vCell in vCells)
                {
                    var newCell = new Grid2d(uCell.Domain, vCell.Domain);

                    // Map type name from U and V type names. In most cases this
                    // should only be one direction, so we inherit directly.
                    if (uCell.Type != null && vCell.Type != null)
                    {
                        newCell.Type = $"{uCell.Type} / {vCell.Type}";
                    }
                    else if (uCell.Type != null)
                    {
                        newCell.Type = uCell.Type;
                    }
                    else if (vCell.Type != null)
                    {
                        newCell.Type = vCell.Type;
                    }

                    newCell.fromGrid = fromGrid;
                    newCell.toGrid = toGrid;
                    newCell.boundariesInGridSpace = boundariesInGridSpace;
                    column.Add(newCell);
                }
                Cells.Add(column);
            }
        }

        /// <summary>
        /// An optional type designation for this cell.  
        /// </summary>
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// Construct a Grid2d with default domain (0,0) to (1,1)
        /// </summary>
        public Grid2d()
        {
            InitializeUV(new Domain1d(), new Domain1d());
        }

        /// <summary>
        /// Construct a 2d grid with two 1d domains
        /// </summary>
        /// <param name="uDomain">The domain along the U axis</param>
        /// <param name="vDomain">The domain along the V axis</param>
        private Grid2d(Domain1d uDomain, Domain1d vDomain)
        {
            InitializeUV(uDomain, vDomain);
        }


        /// <summary>
        /// Construct a Grid2d with specified dimensions for the U and V direction.
        /// </summary>
        /// <param name="uDimension">The size along the U axis</param>
        /// <param name="vDimension">The size along the V axis</param>
        public Grid2d(double uDimension, double vDimension)
        {
            InitializeUV(new Domain1d(0, uDimension), new Domain1d(0, vDimension));
        }

        /// <summary>
        /// Create a Grid2d from a polygon and optional Transform.
        /// If the plane is null or not supplied, the identity transform will be used for the grid origin and orientation.
        /// Currently only transforms parallel to the world XY are supported.
        /// The polygon's bounding box parallel to the supplied transform will be
        /// used as the grid extents. 
        /// </summary>
        /// <param name="boundary"></param>
        /// <param name="t">A transform representing the </param>
        public Grid2d(Polygon boundary, Transform t = null) : this(new Polygon[] { boundary }, t)
        {

        }

        /// <summary>
        /// Create a Grid2d from a list of boundary polygons and an optional transform.
        /// If the plane is null or not supplied, the identity transform will be used for the grid origin and orientation.
        /// Currently only transforms parallel to the world XY are supported.
        /// The polygons' bounding box parallel to the supplied transform will be
        /// used as the grid extents.
        /// </summary>
        /// <param name="boundaries"></param>
        /// <param name="t"></param>
        public Grid2d(IList<Polygon> boundaries, Transform t = null)
        {
            if (!t.ZAxis.IsParallelTo(Vector3.ZAxis))
            {
                throw new Exception("Currently transforms that are not parallel to the XY Plane are not supported.");
            }

            if (t == null)
            {
                t = new Transform();
            }

            toGrid = new Transform(t);
            t.Invert();
            fromGrid = new Transform(t);

            var transformedBoundaries = toGrid.OfPolygons(boundaries);
            boundariesInGridSpace = transformedBoundaries;
            var bbox = new BBox3(transformedBoundaries);

            InitializeUV(new Domain1d(bbox.Min.X, bbox.Max.X), new Domain1d(bbox.Min.Y, bbox.Max.Y));
        }

        /// <summary>
        /// Retrieve the grid cell (as a Grid1d) at a length along the U and V domains. 
        /// </summary>
        /// <param name="uPos">U position</param>
        /// <param name="vPos">V Position</param>
        /// <returns></returns>
        public Grid2d FindCellAtPosition(double uPos, double vPos)
        {
            (var uIndex, var vIndex) = FindCellIndexAtPosition(uPos, vPos);
            if (uIndex < 0 || vIndex < 0) return this;
            return Cells[uIndex][vIndex];
        }

        /// <summary>
        /// Retrieve the U and V indices of a given cell at a position in grid space.
        /// This is used to map between position and indices.
        /// </summary>
        /// <param name="uPos"></param>
        /// <param name="vPos"></param>
        /// <returns></returns>
        private (int u, int v) FindCellIndexAtPosition(double uPos, double vPos)
        {

            //TODO: Optimize for smarter retrieval — via indexing or something
            for (int u = 0; u < Cells.Count; u++)
            {
                if (Cells[u][0].U.Domain.Includes(uPos))
                {
                    for (int v = 0; v < Cells[u].Count; v++)
                    {
                        var cell = Cells[u][v];
                        if (cell.V.Domain.Includes(vPos))
                        {
                            return (u, v);
                        }

                    }

                }
            }
            return (-1, -1);
        }

        /// <summary>
        /// Get a list of all the top-level cells at a given u index.
        /// </summary>
        /// <param name="u">The u index</param>
        /// <returns>A list of the column of all cells with this u index.</returns>
        public List<Grid2d> GetColumnAtIndex(int u)
        {
            return Cells[u];
        }

        /// <summary>
        /// Get a list of all the top-level cells at a given v index.
        /// </summary>
        /// <param name="v">The v index</param>
        /// <returns>A list of the row of all cells with this v index.</returns>
        public List<Grid2d> GetRowAtIndex(int v)
        {
            return Cells.Select(c => c[v]).ToList();
        }

        /// <summary>
        /// Retrieve a single top-level cell at the specified [u,v] indices.
        /// </summary>
        /// <param name="u">The U index</param>
        /// <param name="v">The V index</param>
        /// <returns>The cell at these indices</returns>
        public Grid2d GetCellAtIndices(int u, int v)
        {
            return Cells[u][v];
        }

        /// <summary>
        /// Retrieve a single top-level cell at the specified [u,v] indices.
        /// </summary>
        /// <param name="u">The U index</param>
        /// <param name="v">The V index</param>
        /// <returns>The cell at these indices</returns>
        public Grid2d this[int u, int v]
        {
            get
            {
                return Cells[u][v];
            }
        }


        /// <summary>
        /// Child cells of this Grid. If null, this Grid is a complete cell with no subdivisions.
        /// </summary>
        public List<List<Grid2d>> Cells { get; private set; }

        /// <summary>
        /// A flat list of all the top-level cells in this grid. To get child cells as well, use Grid2d.GetCells() instead.
        /// </summary>
        public List<Grid2d> CellsFlat
        {
            get
            {
                return Cells.SelectMany(c => c).ToList();
            }
        }

        /// <summary>
        /// Recursively retrieve all bottom-level cells from this grid.
        /// </summary>
        /// <returns>A list of all bottom-level cells in the grid.</returns>
        public List<Grid2d> GetCells()
        {
            List<Grid2d> resultCells = new List<Grid2d>();
            foreach (var cell in Cells.SelectMany(c => c))
            {
                if (cell.IsSingleCell)
                {
                    resultCells.Add(cell);
                }
                else
                {
                    resultCells.AddRange(cell.GetCells());
                }
            }
            return resultCells;
        }

        /// <summary>
        /// Get a rectangular polygon representing this untrimmed cell boundary.
        /// </summary>
        /// <returns>A rectangle representing this cell in world coordinates.</returns>
        public Curve GetCellGeometry()
        {
            var baseRect = GetBaseRectangle();
            return fromGrid.OfPolygon(baseRect);
        }

        /// <summary>
        /// Get a list of polygons representing this cell boundary, trimmed by any polygon boundary.
        /// If the cell falls completely outside of the boundary, an empty array will be returned.
        /// </summary>
        /// <returns>Curves representing this cell in world coordinates.</returns>
        public Curve[] GetTrimmedCellGeometry()
        {
            if (boundariesInGridSpace == null || boundariesInGridSpace.Count == 0)
            {
                return new[] { GetCellGeometry() };
            }
            Polygon baseRect = GetBaseRectangle();
            var trimmedRect = Polygon.BooleanTwoSets(new[] { baseRect }, boundariesInGridSpace, BooleanMode.Intersection);
            if (trimmedRect != null && trimmedRect.Count() > 0)
            {
                return fromGrid.OfPolygons(trimmedRect);

            }
            return new Curve[0];
        }

        /// <summary>
        /// Get the base rectangle of this cell in grid coordinates.
        /// </summary>
        /// <returns></returns>
        private Polygon GetBaseRectangle()
        {
            return Polygon.Rectangle(new Vector3(U.Domain.Min, V.Domain.Min), new Vector3(U.Domain.Max, V.Domain.Max));
        }

        /// <summary>
        /// Test if the cell is trimmed by a boundary.
        /// </summary>
        /// <returns>True if the cell is trimmed by the grid boundary.</returns>
        public bool IsTrimmed()
        {
            if (boundariesInGridSpace == null || boundariesInGridSpace.Count == 0)
            {
                return false;
            }

            var baseRect = GetBaseRectangle();
            var trimmedRect = Polygon.BooleanTwoSets(new[] { baseRect }, boundariesInGridSpace, BooleanMode.Intersection);
            if (trimmedRect == null) return false;
            var trimmedArea = trimmedRect.Select(r => r.Area()).Sum();
            var baseRectArea = baseRect.Area();
            return !trimmedArea.ApproximatelyEquals(baseRectArea, 0.001);
        }

        /// <summary>
        /// Returns true if this 1D Grid has no subdivisions / sub-grids. 
        /// </summary>
        public bool IsSingleCell => Cells == null || Cells.Count == 0;


        /// <summary>
        /// The 1d Grid along the U dimension
        /// </summary>
        public Grid1d U { get; private set; }

        /// <summary>
        /// The 1d grid along the V dimension
        /// </summary>
        public Grid1d V { get; private set; }


        private Transform fromGrid = new Transform();
        private Transform toGrid = new Transform();
        private IList<Polygon> boundariesInGridSpace;
    }
}
