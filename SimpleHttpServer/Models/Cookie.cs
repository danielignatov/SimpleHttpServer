namespace SimpleHttpServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Cookie
    {
        #region Constructors
        public Cookie() : this(null, null)
        {

        }

        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        #endregion

        #region Properties
        public string Name { get; private set; }

        public string Value { get; private set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{this.Name}={this.Value}";
        }
        #endregion
    }
}