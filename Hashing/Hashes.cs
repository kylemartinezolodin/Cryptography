using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HashingFunction
{
    public class MD5 {
        /// <summary>
        /// Return 16-bytes of message-digest (a.k.a. hash) using MD5
        /// </summary>
        /// <param name="inputByteArray"></param>
        /// <returns></returns>
        public static byte[] ComputeHash(byte[] inputByteArray) {
            System.Security.Cryptography.MD5 hash = System.Security.Cryptography.MD5.Create();
            return hash.ComputeHash(inputByteArray);
        } 
    }
    public class SHA {
        /// <summary>
        /// Return 16-bytes of message-digest (a.k.a. hash) using SHA1
        /// </summary>
        /// <param name="inputByteArray"></param>
        /// <returns></returns>
        public static byte[] ComputeHash(byte[] inputByteArray) {
            System.Security.Cryptography.SHA1 hash = System.Security.Cryptography.SHA1.Create();
            return hash.ComputeHash(inputByteArray);
        }
    }
}
