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

namespace Elements.Geometry.Solids
{
    #pragma warning disable // Disable all warnings

    /// <summary>An extrusion of a profile, in a direction, to a height.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.27.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Extrude : SolidOperation
    {
        [Newtonsoft.Json.JsonConstructor]
        public Extrude(Profile @profile, double @height, Vector3 @direction, double @rotation, bool @isVoid)
            : base(isVoid)
        {
            Extrude.ValidateConstructorParameters(@profile, @height, @direction, @rotation, @isVoid);
        
            this.Profile = @profile;
            this.Height = @height;
            this.Direction = @direction;
            this.Rotation = @rotation;
        }
    
        /// <summary>The id of the profile to extrude.</summary>
        [Newtonsoft.Json.JsonProperty("Profile", Required = Newtonsoft.Json.Required.AllowNull)]
        public Profile Profile { get; internal set; }
    
        /// <summary>The height of the extrusion.</summary>
        [Newtonsoft.Json.JsonProperty("Height", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Range(0, int.MaxValue)]
        public double Height { get; internal set; }
    
        /// <summary>The direction in which to extrude.</summary>
        [Newtonsoft.Json.JsonProperty("Direction", Required = Newtonsoft.Json.Required.AllowNull)]
        public Vector3 Direction { get; internal set; }
    
        /// <summary>The rotation, in degrees, of the profile.</summary>
        [Newtonsoft.Json.JsonProperty("Rotation", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double Rotation { get; internal set; }
    
    
    }
}