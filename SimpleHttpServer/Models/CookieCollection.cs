namespace SimpleHttpServer.Models
{
    using System.Collections;
    using System.Collections.Generic;

    public class CookieCollection : IEnumerable<Cookie>
    {
        #region Constructor
        public CookieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }
        #endregion

        #region Properties
        public IDictionary<string, Cookie> Cookies { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Add new Cookie to the collection.
        /// </summary>
        public void Add(Cookie cookie)
        {
            this.Cookies.Add(cookie.Name, cookie);
        }

        /// <summary>
        /// Check if Cookie exists by given name.
        /// </summary>
        public bool Contains(string cookieName)
        {
            if (this.Cookies.ContainsKey(cookieName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Count of all cookies in collection.
        /// </summary>
        public int Count { get { return this.Cookies.Count; } }

        public Cookie this[string cookieName]
        {
            get
            {
                return this.Cookies[cookieName];
            }
            set
            {
                // If Cookie exists, modify.
                if (this.Contains(cookieName))
                {
                    this.Cookies[cookieName] = value;
                }
                // Else, add new Cookie.
                else
                {
                    this.Cookies.Add(cookieName, value);
                }
            }
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("; ", Cookies.Values);
        }
        #endregion
    }
}