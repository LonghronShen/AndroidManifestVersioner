using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AndroidManifestVersioner
{
    public sealed class AndroidManifestVersioner
    {
        private readonly string _path;
        private readonly int _versionCode;
        private readonly string _versionName;

        private static readonly Regex VersionCodeRegex = new Regex( "android:versionCode=\"([0-9]*)\"", RegexOptions.Compiled );
        private static readonly Regex VersionNameRegex = new Regex( "android:versionName=\"([a-zA-Z0-9.]*)\"", RegexOptions.Compiled );  // TODO: Support an arbitrary number of decimal points.

        public AndroidManifestVersioner( string path, int versionCode, string versionName )
        {
            _path = path;
            _versionCode = versionCode;
            _versionName = versionName;
        }

        public void Execute()
        {
            using( var manifestFileStream = File.Open( _path, FileMode.Open, FileAccess.ReadWrite ) )
            using( var streamReader = new StreamReader( manifestFileStream ) )
            {
                var manifestFileText = streamReader.ReadToEnd();

                if( VersionCodeRegex.Match( manifestFileText ).Success )
                    manifestFileText = VersionCodeRegex.Replace( manifestFileText, "android:versionCode=\"" + _versionCode + "\"", 1 );
                else
                    throw new InvalidOperationException( "Could not find versionCode section of AndroidManifest.xml." );

                if( VersionNameRegex.Match( manifestFileText ).Success )
                    manifestFileText = VersionNameRegex.Replace( manifestFileText, "android:versionName=\"" + _versionName + "\"", 1 );
                else
                    throw new InvalidOperationException( "Could not find versionName section of AndroidManifest.xml." );

                using( var streamWriter = new StreamWriter( manifestFileStream ) )
                {
                    manifestFileStream.Seek( 0, SeekOrigin.Begin );
                    streamWriter.Write( manifestFileText );
                    manifestFileStream.SetLength( manifestFileText.Length );
                }
            }
        }

        public string Path { get { return _path; } }
        public int VersionCode { get { return _versionCode; } }
        public string VersionName { get { return _versionName; } }
    }
}