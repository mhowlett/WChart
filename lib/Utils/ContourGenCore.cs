using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace WChart.ContourUtils
{


    /// <summary>
    /// Provides core contour generation capability.
    /// </summary>
    public static class ContourGenCore
    {

        /// <summary>
        /// The sampled surface that contours are generated for.
        /// </summary>
        private static double[,] _grid;


        /// <summary>
        /// The contour levels.
        /// </summary>
        private static List<double> _levels;


        /// <summary>
        /// Generates the boolean arrays _horizontalIntersects and _verticalIntersects indicating
        /// the intersection of the "level" plane with the grid. 
        /// </summary>
        /// <param name="level">the contour level</param>
        private static void GenerateIntersectArrays(double level)
        {
            _horizontalIntersects = new bool[_grid.GetLength(0), _grid.GetLength(1)];
            _verticalIntersects = new bool[_grid.GetLength(0), _grid.GetLength(1)];

            int n0 = _grid.GetLength(0);
            int n1 = _grid.GetLength(1);

            for (int i = 0; i < n0; ++i)
            {
                for (int j = 0; j < n1; ++j)
                {
                    _horizontalIntersects[i, j] = false;
                    _verticalIntersects[i, j] = false;
                }
            }


            for (int i = 0; i < n0 - 1; ++i)
            {
                for (int j = 0; j < n1 - 1; ++j)
                {
                    // check for horizontal intersect
                    if ((_grid[i, j] >= level && _grid[i, j + 1] < level) ||
                         (_grid[i, j] < level && _grid[i, j + 1] >= level))
                    {
                        _horizontalIntersects[i, j] = true;
                    }

                    // check for vertical intersect
                    if ((_grid[i, j] >= level && _grid[i + 1, j] < level) ||
                        (_grid[i, j] < level && _grid[i + 1, j] >= level))
                    {
                        _verticalIntersects[i, j] = true;
                    }
                }
            }

            // last column
            for (int i = 0; i < n0 - 1; ++i)
            {
                if ((_grid[i, n1-1] >= level && _grid[i + 1, n1-1] < level) ||
                    (_grid[i, n1-1] < level && _grid[i + 1, n1-1] >= level))
                {
                    _verticalIntersects[i, n1-1] = true;
                }
            }

            // last row
            for (int j = 0; j < n1 - 1; ++j)
            {
                if ((_grid[n0-1, j] >= level && _grid[n0-1, j + 1] < level) ||
                    (_grid[n0-1, j] < level && _grid[n0-1, j + 1] >= level))
                {
                    _horizontalIntersects[n0-1, j] = true;
                }
            }

        }


        /// <summary>
        /// Describes an intersection point of a contour plane with the surface described
        /// by the grid.
        /// 
        /// Intersection is either with a horizontal segmenet between two points, or a 
        /// vertical segment between two points.
        /// 
        /// Note: horizontal/vertical segments corresponding to a particular point extend
        /// right/down from that point.
        /// </summary>
        private class IntersectionPoint
        {

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="row">grid row the interesection corresponds to.</param>
            /// <param name="col">grid column the intersection corresonds to.</param>
            /// <param name="type">whether this is an interesection with a horizontal or vertical segement
            /// between grid points.</param>
            public IntersectionPoint(int row, int col, IntersectionType type)
            {
                _row = row;
                _col = col;
                _type = type;
            }

            
            /// <summary>
            /// the grid row the intersection corresponds to.
            /// </summary>
            public int Row
            {
                get
                {
                    return _row;
                }
            }
            private int _row;


            /// <summary>
            /// the grid column the intersection corresponds to.
            /// </summary>
            public int Col
            {
                get
                {
                    return _col;
                }
            }
            private int _col;

            
            /// <summary>
            /// whether this is an intersection with a horizontal or vertical segement
            /// between grid points.
            /// </summary>
            public IntersectionType Type
            {
                get
                {
                    return _type;
                }
            }
            private IntersectionType _type;


            /// <summary>
            /// Enumerates the two intersection types (horizontal and vertical).
            /// </summary>
            public enum IntersectionType
            {
                Horizontal,
                Vertical
            }

        }


        /// <summary>
        /// used to remember the last point searched to in FindUnusedIntersection.
        /// </summary>
        private static int _lastSearchI = 0;


        /// <summary>
        /// used to remember the last point searched to in FindUnusedIntersection.
        /// </summary>
        private static int _lastSearchJ = 0;


        /// <summary>
        /// searches the _horizontalIntersects and _verticalIntersects arrays for a 
        /// intersection point that hasn't been used yet.
        /// </summary>
        /// <param name="firstTime">If this is the first time this function has been
        /// called, should be true, else false</param>
        /// <returns>An found interesection point or null if there are none remaining.</returns>
        private static IntersectionPoint FindUnusedIntersection(bool firstTime)
        {
            if (firstTime)
            {
                _lastSearchI = 0;
                _lastSearchJ = 0;
            }

            for (int i = _lastSearchI; i < _grid.GetLength(0); ++i)
            {
                for (int j = _lastSearchJ; j < _grid.GetLength(1); ++j)
                {
                    if (_horizontalIntersects[i, j])
                    {
                        return new IntersectionPoint(i, j, IntersectionPoint.IntersectionType.Horizontal);
                    }
                    if (_verticalIntersects[i, j])
                    {
                        return new IntersectionPoint(i, j, IntersectionPoint.IntersectionType.Vertical);
                    }
                }
            }
            return null;
        }

        
        /// <summary>
        /// Finds neighbouring intersection points to p in the _horizontalInteresects and
        /// _verticalIntersects arrays. 
        /// </summary>
        /// <param name="p">interesection point to find neighbours of.</param>
        /// <returns>neighbouring interesection points to p.</returns>
        private static List<IntersectionPoint> FindNeighbours(IntersectionPoint p)
        {
            List<IntersectionPoint> neighbours = new List<IntersectionPoint>();

            if (p.Type == IntersectionPoint.IntersectionType.Horizontal)
            {
                if (p.Row > 0)
                {
                    if (_horizontalIntersects[p.Row - 1, p.Col])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row - 1, p.Col, IntersectionPoint.IntersectionType.Horizontal));
                    }

                    if (_verticalIntersects[p.Row - 1, p.Col])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row - 1, p.Col, IntersectionPoint.IntersectionType.Vertical));
                    }
                }
                if (p.Row < _horizontalIntersects.GetLength(0) - 1)
                {
                    if (_horizontalIntersects[p.Row + 1, p.Col])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row + 1, p.Col, IntersectionPoint.IntersectionType.Horizontal));
                    }
                }

                if (_verticalIntersects[p.Row, p.Col])
                {
                    neighbours.Add(new IntersectionPoint(p.Row, p.Col, IntersectionPoint.IntersectionType.Vertical));
                }

                if (p.Col < _verticalIntersects.GetLength(1) - 1)
                {
                    if (_verticalIntersects[p.Row, p.Col + 1])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row, p.Col + 1, IntersectionPoint.IntersectionType.Vertical));
                    }

                    if (p.Row > 0)
                    {
                        if (_verticalIntersects[p.Row - 1, p.Col + 1])
                        {
                            neighbours.Add(new IntersectionPoint(p.Row - 1, p.Col + 1, IntersectionPoint.IntersectionType.Vertical));
                        }
                    }
                }
            }
            else
            {
                if (_horizontalIntersects[p.Row, p.Col])
                {
                    neighbours.Add(new IntersectionPoint(p.Row, p.Col, IntersectionPoint.IntersectionType.Horizontal));
                }

                if (p.Col > 0)
                {
                    if (_verticalIntersects[p.Row, p.Col-1])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row, p.Col-1, IntersectionPoint.IntersectionType.Vertical));
                    }

                    if (_horizontalIntersects[p.Row, p.Col-1])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row, p.Col-1, IntersectionPoint.IntersectionType.Horizontal));
                    }

                    if (p.Row < _horizontalIntersects.GetLength(0)-1)
                    {
                        if (_horizontalIntersects[p.Row+1, p.Col-1])
                        {
                            neighbours.Add(new IntersectionPoint(p.Row+1, p.Col-1, IntersectionPoint.IntersectionType.Horizontal));
                        }
                    }
                }

                if (p.Row <_horizontalIntersects.GetLength(0)-1)
                {
                    if (_horizontalIntersects[p.Row+1,p.Col])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row+1, p.Col, IntersectionPoint.IntersectionType.Horizontal));
                    }
                }

                if (p.Col < _verticalIntersects.GetLength(1) -1)
                {
                    if (_verticalIntersects[p.Row,p.Col+1])
                    {
                        neighbours.Add(new IntersectionPoint(p.Row, p.Col+1, IntersectionPoint.IntersectionType.Vertical));
                    }
                }
            }
            
            return neighbours;
        }


        /// <summary>
        /// removes the interesection point p from _horizontalInteresects or
        /// _verticalInteresects array.
        /// </summary>
        /// <param name="p">point to remove from interesects arrays</param>
        private static void ForgetPoint(IntersectionPoint p)
        {
            if (p.Type == IntersectionPoint.IntersectionType.Horizontal)
            {
                _horizontalIntersects[p.Row, p.Col] = false;
            }
            else
            {
                _verticalIntersects[p.Row, p.Col] = false;
            }
        }


        /// <summary>
        /// given an interesect point p definition, determines the actual interpolated intersect point.
        /// </summary>
        /// <param name="p">IntersectionPoint to find interpolated intersection point for.</param>
        /// <param name="level">the relevant contour plane level.</param>
        /// <returns>actual interpolated intersect point corresponding to p.</returns>
        private static Pair<double,double> Interpolate( IntersectionPoint p, double level )
        {
            double p_x = p.Col;
            double p_y = p.Row;

            if (p.Type == IntersectionPoint.IntersectionType.Horizontal)
            {
                if (p.Col != _grid.GetLength(1) - 1)
                {
                    p_y = p.Row;
                    Debug.Assert(
                        (_grid[p.Row, p.Col] >= level && _grid[p.Row, p.Col + 1] < level) ||
                        (_grid[p.Row, p.Col] < level && _grid[p.Row, p.Col + 1] >= level));
                    double prop = (level - _grid[p.Row, p.Col]) / (_grid[p.Row, p.Col + 1] - _grid[p.Row, p.Col]);
                    p_x = p.Col + prop;
                }
            }
            else
            {
                if (p.Row != _grid.GetLength(0) - 1)
                {
                    p_x = p.Col;
                    Debug.Assert(
                        (_grid[p.Row, p.Col] >= level && _grid[p.Row + 1, p.Col] < level) ||
                        (_grid[p.Row, p.Col] < level && _grid[p.Row + 1, p.Col] >= level));
                    double prop = (level - _grid[p.Row, p.Col]) / (_grid[p.Row + 1, p.Col] - _grid[p.Row, p.Col]);
                    p_y = p.Row + prop;
                }
            }

            return new Pair<double,double>(p_x, p_y);
        }


        /// <summary>
        /// The last group the CreateLineSegments method created line segments in.
        /// </summary>
        private static int _lastGroup = -1;


        /// <summary>
        /// Inserts values into the _lineGroupLevels and _lineGroups arrays.
        /// </summary>
        /// <param name="p1">from point</param>
        /// <param name="ps">to points</param>
        /// <param name="level">the contour level these line segments correspond to</param>
        private static void CreateLineSegments(IntersectionPoint p1, List<IntersectionPoint> ps, double level)
        {
            for (int i = 0; i < ps.Count; ++i)
            {
                IntersectionPoint p2 = ps[i];
                Pair<double,double> p1i = Interpolate(p1, level);
                Pair<double,double> p2i = Interpolate(p2, level);
                if (_group != _lastGroup)
                {
                    _lastGroup = _group;
                    _lineGroupLevels.Add(level);
                    _lineGroups.Add(new List<LineSegment>());
                }
                else if (_lineGroups.Count == 0) // this was a quick fix, need to invsetigate why it was happening.
                {
                    _lineGroupLevels.Add(level);
                    _lineGroups.Add(new List<LineSegment>());
                }

                _lineGroups[_lineGroups.Count - 1].Add(new LineSegment(p1i.First, p1i.Second, p2i.First, p2i.Second));
            }
        }

        
        /// <summary>
        /// the contour level each of the line segment groups corresponds to.
        /// </summary>
        private static List<double> _lineGroupLevels;

        
        /// <summary>
        /// Each continuous group of line segments is stored together in this container.
        /// </summary>
        private static List<List<LineSegment>> _lineGroups;


        /// <summary>
        /// Generates all contour lines for a particular contour level (may be in more than
        /// one group).
        /// </summary>
        /// <param name="level">contour level to create lines for.</param>
        private static void GenerateLevel(double level)
        {
            // first index is row, second index is column. 
            // 
            //   ._._._.
            //   | | | |
            //
            //  horizontal intersects refer to line to right of grid point.
            //  vertical intersects refer to line to bottom of grid point.

            GenerateIntersectArrays(level);

            while (true)
            {

                IntersectionPoint p = FindUnusedIntersection(true);
                if (p == null)
                {
                    break;
                }

                _group += 1;

                Stack<IntersectionPoint> yetToFollow = new Stack<IntersectionPoint>();
                while (true)
                {
                    List<IntersectionPoint>  neighbours = FindNeighbours(p);
                    foreach (IntersectionPoint pnt in neighbours)
                    {
                        yetToFollow.Push(pnt);
                    }
                    CreateLineSegments(p, neighbours, level);
                    ForgetPoint(p);

                    if (yetToFollow.Count == 0) // optimize this.
                    {
                        break;
                    }
                    p = yetToFollow.Pop();
                }

            }
        }


        /// <summary>
        /// The current group #.
        /// </summary>
        private static int _group;


        /// <summary>
        /// array of intersection points with horizontal lines joining grid points.
        /// </summary>
        private static bool[,] _horizontalIntersects;
        
        
        /// <summary>
        /// array of intersection points with vertical lines joining grid points.
        /// </summary>
        private static bool[,] _verticalIntersects;


        /// <summary>
        /// Generates contours for a surface described by grid. Returns a set of groups of 
        /// line segements, each group corresponding to a continuous contour line.
        /// </summary>
        /// <param name="grid">the grid of values to create line segements for</param>
        /// <param name="levels">the contour levels.</param>
        /// <returns>contour line groups.</returns>
        public static Pair<List<double>, List<List<LineSegment>>> Generate( double[,] grid, List<double> levels )
        {
            Debug.Assert(levels != null, "Levels array not set.");
            Debug.Assert(grid != null, "Grid not set.");

            _grid = grid;
            _group = -1;
            _lineGroups = new List<List<LineSegment>>();
            _lineGroupLevels = new List<double>();

            foreach (double level in levels)
            {
                GenerateLevel(level);
            }

            return new Pair<List<double>, List<List<LineSegment>>>(_lineGroupLevels, _lineGroups);
        }

        
    }
}
