using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroAPI.Models
{
    public class AsteroidsApiData
    {
        public List<Near_Earth_Objects> Near_earth_objects { get; set; }
        
    }
    public class AsteroidsResponse
    {

        public string Id { get; set; }
        public string Magnitude { get; set; }
        public string Name { get; set; }

    }

    public class AsteroidId
    {
        public string id { get; set; }

    }

    public class Near_Earth_Objects
    {
        public string Id { get; set; }
        public string Name_limited { get; set; }
        public float Absolute_magnitude_h { get; set; }

    }
}
