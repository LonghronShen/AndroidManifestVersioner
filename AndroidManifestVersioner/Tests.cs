using System.IO;
using NUnit.Framework;

namespace AndroidManifestVersioner
{
    [TestFixture]
    public class Tests
    {
        [TestCase( @"..\..\..\testData\AndroidManifest.xml", 133, "1.0.133" )]
        public void Test( string path, int versionCode, string versionName )
        {
            string testPath = path + ".test";
            File.Copy( path, testPath, true );

            var versioner = new AndroidManifestVersioner( testPath, versionCode, versionName );
            versioner.Execute();

            string text = File.ReadAllText( testPath );
            Assert.True(text.Contains( " android:versionCode=\"133\" " )  );
            Assert.True(text.Contains( " android:versionName=\"1.0.133\">" )  );
        }
    }
}