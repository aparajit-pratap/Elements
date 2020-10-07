//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.21.0 (Newtonsoft.Json v12.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Properties;
using Elements.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements.Geometry
{
    #pragma warning disable // Disable all warnings

    /// <summary>A triangulated mesh.</summary>
    [Newtonsoft.Json.JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v12.0.0.0)")]
    [UserElement]
	public partial class Mesh : Geometry
    {
        [Newtonsoft.Json.JsonConstructor]
        public Mesh(IList<Vertex> @vertices, IList<Triangle> @triangles, Material @material)
            : base(material)
        {
            var validator = Validator.Instance.GetFirstValidatorForType<Mesh>
            ();
            if(validator != null)
            {
                validator.PreConstruct(new object[]{ @vertices, @triangles, @material});
            }
        
                this.Vertices = @vertices;
                this.Triangles = @triangles;
            
            if(validator != null)
            {
            validator.PostConstruct(this);
            }
            }
    
        /// <summary>The vertices of the mesh.</summary>
        [Newtonsoft.Json.JsonProperty("Vertices", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<Vertex> Vertices { get; set; }
    
        /// <summary>The triangles of the mesh</summary>
        [Newtonsoft.Json.JsonProperty("Triangles", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<Triangle> Triangles { get; set; }
    
    
    }
}