using System;

namespace AndroidManifestVersioner
{
    public class Program
    {
        public static void Main( string[] args )
        {
            if( args.Length == 0 )
            {
                Console.WriteLine( "Android Manifest Versioner.\r\nusage: AndroidManifestVersioner <path> <versionCode> <versionName>.\r\n" );
                return;
            }

            var versioner = new AndroidManifestVersioner( args[0], int.Parse(args[1]), args[2] );
            versioner.Execute();
        }
    }
}