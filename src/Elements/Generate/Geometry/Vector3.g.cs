//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.0.27.0 (Newtonsoft.Json v12.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements.Geometry
{
    #pragma warning disable // Disable all warnings

    /// <summary>A 3D vector.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.27.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Vector3 
    {
        [Newtonsoft.Json.JsonConstructor]
        public Vector3(double @x, double @y, double @z)
        {
            Vector3.ValidateConstructorParameters(@x, @y, @z);
        
            this.X = @x;
            this.Y = @y;
            this.Z = @z;
        }
    
        /// <summary>The X component of the vector.</summary>
        [Newtonsoft.Json.JsonProperty("X", Required = Newtonsoft.Json.Required.Always)]
        public double X { get; internal set; }
    
        /// <summary>The Y component of the vector.</summary>
        [Newtonsoft.Json.JsonProperty("Y", Required = Newtonsoft.Json.Required.Always)]
        public double Y { get; internal set; }
    
        /// <summary>The Z component of the vector.</summary>
        [Newtonsoft.Json.JsonProperty("Z", Required = Newtonsoft.Json.Required.Always)]
        public double Z { get; internal set; }
    
    
    }
}