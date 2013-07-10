using System;
using System.IO;

namespace AndroidManifestVersioner
{
    public class Program
    {
        public static void Main( string[] args )
        {
            Console.WriteLine( "Android Manifest Versioner." );

            if( args.Length == 0 )
            {
                Console.WriteLine( "usage: AndroidManifestVersioner <path> <versionCode> <versionName>.\r\n" );
                return;
            }

            Console.WriteLine( "AndroidManifestVersioner" );
            Console.WriteLine( "Path = " + args[0] );
            Console.WriteLine( "VersionCode = " + args[1] );
            Console.WriteLine( "VersionName = " + args[2] );

            var versioner = new AndroidManifestVersioner( args[0], int.Parse( args[1] ), args[2] );
            versioner.Execute();
        }
    }
}