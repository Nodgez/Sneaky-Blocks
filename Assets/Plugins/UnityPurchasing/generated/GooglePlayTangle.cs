#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ugiLqLqHjIOgDMIMfYeLi4uPionF9AKOCcQHMyz+JVOhji5vIiHDRTnKZVzpOTgCL8YV0D5gxk7lVIRy5YUnj9P0u0E71ZDsR0zaU46qOM7nNOnJ9AFsvthlNNHHgRMX2Qoh6IIquz+Ge5qOo80Mv5euY8lw+ND8CIuFiroIi4CICIuLihGHQrg2Y6t7I9portk6EL+i8IbgBBMCg3LZMiZKODlTi1CC6axVD0ZPfdHlJKmPbgSHN5jXxZG5XD8df7fKLxw+wneNHP+nFYYZrip4/UuqcjBozOECmZIqsgc24Tfmumq+YVAkkZaAjGHCZfm2XyPDWyAbUtwCiwQGgwk0nu0FSOiUHr+PbBCs2sX9qkrp6DjBaNvG4qfvPlgJo4iJi4qL");
        private static int[] order = new int[] { 0,6,4,13,5,10,6,7,12,12,10,11,12,13,14 };
        private static int key = 138;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
