//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.21.0 (Newtonsoft.Json v13.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Spatial;
using Elements.Validators;
using Elements.Serialization.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements.Fittings
{
    #pragma warning disable // Disable all warnings

    /// <summary>A Wye piping connection</summary>
    [JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v13.0.0.0)")]
    public partial class Wye : Fitting
    {
        [JsonConstructor]
        public Wye(Port @trunk, double @angle, Port @sideBranch, Port @mainBranch, PressureCalculationWye @pressureCalculations, bool @canBeMoved, FittingLocator @componentLocator, Transform @transform, Material @material, Representation @representation, bool @isElementDefinition, System.Guid @id, string @name)
            : base(canBeMoved, componentLocator, transform, material, representation, isElementDefinition, id, name)
        {
            this.Trunk = @trunk;
            this.Angle = @angle;
            this.SideBranch = @sideBranch;
            this.MainBranch = @mainBranch;
            this.PressureCalculations = @pressureCalculations;
            }
        
        // Empty constructor
        public Wye()
            : base()
        {
        }
    
        /// <summary>The connector at the trunk</summary>
        [JsonProperty("Trunk", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Port Trunk { get; set; }
    
        /// <summary>The angle the SideBranch makes with the MainBranch</summary>
        [JsonProperty("Angle", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double Angle { get; set; }
    
        /// <summary>The connector that branches from the Trunk, angled from the MainBranch.</summary>
        [JsonProperty("SideBranch", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Port SideBranch { get; set; }
    
        /// <summary>The connector of the main branch from the Trunk.</summary>
        [JsonProperty("MainBranch", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Port MainBranch { get; set; }
    
        /// <summary>Pressure Calculations</summary>
        [JsonProperty("Pressure Calculations", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public PressureCalculationWye PressureCalculations { get; set; }
    
    
    }
}