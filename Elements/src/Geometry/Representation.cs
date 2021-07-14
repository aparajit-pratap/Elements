using System.Collections.Generic;
using Elements.Geometry.Solids;

namespace Elements.Geometry
{
    public partial class Representation
    {
        /// <summary>
        /// Construct a Representation from SolidOperations. This is a convenience constructor
        /// that can be used like this: `new Representation(new Extrude(...))`
        /// </summary>
        /// <param name="solidOperations">The solid operations composing this representation.</param>
        public Representation(params SolidOperation[] solidOperations) : this(new List<SolidOperation>(solidOperations))
        {

        }

        /// <summary>
        /// Automatically convert a single solid operation into a representation containing that operation.
        /// </summary>
        /// <param name="solidOperation">The solid operation composing this Representation.</param>
        public static implicit operator Representation(SolidOperation solidOperation)
        {
            return new Representation(solidOperation);
        }

        /// <summary>
        /// A flag to disable CSG operations on this representation. Instead,
        /// all solids will be meshed, and all voids will be ignored.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public bool SkipCSGUnion { get; set; } = false;
    }
}